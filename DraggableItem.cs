using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    Transform parentAfterDrag;
    public string nameString;


    public void OnBeginDrag(PointerEventData eventData)
    {
        
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();//As a way of not having other UI objects on top of current object
    }

    public void OnDrag(PointerEventData eventData)
    {
        CameraMotion.canMove = false;
        transform.position = Input.mousePosition;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);//putting the Hierarchy back in its preview place
        GridBuildingSystem3D.Instance.InstantiateMachineByDrag(nameString);
        GridBuildingSystem3D.Instance.OpenPlacingButtons();


        Vector3 mousePosition = Mouse3D.GetMouseWorldPositionThoughItem();

        if (mousePosition != new Vector3(0, 0, 0))
        {
            GridBuildingSystem3D.Instance.placedObjectOrigin = GridBuildingSystem3D.Instance.GetGridPosition(mousePosition);
        }

        UI_Inventory uI_Inventory =  GameObject.Find("UI_Player").GetComponent<UI_Inventory>();
        uI_Inventory.RemoveIten(nameString);
        uI_Inventory.RefreshInventory();

        Destroy(this.gameObject);

    }


}
