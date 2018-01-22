using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReset : MonoBehaviour {

	[SerializeField] private GameObject targetObject;
	[SerializeField] private string targetMessage;
	public Color highlightedColor = Color.cyan;


	void OnMouseOver()
	{
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		if (sprite != null) {
			sprite.color = highlightedColor;
		}
	}

	void OnMouseExit()
	{
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		if (sprite != null) {
			sprite.color = Color.white;
		}
	}

	void OnMouseDown()
	{
		transform.localScale = new Vector3 (1.1f, 1.1f, 1.1f);
	}

	void OnMouseUp()
	{
		transform.localScale = Vector3.one;
		if(targetObject != null)
			targetObject.SendMessage(targetMessage);
	}

}
