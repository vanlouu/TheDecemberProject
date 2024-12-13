using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublejumpGive : ButtonInfo {

    public GameObject BackgroundImage;
    public GameObject JumpExplainer;
	public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            //enable backgroud image and all that fun stuff
            //Time.timeScale = 0; //though it would look cool but doesnt work well
            BackgroundImage.SetActive(true);
            JumpExplainer.SetActive(true);
        }
    }

}
