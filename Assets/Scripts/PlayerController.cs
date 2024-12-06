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

    public PlatControls PlatCon;
    #endregion

    // Use this for initialization
    void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(CameraShake()); dont think we need this
        PlatCon = GameObject.Find("Plat Manager").GetComponent<PlatControls>();
	}
	
	// Update is called once per frame
	void Update () {

		Movement();

        
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
            Grounded = false;

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
            //call the platform moving method and give it the button num
            PlatCon.MovePlat(other.gameObject.GetComponent<ButtonInfo>().ButtonNum, other.gameObject.GetComponent<ButtonInfo>().Area, other.gameObject.GetComponent<ButtonInfo>().Removable, other.gameObject.GetComponent<ButtonInfo>().Destination);
            other.gameObject.SetActive(false);
        }
       //if you just want to remove an area (this might not be useful)
       if(other.tag=="TestRemoveButton")
        {
            PlatCon.RemovePlat(other.gameObject.GetComponent<ButtonInfo>().ButtonNum, other.gameObject.GetComponent<ButtonInfo>().removeArea, other.gameObject.GetComponent<ButtonInfo>().removeDestination);
            other.gameObject.SetActive(false);
               
        }
       //if you want to remove an area and move one in
        if (other.tag == "BothButton")
        {
            print("Huh?");
            PlatCon.MovePlat(other.gameObject.GetComponent<ButtonInfo>().ButtonNum, other.gameObject.GetComponent<ButtonInfo>().Area, other.gameObject.GetComponent<ButtonInfo>().Removable, other.gameObject.GetComponent<ButtonInfo>().Destination);
            PlatCon.RemovePlat(other.gameObject.GetComponent<ButtonInfo>().ButtonNum, other.gameObject.GetComponent<ButtonInfo>().removeArea, other.gameObject.GetComponent<ButtonInfo>().removeDestination);
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
        }
        obj.SetActive(false);
    }
    //moved camera shake to PlatControls
}
