using UnityEngine;
using System.Collections;

public class FishSpawnScript : MonoBehaviour 
{
	Vector2 spawnY = new Vector2(-2.5f, 1.5f);
	public GameObject FishPrefab;

	float SpawnDelay = 0f;
	bool spawning = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (spawning)
		{
			if (SpawnDelay <= 0)
			{
				//spawn fish and reset timer
				Vector3 position = transform.localPosition;
				position.y = Random.Range(spawnY.x, spawnY.y);
				GameObject temp = Instantiate (FishPrefab) as GameObject;
				temp.transform.parent = transform.parent;
				temp.transform.localPosition = position;
				SpawnDelay = Random.Range(2f, 4f);
			}
			else
			{
				SpawnDelay -= Time.deltaTime;
			}
		}
	}

	public void StartSpawning()
	{
		spawning = true;
		SpawnDelay = 0f;
	}

	public void KillAllFish()
	{
		GameObject[] Fish = GameObject.FindGameObjectsWithTag("Fish");
		for (int i = 0; i < Fish.Length; i++)
		{
			Destroy (Fish[i]);
		}

		spawning = false;
	}
}
