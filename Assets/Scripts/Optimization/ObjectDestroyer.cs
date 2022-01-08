using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour 
{

	public float time = 5.0f;

	// Use this for initialization
	private void Start () 
	{
		Destroy(gameObject, time);
	}
	
}
