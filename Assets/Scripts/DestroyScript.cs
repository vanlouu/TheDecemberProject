using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Destroy());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(4);
		Destroy(gameObject);
	}
}
