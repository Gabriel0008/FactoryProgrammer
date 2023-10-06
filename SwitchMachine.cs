using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMachine : MonoBehaviour
{
    [SerializeField] private SwitchController switchController;
    [SerializeField] private GameObject ui_canvas;
    [SerializeField] private PlacedObjectTypeSO caseSO;
    private PlacedObject_Done placedObject;
    private MaterialsSO _newMaterialSO;
    private bool _newMaterial = false;
    private bool _instantiated = false;
    private Vector2Int[] _outCase = new Vector2Int[4];
    private Vector2Int[] _caseInGrid = new Vector2Int[4];
    private int _objAttribute;
    private int _check;

    private const int PRIMITIVE_TYPE_STRING = 0; private const int PRIMITIVE_TYPE_INT = 1;
    private const int ATTRIBUTE_NAME = 0; private const int ATTRIBUTE_RARITY = 1; private const int ATTRIBUTE_TYPE = 2; private const int ATTRIBUTE_WEIGHT = 3; private const int ATTRIBUTE_DEFECTIVE = 4; private const int ATTRIBUTE_PRICE = 5;
    private const int CHECK_TRUE = 0; private const int CHECK_FALSE = 1; private const int CHECK_ERROR = 2;

    private void Start()
    {
        placedObject = this.gameObject.GetComponent<PlacedObject_Done>();
        _objAttribute = switchController.ObjAttribute;


    }

    public void OpenMachineInfo()
    {
        ClosePanel();
        GameObject.Find("UI_Player").GetComponent<MachinePanelManager>().OpenMachinePanel(placedObject.GetPlacedObjectTypeSO().nameString);
    }

    public void PlaceCaseInGrid(int index)
    {
        GridBuildingSystem3D.Instance.PlaceMachineInGrid(caseSO, _caseInGrid[index]);
    }
    public void RemoveCaseFromGrid(int index)
    {
        GridBuildingSystem3D.Instance.RemoveMachineFromGrid(_caseInGrid[index]);
    }

    void Update()
    {

        if (placedObject.placed == true && _instantiated == false)
        {
            string direcao = placedObject.DirToString();
            _outCase[0] = placedObject.GetOrigin() + -1 * getLeft(direcao) +  0*placedObject.GetForwardVector();
            _outCase[1] = placedObject.GetOrigin() + -1 * getLeft(direcao) +  -1* placedObject.GetForwardVector();
            _outCase[2] = placedObject.GetOrigin() + -1 * getLeft(direcao) + -2* placedObject.GetForwardVector();
            _outCase[3] = placedObject.GetOrigin() + -1 * getLeft(direcao) + -3* placedObject.GetForwardVector();
            if (direcao == "Up"  )
            {
                _caseInGrid[0] = placedObject.GetOrigin() + new Vector2Int(0,1);
                _caseInGrid[1] = placedObject.GetOrigin() + -1 * placedObject.GetForwardVector()+ new Vector2Int(0, 1);
                _caseInGrid[2] = placedObject.GetOrigin() + -2 * placedObject.GetForwardVector()+ new Vector2Int(0, 1);
                _caseInGrid[3] = placedObject.GetOrigin() + -3 * placedObject.GetForwardVector()+ new Vector2Int(0, 1);

            }
            else if(direcao == "Right")
            {
                _caseInGrid[0] = placedObject.GetOrigin() + new Vector2Int(1, 0);
                _caseInGrid[1] = placedObject.GetOrigin() + -1 * placedObject.GetForwardVector() + new Vector2Int(1, 0);
                _caseInGrid[2] = placedObject.GetOrigin() + -2 * placedObject.GetForwardVector() + new Vector2Int(1, 0);
                _caseInGrid[3] = placedObject.GetOrigin() + -3 * placedObject.GetForwardVector() + new Vector2Int(1, 0);
            }
            else
            {
                _caseInGrid[0] = placedObject.GetOrigin();
                _caseInGrid[1] = placedObject.GetOrigin() + -1 * placedObject.GetForwardVector();
                _caseInGrid[2] = placedObject.GetOrigin() + -2 * placedObject.GetForwardVector();
                _caseInGrid[3] = placedObject.GetOrigin() + -3 * placedObject.GetForwardVector();
            }

            _instantiated = true;
            /*
            ui_canvas.SetActive(true);
            */
            GridBuildingSystem3D.Instance.DeselectObjectType();


        }

        if(_newMaterial == true)
        {
            
            
            
            int numCases;
            int[] casePrimitiType = new int[5];
            int[] caseValueResult = new int[5];
            switchController.GetCasesInfo(out numCases,out casePrimitiType,out caseValueResult);


            for(int i = 2; i<= numCases; i++)
            {
                _check = CheckSwitch(casePrimitiType[i], caseValueResult[i]);
                Debug.Log(CHECK_TRUE.ToString());
                if (_check == CHECK_TRUE)
                {
                    if (!GridBuildingSystem3D.Instance.CheckPosition(_outCase[i-1]))
                    {
                        if (CheckEsteira(GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(_outCase[i-1]), _outCase[i - 1], _caseInGrid[i-1]))
                        {
                            Transform materialcase = Instantiate(_newMaterialSO.prefab, GridBuildingSystem3D.Instance.GridToWorldPosition(_caseInGrid[(i - 1)]), Quaternion.identity);
                            Debug.Log("CaseInGrid - " + _caseInGrid[i - 1]);
                            materialcase.position = Vector3.MoveTowards(materialcase.position, GridBuildingSystem3D.Instance.GridToWorldPosition(_outCase[i - 1]), 5f * Time.deltaTime);
                            MaterialMovment mm = materialcase.GetComponent(typeof(MaterialMovment)) as MaterialMovment;
                            mm.rightLeaning = true;
                            mm.rightNextPosition = _outCase[i-1];
                            placedObject.occupied = false;

                            _newMaterial = false;
                            break;
                           
                        }
                    }
                }
            }
            if (_check == CHECK_FALSE)
            {
                if (!GridBuildingSystem3D.Instance.CheckPosition(_outCase[0]))
                {
                    if (CheckEsteira(GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO(_outCase[0]), _outCase[0], _caseInGrid[0]))
                    {
                        Transform materialcase = Instantiate(_newMaterialSO.prefab, GridBuildingSystem3D.Instance.GridToWorldPosition(_caseInGrid[0]), Quaternion.identity);
                        materialcase.position = Vector3.MoveTowards(materialcase.position, GridBuildingSystem3D.Instance.GridToWorldPosition(_outCase[0]), 5f * Time.deltaTime);
                        MaterialMovment mm = materialcase.GetComponent(typeof(MaterialMovment)) as MaterialMovment;
                        mm.rightLeaning = true;
                        mm.rightNextPosition = _outCase[0];
                        placedObject.occupied = false;
                        _newMaterial = false;
                    }
                }
            }

        }




    }

    public Vector2Int GetOutCase(int index)
    {
        return _outCase[index];
    }

    public int CheckSwitch(int primitiveType, int valueResult)
    {
        if(_objAttribute == ATTRIBUTE_NAME || _objAttribute == ATTRIBUTE_TYPE || _objAttribute == ATTRIBUTE_RARITY)
        {
            if(_objAttribute == ATTRIBUTE_RARITY)
            {
                if(primitiveType == PRIMITIVE_TYPE_INT)
                {
                    return CheckInt(valueResult);
                }
                else
                {
                    return CHECK_ERROR;
                }
            }
            else
            {
                if(primitiveType == PRIMITIVE_TYPE_STRING)
                {
                    return CheckString(valueResult);
                }
                else
                {
                    return CHECK_ERROR;
                }

            }
        }
        else
        {
            return CHECK_ERROR;
        }
    }

    private int CheckString(int stringValue)
    {
        List<string> options = InitialValues.Instance.MaterialsStrings();

        if("'"+_newMaterialSO.nameString+"'" == options[stringValue])
        {
            return CHECK_TRUE;
        }
        else
        {
            return CHECK_FALSE;
        }
    }

    private int CheckInt(int intValue)
    {
        if(_newMaterialSO.rarity == intValue)
        {
            return CHECK_TRUE;
        }
        else
        {
            return CHECK_FALSE;
        }
    }


    private bool CheckEsteira(PlacedObjectTypeSO myTypeSOInArray, Vector2Int myPosInGrid, Vector2Int myOrigin)
    {

        PlacedObject_Done placedObjectDone = GridBuildingSystem3D.Instance.GetPlacedObjectOnGrid(myPosInGrid);
        Vector2Int nextPosition;
        nextPosition = placedObjectDone.GetOrigin() + placedObjectDone.GetForwardVector();


        if (myTypeSOInArray.nameString == "Esteira" && nextPosition != myOrigin && placedObjectDone.occupied == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void ClickMove()
    {
        ui_canvas.SetActive(false);
        GridBuildingSystem3D.Instance.SetposBeforeMoving(placedObject.GetOrigin());
        GridBuildingSystem3D.Instance.SetPlacedObjectTypeSO(placedObject.GetPlacedObjectTypeSO());
        GameObject.Find("UI_Player").GetComponent<MenusController>().OpenOptionsMoveButtons();
    }

    public void ClosePanel()
    {
        ui_canvas.SetActive(false);
    }

    public Vector2Int GetCaseInGrid(int index)
    {
        return _caseInGrid[index];
    }


    public void SetMaterialSO(MaterialsSO setmaterialSO)
    {
        _newMaterialSO = setmaterialSO;
        _newMaterial = true;

    }

    private Vector2Int getLeft(string direcao)
    {
        switch (direcao)
        {
            default:
            case "Up": return new Vector2Int(-1, -1);
            case "Down": return new Vector2Int(1, 0);
            case "Left": return new Vector2Int(0, -1);
            case "Right": return new Vector2Int(-1, 1);

        }
    }
}


