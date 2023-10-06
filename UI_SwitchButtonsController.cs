using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_SwitchButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _intField;
    [SerializeField] private GameObject _stringField;

    private int _primitiveType;
    private int _intValue;
    private int _resultValue;

    public void ShowButtons()
    {
        _buttons.SetActive(true);
    }
    public void HideButtons()
    {
        _buttons.SetActive(false);
    }

    private void Start()
    {
        _stringField.GetComponent<TMP_Dropdown>().ClearOptions();
        List<string> options = InitialValues.Instance.MaterialsStrings();
        _stringField.GetComponent<TMP_Dropdown>().AddOptions(options);


        
    }


    public void ClickGetStringField(int stringNumber)
    {
        _resultValue = stringNumber;
    }

    public void SetTotalNumber(int totalNumber)
    {
        _intValue = totalNumber;
    }

    public int GetResultValue()
    {
        return _resultValue;
    }

    public int GetPrimitiveType()
    {
        return _primitiveType;

    }

    public void ClickGetPrimitiveType(int primitiveType)
    {
        switch (primitiveType)
        {
            case 0://String
                _intField.SetActive(false);
                _stringField.SetActive(true);
                break;
            case 1://Int
                _intField.SetActive(true);
                _stringField.SetActive(false);
                break;
            default:
                Debug.Log("ERROR - Switch Machine- Case Primitive Type Panel");
                break;
        }
    }


}
