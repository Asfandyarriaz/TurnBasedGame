using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ScreenShake.Instance.Shake();
        }
    }
}
