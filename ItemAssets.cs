using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this; 
    }

    public Sprite GetSpriteMaterial(string name)
    {
        switch (name)
        {
            case "SOMinCarvao": return coalOre;
            case "SOMinOuro": return goldOre;
            case "SOMinFerro": return ironOre;
            case "SOMinPrata": return silverOre;
            case "SOBarraAco": return steelBar;
            case "SOBarraFerro": return ironBar;
            case "SOBarraOuro": return goldBar;
            case "SOBarraOuroBranco": return whiteGoldBar;
            case "SOBarraPrata": return silverBar;
            case "SOCarvao": return coal;
            case "SOFerro": return iron;
            case "SOOuro": return gold;
            case "SOPrata": return silver;
            case "SORestos": return waste;
            case "blank": return blank;
            default:
                return ItemAssets.Instance.error;

        }
    }

    public Sprite GetSpriteMachine(string name)
    {
        switch (name)
        {
            case "SOSwitch": return switchMachine;
            case "SOWhile": return whileMachine;
            case "SOIF": return iFmachine;
            case "SOEsteira": return belt;
            case "SOEsteiraConexao": return grabber;
            case "SODestroyer": return destroyer;
            case "SOSmelter": return smelter;
            case "SOSorter": return sorter;
            case "blank": return blank;
            default:
                return ItemAssets.Instance.error;

        }
    }

    
    public Sprite iFmachine;
    public Sprite switchMachine;
    public Sprite whileMachine;
    public Sprite belt;
    public Sprite grabber;
    public Sprite error;
    public Sprite destroyer;
    public Sprite smelter;
    public Sprite sorter;

    public Sprite blank;


    public Sprite goldOre;
    public Sprite ironOre;
    public Sprite silverOre;
    public Sprite coalOre;
    public Sprite gold;
    public Sprite iron;
    public Sprite silver;
    public Sprite coal;
    public Sprite waste;
    public Sprite goldBar;
    public Sprite ironBar;
    public Sprite silverBar;
    public Sprite steelBar;
    public Sprite whiteGoldBar;

}
