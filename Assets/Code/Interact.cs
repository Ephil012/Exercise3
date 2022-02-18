using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public bool onInteract(string tag)
    {
        var result = false;
        if(tag == "Bush") {
            var item = this.gameObject.GetComponent<Bush>();
            result = item.interaction();
        } else if (tag == "Mushroom") {
            var item = this.gameObject.GetComponent<Mushroom>();
            result = item.interaction();
        } else if (tag == "Tree") {
            var item = this.gameObject.GetComponent<Tree>();
            result = item.interaction();
        }
        return result;
    }
}
