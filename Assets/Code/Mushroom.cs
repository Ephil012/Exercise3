using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public bool interaction() {
        PublicVars.mushroomCount++;
        this.gameObject.SetActive(false);
        return true;
    }
}
