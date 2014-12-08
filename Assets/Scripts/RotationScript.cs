using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.RotateAround(transform.position, transform.forward, 360f * Time.deltaTime);
	}
}
