using UnityEngine;
using System.Collections;

public class DebrisScript : MonoBehaviour 
{
	public ParticleSystem[] particles;

	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < particles.Length; i++)
		{
			StartCoroutine(SpawnParticles(i));
		}
	}
	
	IEnumerator SpawnParticles(int i)
	{
		yield return new WaitForSeconds(Random.Range(5f, 25f));
		particles[i].Play();

		StartCoroutine(SpawnParticles(i));

	}
}
