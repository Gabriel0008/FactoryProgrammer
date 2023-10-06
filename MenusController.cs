using UnityEngine.SceneManagement;
using UnityEngine;


public class MenusController : MonoBehaviour
{
    private bool menuActive = false;
    private bool inventoryActive = false;
    private bool infoActive = false;
    private bool _placeHolder = false;
    private bool _optionsMove = false;

    [SerializeField] private UI_Inventory uI_Inventory;
    [SerializeField] private UI_Info uI_Info;

    [SerializeField] private GameObject bottomExitButton;
    [SerializeField] private GameObject bottomInventoryButton;
    [SerializeField] private GameObject bottomItemBar;
    [SerializeField] private RectTransform bottomLeftBar;

    [SerializeField] private GameObject rightInfo;
    [SerializeField] private GameObject rightItemBar;
    [SerializeField] private RectTransform rightBar;

    [SerializeField] GameObject BottomRightPlaceHolder;

    [SerializeField] GameObject BottomRightPlaceHolderMovingOptions;

    [SerializeField] GameObject pause;
    [SerializeField] GameObject play;

    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;
    [SerializeField] GameObject next;
    private void Awake()
    {
        menuActive = false;
        inventoryActive = false;
        infoActive = false;
        bottomExitButton.SetActive(false);
        bottomInventoryButton.SetActive(false);
        bottomItemBar.SetActive(false);
        rightItemBar.SetActive(false);

    }

    public bool getInfoActive()
    {
        return infoActive;
    }

    public bool getInventoryActive()
    {
        return inventoryActive;
    }

    public bool getMenuActive()
    {
        return menuActive;
    }
    public void ClickMenu()
    {
        if (menuActive == true)
        {
            CloseMenu();
            menuActive = false;
        }
        else
        {
            OpenMenu();
            menuActive = true;
        }
    }

    public void ClickInfo()
    {
        if (infoActive == false)
        {
            if (inventoryActive)
            {
                CloseInventory();
            }
            OpenInfo();
        }
        else
        {
            CloseInfo();
        }

    }

    public void ClickExitGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ClickNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    public void ClickPlaceObject()
    {
        GridBuildingSystem3D.Instance.ClickPlaceMachine();
    }

    public void ClickOpenInventory()
    {
        if (inventoryActive == false)
        {
            if (infoActive)
            {
                CloseInfo();
            }
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    public void CloseAllMenus()
    {
        if(inventoryActive == true)
        {
            CloseInventory();
        }

        if (infoActive)
        {
            CloseInfo();
        }

    }

    public void EndScreen(int stars,bool passed)
    {
        endScreen.SetActive(true);
        if (passed == true)
        {
            switch (stars)
            {
                case 1:
                    star1.SetActive(true);
                    Debug.Log("1 Star");
                    break;
                case 2:
                    star1.SetActive(true);
                    star2.SetActive(true);
                    Debug.Log("2 Star");
                    break;
                case 3:
                    star1.SetActive(true);
                    star2.SetActive(true);
                    star3.SetActive(true);
                    Debug.Log("3 Star");
                    break;
                default:
                    break;
            }
            next.SetActive(true);
        }

    }

    public void ClickPlay()
    {
        if(GridBuildingSystem3D.Instance.canSpawn == false)
        {
            GridBuildingSystem3D.Instance.canSpawn = true;
        }
        Time.timeScale = 1;
        FinalValues.Instance.start = true;
        play.SetActive(false);
        pause.SetActive(true);

    }
    public void ClickPause()
    {
        Time.timeScale = 0;
        play.SetActive(true);
        pause.SetActive(false);
    }

    public void ClickReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    void CloseMenu()
    {
        bottomExitButton.SetActive(false);
        bottomInventoryButton.SetActive(false);
    }

    void OpenMenu()
    {

        bottomExitButton.SetActive(true);
        bottomInventoryButton.SetActive(true);
    }

    void OpenInventory()
    {
        if (GridBuildingSystem3D.Instance.canSpawn == false)
        {
            bottomItemBar.SetActive(true);
            bottomLeftBar.anchoredPosition = new Vector2(bottomLeftBar.anchoredPosition.x, bottomLeftBar.anchoredPosition.y + 200);
            bottomInventoryButton.transform.Rotate(new Vector3(0, 0, 180));
            inventoryActive = true;
            uI_Inventory.RefreshInventory();
        }


    }
    void CloseInventory()
    {

        bottomLeftBar.anchoredPosition = new Vector2(bottomLeftBar.anchoredPosition.x, bottomLeftBar.anchoredPosition.y - 200);
        bottomInventoryButton.transform.Rotate(new Vector3(0, 0, 180));
        inventoryActive = false;
        bottomItemBar.SetActive(false);
    }
    void OpenInfo()
    {
        rightItemBar.SetActive(true);
        rightBar.anchoredPosition = new Vector2(rightBar.anchoredPosition.x - 200, rightBar.anchoredPosition.y);
        rightInfo.transform.Rotate(new Vector3(0, 0, 180));
        infoActive = true;
        uI_Info.RefreshInventory();


    }
    void CloseInfo()
    {

        rightItemBar.SetActive(false);
        rightBar.anchoredPosition = new Vector2(rightBar.anchoredPosition.x + 200, rightBar.anchoredPosition.y);
        rightInfo.transform.Rotate(new Vector3(0, 0, 180));
        infoActive = false;
    }

    public void OpenOptionsMoveButtons()
    {
        if(_placeHolder == true)
        {
            ClosePlacingButtons();

        }
        _optionsMove = true;
        BottomRightPlaceHolderMovingOptions.SetActive(true);
    }

    public void CloseOptionsMoveButtons()
    {
        _optionsMove = true;
        BottomRightPlaceHolderMovingOptions.SetActive(false);
    }

    public void OpenPlacingButtons()
    {
        if (_optionsMove == true)
        {
            CloseOptionsMoveButtons();

        }
        _placeHolder = true;
        BottomRightPlaceHolder.SetActive(true);
    }
    public void ClosePlacingButtons()
    {
        _placeHolder = false;
        BottomRightPlaceHolder.SetActive(false);
    }




}
