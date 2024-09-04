using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // object ref
    public MaceScript mace;
    public LineRenderer lineRend;

    // stats
    public float speed;
    public float sprintMod;
    public float health;
    public float pull;
    public float pullFORCE;
    public Vector3 move;

    // states
    public int xDir;
    public int yDir;
    public bool sprinting;
    public bool locked;

    // cooldowns
    public float tackleCD;
    public float tackleTimer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // move
        // decide direction based on inputs
        if (Input.GetKey(KeyCode.D))
        {
            xDir = 10;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            xDir = -10;
        }
        else
        {
            xDir = 0;
        }
        if (Input.GetKey(KeyCode.W))
        {
            yDir = 10;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            yDir = -10;
        }
        else
        {
            yDir = 0;
        }

        // sprint
        sprinting = false;
        if (Input.GetKey(KeyCode.LeftShift) && !sprinting)
        {
            sprinting = true;
        }

        // finally fucking move
        move = new Vector3(xDir, yDir, 0);
        move += transform.position;

        if (sprinting && !locked)
        {
            transform.position = Vector3.MoveTowards(transform.position, move, speed * sprintMod * Time.deltaTime);
        }
        else if (!locked)
        {
            transform.position = Vector3.MoveTowards(transform.position, move, speed * Time.deltaTime);
        }


        // tackle



        // MACE PHYSICS
        Vector3 forceTemp = transform.position - mace.transform.position;
        //Mathf.Sign(forceTemp.x) * 
        forceTemp = new Vector3(forceTemp.x, forceTemp.y);
        Vector3 force = pullFORCE * Time.deltaTime * forceTemp;
        Debug.Log(force);
        mace.rb.AddForce(force);

        // line renderer positions
        lineRend.SetPosition(0, mace.transform.position);
        lineRend.SetPosition(1, transform.position);
    }

    public void Tackle()
    {

    }
}
