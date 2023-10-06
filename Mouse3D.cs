using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse3D : MonoBehaviour {

    public static Mouse3D Instance { get; private set; }


    [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();

    private void Awake() {
        Instance = this;
        
        
    }

    private void Update() {


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f)) {
            transform.position = raycastHit.point;
        }
    }

    public static Vector3 GetMouseWorldPosition() => Instance.GetMouseWorldPosition_Instance();

    public static Vector3 GetMouseWorldPositionThoughItem() => Instance.GetMouseWorldPositionThoughItem_Instance();





    private Vector3 GetMouseWorldPositionThoughItem_Instance()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 GetMouseWorldPosition_Instance() {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return Vector3.zero;

        }


        else
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
            {
                return raycastHit.point;
            }
            else
            {
                return Vector3.zero;
            }
        }
        
    }

}
