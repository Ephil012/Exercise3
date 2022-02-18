using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public void onInteract(string tag)
    {
        if(tag == "Bush") {
            var item = this.gameObject.GetComponent<Bush>();
            item.interaction();
        } else if (tag == "Mushroom") {
            var item = this.gameObject.GetComponent<Mushroom>();
            item.interaction();
        } else if (tag == "Tree") {
            var item = this.gameObject.GetComponent<Tree>();
            item.interaction();
        }
    }
}
