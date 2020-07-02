using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int _camSpeed = 100;
    [SerializeField] private int _scrollSpeed = 100;
    [SerializeField] private int _panSpeed = 100;

    [SerializeField] private float _mouseScreenPercentile = .2f;
    [SerializeField] private float _mouseScrollSensitivity = 1f;

    [SerializeField] private Camera GameCam;
    [SerializeField] private float _CamBoundsZmin;
    [SerializeField] private float _CamBoundsZmax;
    [SerializeField] private float _CamBoundsXmin;
    [SerializeField] private float _CamBoundsXmax;

    void Start()
    {
        GameCam = Camera.main;
    }

    void Update()
    {
        MouseCamCtrl();
        CamCtrl();     
    }

    private void CamCtrl()
    {
        //WASD/ArrowKeys cam movement
        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(_verticalInput, 0, -_horizontalInput);

        this.transform.Translate(direction * _camSpeed * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, _CamBoundsXmin, _CamBoundsXmax);
        pos.z = Mathf.Clamp(pos.z, _CamBoundsZmin, _CamBoundsZmax);
        transform.position = pos;

        //zoom in/out
        GameCam.fieldOfView += Input.mouseScrollDelta.y * _scrollSpeed * Time.deltaTime;
        GameCam.fieldOfView = Mathf.Clamp(GameCam.fieldOfView, 10, 50);
    }

    private void MouseCamCtrl()
    {
        Vector3 direction = new Vector3();

        if (Input.mousePosition.x < PercentileWidthMin())
        {
            direction = Vector3.forward *_panSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.x > PercentileWidthMax())
        {
            direction = Vector3.back * _panSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.y < PercentileHeightMin())
        {
            direction = Vector3.left * _panSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.y > PercentileHeightMax())
        {
            direction = Vector3.right * _panSpeed * Time.deltaTime;
        }

        this.transform.Translate(direction * Time.deltaTime);
    }

    private float PercentileWidthMin()
    {
        float mouseBorderWidth = _mouseScreenPercentile * Screen.width;
        return mouseBorderWidth;
    }

    private float PercentileWidthMax()
    {
        float mouseBorderWidth = Screen.width - (_mouseScreenPercentile * Screen.width);
        return mouseBorderWidth;
    }

    private float PercentileHeightMin()
    {
        float MouseBorderHeight = _mouseScreenPercentile * Screen.height;
        return MouseBorderHeight;
    }
    private float PercentileHeightMax()
    {
        float MouseBorderHeight = Screen.height - (_mouseScreenPercentile * Screen.height);
        return MouseBorderHeight;
    }
}
