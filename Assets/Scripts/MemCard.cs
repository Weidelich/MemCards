using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemCard : MonoBehaviour {

	// Use this for initialization
	[SerializeField] private GameObject cardBack;
	[SerializeField] private Sprite image;
	[SerializeField] private SceneController controller;

	private int _id;
	public int id {
		get {return _id; }
	}
		
	public void SetCard(int id, Sprite image)
	{
		_id = id;
		GetComponent<SpriteRenderer> ().sprite = image;

	}

	public void OnMouseDown()
	{
		if (cardBack.activeSelf && controller.canRevealed)
		{
			//Debug.Log("Heil");
			cardBack.SetActive(false);
			controller.CardRevealed (this);
		}
	}

	public void Unreveal()
	{
		cardBack.SetActive (true);
	}

}
