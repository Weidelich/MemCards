using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

	public float offsetX = 1f;
	public float offsetY = 0.75f;

	private MemCard firstRevealed;
	private MemCard secondRevealed;

	private int score = 0;
    private int scoreMax;

	public bool canRevealed {
		get { return secondRevealed == null;}
	}

	[SerializeField] private MemCard originalCard;
	[SerializeField] private Sprite[] images;
	[SerializeField] private TextMesh scoreLabel;
    [SerializeField] private TextMesh congratulation;
	// Use this for initialization
	void Start () {
        congratulation.gameObject.SetActive(false);
	}

    public void GameStart(int param)
    {
        int[] easy = { 0, 0, 1, 1, 2, 2, 3, 3 };
        int[] medium = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        int[] hard = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11 };
        int[] numbers = { };
        int[] gridRows = { 2, 4, 4 };
        int[] gridCols = { 4, 4, 6 };
        int[] sc = { 4, 8, 12 };
        scoreMax = sc[param];
        switch (param)
        {
            case 0:
                numbers = easy;
                originalCard.transform.Translate(new Vector3(offsetX / 2, -offsetY / 2.5f));
                offsetY /= 2;
                offsetX /= 2;
                break;
            case 1:
                numbers = medium;
                originalCard.transform.Translate(new Vector3(offsetX / 3f, 0f));
                offsetX /= 1.5f;
                offsetY /= 2.5f;
                break;
            case 2:
                numbers = hard;
                offsetX /= 2f;
                offsetY /= 2.5f;
                break;
            default:
                break;
        }
        //Debug.Log(gridCols[2]);
        Vector3 startPos = originalCard.transform.position;
        numbers = ShuffleArray(numbers);
        for (int i = 0; i < gridCols[param]; i++)
        {
            for (int j = 0; j < gridRows[param]; j++)
            {
                MemCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemCard;
                    card.transform.localScale = new Vector3(0.32f, 0.32f, 1);
                }
                int index = j * gridCols[param] + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = offsetX * i + startPos.x;
                float posY = -offsetY * j + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

	public void CardRevealed (MemCard m)
	{
		if (firstRevealed == null) {
			firstRevealed = m;
		} else {
			secondRevealed = m;
			//Debug.Log (firstRevealed && secondRevealed ? "true" : "false");
			StartCoroutine(CheckMatch());
		}
	}

	private IEnumerator CheckMatch() {
		if (firstRevealed.id == secondRevealed.id) {
			score++;
			scoreLabel.text = "Score:  " + score;
            if (score == scoreMax)
            {
                congratulation.gameObject.SetActive(true);
                yield return new WaitForSeconds(2f);
                this.Restart();
            }
		} else {
			yield return new WaitForSeconds (1f);
			firstRevealed.Unreveal ();
			secondRevealed.Unreveal ();
		}
		firstRevealed = null;
		secondRevealed = null;
	}

	private int[]  ShuffleArray(int [] arr)
	{
		int[] newArr = arr.Clone () as int[];
		for (int i = 0; i < newArr.Length; i++) {
			int tmp = newArr [i];
			int r = Random.Range (i, newArr.Length);
			newArr [i] = newArr [r];
			newArr [r] = tmp;
		}
		return newArr;
	}

	public void Restart()
	{
		Application.LoadLevel("MemC");
		//SceneManager.LoadScene = "MemC";
	}

}
