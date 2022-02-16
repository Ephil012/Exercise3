using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float lookSpeed;
    public float jumpForce = 300;
    public Transform camTrans;

    Rigidbody _rigidbody;
    private Vector2 rotation = Vector2.zero;
    public Transform footTrans;
    public LayerMask groundLayer;
    public float groundCheckDistance;
    bool grounded = false;

    private float raycastDist = 50;
    private bool reticleTarget = false;
    public Image reticle;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        recticleRun();
        Vector3 moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        moveDirection *= moveSpeed;
        moveDirection.y = _rigidbody.velocity.y;
        _rigidbody.velocity = moveDirection;

        grounded = Physics.CheckSphere(footTrans.position , groundCheckDistance, groundLayer);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit hit;
            if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, raycastDist))
            {
                var item = hit.collider.gameObject;
                if (item.tag == "Bush" || item.tag == "Mushroom") {
                    var interact = item.GetComponent<Interact>();
                    if (interact != null) {
                        interact.onInteract(item.tag);
                    }
                }
            }
        }

        rotation.y += Input.GetAxis("Mouse X") * 2; // mouse x movement
        rotation.x -= Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x , -30, 30); //never go past -30 and 30 degrees
        camTrans.localEulerAngles = new Vector3(rotation.x, 0, 0) * lookSpeed;
        transform.eulerAngles = new Vector3( 0, rotation.y,  0) * lookSpeed;
        
        if(grounded && Input.GetButtonDown("Jump")){
            _rigidbody.AddForce(new Vector3(0, jumpForce, 0));
        }
    }

    private void recticleRun()
    {
        RaycastHit hit;
        if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, raycastDist))
        {
            //Cast a ray and if the retical is not already red change its color
            var item = hit.collider.gameObject;
            if (!reticleTarget && (item.tag == "Bush" || item.tag == "Mushroom"))
            {
                reticleTarget = true; //This bool keeps the color from updating if there is no change
            }
        }
        else if (reticleTarget) //if no target is hit and the reticle is active then change it back to white
        {
            reticleTarget = false;
        }
    }
}
