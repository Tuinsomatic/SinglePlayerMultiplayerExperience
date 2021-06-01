using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody rb;
    public bool touching;
    //private Quaternion q;

    public Position position;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        touching = true;
    }

    void Update()
    {
        if (touching == true)
        {
            //BASIC CONTROLS
            if (Input.GetKey("left"))
            {
                transform.Rotate(0, -0.5f, 0);
            }
            if (Input.GetKey("right"))
            {
                transform.Rotate(0, 0.5f, 0);
            }
            if (Input.GetKey("up"))
            {
                rb.AddForce(transform.forward * 400);
            }
            if (Input.GetKey("down"))
            {
                rb.AddForce(-transform.forward * 400);
            }
        }
        if (Input.GetKeyDown("space"))
        {
            transform.position = position.currentCheckpoint.transform.position - new Vector3(0, 7, 0);
            transform.rotation = position.currentCheckpoint.transform.rotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (touching == false)
        {
            StartCoroutine(Wait());
        }
        
    }
    void OnCollisionExit(Collision collision)
    {
        if (touching == true)
        {
            StartCoroutine(Wait());
        }    
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Trigger")
        {
            Checkpoint cp = collider.gameObject.GetComponent("Checkpoint") as Checkpoint;
            position.currentPosition = cp.position;
            position.currentCheckpoint = cp;
            cp.position += 1;
        }
        if (collider.gameObject.tag == "Void")
        {
            transform.position = position.currentCheckpoint.transform.position;
        }
    }

    IEnumerator Wait()
    {
        if (touching == false)
        {
            touching = true;
            yield return new WaitForSeconds(2);
        }
        else if (touching == true)
        {
            touching = false;
            transform.Translate(0, 0.02f, 0);
        }
    }
}
