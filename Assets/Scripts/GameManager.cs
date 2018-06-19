using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{

    private float CurrentLevel;
    public float NrOfLevels;

    private GameObject Player;
    private GameObject CheckPointObj;

    private PlayerMovement PlayerMov;
    private CheckPoints CPs;

    private GameObject Victory;
    private Text VictoryText;

    private Text CountdownText;
    private Image CountdownImage;

    private float Timer;

    private bool Started;
    private bool Finished;

    void Awake() {

        CurrentLevel = PlayerPrefs.GetFloat("ChosenMap");
        NrOfLevels = PlayerPrefs.GetFloat("NrOfLevels");

		Player = GameObject.Find("Player").transform.FindChild("Sphere").gameObject;
    }

    void Start()
    {

        PlayerMov = Player.GetComponent<PlayerMovement>();

        Time.timeScale = 1;

        Application.targetFrameRate = 120;

        Debug.Log("Level = " + CurrentLevel);
        Debug.Log("Number of levels = " + NrOfLevels);

        CPs = Player.GetComponent<CheckPoints>();

        transform.position = new Vector3(0, 0, 0);

		StartGame ();
    }

    public void StartGame()
    {
        CountdownText = GameObject.Find("Canvas/CountText").gameObject.GetComponent<Text>();
        CountdownImage = GameObject.Find("Canvas/Image").gameObject.GetComponent<Image>();

        Timer = 0;
        Started = false;
        StartCoroutine("StartCountdown");

        GoTo(CurrentLevel);
    }

    public float GetLevel()
    {
        return CurrentLevel;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Portal")
        {
            NrOfLevels--;

            if(NrOfLevels <= 0 && CPs.CheckCP())
            {
                PlayerMov.SetMove(false);
                Time.timeScale = 0f;
                Victory = Instantiate(Resources.Load("Prefabs/VictoryCanvas")) as GameObject;
                VictoryText = Victory.transform.FindChild("Text").GetComponent<Text>();
                VictoryText.text = "Finished in : " + Mathf.Round(Timer * 100) / 100 +  " Sec !";
                Victory.transform.FindChild("Button").gameObject.GetComponent<Button>().onClick.AddListener(() => GoBack());
                Finished = true;
            }

            else
            {
                if(CPs.CheckCP())
                    GoToRandom();
            }
        }
    }

    void Update()
    {
        if (!Started)
            return;

        Timer += Time.deltaTime;

        if (Finished)
            if (Input.GetKey(KeyCode.Joystick1Button0))
                GoBack();
    }

    private void GoToRandom()
    {
        GoTo(Random.Range(1, 5));
    }

    public void GoTo(float i)
    {
        switch ((int)i)
        {
            case 1:
                CheckPointObj = GameObject.Find("Level 1/CheckPoints");
                CPs.SetCheckPointObj(CheckPointObj);
                Player.transform.eulerAngles = new Vector3(0, 90, 0);
                Player.transform.position = new Vector3(2530, 110, 372);
                CurrentLevel = 1;
                break;
            case 2:
                CheckPointObj = GameObject.Find("Level 2/CheckPoints");
                CPs.SetCheckPointObj(CheckPointObj);
                Player.transform.eulerAngles = new Vector3(0, 165, 0);
                Player.transform.position = new Vector3(417, 175, 360);
                CurrentLevel = 2;
                break;
            case 3:
                CheckPointObj = GameObject.Find("Level 3/CheckPoints");
                CPs.SetCheckPointObj(CheckPointObj);
                Player.transform.eulerAngles = new Vector3(0, 90, 0);
                Player.transform.position = new Vector3(2775, 15, -1676);
                CurrentLevel = 3;
                break;
            case 4:
                CheckPointObj = GameObject.Find("Level 4/CheckPoints");
                CPs.SetCheckPointObj(CheckPointObj);
                Player.transform.eulerAngles = new Vector3(0,-45, 0);
                Player.transform.position = new Vector3(775, 55, -2275);
                CurrentLevel = 4;
                break;
            default:
                CPs.SetCheckPointObj(GameObject.Find("Level 1/CheckPoints"));
                Player.transform.position = new Vector3(1733, 137, 15);
                Player.transform.eulerAngles = new Vector3(0, -90, 0);
                CurrentLevel = 1;
                break;
        }
    }

    public void GoBack()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("MainMenu");
    }

    IEnumerator StartCountdown()
    {
        for (int i = 0; i < 5; i++)
        {
            CountdownText.text = "" + (5 - i);
            CountdownImage.CrossFadeAlpha(.8f - ((float)i/5), 1f, false);
            yield return new WaitForSeconds(1f);
        }
        CountdownImage.CrossFadeAlpha(0, .1f, false);
        PlayerMov.SetMove(true);
        Started = true;
        CountdownText.text = "GO !";
        CountdownText.CrossFadeAlpha(0f, 3f, false);   
    }
}
