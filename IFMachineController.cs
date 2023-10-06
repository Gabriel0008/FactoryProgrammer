using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class IFMachineController : MonoBehaviour
{
        [SerializeField] private GameObject uIIFMachine;
        [SerializeField] private GameObject intField;
        [SerializeField] private GameObject floatField;
        [SerializeField] private GameObject stringField;
        [SerializeField] private GameObject boolField;

        public TMP_Dropdown dropdown;

        private float floatValue =0f;
        private float intValue =0f;
        [HideInInspector] public float intFloatValue;
        [HideInInspector] public int stringValue;
        [HideInInspector] public bool boolValue;
        [HideInInspector] public int objCharacteristics;
        [HideInInspector] public int optSignal;
        [HideInInspector] public int optPrimitiveType;

        




        private void Awake() {
            intField.SetActive(true);
            floatField.SetActive(false);
            boolField.SetActive(false);
            intValue = 0f;
            floatValue = 0f;            
            intFloatValue = floatValue + intValue;
            stringValue = 0;
            boolValue = false;
            objCharacteristics = 0;
            optSignal = 0;
            optPrimitiveType = 0;
        }

        private void Start() {
            dropdown.ClearOptions();
            List<string> options = InitialValues.Instance.MaterialsStrings();
            dropdown.AddOptions(options);
            
        
        }

   

    
    
    public void GetInfo(int info){
        objCharacteristics = info;
        
    }
    public void GetStringValue (int value){
        stringValue = value;
    }
    public void GetComparingSignal(int signal){
        optSignal = signal;

    }
    public void GetPrimitiveTypes(int primitiveType){
        optPrimitiveType = primitiveType;
        switch(primitiveType){
            case 0://int
            floatValue = 0f;  
            intField.SetActive(true);
            floatField.SetActive(false);
            boolField.SetActive(false);
            stringField.SetActive(false);
             break;
            case 1:  //float
            intField.SetActive(true);
            floatField.SetActive(true);
            boolField.SetActive(false);
            stringField.SetActive(false);
            break;
            case 2: //string
            intField.SetActive(false);
            floatField.SetActive(false);
            boolField.SetActive(false);
            stringField.SetActive(true);
            break;
            case 3: //bool
            intField.SetActive(false);
            floatField.SetActive(false);
            boolField.SetActive(true);
            stringField.SetActive(false);
            break;
            default:  
            intField.SetActive(true);
            floatField.SetActive(false);
            boolField.SetActive(false);
            stringField.SetActive(false);
            break;


        }

    }

    

    public void SetTotalNumber (float totalNumber){
        intValue = totalNumber;
        intFloatValue = floatValue + intValue;
    }
    public void SetFloatNumber (float floatNumber){
        
        floatValue = floatNumber;
        intFloatValue = floatValue + intValue;
        
    }

     public void GetBoolValue(int getBoolValue){
        if(getBoolValue == 0){
            boolValue = true;
        }else{
            boolValue = false;
        }
    }


    public void CloseTab(){
        uIIFMachine.SetActive(false);
    }



}

