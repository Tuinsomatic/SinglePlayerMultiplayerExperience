using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int number;
    public bool isFinal;
    public int position;

    // Start is called before the first frame update
    void Start()
    {
        position = 1;
    }

}
