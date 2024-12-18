using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatControls : MonoBehaviour {
    //I moved the platform controls over here, this just goes on an empty game object (doesnt matter where)pu

	#region Vars
	public bool Movement,RemovingMovement;
    public GameObject mCamera,DestroyedV;
    [HideInInspector] //these get filled by the button
    public GameObject Area, Removable, Destination,removeDestination,removeArea;
    public bool horozontal;

    AudioSource aud;
	#endregion
	// Use this for initialization
	void Start () 
	{
		aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        //moved movements into their own areas
        TheMovement();
        TheRemovement();
	}

    public void TheMovement()
    {
        if (Area.transform.position == Destination.transform.position&&Movement)
        {
            Movement = false;
            //making the destruction anim
            if (horozontal && Removable != null)
            {
                Instantiate(DestroyedV, Removable.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                Removable.SetActive(false);

            }
            else if (Removable != null)
            {
                Instantiate(DestroyedV, Removable.transform.position, Quaternion.identity);
                Removable.SetActive(false);
            }
            Removable = null; //removable needs to no longer exsist so it wont play when there is no removable

           //stop the sound
           aud.Pause(); 
        }
        else if (Movement)
        {
            Area.transform.position = Vector3.MoveTowards(Area.transform.position, Destination.transform.position, 3f * Time.deltaTime);
        }
    }
    public void TheRemovement()
    {
        if(removeArea.transform.position == removeDestination.transform.position)
        {
            //maybe set something active
            RemovingMovement = false;
        }
        else if(RemovingMovement)
        {
            removeArea.transform.position = Vector3.MoveTowards(removeArea.transform.position, removeDestination.transform.position,3f*Time.deltaTime);
        }
    }

    //my idea is to have all of the platform moving stuff here, we can change all of the public variables based on the button number (which gets passed through by player)
    public void MovePlat(int ButtonNum, GameObject tempArea, GameObject tempRemovable,GameObject tempDestination, bool isHorz)
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
        if(ButtonNum==2)
        {
            Area = tempArea;
            Removable = tempRemovable;
            Destination = tempDestination;
            Movement = true;
            StartCoroutine(CameraShake());
        }
        if (ButtonNum == 3)
        {
            Area = tempArea;
            Removable = tempRemovable;
            Destination = tempDestination;
            Movement = true;
            StartCoroutine(CameraShake());
        }
        if(ButtonNum==4)
        {
            Area = tempArea;
       //     Removable = tempRemovable;
            Destination = tempDestination;
            Movement = true;
        }
        if(ButtonNum==5)
        {
            Area = tempArea;
            Removable = tempRemovable;
            Destination = tempDestination;
            Movement = true;
            StartCoroutine(CameraShake());
        }
        //move with no camera shake
        if(ButtonNum == 6)
        {
            Area = tempArea;
            Removable = tempRemovable;
            Destination = tempDestination;
            Movement = true;
        }
        horozontal = isHorz;
        aud.time = 0;
        aud.Play();
    }

    //used to remove areas
    public void RemovePlat(GameObject tempArea,GameObject tempDestination)
    {
        removeArea = tempArea;
        removeDestination = tempDestination;
        RemovingMovement = true;
        StartCoroutine(CameraShake());
        
    }

    //shake the camera if an area is moving
    IEnumerator CameraShake()
    {
        while(Movement||RemovingMovement)
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
        }
        yield return new WaitForEndOfFrame();
    }
}
