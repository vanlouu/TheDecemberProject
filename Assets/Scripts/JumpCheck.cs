using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour
{
	#region Variables
	PlayerController PC;
	#endregion
	// Use this for initialization
	void Start()
	{
		//get our player's controller
		PC = GameObject.Find("Player").GetComponent<PlayerController>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		//check if we collided with the ground
		if (other.tag == "Ground")
		{
			PC.Grounded = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
    {
		//if we leave ground, we're no longer grounded (crazy I know)
		if(other.tag == "Ground")
        {
			PC.Grounded = false;
        }
    }
}