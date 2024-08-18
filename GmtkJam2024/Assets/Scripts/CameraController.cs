using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private readonly float _zoomSpeed = 5;
    private float _targetLensSize;
    public static UnityEvent<float> ChangeLensSize = new UnityEvent<float>();

    private void Start()
    {
        _targetLensSize = _virtualCamera.m_Lens.OrthographicSize;
        ChangeLensSize.AddListener((float newSize) => { _targetLensSize = newSize; });
    }

    private void Update()
    {
        _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_virtualCamera.m_Lens.OrthographicSize, _targetLensSize, Time.fixedDeltaTime * _zoomSpeed);
    }
}
