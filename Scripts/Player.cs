using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    


    private Rigidbody rigidbodyComponent;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private int superJumpsRemaining;
    private float horizontalSpeed = 5f;




    // Start is called before the first frame update
    void Start()
    {
        //Transferring transform datas to a variable for having more performance in game
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //Check 'Space' input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        //Defining horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

    }

    // FixedUpdate is called once every physic update
    private void FixedUpdate()
    {
        
        //Giving player velocity on the x plane
        rigidbodyComponent.velocity = new Vector3(horizontalInput * horizontalSpeed, rigidbodyComponent.velocity.y, 0);

        //Creating a sphere on the bottom of player to check is it touching the ground and calculating with LayerMask
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f,playerMask).Length == 0)
        {
            return;
        }

        //Check 'Space' input and vector calculations
        if (jumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if(superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }

                
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed= false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);

            horizontalSpeed--;

            if (horizontalSpeed <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (other.gameObject.layer == 11)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }

        if (other.gameObject.layer == 9)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (other.gameObject.layer == 10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        }
    }



}
