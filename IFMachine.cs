using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFMachine : MonoBehaviour
{
    [SerializeField] private PlacedObject_Done placedObject;
    [SerializeField] private IFMachineController iFMachineController;
    [SerializeField] private GameObject UI;
    MaterialsSO materialS0;
    [HideInInspector]public Vector2Int outIf;
    [HideInInspector]public Vector2Int outElse;
    [HideInInspector] public Vector2Int outElse2;


    private bool newMaterial = false;
    
    private const int objCharacteristics_NOME = 0; private const int objCharacteristics_RARIDADE = 1;private const int objCharacteristics_TIPO = 2; private const int objCharacteristics_PUREZA =3; private const int objCharacteristics_DEFEITUOSO = 4; private const int objCharacteristics_PRECO = 5;
    private const int optSignal_MAIOR = 0;private const int optSignal_MAIOR_IGUAL = 1;private const int optSignal_MENOR = 2;private const int optSignal_MENOR_IGUAL = 3;private const int optSignal_IGUAL = 4;private const int optSignal_DIFERENTE = 5;
    private const int optPrimitiveType_INT = 0; private const int optPrimitiveType_FLOAT = 1; private const int optPrimitiveType_STRING = 2;private const int optPrimitiveType_BOOL = 3;
    private const int check_TRUE = 0;  private const int check_FALSE = 1; private const int check_ERROR = 2;
    private bool instantiated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(placedObject.placed == true && instantiated == false){

            outIf = placedObject.GetOrigin() + -1* placedObject.GetForwardVector();
            Debug.Log("oUTiF - " + outIf);
            string direcao = placedObject.DirToString();
            outElse = placedObject.GetOrigin() + -1*getLeft(direcao);
            outElse2 = placedObject.GetOrigin() + getLeft(direcao);
            instantiated = true;
            /*
            GameObject.Find("UI_Player").GetComponent<MenusController>().CloseAllMenus();
            UI.SetActive(true);
            */
            GridBuildingSystem3D.Instance.DeselectObjectType();
        
        }

        if(newMaterial == true){
            Debug.Log("New");
            int check = CheckIF();
            if(check == check_TRUE){
                Debug.Log("True + outIf - " +outIf);
                if (!GridBuildingSystem3D.Instance.CheckPosition(outIf)){
                    Debug.Log("True23 + outIf - " + outIf);
                    if (CheckEsteira(GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(outIf), outIf, placedObject.GetOrigin())) {
                        Transform materialtrue = Instantiate(materialS0.prefab, GridBuildingSystem3D.Instance.GridToWorldPosition(outIf), Quaternion.identity);
                        materialtrue.position = Vector3.MoveTowards(GridBuildingSystem3D.Instance.GridToWorldPosition(placedObject.GetOrigin()), GridBuildingSystem3D.Instance.GridToWorldPosition(outIf), 5f * Time.deltaTime);
                        MaterialMovment mm = materialtrue.GetComponent(typeof(MaterialMovment)) as MaterialMovment;
                        mm.backLeanning = true;
                        mm.rightNextPosition = outIf;
                        placedObject.occupied = false;
                        newMaterial = false;
                    } 
                }
            }else if(CheckIF() == check_FALSE)
            {
                if (!GridBuildingSystem3D.Instance.CheckPosition(outElse))
                {
                    if (CheckEsteira(GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(outElse), outElse, placedObject.GetOrigin()))
                    {
                        Transform materialfalse = Instantiate(materialS0.prefab, GridBuildingSystem3D.Instance.GridToWorldPosition(outElse), Quaternion.identity);
                        materialfalse.position = Vector3.MoveTowards(GridBuildingSystem3D.Instance.GridToWorldPosition(placedObject.GetOrigin()), GridBuildingSystem3D.Instance.GridToWorldPosition(outElse), 5f * Time.deltaTime);
                        MaterialMovment mm = materialfalse.GetComponent(typeof(MaterialMovment)) as MaterialMovment;
                        mm.leftLeaning = true;
                        placedObject.occupied = false;
                        newMaterial = false;
                    }
                
                    

                }else if (!GridBuildingSystem3D.Instance.CheckPosition(outElse2))
                 if (CheckEsteira(GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(outElse2), outElse2, placedObject.GetOrigin()))
                {
                    Transform materialfalse = Instantiate(materialS0.prefab, GridBuildingSystem3D.Instance.GridToWorldPosition(outElse2), Quaternion.identity);
                    materialfalse.position = Vector3.MoveTowards(GridBuildingSystem3D.Instance.GridToWorldPosition(placedObject.GetOrigin()), GridBuildingSystem3D.Instance.GridToWorldPosition(outElse2), 5f * Time.deltaTime);
                    MaterialMovment mm = materialfalse.GetComponent(typeof(MaterialMovment)) as MaterialMovment;
                    mm.rightLeaning = true;
                    mm.rightNextPosition = outElse2;
                    placedObject.occupied = false;
                    newMaterial = false;
                }
            }
            else
            {
            Debug.Log("ERROR");
            }



        }


    }

    public void OpenMachineInfo()
    {
        iFMachineController.CloseTab();
        GameObject.Find("UI_Player").GetComponent<MachinePanelManager>().OpenMachinePanel(placedObject.GetPlacedObjectTypeSO().nameString);
    }

    public void ClickMove()
    {
        UI.SetActive(false);
        GridBuildingSystem3D.Instance.SetposBeforeMoving(placedObject.GetOrigin());
        GridBuildingSystem3D.Instance.SetPlacedObjectTypeSO(placedObject.GetPlacedObjectTypeSO());
        GameObject.Find("UI_Player").GetComponent<MenusController>().OpenOptionsMoveButtons();
    }

    private int CheckIF(){

        switch(iFMachineController.objCharacteristics){
            case objCharacteristics_NOME:return CheckName();
            case objCharacteristics_RARIDADE:return CheckRarity();
            case objCharacteristics_TIPO:return CheckType();
            case objCharacteristics_PUREZA:return CheckPurity();
            case objCharacteristics_PRECO:return CheckPrice();
            case objCharacteristics_DEFEITUOSO: return CheckDefective();
            default:return check_ERROR;
        }

    }

    private int CheckName(){

        if(iFMachineController.optPrimitiveType != optPrimitiveType_STRING ){
                Debug.Log("1 OBJETOS NAO PODEM SER COMPARADOS");
                return check_ERROR;

            }else{
                List<string> options = InitialValues.Instance.MaterialsStrings();
                switch(iFMachineController.optSignal){
                    case optSignal_IGUAL:
                        if ("'"+materialS0.nameString+"'" == options[iFMachineController.stringValue] ){
                        return check_TRUE;
                        }else{
                        return check_FALSE;
                        }
                    case optSignal_DIFERENTE:
                        if("'"+materialS0.nameString+"'" != options[iFMachineController.stringValue] ){
                        return check_TRUE;
                        }else{
                        return check_FALSE;
                        }
                    default:
                        return check_ERROR;
                }
                

            }
        
    }

    private int CheckType(){
        if(iFMachineController.optPrimitiveType != optPrimitiveType_STRING ){
                Debug.Log("2 OBJETOS NAO PODEM SER COMPARADOS");
                return check_ERROR;

            }else{
                List<string> options = InitialValues.Instance.MaterialsStrings();
                switch(iFMachineController.optSignal){
                    case optSignal_IGUAL:
                        if("'"+materialS0.type+"'" == options[iFMachineController.stringValue] ){
                        return check_TRUE;
                        }else{
                        return check_FALSE;
                        }
                    case optSignal_DIFERENTE:
                        if("'"+materialS0.type+"'" != options[iFMachineController.stringValue] ){
                        return check_TRUE;
                        }else{
                        return check_FALSE;
                        }
                    default:
                        return check_ERROR;
                }
                

            }

    }

    private int CheckRarity(){
        if((iFMachineController.optPrimitiveType == optPrimitiveType_INT)|(iFMachineController.optPrimitiveType == optPrimitiveType_FLOAT)){
            switch(iFMachineController.optSignal){
                case optSignal_MAIOR:
                if(materialS0.rarity > iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_MAIOR_IGUAL:
                if(materialS0.rarity >= iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_MENOR:
                if(materialS0.rarity < iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_MENOR_IGUAL:
                if(materialS0.rarity <= iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_IGUAL:
                if(materialS0.rarity == iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_DIFERENTE:
                if(materialS0.rarity != iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                default:return check_ERROR;
            }
        }else{
            Debug.Log("3 OBJETOS NAO PODEM SER COMPARADOS");
            return check_ERROR;
        }

    }

    private int CheckPurity(){
        if((iFMachineController.optPrimitiveType == optPrimitiveType_INT)|(iFMachineController.optPrimitiveType == optPrimitiveType_FLOAT)){
            switch(iFMachineController.optSignal){
                case optSignal_MAIOR:
                if(materialS0.purity > iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_MAIOR_IGUAL:
                if(materialS0.purity >= iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_MENOR:
                if(materialS0.purity < iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_MENOR_IGUAL:
                if(materialS0.purity <= iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_IGUAL:
                if(materialS0.purity == iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_DIFERENTE:
                if(materialS0.purity != iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                default:return check_ERROR;
            }
        }else{
            Debug.Log("4 OBJETOS NAO PODEM SER COMPARADOS");
            return check_ERROR;
        }
    }

    private int CheckPrice(){
        if((iFMachineController.optPrimitiveType == optPrimitiveType_INT)|(iFMachineController.optPrimitiveType == optPrimitiveType_FLOAT)){
            switch(iFMachineController.optSignal){
                case optSignal_MAIOR:
                if(materialS0.price > iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_MAIOR_IGUAL:
                if(materialS0.price >= iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_MENOR:
                if(materialS0.price < iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_MENOR_IGUAL:
                if(materialS0.price <= iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_IGUAL:
                if(materialS0.price == iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                case optSignal_DIFERENTE:
                if(materialS0.price != iFMachineController.intFloatValue){return check_TRUE;}
                else{return check_FALSE;}
                default:return check_ERROR;
            }
        }else{
            Debug.Log("5 OBJETOS NAO PODEM SER COMPARADOS");
            return check_ERROR;
        }
    }

    private int CheckDefective(){
        if(iFMachineController.optPrimitiveType != optPrimitiveType_BOOL ){
                Debug.Log("6 OBJETOS NAO PODEM SER COMPARADOS");
                return check_ERROR;
            }else{
                switch(iFMachineController.optSignal){
                    case optSignal_IGUAL:
                        if(materialS0.defective == iFMachineController.boolValue){return check_TRUE;}
                        else{return check_FALSE;}
                    case optSignal_DIFERENTE:
                        if(materialS0.defective != iFMachineController.boolValue){return check_TRUE;}
                        else{return check_FALSE;}
                    default:return check_ERROR;
                }
            }
    }

    private bool CheckEsteira(PlacedObjectTypeSO myTypeSOInArray,Vector2Int myPosInGrid,Vector2Int myOrigin){

        PlacedObject_Done  placedObjectDone = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(myPosInGrid);
        Vector2Int nextPosition;
        nextPosition = placedObjectDone.GetOrigin() + placedObjectDone.GetForwardVector();


        if(myTypeSOInArray.nameString == "Esteira" && nextPosition != myOrigin && placedObjectDone.occupied == false){
            return true;
        }else{
            return false;
        }
    }



    public void getMaterialSO(MaterialsSO getmaterialSO){
        materialS0 = getmaterialSO;
        newMaterial = true;

    }

    private Vector2Int getLeft(string direcao){
        switch(direcao){
                default:
                case "Up": return new Vector2Int(-1, 0);
                case "Down": return new Vector2Int(1, 0);
                case "Left": return new Vector2Int(0, -1);
                case "Right": return new Vector2Int(0, 1);

        }
    }


}
