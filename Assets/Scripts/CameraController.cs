using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private float _cameraMoveSpeed = 10f;
    private float _rotationSpeed = 100f;
    private CinemachineTransposer _cinemachineTransposer;
    private Vector3 _targetFollowOffset;
    private float _zoomAmount = 1f;
    private float _min_Follow_Y_Offset = 2f;
    private float _max_Follow_Y_Offset = 12f;
    private float _zoomSpeed = 5f;

    private void Start()
    {
        _cinemachineTransposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _targetFollowOffset = _cinemachineTransposer.m_FollowOffset;
    }
    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 _inputMoveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            _inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _inputMoveDir.x = +1f;
        }


        Vector3 moveVector = transform.forward * _inputMoveDir.z + transform.right * _inputMoveDir.x;
        transform.position += moveVector * _cameraMoveSpeed * Time.deltaTime;
    }
    private void HandleRotation()
    {
        // Rotation 
        Vector3 rotationVector = Vector3.zero;
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;

        }
        transform.eulerAngles += rotationVector * _rotationSpeed * Time.deltaTime;
    }
    private void HandleZoom()
    {
        // Zoom 
        if (Input.mouseScrollDelta.y > 0)
        {
            _targetFollowOffset.y -= _zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            _targetFollowOffset.y += _zoomAmount;

        }

        _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y, _min_Follow_Y_Offset, _max_Follow_Y_Offset);
        _cinemachineTransposer.m_FollowOffset = Vector3.Lerp(_cinemachineTransposer.m_FollowOffset, _targetFollowOffset, _zoomSpeed * Time.deltaTime);
    }
}
