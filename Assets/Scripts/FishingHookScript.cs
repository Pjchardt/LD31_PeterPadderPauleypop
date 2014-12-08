using UnityEngine;
using System.Collections;

public class FishingHookScript : MonoBehaviour 
{
	GameObject CaughtFish = null;
	public GameObject Tongue;
	public GameObject Player;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (CaughtFish == null)
		{
			if (coll.tag == "Fish")
			{
				//Debug.Log ("Caught");
				coll.tag = "CaughtFish";
				coll.gameObject.GetComponent<FishScript>().Caught();
				CaughtFish = coll.gameObject;
				coll.transform.parent = transform;
				coll.enabled = false;
				Vector3 temp = coll.transform.localPosition;
				temp.y = 0f;
				temp.x = 0f;
				coll.transform.localPosition = temp;
				Tongue.GetComponent<Tongue_Script>().FishReference = CaughtFish;
				Player.GetComponent<Player>().HasFish = true;
				StartCoroutine(RotateFish ());
			}
		}
	}

	IEnumerator RotateFish()
	{
		float timeElapsed = 0f;

		while (timeElapsed < 1)
		{
			CaughtFish.transform.RotateAround(CaughtFish.transform.position, CaughtFish.transform.forward, -360f * Time.deltaTime);
			timeElapsed += Time.deltaTime * 4f;
			yield return new WaitForEndOfFrame();
		}
	}
}
