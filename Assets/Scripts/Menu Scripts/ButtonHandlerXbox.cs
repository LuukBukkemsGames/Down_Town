using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonHandlerXbox : MonoBehaviour
{


    public int SelectedButton;
    public int NrofButtons;

    public float Timer;

    public GameObject[] Buttons;

    private Menu MenuHandler;

    void Start()
    {
        Timer = 0;

        SelectedButton = 0;
        NrofButtons = GameObject.Find("Canvas/BackGrounds").gameObject.transform.childCount;
        Buttons = new GameObject[NrofButtons];

        MenuHandler = GetComponent<Menu>();

        for (int i = 0; i < NrofButtons; i++)
        {
            Buttons[i] = GameObject.Find("Canvas/BackGrounds").gameObject.transform.GetChild(i).gameObject;
        }

        if (NrofButtons >= 1)
        {
            Buttons[SelectedButton].GetComponent<Image>().color = Color.gray;
        }
    }

    void Update()
    {
        Timer += Time.deltaTime;

        if (Input.GetAxis("Vertical") < -.7f && Timer > .2f)
        {
            SoundSingleton.Instance.PlaySound();
            Timer = 0;
            SelectedButton++;
            if(SelectedButton >= NrofButtons )
                SelectedButton = 0;
            if (Buttons[SelectedButton].activeSelf == false)
                SelectedButton++;
            if (SelectedButton >= NrofButtons )
                SelectedButton = 0;
        }

        else if (Input.GetAxis("Vertical") > .7f && Timer > .2f)
        {
            SoundSingleton.Instance.PlaySound();
            Timer = 0;
            SelectedButton--;
            if (SelectedButton < 0)
                SelectedButton = NrofButtons - 1;
            if (Buttons[SelectedButton].activeSelf == false)
                SelectedButton--;
            if (SelectedButton < 0)
                SelectedButton = NrofButtons - 1;
        }

        for(int i = 0; i < NrofButtons; i++)
        {
            Buttons[i].GetComponent<Image>().color = Color.white;
            if(i == SelectedButton)
            {
                Buttons[i].GetComponent<Image>().color = Color.gray;
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && SelectedButton == 2 && Timer > .2f)
            MenuHandler.StartGame();

        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && SelectedButton == 3 && Timer > .2f)
            MenuHandler.GoToSettings();

        if (Input.GetAxis("Horizontal") > .7f && Timer > .2f)
        {
            Timer = 0;
            if (SelectedButton == 0)
            {
                MenuHandler.ChangeGameMode(true);
            }
            else if (SelectedButton == 1)
            {
                MenuHandler.ChangeAmount(true);
            }
        }

        if (Input.GetAxis("Horizontal") < -.7f && Timer > .2f)
        {
            Timer = 0;
            if (SelectedButton == 0)
            {
                MenuHandler.ChangeGameMode(false);
            }
            else if (SelectedButton == 1)
            {
                MenuHandler.ChangeAmount(false);
            }
        }
    }

     public void HoverOver(int Button)
    {
        if (SelectedButton != Button)
            SoundSingleton.Instance.PlaySound();
        SelectedButton = Button;
    }
}
