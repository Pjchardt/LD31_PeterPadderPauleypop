using UnityEngine;
using System.Collections;

public class FadeInScript : MonoBehaviour 
{
	float fadeDirection;
	bool fading = false;

	SpriteRenderer spriteRef;

	void Start()
	{
		spriteRef = GetComponent<SpriteRenderer>();
		spriteRef.enabled = true;
	}

	void Update () 
	{
		if (fading);
		{
			Color temp = Color.black;

			temp = spriteRef.color;
			temp.a += Time.deltaTime * fadeDirection * .5f;
			spriteRef.color = temp;

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
	
	public void Fade(float sign)
	{
		fadeDirection = sign;
		fading = true;
	}
}
