using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move()
    {
        Vector3 moveVector = new Vector3(10, 0, 0);
        transform.Translate(moveVector);
    }
}
