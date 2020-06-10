using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int _camSpeed = 100;
    [SerializeField] private int _scrollSpeed = 100;

    [SerializeField] private float _mouseScreenPercentile = .2f;
    [SerializeField] private float _mouseScrollSensitivity = 1f;

    [SerializeField] private Camera GameCam;

    void Start()
    {
        GameCam = Camera.main;
    }

    void Update()
    {
        MouseCamCtrl();
        CamCtrl();
        GameCam.fieldOfView = Mathf.Clamp(GameCam.fieldOfView, 10, 50);
    }

    private void CamCtrl()
    {
        //WASD/ArrowKeys cam movement
        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(_verticalInput, 0,-_horizontalInput);
        this.transform.Translate(direction * _camSpeed * Time.deltaTime);

        //zoom in/out
        GameCam.fieldOfView += Input.mouseScrollDelta.y * _scrollSpeed * Time.deltaTime;
    }

    private void MouseCamCtrl()
    {
        if (Input.mousePosition.x < PercentileWidthMin())
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        else if (Input.mousePosition.x > PercentileWidthMax())
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else if (Input.mousePosition.y < PercentileHeightMin())
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
        else if (Input.mousePosition.y > PercentileHeightMax())
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
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
