using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

    public GameObject[] Levels;

    public GameObject[] InnerRings;
    public GameObject[] OuterRings;

    private GameObject LevelObj;

    public int Rings;

    void Start () {
        Rings = 0;

        LevelObj = GameObject.Find("Levels").gameObject;

        Levels = new GameObject[LevelObj.transform.childCount];

        for (int i = 0; i < LevelObj.transform.childCount; i++)
        {
            Levels[i] = LevelObj.transform.GetChild(i).gameObject;
            Rings += 1;
        }

        InnerRings = new GameObject[Rings];
        OuterRings = new GameObject[Rings];

        for (int i = 0; i < Levels.Length; i++)
        {
            OuterRings[i] = Levels[i].transform.FindChild("FinishLine").transform.FindChild("Finish1").gameObject;
            InnerRings[i] = Levels[i].transform.FindChild("FinishLine").transform.FindChild("Finish2").gameObject;
        }
    }
	
	void FixedUpdate() {

        for(int i = 0; i < Rings; i++)
        {
            OuterRings[i].transform.Rotate(0, 0, Random.Range(20, 30) * Time.deltaTime);
            InnerRings[i].transform.Rotate(0, 0, -Random.Range(20, 30) * Time.deltaTime);
        }
    }
}
