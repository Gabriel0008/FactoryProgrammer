using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IFIntField : MonoBehaviour
{
    [SerializeField] private IFMachineController iFMachineController;
    [SerializeField] private TextMeshProUGUI number100;
    [SerializeField] private TextMeshProUGUI number10;
    [SerializeField] private TextMeshProUGUI number1;
    private int intNumber100 =0;
    private int intNumber10 =0;
    private int intNumber1 =0;

    private float Finalnumber;

    private void Start() {
        number100.text = ""+ intNumber100;
        number10.text = ""+ intNumber10;
        number1.text = ""+ intNumber1;
        Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
        iFMachineController.SetTotalNumber(Finalnumber);
        
    }
        public void AddNumber100(){
        if(intNumber100 >= 9){
            intNumber100 = 0;
            number100.text = ""+ intNumber100;
            Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
            iFMachineController.SetTotalNumber(Finalnumber);
        }else{
            intNumber100++;
            number100.text = ""+ intNumber100;
            Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
            iFMachineController.SetTotalNumber(Finalnumber);
        }
        
        
    }
    public void SubtractNumber100(){
        if(intNumber100 <= 0){
            intNumber100 = 9;
            number100.text = ""+ intNumber100;
            Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
            iFMachineController.SetTotalNumber(Finalnumber);
        }else{
            intNumber100--;
            number100.text = ""+ intNumber100;
            Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
            iFMachineController.SetTotalNumber(Finalnumber);
        }
        
    }

    public void AddNumber10(){
        if(intNumber10 >= 9){
            intNumber10 = 0;
            number10.text = ""+ intNumber10;
            Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
            iFMachineController.SetTotalNumber(Finalnumber);
        }else{
            intNumber10++;
            number10.text = ""+ intNumber10;
            Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
            iFMachineController.SetTotalNumber(Finalnumber);
        }
        
        
    }
    public void SubtractNumber10(){
        if(intNumber10 <= 0){
            intNumber10 = 9;
            number10.text = ""+ intNumber10;
            Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
            iFMachineController.SetTotalNumber(Finalnumber);
        }else{
            intNumber10--;
            number10.text = ""+ intNumber10;
            Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
            iFMachineController.SetTotalNumber(Finalnumber);
        }
        
    }

    public void AddNumber1(){
        if(intNumber1 >= 9){
            intNumber1 = 0;
            number1.text = ""+ intNumber1;
            Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
            iFMachineController.SetTotalNumber(Finalnumber);
        }else{
            intNumber1++;
            number1.text = ""+ intNumber1;
            Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
            iFMachineController.SetTotalNumber(Finalnumber);
        }
        
        
    }
    public void SubtractNumber1(){
        if(intNumber1 <= 0){
            intNumber1 = 9;
            number1.text = ""+ intNumber1;
            Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
            iFMachineController.SetTotalNumber(Finalnumber);
        }else{
            intNumber1--;
            number1.text = ""+ intNumber1;
            Finalnumber = intNumber100*100 + intNumber10*10+intNumber1*1;
            iFMachineController.SetTotalNumber(Finalnumber);
        }
        
    }
}


