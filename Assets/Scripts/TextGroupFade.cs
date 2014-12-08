using UnityEngine;
using System.Collections;

public class TextGroupFade : MonoBehaviour 
{
	float fadeDirection;
	bool fading = false;
	TextMesh [] meshRef;
	// Use this for initialization
	void Start () 
	{
		meshRef = gameObject.GetComponentsInChildren<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (fading);
		{
			Color temp = Color.black;
			for (int i = 0; i < meshRef.Length; i++)
			{
				temp = meshRef[i].color;
				temp.a += Time.deltaTime * fadeDirection * .5f;
				meshRef[i].color = temp;
			}
			if (fadeDirection > 0)
			{
				if (temp.a >= 1)
				{
					fading = false;
				}
			}
			else
			{
				if (temp.a <= 0)
				{
					fading = false;
				}
			}
		}
	}

	public void ZeroAlpha()
	{
		for (int i = 0; i < meshRef.Length; i++)
		{
			Color temp = meshRef[i].color;
			temp.a = 0f;
			meshRef[i].color = temp;
		}
	}

	public void Fade(float sign)
	{
		fadeDirection = sign;
		fading = true;
	}
}
