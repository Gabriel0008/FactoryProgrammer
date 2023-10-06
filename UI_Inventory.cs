using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;
    int y = 0;
    float itemSlotCellSize = 92.5f;
    private List<Machines> machines = null;
    private int _firstInventorySpace = 0;

    void Start()
    {
        RefreshInventory();
        

    }


    public void RemoveIten(string name)
    {
        for (int i = 0; i < machines.Count; i++)
        {
            if(name == machines[i].machine.nameString)
            {
                if (machines[i].quantidade == -111)//endless
                {

                }
                else
                {
                    machines[i].RemoveIten();
                }
            }

        }

    }

    public void AddIten(string name)
    {
        for (int i = 0; i < machines.Count; i++)
        {
            if (name == machines[i].machine.nameString)
            {
                if (machines[i].quantidade == -111)//endless
                {

                }
                else
                {
                    machines[i].AddIten();
                }
                
            }

        }

    }

    public void MoveLeft()
    {
        if (_firstInventorySpace > 0) {
            _firstInventorySpace--;
            RefreshInventory();
        }
    }

    public void MoveRight()
    {


        if (_firstInventorySpace > 0)
        {
            _firstInventorySpace--;
            RefreshInventory();
        }
    }


    // Update is called once per frame
    public void RefreshInventory()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        machines = InitialValues.Instance.machines;

        if (machines != null)
        {
            int j = _firstInventorySpace;
            int k = 0;
            for (int i = 0; i < machines.Count; i++)
            {
                


                if (machines[i].quantidade  == -111) //endless
                {
                    k++;
                    RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>(); //To instantiate you Icon on a pre-defined container
                    itemSlotRectTransform.gameObject.SetActive(true);
                    itemSlotRectTransform.anchoredPosition = new Vector2(j * itemSlotCellSize, y * itemSlotCellSize);
                    Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                    image.sprite = ItemAssets.Instance.GetSpriteMachine(machines[i].machine.name);
                    itemSlotRectTransform.GetComponent<DraggableItem>().nameString = machines[i].machine.nameString;
                    j++;
                    TextMeshProUGUI uiText = itemSlotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>();
                    uiText.SetText("-");

                }
                else if (machines[i].quantidade == 0)//Not instantiated
                {

                }
                else if(machines[i].quantidade > 0)
                {
                    k++;
                    RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>(); //To instantiate you Icon on a pre-defined container
                    itemSlotRectTransform.gameObject.SetActive(true);
                    itemSlotRectTransform.anchoredPosition = new Vector2(j * itemSlotCellSize, y * itemSlotCellSize);
                    Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                    image.sprite = ItemAssets.Instance.GetSpriteMachine(machines[i].machine.name);
                    itemSlotRectTransform.GetComponent<DraggableItem>().nameString = machines[i].machine.nameString;
                    j++;
                    TextMeshProUGUI uiText = itemSlotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>() as TextMeshProUGUI;
                    uiText.SetText(machines[i].quantidade.ToString());
                }
                else
                {
                    Debug.Log("Null");
                }

                if (k > 5)
                {
                    k = 0;
                    return;
                }


                        
            }


        }
    }
    
 

}
