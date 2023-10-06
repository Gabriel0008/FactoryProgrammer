using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_MaterialPanel : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI _nameField;
    [SerializeField] private TextMeshProUGUI _rarityField;
    [SerializeField] private TextMeshProUGUI _typeField;
    [SerializeField] private TextMeshProUGUI _purityField;
    [SerializeField] private TextMeshProUGUI _defectiveField;
    [SerializeField] private TextMeshProUGUI _priceField;
    [SerializeField] private GameObject Panel;


    public static bool isOpen = false;



    public void OpenMaterialPanel(MaterialsSO material)
    {
        Panel.SetActive(true);
        isOpen = true;
        Time.timeScale = 0;
        image.sprite = ItemAssets.Instance.GetSpriteMaterial(material.name);
        _nameField.SetText(material.nameString);
        _rarityField.SetText(material.rarity.ToString());
        _typeField.SetText(material.type);
        _purityField.SetText(material.purity.ToString());
        _defectiveField.SetText(material.defective.ToString());
        _priceField.SetText(material.price.ToString());

    }

    public void RefreshPanel(MaterialsSO material)
    {
        image.sprite = ItemAssets.Instance.GetSpriteMaterial(material.name); 
        _nameField.SetText(material.nameString);
        _rarityField.SetText(material.rarity.ToString());
        _typeField.SetText(material.type);
        _purityField.SetText(material.purity.ToString());
        _defectiveField.SetText(material.defective.ToString());
        _priceField.SetText(material.price.ToString());

    }

    public void CloseMaterialPanel()
    {
        Time.timeScale = 1;
        isOpen = false;
        Panel.SetActive(false);
    }

    
}
