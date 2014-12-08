using UnityEngine;
using System.Collections;

public class MusicPlayScript : MonoBehaviour 
{
	bool played = false;

	// Use this for initialization


	void OnTriggerEnter2D(Collider2D coll)
	{
		if (!played && coll.tag == "Player")
		{
			audio.Play();
			played = true;


		}
	}
}
