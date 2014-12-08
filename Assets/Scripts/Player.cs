using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	enum PlayerState {Start, FirstWalk, Standing, Sitting, FinalSit, Finished, None};
	private PlayerState currentState;

	private Animator animatorRef;
	public GameObject FishingLine;
	float fishingLineLength = 0f;

	public bool HasFish = false;
	public GameObject Tongue;
	public GameObject WindObject;
	public GameObject FishingHook;

	public GameObject FishSpawner;

	public GameObject DownArrowForFishing;
	public GameObject UpArrowForLine;
	public GameObject DownArrowForLine;
	public GameObject LeftArrowForStopFishing;
	public GameObject UpArrowForFeeding;

	public GameObject[] TextGroups;
	int whichTextGroup;

	public bool DoneFirstWalk = false;
	// Use this for initialization

	public GameObject FadeSprite;

	public AudioClip WalkLeft;
	public AudioClip WalkRight;

	public GameObject Gradient;

	void Start () 
	{
		currentState = PlayerState.None;

		//hide all prompts
		DownArrowForFishing.SetActive(false);
		UpArrowForLine.SetActive(false);
		DownArrowForLine.SetActive(false);
		LeftArrowForStopFishing.SetActive(false);
		UpArrowForFeeding.SetActive(false);

		animatorRef = gameObject.GetComponent<Animator>();

		FishingLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0f, 0f, 0f));

		whichTextGroup = 0;

		for (int i = 0; i < TextGroups.Length; i++)
		{
			TextGroups[i].GetComponent<TextGroupFade>().ZeroAlpha();
		}
		TextGroups[TextGroups.Length-1].transform.FindChild("Arrow").renderer.enabled = false;

		Gradient.SetActive(false);

		StartCoroutine(WaitToStart());
	}

	IEnumerator WaitToStart()
	{
		yield return new WaitForSeconds(1f);

		currentState = PlayerState.Start;
		UpArrowForFeeding.transform.FindChild("Feed").renderer.enabled = true;

		FadeSprite.GetComponent<FadeInScript>().Fade(-1f);
		ShowTextGroup();
	}

	// Update is called once per frame
	void Update () 
	{
		switch (currentState)
		{
		case PlayerState.Start:
			DoStartStuff ();
			break;
		case PlayerState.FirstWalk:
			DoFirstWalkStuff ();
			break;
		case PlayerState.Standing:
			DoStandingStuff ();
			break;
		case PlayerState.Sitting:
			DoSittingStuff();
			break;
		case PlayerState.FinalSit:
			DoFinalSitStuff();
			break;
		}


		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void DoStartStuff ()
	{
		if (animatorRef.GetBool("DoSit"))
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				animatorRef.SetBool("DoSit", false);

				HideTextGroups();
				TextGroups[0].transform.FindChild("Arrow").gameObject.SetActive(false);
			}
			Vector3 tempPos = transform.position;
			tempPos.z = -10f;
			Camera.main.transform.position = tempPos;
		}
		else
		{
			Vector3 tempPos = transform.position;
			tempPos.z = -10f;
			Camera.main.transform.position = tempPos;
			
			int desiredHash = Animator.StringToHash("Base Layer.Idle");
			int currentHash = animatorRef.GetCurrentAnimatorStateInfo(0).nameHash;
			//Debug.Log (currentHash);
			//Debug.Log (desiredHash);
			if (currentHash == desiredHash) 
			{
				currentState = PlayerState.FirstWalk;
				//Debug.Log ("Success");
			}
		}

	}

	void DoFirstWalkStuff ()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.localPosition += Vector3.left * Time.deltaTime * .7f;
			animatorRef.SetBool("Walking", true);
			if (transform.localScale.x > 0)
			{
				Vector3 temp = transform.localScale;
				temp.x *= -1;
				transform.localScale = temp;
			}
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.localPosition += -Vector3.left * Time.deltaTime  * .7f;
			if (transform.localPosition.x > 11.77914f)
			{
				Vector3 tempPos = transform.localPosition;
				tempPos.x = 11.77914f;
				transform.localPosition = tempPos;
			}
			animatorRef.SetBool("Walking", true);
			if (transform.localScale.x < 0)
			{
				Vector3 temp = transform.localScale;
				temp.x *= -1;
				transform.localScale = temp;
			}
		}
		else
		{
			animatorRef.SetBool("Walking", false);
		}
		
		//look for camera zoom stuff
		if (transform.localPosition.x < 9.319506f)
		{
			if (transform.localPosition.x > 4.704854f)
			{
				float normal = 9.319506f - 4.704854f; 
				float posNormal = (transform.localPosition.x - 4.704854f) / normal;
				
				float half = 9.319506f - (normal/2f);
				
				//if (transform.localPosition.x < half)
				//{
				Vector3 playerPos = transform.position;
				playerPos.z = -10f;
				Vector3 target = new Vector3(0f, 0f, -10f);
				Camera.main.transform.position = Vector3.Lerp (target, playerPos, posNormal); 
				//}
				
				float size = Mathf.Lerp(25f, 4f, posNormal);
				Camera.main.orthographicSize = size;
				
				WindObject.audio.volume = Mathf.Lerp(.1f, .25f, posNormal);
				UpArrowForFeeding.SetActive(false);
			}
			else if (transform.localPosition.x < 3.9)
			{
				UpArrowForFeeding.SetActive(true);
				if (HasFish)
				{
					UpArrowForFeeding.transform.FindChild("Feed").renderer.enabled = true;
				}
				if (Input.GetKey(KeyCode.UpArrow))
				{
					if (HasFish)
					{
						Tongue.GetComponent<Animator>().SetBool("Eat", true);
						HasFish = false;
						UpArrowForFeeding.SetActive(false);
						UpArrowForFeeding.transform.FindChild("Feed").renderer.enabled = true;
						UpArrowForFeeding.transform.FindChild("no fish").renderer.enabled = false;
					}
					else
					{
						UpArrowForFeeding.transform.FindChild("Feed").renderer.enabled = false;
						UpArrowForFeeding.transform.FindChild("no fish").renderer.enabled = true;
						currentState = PlayerState.Standing;
						DoneFirstWalk = true;
						whichTextGroup++;
					}
					
				}
			}
			else
			{
				UpArrowForFeeding.SetActive(false);
			}
		}
		else
		{
			Vector3 tempPos = transform.position;
			tempPos.z = -10f;
			Camera.main.transform.position = tempPos;
		}
	}

	void DoStandingStuff()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.localPosition += Vector3.left * Time.deltaTime * .7f;
			animatorRef.SetBool("Walking", true);
			if (transform.localScale.x > 0)
			{
				Vector3 temp = transform.localScale;
				temp.x *= -1;
				transform.localScale = temp;
			}
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.localPosition += -Vector3.left * Time.deltaTime  * .7f;
			if (transform.localPosition.x > 11.77914f)
			{
				Vector3 tempPos = transform.localPosition;
				tempPos.x = 11.77914f;
				transform.localPosition = tempPos;
			}
			animatorRef.SetBool("Walking", true);
			if (transform.localScale.x < 0)
			{
				Vector3 temp = transform.localScale;
				temp.x *= -1;
				transform.localScale = temp;
			}
		}
		else
		{
			animatorRef.SetBool("Walking", false);

			if (transform.localPosition.x > 11.77f)
			{
				DownArrowForFishing.SetActive(true);
				if (Input.GetKey(KeyCode.DownArrow))
				{
					animatorRef.SetBool("DoSit", true);
					currentState = PlayerState.Sitting;

					Gradient.SetActive(true);

					DownArrowForFishing.SetActive(false);
					if (whichTextGroup < TextGroups.Length - 1)
					{
						ShowTextGroup();
						FishSpawner.GetComponent<FishSpawnScript>().StartSpawning();
					}
					else
					{
						//end game
						ShowTextGroup();
						TextGroups[whichTextGroup].transform.FindChild("Arrow").renderer.enabled = true;
						currentState = PlayerState.FinalSit;
					}
				}
			}
			else
			{
				DownArrowForFishing.SetActive(false);
			}
		}

		//look for camera zoom stuff
		if (transform.localPosition.x < 9.319506f)
		{
			if (transform.localPosition.x > 4.704854f)
			{
				float normal = 9.319506f - 4.704854f; 
				float posNormal = (transform.localPosition.x - 4.704854f) / normal;

				float half = 9.319506f - (normal/2f);

				//if (transform.localPosition.x < half)
				//{
					Vector3 playerPos = transform.position;
					playerPos.z = -10f;
					Vector3 target = new Vector3(0f, 0f, -10f);
					Camera.main.transform.position = Vector3.Lerp (target, playerPos, posNormal); 
				//}

				float size = Mathf.Lerp(25f, 4f, posNormal);
				Camera.main.orthographicSize = size;

				WindObject.audio.volume = Mathf.Lerp(.15f, .35f, posNormal);
				UpArrowForFeeding.SetActive(false);
			}
			else if (transform.localPosition.x < 3.9)
			{
				UpArrowForFeeding.SetActive(true);
				if (HasFish)
				{
					UpArrowForFeeding.transform.FindChild("Feed").renderer.enabled = true;
				}

				if (Input.GetKeyDown(KeyCode.UpArrow))
				{
					if (HasFish)
					{
						Tongue.GetComponent<Animator>().SetBool("Eat", true);
						Tongue.audio.Play();
						Tongue.transform.FindChild("Particle_1").particleSystem.Play();
						HasFish = false;
						whichTextGroup++;
						UpArrowForFeeding.SetActive(false);
						UpArrowForFeeding.transform.FindChild("Feed").renderer.enabled = true;
						UpArrowForFeeding.transform.FindChild("no fish").renderer.enabled = false;
					}
					else
					{
						UpArrowForFeeding.transform.FindChild("Feed").renderer.enabled = false;
						UpArrowForFeeding.transform.FindChild("no fish").renderer.enabled = true;
					}

				}
			}
			else
			{
				UpArrowForFeeding.SetActive(false);
			}
		}
		else
		{
			Vector3 tempPos = transform.position;
			tempPos.z = -10f;
			Camera.main.transform.position = tempPos;
		}
	}

	void DoSittingStuff()
	{
		if (animatorRef.GetBool("DoSit"))
		{

			if (Input.GetKey(KeyCode.LeftArrow) && fishingLineLength <= .05f)
			{
				animatorRef.SetBool("DoSit", false);
				FishSpawner.GetComponent<FishSpawnScript>().KillAllFish();
				UpArrowForFeeding.transform.FindChild("Feed").renderer.enabled = true;
				HideTextGroups();
				Gradient.SetActive(false);
				//FishingHook.transform.GetChild(0).gameObject.renderer.enabled = false;
			}
			else
			{
				int desiredHash = Animator.StringToHash("Base Layer.Sit");
				int currentHash = animatorRef.GetCurrentAnimatorStateInfo(0).nameHash;
				//Debug.Log (currentHash);
				//Debug.Log (desiredHash);
				if (currentHash == desiredHash) 
				{
					DoFishingStuff();
					UpArrowForLine.SetActive(true);
					DownArrowForLine.SetActive(true);
					if (fishingLineLength <= .05f)
					{
						LeftArrowForStopFishing.SetActive(true);
					}
					else
					{
						LeftArrowForStopFishing.SetActive(false);
					}
				}
				else
				{
					Vector3 tempPos = transform.position;
					tempPos.z = -10f;
					Camera.main.transform.position = tempPos;
				}

			}


		}
		else
		{
			Vector3 tempPos = transform.position;
			tempPos.z = -10f;
			Camera.main.transform.position = tempPos;

			UpArrowForLine.SetActive(false);
			DownArrowForLine.SetActive(false);
			LeftArrowForStopFishing.SetActive(false);

			int desiredHash = Animator.StringToHash("Base Layer.Idle");
			int currentHash = animatorRef.GetCurrentAnimatorStateInfo(0).nameHash;
			//Debug.Log (currentHash);
			//Debug.Log (desiredHash);
			if (currentHash == desiredHash) 
			{
				currentState = PlayerState.Standing;
				//Debug.Log ("Success");
			}
		}


	}

	void DoFishingStuff()
	{
		//FishingHook.transform.GetChild(0).gameObject.renderer.enabled = true;
		if (Input.GetKey(KeyCode.DownArrow) && fishingLineLength < 8)
		{
			fishingLineLength += Time.deltaTime;
			FishingLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0f, -fishingLineLength, 0f));
		}
		if (Input.GetKey(KeyCode.UpArrow) && fishingLineLength > 0)
		{
			fishingLineLength += -Time.deltaTime;
			FishingLine.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0f, -fishingLineLength, 0f));
		}

		Vector3 toWorld = FishingLine.transform.localPosition;
		toWorld.y += -fishingLineLength;

		FishingHook.transform.localPosition = toWorld;

		Vector3 tempPos = FishingHook.transform.position;
		tempPos.z = -10f;

		Camera.main.transform.position = Camera.main.transform.position * .75f + tempPos * .25f;

	}

	void DoFinalSitStuff()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void ShowTextGroup()
	{
		TextGroups[whichTextGroup].GetComponent<TextGroupFade>().Fade(1f);
	}

	void HideTextGroups()
	{
		TextGroups[whichTextGroup].GetComponent<TextGroupFade>().Fade(-1f);
	}

	public void PlayLeftFoot()
	{
		audio.PlayOneShot(WalkLeft, .5f);
	}

	public void PlayRightFoot()
	{
		audio.PlayOneShot(WalkRight, .5f);
	}

}
