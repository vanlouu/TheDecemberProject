using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatControls : MonoBehaviour {
    //I moved the platform controls over here, this just goes on an empty game object (doesnt matter where)pu

	#region Vars
	public bool Movement;
    public GameObject mCamera;
    [HideInInspector] //these get filled by the button
    public GameObject Area, Removable, Destination;
	#endregion
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Area.transform.position == Destination.transform.position)
		{
			Movement = false;
			Removable.SetActive(false);
		}
		else if (Movement)
		{
			Area.transform.position = Vector3.MoveTowards(Area.transform.position, Destination.transform.position, 3f * Time.deltaTime);
		}
	}

    //my idea is to have all of the platform moving stuff here, we can change all of the public variables based on the button number (which gets passed through by player)
    public void MovePlat(int ButtonNum, GameObject tempArea, GameObject tempRemovable,GameObject tempDestination)
    {
        //for example
        if(ButtonNum == 1)
        {
            Area = tempArea;
            Removable = tempRemovable;
            Destination = tempDestination;
            Movement = true;
            StartCoroutine(CameraShake());
        }
    }

    //shake the camera if an area is moving
    IEnumerator CameraShake()
    {
        if (Movement)
        {
            mCamera.transform.position += new Vector3(0, .1f, 0);
            yield return new WaitForEndOfFrame();
            mCamera.transform.position += new Vector3(.1f, 0, 0);
            yield return new WaitForEndOfFrame();
            mCamera.transform.position += new Vector3(0, -.1f, 0);
            yield return new WaitForEndOfFrame();
            mCamera.transform.position += new Vector3(0, -.1f, 0);
            yield return new WaitForEndOfFrame();
            mCamera.transform.position += new Vector3(-.1f, 0, 0);
            yield return new WaitForEndOfFrame();
            mCamera.transform.position += new Vector3(-.1f, 0, 0);
            yield return new WaitForEndOfFrame();
            mCamera.transform.position += new Vector3(0, .1f, 0);
            yield return new WaitForEndOfFrame();
            mCamera.transform.position += new Vector3(.1f, 0, 0);
            yield return new WaitForEndOfFrame();
            StartCoroutine(CameraShake());
        }
        yield return new WaitForEndOfFrame();
    }
}
