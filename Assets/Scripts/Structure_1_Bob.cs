using UnityEngine;
using System.Collections;

public class Structure_1_Bob : MonoBehaviour 
{
	public float Speed;
	public float Magnitude;
	private Vector3 startPosition;

	// Use this for initialization
	void Start () 
	{
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 tempPos = transform.position;
		tempPos.y = startPosition.y + Mathf.Sin(Time.timeSinceLevelLoad * Speed) * Magnitude;
		transform.position = tempPos;

	}
}
