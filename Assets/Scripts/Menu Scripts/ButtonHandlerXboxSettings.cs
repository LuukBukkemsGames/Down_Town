using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonHandlerXboxSettings : MonoBehaviour {



    private int SelectedButton;
    private int NrofButtons;

    private float Timer;

    public GameObject[] Buttons;

    private SettingsMenu MenuHandler;

    void Start()
    {
        Timer = 0;

        SelectedButton = 0;
        NrofButtons = GameObject.Find("Canvas/Backgrounds").gameObject.transform.childCount;
        Buttons = new GameObject[NrofButtons];

        MenuHandler = GetComponent<SettingsMenu>();

        for (int i = 0; i < NrofButtons; i++)
        {
            Buttons[i] = GameObject.Find("Canvas/Backgrounds").gameObject.transform.GetChild(i).gameObject;
        }

        if (NrofButtons >= 1)
        {
            Buttons[SelectedButton].GetComponent<Image>().color = Color.gray;
        }
    }

    void Update()
    {

        if (Input.GetAxis("Vertical") < -.7f && Timer > .2f)
        {
            Timer = 0;
            SoundSingleton.Instance.PlaySound();
            SelectedButton++;
            if (SelectedButton >= NrofButtons)
                SelectedButton = 0;
            if (Buttons[SelectedButton].activeSelf == false)
                SelectedButton++;
            if (SelectedButton >= NrofButtons)
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

        for (int i = 0; i < NrofButtons; i++)
        {
            Buttons[i].GetComponent<Image>().color = Color.white;
            if (i == SelectedButton)
            {
                Buttons[i].GetComponent<Image>().color = Color.gray;
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && SelectedButton == 3 && Timer > .2f)
            MenuHandler.GoBack();

        if (Input.GetAxis("Horizontal") > .7f && Timer > .2f)
        {
            Timer = 0;
            if (SelectedButton == 0)
            {
                MenuHandler.SetVSync(true);
            }
            else if (SelectedButton == 1)
            {
                MenuHandler.SetVolume(true);
            }
            else if (SelectedButton == 2)
            {
                MenuHandler.SetAntiAll(true);
            }
        }

        if (Input.GetAxis("Horizontal") < -.7f && Timer > .2f)
        {
            Timer = 0;
            if (SelectedButton == 0)
            {
                MenuHandler.SetVSync(false);
            }
            else if (SelectedButton == 1)
            {
                MenuHandler.SetVolume(false);
            }
            else if (SelectedButton == 2)
            {
                MenuHandler.SetAntiAll(false);
            }
        }

        Timer += Time.deltaTime;
    }

    public void HoverOver(int Button)
    {
        if(SelectedButton != Button)
            SoundSingleton.Instance.PlaySound();
        SelectedButton = Button;
    }
}
