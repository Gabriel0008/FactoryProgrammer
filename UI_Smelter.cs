using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_Smelter : MonoBehaviour
{
    private const int OPT_IRON = 0; private const int OPT_STEEL = 1; private const int OPT_SILVER = 2; private const int OPT_GOLD = 3;

    [SerializeField] private GameObject recipePanel;
    [SerializeField] private Image ingredient01;
    [SerializeField] private Image ingredient02;
    [SerializeField] private Image result;
    [HideInInspector] public int CurrentRecipe { get; private set; }


    private void Start()
    {
        ingredient01.sprite = ItemAssets.Instance.GetSpriteMaterial("SOFerro");
        ingredient01.gameObject.GetComponent<ClickOpenPanel>().material = InitialValues.Instance.getMaterialSObyName("SOFerro");
        ingredient02.sprite = ItemAssets.Instance.GetSpriteMaterial("blank");
        result.sprite = ItemAssets.Instance.GetSpriteMaterial("SOBarraFerro");
        result.gameObject.GetComponent<ClickOpenPanel>().material = InitialValues.Instance.getMaterialSObyName("SOBarraFerro");

    }
    public void onClickRecipe()
    {
        if (recipePanel.gameObject.activeSelf == false)
        {
            recipePanel.SetActive(true);
        }
    }

    

    

    public void onClickIron()
    {
        ingredient01.sprite = ItemAssets.Instance.GetSpriteMaterial("SOFerro");
        ingredient01.gameObject.GetComponent<ClickOpenPanel>().material = InitialValues.Instance.getMaterialSObyName("SOFerro");
        ingredient02.sprite = ItemAssets.Instance.GetSpriteMaterial("blank");
        result.sprite = ItemAssets.Instance.GetSpriteMaterial("SOBarraFerro");
        result.gameObject.GetComponent<ClickOpenPanel>().material = InitialValues.Instance.getMaterialSObyName("SOBarraFerro");
        recipePanel.SetActive(false);
        CurrentRecipe = OPT_IRON;
    }

    public void onClickSteel()
    {
        ingredient01.sprite = ItemAssets.Instance.GetSpriteMaterial("SOFerro");
        ingredient01.gameObject.GetComponent<ClickOpenPanel>().material = InitialValues.Instance.getMaterialSObyName("SOFerro");
        ingredient02.sprite = ItemAssets.Instance.GetSpriteMaterial("SOCarvao");
        ingredient02.gameObject.GetComponent<ClickOpenPanel>().material = InitialValues.Instance.getMaterialSObyName("SOCarvao");
        result.sprite = ItemAssets.Instance.GetSpriteMaterial("SOBarraAco");
        result.gameObject.GetComponent<ClickOpenPanel>().material = InitialValues.Instance.getMaterialSObyName("SOBarraAco");
        recipePanel.SetActive(false);
        CurrentRecipe = OPT_STEEL;
    }

    public void onClickSilver()
    {
        ingredient01.sprite = ItemAssets.Instance.GetSpriteMaterial("SOPrata");
        ingredient01.gameObject.GetComponent<ClickOpenPanel>().material = InitialValues.Instance.getMaterialSObyName("SOPrata");
        ingredient02.sprite = ItemAssets.Instance.GetSpriteMaterial("blank");
        result.sprite = ItemAssets.Instance.GetSpriteMaterial("SOBarraPrata");
        result.gameObject.GetComponent<ClickOpenPanel>().material = InitialValues.Instance.getMaterialSObyName("SOBarraPrata");
        recipePanel.SetActive(false);
        CurrentRecipe = OPT_SILVER;
    }

    public void onClickGold()
    {
        ingredient01.sprite = ItemAssets.Instance.GetSpriteMaterial("SOOuro");
        ingredient01.gameObject.GetComponent<ClickOpenPanel>().material = InitialValues.Instance.getMaterialSObyName("SOOuro");
        ingredient02.sprite = ItemAssets.Instance.GetSpriteMaterial("blank");
        result.sprite = ItemAssets.Instance.GetSpriteMaterial("SOBarraOuro");
        result.gameObject.GetComponent<ClickOpenPanel>().material = InitialValues.Instance.getMaterialSObyName("SOBarraOuro");
        recipePanel.SetActive(false);
        CurrentRecipe = OPT_GOLD;
    }

    /*
    public bool checkCurrentMaterial (string name)
    {
        switch (CurrentRecipe)
        {
            case OPT_IRON:
                if (name == "SOFerro")
                    
                

        }
    }
    */

}
