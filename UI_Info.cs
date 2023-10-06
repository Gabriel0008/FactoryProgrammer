using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Info : MonoBehaviour
{
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;

    int x = 0;
    float itemSlotCellSize = 70f;
    private List<Materials> materials = null;

    void Start()
    {
        RefreshInventory();


    }


    // Update is called once per frame
    public void RefreshInventory()
    {
        
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        materials = InitialValues.Instance.materials;

        if (materials != null)
        {
            
            int j = 0;
            for (int i = 0; i < materials.Count; i++)
            {

             if (materials[i].quantidade <= 0)//Not instantiated
                {
                }
                else
                {
                    RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>(); //To instantiate you Icon on a pre-defined container
                    itemSlotRectTransform.gameObject.SetActive(true);
                    itemSlotRectTransform.gameObject.GetComponent<ClickOpenPanel>().material = materials[i].material;
                    itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize,- j * itemSlotCellSize);
                    Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                    image.sprite = ItemAssets.Instance.GetSpriteMaterial(materials[i].material.name);
                    j++;
                    TextMeshProUGUI uiText = itemSlotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>() as TextMeshProUGUI;
                    uiText.SetText(materials[i].quantidade.ToString());
                }

            }


        }
    }



}
