﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    public float MoveSpeed = .05f;

    #region Jump Vars
    public float JumpPower = 300; //amount of force we jump with
    [HideInInspector]
    public bool Grounded; //for checking if we should be able to jump

    public float CoyoteTime = .2f; //amount of time after walking off a platform we cazzn still jump (makes it feel good)
    private float CoyoteCounter;
    public float JumpBuffer = .2f; //amount of time player can buffer the jump (in simple terms you can press space to jump a little before hitting the ground)
    private float JumpBufferCounter;
    public float fallMult = 2.5f, lowJumpMult = 2;

    public bool HaveDoubleJump = false; //bool that checks if we have a double jump available
    //[HideInInspector]
    public bool CanDoubleJump = false; //bool that check is we have the double jump ability (planning on putting this on button 5)
    #endregion

    
    Rigidbody2D rb;

    public PlatControls PlatCon;
    public PlatControls PlatCon2;

    public GameObject dJump,StartMenu,WinMenu;

    Animator anim;

    public bool Started=false;

    AudioSource aud;
    public AudioClip NormalJump, DoubleJump, Death, Button, Smoke;
    #endregion

    // Use this for initialization
    void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(CameraShake()); dont think we need this
        PlatCon = GameObject.Find("Plat Manager").GetComponent<PlatControls>();
        PlatCon2 = GameObject.Find("Plat Manager2").GetComponent<PlatControls>();
        anim = GetComponent<Animator>();
        aud=GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

		Movement();

        anim.SetBool("Grounded", Grounded);
	}

	public void Movement()
	{

        if (Started)
        {
            //moving right
            if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.position += new Vector3(MoveSpeed * Time.deltaTime, 0, 0);
                transform.localEulerAngles = new Vector3(0, 0, 0); //rotate the player to face the right direction
                anim.SetBool("Moving", true);
            }

            //moving left
            else if (Input.GetKey(KeyCode.A))
            {
                gameObject.transform.position += new Vector3(-MoveSpeed * Time.deltaTime, 0, 0);
                transform.localEulerAngles = new Vector3(0, 180, 0); //rotate the player to face the right direction
                anim.SetBool("Moving", true);
            }

            else
            {
                anim.SetBool("Moving", false);
            }

            #region Jump Controls
            //if we're on the ground, reset the coyote timer
            if (Grounded)
            {
                CoyoteCounter = CoyoteTime;
            }
            //if we arent grounded, start counting
            else
            {
                CoyoteCounter -= Time.deltaTime;
            }

            //when we press space, we reset the buffer
            if (Input.GetKeyDown(KeyCode.Space))
                JumpBufferCounter = JumpBuffer;
            else
                JumpBufferCounter -= Time.deltaTime;

            //if player has pressed space (jump buffer) and we are still within coyote time, jump
            if ((JumpBufferCounter > 0f && CoyoteCounter > 0f) || HaveDoubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                //if we're not grounded and jumping, it means we're double jumping 
                if (!Grounded && CoyoteCounter <= 0)
                    HaveDoubleJump = false;
                else
                {
                    //we're jumping so we can reset the buffer 
                    JumpBufferCounter = 0;
                    CoyoteCounter = 0;
                    Grounded = false;
                }
                //actually jumping
                if (HaveDoubleJump == false && CanDoubleJump) //if we have double jump ability and are using the double jump
                {
                    rb.velocity = new Vector2(rb.velocity.x, JumpPower / 1.5f); //give weaker jump
                    Instantiate(dJump, GameObject.Find("JumpCheck").transform.position, Quaternion.identity);
                    aud.PlayOneShot(DoubleJump);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, JumpPower); //otherwise jump normally
                                                                         //     aud.PlayOneShot(NormalJump);
                }
            }

            //could have sworn i put this in earlier
            //player fall faster
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMult - 1) * Time.deltaTime;
            }

            //player can short hop
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMult - 1) * Time.deltaTime;
                CoyoteCounter = 0f;
            }

            #endregion
        }
        }


        //trigger enter stuff
        private void OnTriggerEnter2D(Collider2D other)
    {
        //fade away the black part
       if (other.tag=="Fade")
       {
           other.GetComponent<BoxCollider2D>().enabled = false; //so we dont reactivate the fading animation
            aud.PlayOneShot(Smoke);
           StartCoroutine(Fade(other.gameObject));
       }
       //start the movement
       if(other.tag=="TestButton")
        {
            //check if this is button number 5
            if (other.gameObject.name == "Button5")
            {
                CanDoubleJump = true;
                print("Obtained double jump ability");
            }
            //call the platform moving method and give it the button num
            PlatCon.MovePlat(other.gameObject.GetComponent<ButtonInfo>().ButtonNum, other.gameObject.GetComponent<ButtonInfo>().Area, other.gameObject.GetComponent<ButtonInfo>().Removable, other.gameObject.GetComponent<ButtonInfo>().Destination, other.gameObject.GetComponent<ButtonInfo>().horozontal); other.GetComponent<BoxCollider2D>().enabled = false;
            other.GetComponent<SpriteRenderer>().enabled = false;
            aud.PlayOneShot(Button);
        }
       //if you just want to remove an area (this might not be useful)
       if(other.tag=="TestRemoveButton")
        {
            PlatCon.RemovePlat(other.gameObject.GetComponent<ButtonInfo>().removeArea, other.gameObject.GetComponent<ButtonInfo>().removeDestination);
            other.gameObject.SetActive(false);
            aud.PlayOneShot(Button);
        }
       //if you want to remove an area and move one in
        if (other.tag == "BothButton")
        {
            print("Huh?");
            PlatCon.MovePlat(other.gameObject.GetComponent<ButtonInfo>().ButtonNum, other.gameObject.GetComponent<ButtonInfo>().Area, other.gameObject.GetComponent<ButtonInfo>().Removable, other.gameObject.GetComponent<ButtonInfo>().Destination, other.gameObject.GetComponent<ButtonInfo>().horozontal); PlatCon.RemovePlat(other.gameObject.GetComponent<ButtonInfo>().removeArea, other.gameObject.GetComponent<ButtonInfo>().removeDestination);
            other.gameObject.SetActive(false);
            aud.PlayOneShot(Button);
        }
        //move two areas at once (crazy i know)
        if (other.tag == "DoubleButton")
        {
            PlatCon.MovePlat(other.gameObject.GetComponent<ButtonInfo>().ButtonNum, other.gameObject.GetComponent<ButtonInfo>().Area, other.gameObject.GetComponent<ButtonInfo>().Removable, other.gameObject.GetComponent<ButtonInfo>().Destination, other.gameObject.GetComponent<ButtonInfo>().horozontal);
            PlatCon.RemovePlat(other.gameObject.GetComponent<ButtonInfo>().removeArea, other.gameObject.GetComponent<ButtonInfo>().removeDestination);
            PlatCon2.MovePlat(other.gameObject.GetComponent<ButtonInfo>().ButtonNum2, other.gameObject.GetComponent<ButtonInfo>().Area2, other.gameObject.GetComponent<ButtonInfo>().Removable2, other.gameObject.GetComponent<ButtonInfo>().Destination2, other.gameObject.GetComponent<ButtonInfo>().horozontal2);
            PlatCon2.RemovePlat(other.gameObject.GetComponent<ButtonInfo>().removeArea2, other.gameObject.GetComponent<ButtonInfo>().removeDestination2);
            other.gameObject.SetActive(false);
            aud.PlayOneShot(Button);
        }
        
        //teleport player to the destination thats on the teleporter
        if(other.tag =="Kill")
        {
            gameObject.transform.position = other.gameObject.GetComponent<KillInfo>().tele.transform.position;
           aud.PlayOneShot(Death);
        }
        if(other.tag=="Win")
        {
            WinMenu.SetActive(true);
        }

   }




    public void StartTheGame()
    {
        StartMenu.SetActive(false );
        Started = true;
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
