using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinePanelManager : MonoBehaviour
{
    public GameObject smelter;
    public GameObject sorter;
    public GameObject grabber;
    public GameObject belt;
    public GameObject ifMachine;
    public GameObject switchMachine;

    public void OpenMachinePanel(string name)
    {
        GameObject.Find("UI_Player").GetComponent<MenusController>().CloseAllMenus();
        switch (name)
        {
            case "Esteira":
                belt.SetActive(true);
                break;
            case "conexao":
                grabber.SetActive(true);
                break;
            case "IFMachine":
                ifMachine.SetActive(true);
                break;
            case "Smelter":
                smelter.SetActive(true);
                break;
            case "Sorter":
                sorter.SetActive(true);
                break;
            case "Switch":
                switchMachine.SetActive(true);
                break;
            default:
                Debug.Log("MachinePanelManager - error");
                break;
        }
    }

}
