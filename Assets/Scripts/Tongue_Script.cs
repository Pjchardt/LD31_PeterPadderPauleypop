using UnityEngine;
using System.Collections;

public class Tongue_Script : MonoBehaviour 
{
	public GameObject FishReference = null;
	public GameObject HeartReference;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void DestroyFish()
	{
		if (FishReference != null)
		{
			Destroy(FishReference);
			FishReference = null;
			gameObject.GetComponent<Animator>().SetBool("Eat", false);
		}
	}

	public void DoneEating()
	{
		//beat heart
		HeartReference.GetComponent<Animator>().SetTrigger("BeatHeart");
		HeartReference.GetComponent<AudioSource>().Play();
	}
}
