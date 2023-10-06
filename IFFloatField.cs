using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IFFloatField : MonoBehaviour
{
    [SerializeField] private IFMachineController iFMachineController;
    [SerializeField] private TextMeshProUGUI number001;
    [SerializeField] private TextMeshProUGUI number01;
    private float floatNumber001 =0;
    private float floatNumber01 =0;

    private float finalFloatNumber;

    private void Awake() {
        number001.text = ""+ floatNumber01;
        number01.text = ""+ floatNumber01;
        finalFloatNumber = floatNumber001/100 + floatNumber01/10;
        iFMachineController.SetFloatNumber(finalFloatNumber);
        
    }
        public void AddNumber001(){
        if(floatNumber001 >= 9){
            floatNumber001 = 0;
            number001.text = ""+ floatNumber001;
            finalFloatNumber = floatNumber001/100 + floatNumber01/10;
            iFMachineController.SetFloatNumber(finalFloatNumber);
        }else{
            floatNumber001++;
            number001.text = ""+ floatNumber001;
            finalFloatNumber = floatNumber001/100 + floatNumber01/10;
            iFMachineController.SetFloatNumber(finalFloatNumber);
        }
        
        
    }
    public void SubtractNumber001(){
        if(floatNumber001 <= 0){
            floatNumber001 = 9;
            number001.text = ""+ floatNumber001;
            finalFloatNumber = floatNumber001/100 + floatNumber01/10;
            iFMachineController.SetFloatNumber(finalFloatNumber);
        }else{
            floatNumber001--;
            number001.text = ""+ floatNumber001;
            finalFloatNumber = floatNumber001/100 + floatNumber01/10;
            iFMachineController.SetFloatNumber(finalFloatNumber);
        }
        
    }

    public void AddNumber01(){
        if(floatNumber01 >= 9){
            floatNumber01 = 0;
            number01.text = ""+ floatNumber01;
            finalFloatNumber = floatNumber001/100 + floatNumber01/10;
            iFMachineController.SetFloatNumber(finalFloatNumber);
        }else{
            floatNumber01++;
            number01.text = ""+ floatNumber01;
            finalFloatNumber = floatNumber001/100 + floatNumber01/10;
            iFMachineController.SetFloatNumber(finalFloatNumber);
        }
        
        
    }
    public void SubtractNumber01(){
        if(floatNumber01 <= 0){
            floatNumber01 = 9;
            number01.text = ""+ floatNumber01;
            finalFloatNumber = floatNumber001/100 + floatNumber01/10;
            iFMachineController.SetFloatNumber(finalFloatNumber);
        }else{
            floatNumber01--;
            number01.text = ""+ floatNumber01;
            finalFloatNumber = floatNumber001/100 + floatNumber01/10;
            iFMachineController.SetFloatNumber(finalFloatNumber);
        }
        
    }

}


