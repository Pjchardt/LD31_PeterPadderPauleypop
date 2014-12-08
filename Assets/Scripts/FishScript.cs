using UnityEngine;
using System.Collections;

public class FishScript : MonoBehaviour 
{
	//do a sine wave movement if not yet caught
	//when far enough along X, destroy fish
	float startY;
	bool caught = false;
	float timeElapsed = 0f;
	// Use this for initialization
	void Start () 
	{
		startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!caught && transform.localPosition.x < 6)
		{
			Destroy(gameObject);
		}
		else if (!caught)
		{
			timeElapsed += Time.deltaTime * 2f;
			Vector3 temp = transform.position;
			temp.x += -1f * Time.deltaTime;
			temp.y = startY + Mathf.Sin (timeElapsed) * .09f;
			transform.position = temp;
		}
	}

	public void Caught()
	{
		caught = true;
	}
}
