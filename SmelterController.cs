using UnityEngine;

public class SmelterController : MonoBehaviour
{
    [SerializeField] private float smeltingTime = 5f;
    private float _timer;
    [HideInInspector]public bool smelting = false;
    private const int OPT_IRON = 0; private const int OPT_STEEL = 1; private const int OPT_SILVER = 2; private const int OPT_GOLD = 3;
    private Materials Ingrediente1;
    private Materials Ingrediente2;
    [HideInInspector] public Materials result;

    public GameObject UI;

    public void Start()
    {
        Ingrediente2 = new Materials();
        result = new Materials();
        result.quantidade = 0;
        Ingrediente1 = new Materials();
    }

    public void OpenMachineInfo()
    {
        onClickClose();
        GameObject.Find("UI_Player").GetComponent<MachinePanelManager>().OpenMachinePanel(gameObject.GetComponent<PlacedObject_Done>().GetPlacedObjectTypeSO().nameString);
    }


    void Update()
    {

        if (Ingrediente1.quantidade > 0 && smelting == false)
        {
            SmeltingProcess();
        }

        if (_timer >= 0 && smelting == true)
        {
            _timer = _timer - Time.deltaTime;
        }
        if (_timer < 0)
        {
            SmeltingResult();
            smelting = false;
        }
    }

    public void onClickClose()
    {
        UI.SetActive(false);
    }
    public void onClickMove()
    {
        UI.SetActive(false);
        PlacedObject_Done placedObject = this.gameObject.GetComponent<PlacedObject_Done>();
        GridBuildingSystem3D.Instance.SetposBeforeMoving(placedObject.GetOrigin());
        GridBuildingSystem3D.Instance.SetPlacedObjectTypeSO(placedObject.GetPlacedObjectTypeSO());
        GameObject.Find("UI_Player").GetComponent<MenusController>().OpenOptionsMoveButtons();
    }

    private void SmeltingResult()
    {
        switch (this.gameObject.GetComponent<UI_Smelter>().CurrentRecipe)
        {
            case OPT_IRON:
                if (result.material == null)
                {
                    Debug.Log("1");
                    result.material = InitialValues.Instance.getMaterialSObyName("SOBarraFerro");
                    result.quantidade = 1;
                }
                else if (result.material.name != "SOBarraFerro")
                {
                    Debug.Log("2");
                    result.material = InitialValues.Instance.getMaterialSObyName("SOBarraFerro");
                    result.quantidade = 1;
                }
                else
                {
                    Debug.Log("3");
                    result.AddQuantidade();
                    Debug.Log("3 -  " + result.quantidade.ToString());
                }

                break;

            case OPT_SILVER:
                if (result.material == null)
                {
                    result.material = InitialValues.Instance.getMaterialSObyName("SOBarraPrata");
                    result.AddQuantidade();
                }
                else if (result.material.name != "SOBarraPrata")
                {
                    result.material = InitialValues.Instance.getMaterialSObyName("SOBarraPrata");
                    result.quantidade = 1;
                }
                else
                {
                    result.AddQuantidade();
                }

                break;

            case OPT_STEEL:
                if (result.material == null)
                {
                    result.material = InitialValues.Instance.getMaterialSObyName("SOBarraAco");
                    result.AddQuantidade();
                }
                else if (result.material.name != "SOBarraAco")
                {
                    result.material = InitialValues.Instance.getMaterialSObyName("SOBarraAco");
                    result.quantidade = 1;
                }
                else
                {
                    result.AddQuantidade();
                }
                break;
            case OPT_GOLD:
                if (result.material == null)
                {
                    result.material = InitialValues.Instance.getMaterialSObyName("SOBarraOuro");
                    result.AddQuantidade();
                }
                else if (result.material.name != "SOBarraOuro")
                {
                    result.material = InitialValues.Instance.getMaterialSObyName("SOBarraOuro");
                    result.quantidade = 1;
                }
                else
                {
                    result.AddQuantidade();
                }

                break;

            default:
                Debug.Log("erro");
                break;

        }
        _timer = smeltingTime;
    }

