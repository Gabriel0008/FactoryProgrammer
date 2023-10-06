using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private GameObject caseTemplate;
    [SerializeField] private GameObject defaultTemplate;
    [SerializeField] private GameObject[]  caseOpt = new GameObject[5];
    [SerializeField] private SwitchMachine switchMachine;
    private int _numCases = 2;
    private int[] _casesPrimitiveTypes = new int[5];
    private int[] _casesValueResult = new int[5];

    
    public int ObjAttribute { get; private set; }

    
    public void GetCasesInfo(out int numCase, out int[] casesPrimitiveType, out int[] casesValueResult)
    {
        _casesPrimitiveTypes[1] = 0;
        _casesValueResult[1] = 0;
        for (int i = 2; i < 5;i++)
        {
            if (caseOpt[i].activeSelf == true)
            {
                _casesPrimitiveTypes[i] = caseOpt[i].GetComponent<UI_SwitchButtonsController>().GetPrimitiveType();
                _casesValueResult[i] = caseOpt[i].GetComponent<UI_SwitchButtonsController>().GetResultValue();
            }
            else
            {
                _casesPrimitiveTypes[i] = 0;
                _casesValueResult[i] = 0;
            }
            
        }
        numCase = _numCases;
        casesPrimitiveType = _casesPrimitiveTypes;
        casesValueResult = _casesValueResult;
    }


    

    public void ClickGetAttribute(int atribute)
    {
        ObjAttribute = atribute;
    }



    public void ClickAddOpt()
    {
        if (_numCases < 4)
        {

            Vector2Int posInGrid = new Vector2Int();
            posInGrid = switchMachine.GetCaseInGrid(_numCases);
            if (GridBuildingSystem3D.Instance.CheckCanBuild(posInGrid)){
                switchMachine.PlaceCaseInGrid(_numCases);
                caseOpt[_numCases].GetComponent<UI_SwitchButtonsController>().HideButtons();
                _numCases++;
                caseOpt[_numCases].SetActive(true);
            }
        }

    }
    public void ClickSubtractOpt()
    {
        if (_numCases > 2)
        {
            caseOpt[_numCases].SetActive(false);
            _numCases--;
            switchMachine.RemoveCaseFromGrid(_numCases);
            caseOpt[_numCases].GetComponent<UI_SwitchButtonsController>().ShowButtons();



        }

    }

    public int GetNumCases()
    {
        return _numCases;
    }



}
