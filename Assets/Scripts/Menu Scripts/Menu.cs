using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    private GameObject Canvas;
    private Text LevelsText;
    private Text GameModeText;
    private Text GameModeSubText;

    private GameObject ButtonAdd;
    private GameObject ButtonMin;
    private GameObject MiddleBackground;

    private GameModes Gamemode;

    private GameObject Logo;

    private float Levels;
    private float ChosenLevel;

    private enum GameModes
    {
        Random,
        OneLevel,
        RandomLevels
    };

	void Start () {

        if (PlayerPrefs.HasKey("Volume"))
            AudioListener.volume = (PlayerPrefs.GetFloat("Volume") / 100);

        if (PlayerPrefs.HasKey("VSync"))
            QualitySettings.vSyncCount = (int)PlayerPrefs.GetFloat("VSync");

        if (PlayerPrefs.HasKey("AntiAliasing"))
            QualitySettings.antiAliasing = (int)PlayerPrefs.GetFloat("AntiAliasing");

        Gamemode = GameModes.RandomLevels;

        Levels = 5;
        ChosenLevel = 1;

        Canvas = GameObject.Find("Canvas");

        LevelsText = Canvas.transform.FindChild("NrOfLevels").gameObject.GetComponent<Text>();
        GameModeSubText = Canvas.transform.FindChild("Levels").gameObject.GetComponent<Text>();
        GameModeText = Canvas.transform.FindChild("GameMode").gameObject.GetComponent<Text>();

        ButtonAdd = Canvas.transform.FindChild("IncreaseLevels").gameObject;
        ButtonMin = Canvas.transform.FindChild("DecreaseLevels").gameObject;
        MiddleBackground = Canvas.transform.FindChild("BackGrounds").gameObject.transform.FindChild("Levels Background").gameObject;

        Logo = Canvas.transform.FindChild("Logo").gameObject;
    }

    void FixedUpdate()
    {
        Logo.transform.Rotate(0, 50 * Time.fixedDeltaTime, 0);
        Camera.main.transform.Rotate(0, -1 * Time.fixedDeltaTime, 0);
    }

    public void GoToSettings()
    {
        SoundSingleton.Instance.PlaySound();
        Application.LoadLevel("Settings");
    }

    public void ChangeAmount (bool Inc)
    {
        SoundSingleton.Instance.PlaySound();

        if (Gamemode == GameModes.RandomLevels)
        {
            if (Inc && Levels < 20)
            {
                Levels++;
            }

            else if (!Inc && Levels > 1)
            {
                Levels--;
            }
            LevelsText.text = "" + Levels;
        }

        else if (Gamemode == GameModes.OneLevel)
        {
            if (Inc && ChosenLevel < 4)
            {
                ChosenLevel++;
            }

            else if (ChosenLevel > 1 && !Inc)
            {
                ChosenLevel--;
            }
            LevelsText.text = "" + ChosenLevel;
        }
    }

    public void ChangeGameMode(bool state)
    {
        SoundSingleton.Instance.PlaySound();

        switch (Gamemode)
        {
            case GameModes.Random:
                if (state)
                    SetGameMode(GameModes.OneLevel);
                else
                    SetGameMode(GameModes.RandomLevels);
                break;
            case GameModes.OneLevel:
                if (state)
                    SetGameMode(GameModes.RandomLevels);
                else
                    SetGameMode(GameModes.Random);
                break;
            case GameModes.RandomLevels:
                if (state)
                    SetGameMode(GameModes.Random);
                else
                    SetGameMode(GameModes.OneLevel);
                break;
        }

    }

    public void StartGame()
    {
        SoundSingleton.Instance.PlaySound();

        if (Gamemode == GameModes.RandomLevels)
        {
            PlayerPrefs.SetFloat("NrOfLevels", Levels);
            PlayerPrefs.SetFloat("ChosenMap", Random.Range(1, 5));
        }

        else if (Gamemode == GameModes.Random)
        {
            PlayerPrefs.SetFloat("NrOfLevels", 1);
            if (Random.Range(0,2) == 0)
            { PlayerPrefs.SetFloat("NrOfLevels", Random.Range(3, 10)); }
            PlayerPrefs.SetFloat("ChosenMap", Random.Range(1, 5));
        }

        else if (Gamemode == GameModes.OneLevel)
        {
            PlayerPrefs.SetFloat("ChosenMap", ChosenLevel);
            PlayerPrefs.SetFloat("NrOfLevels", 1);
        }

        /*
        Canvas.SetActive(false);
        Instantiate(Resources.Load("Prefabs/LoadingScreen"));
        Application.LoadLevelAsync("MultipleLevels");*/

        Application.LoadLevel("MultipleLevels");
    }

    private void SetGameMode( GameModes mode)
    {
        Gamemode = mode;
        SoundSingleton.Instance.PlaySound();

        if (Gamemode == GameModes.OneLevel)
        {
            GameModeSubText.text = "Level";
            GameModeText.text = "One Level";
            LevelsText.text = "" + ChosenLevel;

            LevelsText.enabled = true;
            GameModeSubText.enabled = true;
            ButtonAdd.SetActive(true);
            ButtonMin.SetActive(true);
            MiddleBackground.SetActive(true);

        }

        else if (Gamemode == GameModes.RandomLevels)
        {
            GameModeSubText.text = "Levels";
            GameModeText.text = "Random Levels";
            LevelsText.text = "" + Levels;

            LevelsText.enabled = true;
            GameModeSubText.enabled = true;
            ButtonAdd.SetActive(true);
            ButtonMin.SetActive(true);
            MiddleBackground.SetActive(true);

        }

        else if (Gamemode == GameModes.Random)
        {
            GameModeText.text = "Random Mode";

            LevelsText.enabled = false;
            GameModeSubText.enabled = false;
            ButtonAdd.SetActive(false);
            ButtonMin.SetActive(false);
            MiddleBackground.SetActive(false);

        }
    }

    public void GoToLobby()
    {
        Application.LoadLevel("InBetweenScenes");
    }
}
