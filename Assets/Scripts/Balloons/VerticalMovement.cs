using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour 
{

	private float speed;
	
	private void Start ()
    {
		speed = Random.Range(5.0f, 15.0f);
    }

	// Update is called once per frame
	void Update () 
	{
		transform.Translate(0.0f, speed * Time.deltaTime, 0.0f);	
	}
}
