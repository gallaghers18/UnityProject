using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour {

    public Vector3 direction;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = transform.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.MovePosition(rb.position + direction * 2 * Time.fixedDeltaTime);
    }

    public void Attack()
    {

    }
}
