using UnityEngine;
using System.Collections;

public class SunriseSunsetShit : MonoBehaviour 
{
	Vector3 startPosition;
	public Vector3 targetPosition;

	enum DayState {Sunrise, Day, Sunset, Night};

	DayState currentState;

	// Use this for initialization
	void Start () 
	{
		startPosition = transform.position;
		currentState = DayState.Day;

		StartCoroutine(DayNightCycle());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine(DoSunset());
		}
	}

	IEnumerator DoSunset()
	{
		float timeElapsed = 0f;

		while (timeElapsed < 1)
		{
			transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed);
			timeElapsed += Time.deltaTime * .05f;
			yield return new WaitForEndOfFrame();
		}

		transform.position = targetPosition;
		currentState = DayState.Night;
	}

	IEnumerator DoSunrise()
	{
		float timeElapsed = 0f;
		
		while (timeElapsed < 1)
		{
			transform.position = Vector3.Lerp(targetPosition, startPosition, timeElapsed);
			timeElapsed += Time.deltaTime * .05f;
			yield return new WaitForEndOfFrame();
		}
		
		transform.position = startPosition;
		currentState = DayState.Day;
	}

	IEnumerator DayNightCycle()
	{
		yield return new WaitForSeconds(30f);

		if (currentState == DayState.Day)
		{
			StartCoroutine(DoSunset());
			currentState = DayState.Sunset;
		}
		else if (currentState == DayState.Night)
		{
			StartCoroutine(DoSunrise());
			currentState = DayState.Sunrise;
		}

		StartCoroutine(DayNightCycle());
	}
}
