using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject Tutorial01;
    public GameObject Tutorial02;
    public GameObject Tutorial03;
    public GameObject Tutorial04;
    public GameObject Tutorial05;
    public GameObject Tutorial06;
    public GameObject Tutorial07;
    public GameObject Tutorial08;
    public GameObject Tutorial09;

    public void onClickTutorial01()
    {
        Tutorial01.SetActive(false);
        Tutorial02.SetActive(true);
        Time.timeScale = 0;
    }

    public void onClickTutorial02()
    {
        Tutorial02.SetActive(false);
        Tutorial03.SetActive(true);
    }

    public void onClickTutorial03()
    {
        Tutorial03.SetActive(false);
        if (!GameObject.Find("UI_Player").GetComponent<MenusController>().getInfoActive())
        {
            GameObject.Find("UI_Player").GetComponent<MenusController>().ClickInfo();
        }
        Tutorial04.SetActive(true);

    }

    public void onClickTutorial04()
    {
        Tutorial04.SetActive(false);
        if (UI_MaterialPanel.isOpen)
        {
            GameObject.Find("UI_Player").GetComponent<UI_MaterialPanel>().CloseMaterialPanel();
            Time.timeScale = 0;
        }
        if (GameObject.Find("UI_Player").GetComponent<MenusController>().getInfoActive())
        {
            GameObject.Find("UI_Player").GetComponent<MenusController>().ClickInfo();
        }
        Tutorial05.SetActive(true);
    }

    public void onClickTutorial05()
    {
        Tutorial05.SetActive(false);
        Tutorial06.SetActive(true);
    }

    public void onClickTutorial06()
    {
        Tutorial06.SetActive(false);
        if (!GameObject.Find("UI_Player").GetComponent<MenusController>().getMenuActive())
        {
            GameObject.Find("UI_Player").GetComponent<MenusController>().ClickMenu();
        }

        Tutorial07.SetActive(true);
    }
    public void onClickTutorial07()
    {
        Tutorial07.SetActive(false);
        if (!GameObject.Find("UI_Player").GetComponent<MenusController>().getInventoryActive())
        {
            GameObject.Find("UI_Player").GetComponent<MenusController>().ClickOpenInventory();
        }

            Tutorial08.SetActive(true);
    }
    public void onClickTutorial08()
    {
        Tutorial08.SetActive(false);
        Time.timeScale = 1;

    }
}
