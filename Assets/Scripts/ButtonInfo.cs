using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfo : MonoBehaviour {
	//this just stores all f the info on what will move when hit
	public int ButtonNum;
	public GameObject Area, Removable, Destination,removeArea,removeDestination;
	public bool horozontal;

	//has the same thing as the first buttons, but with double the amount of info
	public int ButtonNum2;
	public GameObject Area2, Removable2, Destination2, removeArea2, removeDestination2;
	public bool horozontal2;
}
