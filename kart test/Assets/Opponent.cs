using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    public Transform lapcp; //holds the transform component of the lap checkpoint
    public Transform cp; //holds the transform of the current checkpoint
    public Rigidbody rb; //rigidbody of opponent
    public Position position; //opponent's race position and current checkpoint values

    private bool drawRay = false;
    public int degree = 90;
    private Vector3 angle;
    private RaycastHit vision;
    public float length;

    public bool move = true;


    // Start is called before the first frame update
    void Start()
    {
        this.transform.Rotate(0, Random.Range(-15.0f, 15.0f), 0);
        position.currentCheckpoint = lapcp.gameObject.GetComponent("Checkpoint") as Checkpoint;
        length = 800f;
    }

    // Update is called once per frame
    void Update()
    {
        if(move == true)
        {
            rb.AddForce(transform.forward * 400);          
        }
        if (drawRay == true)
        {
            angle = new Vector3(degree, 0, 0);
            Debug.DrawRay(transform.position, angle * length, Color.red, 0.5f);

            if (Physics.Raycast(transform.position, angle, out vision, length))
            {
                //Debug.Log("found something");
                if (vision.collider.tag == "Trigger")
                {
                    Checkpoint cpv = vision.collider.gameObject.GetComponent("Checkpoint") as Checkpoint;

                    if (cpv.number == position.currentCheckpoint.number + 1)
                    {
                        Debug.Log(cpv.number);
                        this.transform.LookAt(vision.transform);
                        this.transform.Rotate(0, Random.Range(-15.0f, -5.0f), 0);
                        drawRay = false;
                    }
                }
            }
            degree -= 10;
            StartCoroutine("TryForCP");
            if (degree < -90)
            {
                degree = 90;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        StartCoroutine(Wait());
    }       

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Trigger")
        {
            Checkpoint cp = collider.gameObject.GetComponent("Checkpoint") as Checkpoint;
            position.currentPosition = cp.position;
            cp.position += 1;
            position.currentCheckpoint = cp;
            drawRay = true;
        }
        if (collider.gameObject.tag == "Void")
        {
            StartCoroutine("Wait");
            transform.position = position.currentCheckpoint.transform.position - new Vector3(0, 7, 0);
            transform.rotation = position.currentCheckpoint.transform.rotation;
        }
    }

    IEnumerator Wait()
    {
        move = false;
        yield return new WaitForSeconds(2);
        move = true;
    }
    IEnumerator TryForCP()
    {
        yield return new WaitForSeconds(0.2f);
    }
}
