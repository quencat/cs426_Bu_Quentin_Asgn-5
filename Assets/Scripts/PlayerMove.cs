using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerMove : NetworkBehaviour
{
    public Camera mycam;
    public bool powered = false; //only set to public for testing reasons.
    public float movingSpeed = 20f;
    public float rotationSpeed = 150;
    public float jumpForce = 350f;



    protected bool grounded;
    Rigidbody rb;
    Transform t;

    float timeElapsed;
    float timerSpeed = 30f;

    Text text1;
    Text text2;
    Text text3;
    Text text4;

    [SyncVar] protected Color randomColor;
    // Start is called before the first frame update
    void Start()
    {
        SetColor(randomColor);
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();

        //mycam = GetComponent<Camera>();
        if (!isLocalPlayer) return;
        mycam.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        //used default arrow key map
        /*       var x = Input.GetAxis("Horizontal") * movingSpeed;
               var z = Input.GetAxis("Vertical") * movingSpeed;
               transform.Translate(x, 0, z);
        */
        //replaced with rotation control
        if (Input.GetKey(KeyCode.W))
        { rb.velocity += this.transform.right * movingSpeed * Time.deltaTime;
        }


        else if (Input.GetKey(KeyCode.S))
            rb.velocity -= this.transform.right * movingSpeed * Time.deltaTime;

        // Quaternion returns a rotation that rotates x degrees around the x axis and so on
        if (Input.GetKey(KeyCode.D))
            t.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.A))
            t.rotation *= Quaternion.Euler(0, -rotationSpeed * Time.deltaTime, 0);


        //jump functionality if powered using space
        if (powered)
        {
          timeElapsed  += Time.deltaTime; // start counting down
          if (timeElapsed >= timerSpeed) {
            timeElapsed  = 0;
            this.powered = false; // reset properties when powerup is over
          }

            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                //Debug.Log("here");
                rb.AddForce(t.up * jumpForce);
                grounded = false;
            }

        }


        //exit function
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }



    public override void OnStartLocalPlayer()
    {
        //player's own score will be red
        text1 = GameObject.Find("GameManager/playerCanvas/Text1").GetComponent<Text>();
        text2 = GameObject.Find("GameManager/playerCanvas/Text2").GetComponent<Text>();
        text3 = GameObject.Find("GameManager/playerCanvas/Text3").GetComponent<Text>();
        text4 = GameObject.Find("GameManager/playerCanvas/Text4").GetComponent<Text>();


        uint number = GetComponent<NetworkIdentity>().netId.Value;
        if (number % 4 == 1) {
            text1.color = Color.red;
        }else if (number % 4 == 2)
        {
            text2.color = Color.red;
        }else if (number % 4 == 3)
        {
            text3.color = Color.red;
        }
        else if (number % 4 == 0)
        {
            text4.color = Color.red;
        }
    }


    public override void OnStartClient()
    {
        //deleted the red example
        TransmitColor();
        if (isServer)
        {
            randomColor = randomizeColor();
        }
    }


    //grounding functions to prevent double jumps
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            grounded = true;
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8)
            grounded = false;
    }

    // a set of function to generate random colors for each player. don't worry too much about them.
    void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
    Color randomizeColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
    [Command]
    void Cmd_ProvideColorToServer(Color c)
    {

        randomColor = c;
    }
    [ClientCallback]
    void TransmitColor()
    {
        if (isLocalPlayer)
        {
            Cmd_ProvideColorToServer(randomColor);
        }
    }

    public void magicCat()
    {
        this.powered= true;

    }


}


/*
 * References:
 * https://answers.unity.com/questions/1082956/how-to-sync-color-with-other-players-unet.html
 * https://stackoverflow.com/questions/58377170/how-to-jump-in-unity-3d
 */
/*Notes:
 * Quentin: added randomized player colors that syncs, jump fuction if powered and cannot double jump, and changed to rigid body movement.
 *
 */
