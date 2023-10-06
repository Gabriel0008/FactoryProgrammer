using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    public void OpenPanel()
    {
        UI.SetActive(true);
    }
}
