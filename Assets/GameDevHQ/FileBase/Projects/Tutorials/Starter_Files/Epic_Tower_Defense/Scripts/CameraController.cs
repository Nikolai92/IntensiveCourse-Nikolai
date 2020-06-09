using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int _camSpeed = 100;
    [SerializeField] private GameObject GameCam;

    [SerializeField] private float _horizontalInput;

    void Start()
    {
        GameCam.gameObject.GetComponent<Camera>().fieldOfView = Mathf.Clamp(35, 10, 50);
        //Why isn't this working?
    }

    void Update()
    {
        CamCtrl();
        
    }

    private void CamCtrl()
    {
        float _horizontalInput = Input.GetAxis("Horizontal");
        GameCam.transform.Translate(new Vector3(_horizontalInput, 0, 0) * _camSpeed * Time.deltaTime);

        float _verticalInput = Input.GetAxis("Vertical");
        GameCam.transform.Translate(new Vector3(0, _verticalInput, 0) * _camSpeed * Time.deltaTime);
        //Do these work as an if statement ?


        if (Input.mouseScrollDelta.y < 0)
        {
            Debug.Log("trying to zoom in");
            GameCam.gameObject.GetComponent<Camera>().fieldOfView += _camSpeed;
            /*float zoomIn = GameCam.gameObject.GetComponent<Camera>().fieldOfView;
            //zoomIn = Mathf.Clamp(zoomIn, 10, 50);
            //zoomIn = zoomIn * Time.deltaTime * _camSpeed;
            zoomIn += _camSpeed;*/
        }

        else if (Input.mouseScrollDelta.y > 0)
        {
            Debug.Log("trying to zoom out");
            GameCam.gameObject.GetComponent<Camera>().fieldOfView -= _camSpeed;
            /*float zoomOut = GameCam.gameObject.GetComponent<Camera>().fieldOfView;
            zoomOut = Mathf.Clamp(zoomOut, 10, 50);
            zoomOut = zoomOut * Time.deltaTime * _camSpeed;
            zoomOut -= _camSpeed;*/
        }
    }
}
