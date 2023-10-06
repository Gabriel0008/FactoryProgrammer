using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinePanel : MonoBehaviour
{
    public GameObject myUI;
    // Start is called before the first frame update
    public void OpenMachinePanel()
    {
        
        GameObject.Find("UI_Player").GetComponent<MachinePanelManager>().OpenMachinePanel(gameObject.GetComponentInParent<DraggableItem>().nameString);
    }

    public void CloseMachinePanel()
    {
        myUI.SetActive(false);
    }
}
