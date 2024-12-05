using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables

    #region Jump Vars
    float JumpPower = 300; //amount of force we jump with
    public   bool Grounded; //for checking if we should be able to jump

    public float CoyoteTime = .2f; //amount of time after walking off a platform we cazzn still jump (makes it feel good)
    private float CoyoteCounter;
    public float JumpBuffer = .2f; //amount of time player can buffer the jump (in simple terms you can press space to jump a little before hitting the ground)
    private float JumpBufferCounter;
    #endregion

    public bool testMovement = false;
    


    public GameObject mCamera, testArea,testRemovable,testDestination;
    Rigidbody2D rb;
    #endregion

    // Use this for initialization
    void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CameraShake());
	}
	
	// Update is called once per frame
	void Update () {

		Movement();

        //THIS COULD PROBABLY BE MOVED SOMEWHERE ELSE IN THE FUTURE
        //moves the area to where it needs to be, and if it is there it stops the shake
        if(testArea.transform.position==testDestination.transform.position)
        {
            testMovement = false;
            testRemovable.SetActive(false);
        }
        else if(testMovement)
        {
            testArea.transform.position = Vector3.MoveTowards(testArea.transform.position, testDestination.transform.position, 3f*Time.deltaTime);
        }

	}

	public void Movement()
	{
        //moving right
		if(Input.GetKey(KeyCode.D))
		{
			gameObject.transform.position += new Vector3(.05f,0,0);
		}
        //moving left
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position += new Vector3(-.05f, 0, 0);
        }

        #region Jump Controls
        //if we're on the ground, reset the coyote timer
        if(Grounded)
        {
            CoyoteCounter = CoyoteTime;
        }
        //if we arent grounded, start counting
        else
        {
            CoyoteCounter -= Time.deltaTime;
        }

        //when we press space, we reset the buffer
        if(Input.GetKeyDown(KeyCode.Space))
            JumpBufferCounter = JumpBuffer;
        else
            JumpBufferCounter -= Time.deltaTime;

        //if player has pressed space (jump buffer) and we are still within coyote time, jump
        if(JumpBufferCounter > 0f && CoyoteCounter > 0f)
        {
            //we're jumping so we can reset the buffer 
            JumpBufferCounter = 0;
            CoyoteCounter = 0;

            //actually jumping
            rb.AddForce(new Vector2(0, JumpPower));
        }
        #endregion
    }


    //trigger enter stuff
   private void OnTriggerEnter2D(Collider2D other)
    {
        //fade away the black part
       if (other.tag=="Fade")
       {
           StartCoroutine(Fade(other.gameObject));
       }
       //start the movement
       if(other.tag=="TestButton")
        {
            testMovement = true;
            StartCoroutine(CameraShake());
            other.gameObject.SetActive(false);
        }

   }

    //fade an object and then make it inactive
    IEnumerator Fade(GameObject obj)
    {
        for (float i = 255f; i>=0; i-=5f)
        {
            obj.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i/255f);
            yield return new WaitForEndOfFrame();
            print(i);
        }
        obj.SetActive(false);
    }


    //shake the camera if an area is moving
    IEnumerator CameraShake()
    {
        if (testMovement)
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
