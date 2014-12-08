using UnityEngine;
using System.Collections;

public class TitleTrigger : MonoBehaviour 
{
	public GameObject Title;
	public GameObject FeedText;
	public GameObject NoFishText;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Player")
		{

			Player p = coll.gameObject.GetComponent<Player>();
			if (p.DoneFirstWalk)
			{
				if (Title.activeSelf)
				{
					Title.SetActive(false);
				}

				if (p.HasFish)
				{
					FeedText.renderer.enabled = true;
					NoFishText.renderer.enabled = false;
				}
				else
				{
					FeedText.renderer.enabled = false;
					NoFishText.renderer.enabled = true;
				}
			}
		}
	}
}
