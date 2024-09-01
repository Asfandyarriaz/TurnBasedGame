using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld instance;
    [SerializeField] LayerMask _mousePlaneLayerMask;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        transform.position = MouseWorld.GetPosition();
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, MouseWorld.instance._mousePlaneLayerMask);
        return raycastHit.point;
    }
}
