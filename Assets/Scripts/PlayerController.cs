using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {

		Movement();

	}

	public void Movement()
	{
		if(Input.GetKey(KeyCode.D))
		{
			gameObject.transform.position += new Vector3(.05f,0,0);
		}
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position += new Vector3(-.05f, 0, 0);
        }
		if(Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce(new Vector2(0,300));
		}
    }
}
