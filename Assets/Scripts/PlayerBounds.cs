using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBounds : MonoBehaviour {

    private GameManager _GameMan;
    private Text GameText;

    void Start()
    {
        _GameMan = GameObject.Find("Player/Sphere").gameObject.GetComponent<GameManager>();
        GameText = GameObject.Find("Canvas/Text").gameObject.GetComponent<Text>();
        GameText.CrossFadeAlpha(0, 0, true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LevelWall")
        {
            _GameMan.GoTo(_GameMan.GetLevel());
            GameText.CrossFadeAlpha(1f, 0, false);
            GameText.CrossFadeAlpha(0f, 3f, false);
        }
    }


}
