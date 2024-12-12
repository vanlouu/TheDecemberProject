using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat : MonoBehaviour {

	#region
	public float speed; //speed of the plat, who would have guessed
	public int startingPoint; //what point the plat starts at
	public Transform[] points; //array of transforms that the plat moves towards

	private int i; //index for array
	#endregion

	void Start () 
	{
		transform.position = points[startingPoint].position; //set the plat to the starting position
	}
	
	// Update is called once per frame
	void Update () 
	{
		//check if the plat is at its destination (or close enough)
		if(Vector2.Distance(transform.position, points[i].position) < .02f)
        {
			i++;
			if(i == points.Length) //reset when we reach the end of the array
            {
				i = 0;
            }
        }

		transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
	}
}
