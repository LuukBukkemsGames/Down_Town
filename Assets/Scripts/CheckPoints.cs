using UnityEngine;
using System.Collections;

public class CheckPoints : MonoBehaviour {

    private bool [] CheckPointArr;
    private GameObject CheckPointObj;

    void Awake()
    {
        CheckPointArr = new bool[5];
        //InitArray();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CheckPoint")
        {
            for (int i = 0; i < CheckPointObj.transform.childCount; i++)
            {
                if (other.transform == CheckPointObj.transform.GetChild(i).FindChild("CheckPointInner"))
                {
                    CheckPointArr[i] = true;
                }
            }
        }
    }

    void InitArray()
    {
        switch ((int)PlayerPrefs.GetFloat("Level"))
        {
            case 1:
                CheckPointObj = GameObject.Find("Levels/Level 1/CheckPoints").gameObject;
                break;
            case 2:
                CheckPointObj = GameObject.Find("Levels/Level 2/CheckPoints").gameObject;
                break;
            case 3:
                CheckPointObj = GameObject.Find("Levels/Level 3/CheckPoints").gameObject;
                break;
            case 4:
                CheckPointObj = GameObject.Find("Levels/Level 4/CheckPoints").gameObject;
                break;
            default:
                CheckPointObj = GameObject.Find("Levels/Level 1/CheckPoints").gameObject;
                break;
        }


        for (int i = 0; i < CheckPointArr.Length; i++)
        {
            CheckPointArr[i] = true;
        }

        for (int i = 0; i < CheckPointObj.transform.childCount; i++)
        {
            CheckPointArr[i] = false;
        }
    }

    public bool CheckCP()
    {
        for(int i = 0; i < CheckPointObj.transform.childCount; i++)
        {
            if(CheckPointArr[i] == false)
            {
                return false;
            }
        }
        return true;
    }

    public void SetCheckPointObj(GameObject CPObj)
    {
        CheckPointObj = CPObj;
        //InitArray();
    }
}
