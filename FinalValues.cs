using System;
using UnityEngine;

public class FinalValues : MonoBehaviour
{
    // Start is called before the first frame update

    public static FinalValues Instance;
    [SerializeField] LevelsSO levelSO;

    public event EventHandler OnFinishGame;

    private int _total = 0;
    private int _correct = 0;
    private int _wrong = 0;
    private int _wasted = 0;
    private float _time = 8;

    private bool _materialsActive = true;
    public bool start = false;
    public bool failure = false;


    [HideInInspector] public int stars = 0;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _total = InitialValues.Instance.GetTotalEndMaterials();
    }

    private void Update()
    {
        
        
        if (start == true)
        {
            _time -= Time.deltaTime;

            if (_time <= 0)
            {
                _materialsActive = false;
            }


            if(_materialsActive == false)
            {
                start = false;
                EndGame();
            }
        }
    }

    public void materialActive()
    {
        _time = 6f;
    }

    private void EndGame()
    {
        OnFinishGame?.Invoke(this, EventArgs.Empty);
        SetEndResult();
    }

    public void SetEndResult()
    {
        if (failure == false)
        {
            if (_total <= _correct)
            {
                Debug.Log("1º Star : total -" + _total + " correct -" + _correct);
                stars++;
            }
            if (_wrong == 0)
            {
                Debug.Log("2º Star : erros -" + _wrong);
                stars++;
            }
            if (_wasted == 0)
            {
                Debug.Log("3º Star : desperdicio -" + _wasted);
                stars++;
            }

            GameObject.Find("UI_Player").GetComponent<MenusController>().EndScreen(stars, true);
        }
        else
        {

            GameObject.Find("UI_Player").GetComponent<MenusController>().EndScreen(stars, false);
        }

        


    }

    public void SetCorrectEndValue()
    {
        _correct++;
        _total--;
    }

    public void SetWrongEndValue()
    {
        _wrong++;
        _total--;
    }

    public void SetWastedEndValue()
    {
        _wasted++;
        _total--;
    }

}
