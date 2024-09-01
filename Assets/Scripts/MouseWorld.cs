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

        /*if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue);
            Debug.Log("Clicked : " + raycastHit.transform.gameObject.name);
        }*/
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, MouseWorld.instance._mousePlaneLayerMask);
        return raycastHit.point;
    }
}
