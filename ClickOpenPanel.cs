using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOpenPanel : MonoBehaviour
{
    public MaterialsSO material;

    public void OnClickOpenMaterialPanel()
    {


        UI_MaterialPanel uI_MaterialPanel = GameObject.Find("UI_Player").GetComponent<UI_MaterialPanel>();
        if (UI_MaterialPanel.isOpen == false)
        {
            uI_MaterialPanel.OpenMaterialPanel(material);
        }
        else
        {
            uI_MaterialPanel.RefreshPanel(material);
        }

    }

}
