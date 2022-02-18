using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    int appleIndex;
    int maxApples;
    // Start is called before the first frame update
    void Start()
    {
        appleIndex = 0;
        maxApples = this.gameObject.transform.childCount;
    }
    public bool interaction() {
        if (appleIndex < maxApples)
        {
            this.gameObject.transform.GetChild(appleIndex).gameObject.SetActive(false);
            appleIndex++;
            PublicVars.appleCount++;
            return true;
        }   
        return false;
    }
}
