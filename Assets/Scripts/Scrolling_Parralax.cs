using UnityEngine;
using System.Collections;

public class Scrolling_Parralax : MonoBehaviour 
{
	public float Speed;
	private float xOffset = 0f;

	private Material materialRef;

	// Use this for initialization
	void Start () 
	{
		materialRef = renderer.material;
	}
	
	// Update is called once per frame
	void Update () 
	{
		xOffset += Time.deltaTime * Speed;
		Vector2 offset = new Vector2(xOffset, 0f);
		materialRef.SetTextureOffset("_MainTex", offset);
	}
}
