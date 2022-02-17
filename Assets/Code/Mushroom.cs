using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public void interaction() {
        this.gameObject.SetActive(false);
        PublicVars.mushroomCount++;
        print("Mush count: " + PublicVars.mushroomCount);
    }

}
