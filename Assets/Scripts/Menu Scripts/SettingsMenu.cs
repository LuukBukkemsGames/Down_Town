using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    private GameObject Canvas;
    private Text VSyncText;
    private Text VolumeText;
    private Text AntiAliText;

    private GameObject Logo;

    float VSync;
    float Volume;
    public float AntiAliasing;
    float MaxVSync;

    void Start () {

        Canvas = GameObject.Find("Canvas");
        VSyncText = Canvas.transform.FindChild("VSync Settings").gameObject.GetComponent<Text>();
        VolumeText = Canvas.transform.FindChild("Volume Settings").gameObject.GetComponent<Text>();
        AntiAliText = Canvas.transform.FindChild("AntiAliasing Settings").gameObject.GetComponent<Text>();

        Logo = Canvas.transform.FindChild("Logo").gameObject;

        VSync = QualitySettings.vSyncCount;
        AntiAliasing = QualitySettings.antiAliasing;

        if (PlayerPrefs.HasKey("Volume"))
            Volume = PlayerPrefs.GetFloat("Volume");
        else
            PlayerPrefs.SetFloat("Volume", 100);


        if (PlayerPrefs.HasKey("VSync"))
            VSync = PlayerPrefs.GetFloat("VSync");
        else 
            PlayerPrefs.SetFloat("VSync", QualitySettings.vSyncCount);

        if (PlayerPrefs.HasKey("AntiAliasing"))
            AntiAliasing = PlayerPrefs.GetFloat("AntiAliasing");
        else
            PlayerPrefs.SetFloat("AntiAliasing", QualitySettings.antiAliasing);

        MaxVSync = 2;

        if (Screen.currentResolution.refreshRate < 120)
            MaxVSync = 1;

        InitLabels();
    }

    void InitLabels()
    {

        if (VSync > MaxVSync)
            VSync = MaxVSync;
        switch ((int)VSync)
        {
            case 0:
                VSyncText.text = "Off";
                break;
            case 1:
                VSyncText.text = "On";
                break;
            case 2:
                VSyncText.text = "Half";
                break;
        }

        switch ((int)AntiAliasing)
        {
            case 0:
                AntiAliText.text = "Off";
                break;
            case 2:
                AntiAliText.text = "2x";
                break;
            case 4:
                AntiAliText.text = "4x";
                break;
            case 8:
                AntiAliText.text = "8x";
                break;
        }

        VolumeText.text = Volume + "%";

    }

    void FixedUpdate()
    {
        Logo.transform.Rotate(0, 50 * Time.fixedDeltaTime, 0);
        Camera.main.transform.Rotate(0, -1 * Time.fixedDeltaTime, 0);
    }

    public void SetVSync(bool Add)
    {
        SoundSingleton.Instance.PlaySound();
        if (Add)
        {
            if (VSync < MaxVSync)
            {
                VSync++;
            }
            else
                VSync = 0;
        }
        else
        {
            if (VSync > 0)
                VSync--;
            else
                VSync = 3;
        }

        switch ((int)VSync)
        {
            case 0:
                VSyncText.text = "Off";
                QualitySettings.vSyncCount = 0;
                break;
            case 1:
                VSyncText.text = "On";
                QualitySettings.vSyncCount = 1;
                break;
            case 2:
                VSyncText.text = "Half";
                QualitySettings.vSyncCount = 2;
                break;
        }

        PlayerPrefs.SetFloat("VSync", VSync);
    }

    public void SetVolume(bool Add)
    {
        SoundSingleton.Instance.PlaySound();
        if (Add)
        {
            if (Volume < 100)
                Volume += 10;
        }
        else
        {
            if (Volume > 0)
                Volume -= 10;
        }

        PlayerPrefs.SetFloat("Volume", Volume);
        VolumeText.text = Volume + "%";
        AudioListener.volume = Volume / 100;
    }

    public void SetAntiAll(bool Add)
    {
        SoundSingleton.Instance.PlaySound();
        if (Add)
        {
            if (AntiAliasing == 0)
                AntiAliasing = 2;
            else if (AntiAliasing == 2)
                AntiAliasing = 4;
            else if (AntiAliasing == 4)
                AntiAliasing = 8;
            else if (AntiAliasing == 8)
                AntiAliasing = 0;
        }

        else
        {
            if (AntiAliasing == 0)
                AntiAliasing = 8;
            else if (AntiAliasing == 2)
                AntiAliasing = 0;
            else if (AntiAliasing == 4)
                AntiAliasing = 2;
            else if (AntiAliasing == 8)
                AntiAliasing = 4;
        }

        switch ((int)AntiAliasing)
        {
            case 0:
                AntiAliText.text = "Off";
                QualitySettings.antiAliasing = 0;
                break;
            case 2:
                AntiAliText.text = "2x";
                QualitySettings.antiAliasing = 2;
                break;
            case 4:
                AntiAliText.text = "4x";
                QualitySettings.antiAliasing = 4;
                break;
            case 8:
                AntiAliText.text = "8x";
                QualitySettings.antiAliasing = 8;
                break;
        }
        PlayerPrefs.SetFloat("AntiAliasing", AntiAliasing);
    }

    public void GoBack()
    {
        SoundSingleton.Instance.PlaySound();
        Application.LoadLevel("MainMenu");
    }
}
