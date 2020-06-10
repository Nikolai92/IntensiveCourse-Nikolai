using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int _camSpeed = 100;
    [SerializeField] private int _scrollSpeed = 40;
    [SerializeField] private Camera GameCam;



    void Start()
    {
        GameCam = Camera.main;
    }

    void Update()
    {
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
}
