using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    bool isDay = true;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(.03f, 0, 0);
    }
    
    // Update is called once per frame
    private void FixedUpdate() {
        var x = this.gameObject.transform.eulerAngles.x;
        if (x < 180 && !isDay) {
            PublicVars.dayCount += 1;
            isDay = true;
        } else if (x > 180 && isDay) {
            isDay = false;
        }
    }
}
