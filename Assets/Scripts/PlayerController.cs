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

    Rigidbody2D rb;
    #endregion

    // Use this for initialization
    void Start () 
	{
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
}
