using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

    private GameObject Logo;
    private Text LoadingText;
    private float Dots;
    private string MyText;

	void Start () {
        Logo = transform.FindChild("Logo").gameObject;
        LoadingText = transform.FindChild("Loading").gameObject.GetComponent<Text>();
        Dots = 1;
        StartCoroutine("LoadingTextUpdate");
    }

    void FixedUpdate()
    {
        Logo.transform.Rotate(0, 50 * Time.fixedDeltaTime, 0);
        transform.Rotate(0, -1 * Time.fixedDeltaTime, 0);      
    }

    IEnumerator LoadingTextUpdate()
    {
        for (int i = 0; i < 60; i++)
        {
            Dots++;
            if (Dots >= 4)
                Dots = 1;

            MyText = "Loading";

            for (int Index = 0; Index < Dots; Index++)
                MyText += ".";

            LoadingText.text = MyText;

            yield return new WaitForSeconds(.5f);
        }
    }


}
