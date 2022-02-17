using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    int berryIndex;
    int maxBerries;
    // Start is called before the first frame update
    void Start()
    {
        berryIndex = 0;
        maxBerries = this.gameObject.transform.childCount;
    }
    public void interaction() {
        if (berryIndex < maxBerries)
        {
            this.gameObject.transform.GetChild(berryIndex).gameObject.SetActive(false);
            berryIndex++;
            PublicVars.berryCount++;
        }
        Debug.Log("Removed berry " + berryIndex);
       
    }
}
