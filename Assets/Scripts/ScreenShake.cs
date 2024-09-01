using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Editor;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance { get; private set; }

    private CinemachineImpulseSource _cinemachineImpulseSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one ScreenShake! " + transform + " - " + Instance);
            Destroy(Instance);
            return;
        }
        Instance = this;

        _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }


    public void Shake(float intensity = 1f)
    {
        _cinemachineImpulseSource.GenerateImpulse();
    }
}