    private void SmeltingProcess()
    {
        switch (this.gameObject.GetComponent<UI_Smelter>().CurrentRecipe)
        {
            case OPT_IRON:
                if(Ingrediente1.material.name == "SOFerro" && Ingrediente1.quantidade > 0)
                {
                    Ingrediente1.quantidade--;
                    smelting = true;
                }
                break;

            case OPT_SILVER:
                if (Ingrediente1.material.name == "SOPrata" && Ingrediente1.quantidade > 0)
                {
                    Ingrediente1.quantidade--;
                    smelting = true;
                }
                break;

            case OPT_STEEL:
                if ((Ingrediente1.material.name == "SOFerro" && Ingrediente1.quantidade > 0)&&
                    (Ingrediente2.material.name == "SOCarvao" && Ingrediente2.quantidade > 0))
                {
                    Ingrediente1.quantidade--;
                    Ingrediente2.quantidade--;
                    smelting = true;
                }
                break;


            case OPT_GOLD:
                if (Ingrediente1.material.name == "SOOuro" && Ingrediente1.quantidade > 0)
                {
                    Ingrediente1.quantidade--;
                    smelting = true;
                }
                break;

        }
    }

    public void SetMaterial(MaterialsSO ingrediente)
    {
        switch (this.gameObject.GetComponent<UI_Smelter>().CurrentRecipe)
        {
            case OPT_IRON:
                if (Ingrediente1.material == null)
                {
                    Ingrediente1.material = ingrediente;
                    Ingrediente1.AddQuantidade();
                }
                else if (Ingrediente1.material.name != "SOFerro")
                {
                    Ingrediente1.material = ingrediente;
                    Ingrediente1.quantidade = 1;
                }
                else
                {
                    Ingrediente1.AddQuantidade();
                }

                break;

            case OPT_SILVER:
                if (Ingrediente1.material == null)
                {
                    Ingrediente1.material = ingrediente;
                    Ingrediente1.AddQuantidade();
                }
                else if (Ingrediente1.material.name != "SOPrata")
                {
                    Ingrediente1.material = ingrediente;
                    Ingrediente1.quantidade = 1;
                }
                else
                {
                    Ingrediente1.AddQuantidade();
                }

                break;

            case OPT_STEEL:

                if (ingrediente.name == "SOFerro")
                {

                    if (Ingrediente1.material == null)
                    {
                        Ingrediente1.material = ingrediente;
                        Ingrediente1.AddQuantidade();
                    }
                    else if (Ingrediente1.material.name != "SOFerro")
                    {
                        Ingrediente1.material = ingrediente;
                        Ingrediente1.quantidade = 1;
                    }
                    else
                    {
                        Ingrediente1.AddQuantidade();
                    }
                }
                else
                {
                    if (Ingrediente2.material == null)
                    {
                        Ingrediente2.material = ingrediente;
                        Ingrediente2.AddQuantidade();
                    }
                    else if (Ingrediente2.material.name != "SOCarvao")
                    {
                        Ingrediente2.material = ingrediente;
                        Ingrediente2.quantidade = 1;
                    }
                    else
                    {
                        Ingrediente2.AddQuantidade();
                    }
                }
                break;
            case OPT_GOLD:
                if (Ingrediente1.material == null)
                {
                    Ingrediente1.material = ingrediente;
                    Ingrediente1.AddQuantidade();
                }
                else if (Ingrediente1.material.name != "SOOuro")
                {
                    Ingrediente1.material = ingrediente;
                    Ingrediente1.quantidade = 1;
                }
                else
                {
                    Ingrediente1.AddQuantidade();
                }

                break;

            default:
                Debug.Log("erro");
                    break;
        
        }
        smelting = true;
        _timer = smeltingTime;
    

    }

    public bool CheckIfCorretIngredient(MaterialsSO ingrediente)
    {
        switch (this.gameObject.GetComponent<UI_Smelter>().CurrentRecipe)
        {
            case OPT_IRON:
                if(ingrediente.name == "SOMinFerro")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case OPT_SILVER:
                if (ingrediente.name == "SOMinPrata")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case OPT_STEEL:
                if (ingrediente.name == "SOMinFerro" || ingrediente.name == "SOMinCarvao")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case OPT_GOLD:
                if (ingrediente.name == "SOMinOuro")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            default:
                return false;
        }
    }
}
