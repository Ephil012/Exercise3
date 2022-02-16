using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        Vector3 moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        moveDirection *= moveSpeed;
        moveDirection.y = _rigidbody.velocity.y;
        _rigidbody.velocity = moveDirection;

        grounded = Physics.CheckSphere(footTrans.position , groundCheckDistance, groundLayer);

    }
    // Update is called once per frame
    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X") * 2; // mouse x movement
        rotation.x -= Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x , -30, 30); //never go past -30 and 30 degrees
        camTrans.localEulerAngles = new Vector3(rotation.x, 0, 0) * lookSpeed;
        transform.eulerAngles = new Vector3( 0, rotation.y,  0) * lookSpeed;
        
        if(grounded && Input.GetButtonDown("Jump")){
            _rigidbody.AddForce(new Vector3(0, jumpForce, 0));
        }
    }
}
