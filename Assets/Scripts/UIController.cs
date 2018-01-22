using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] private SceneController SceneController;
    [SerializeField] private Dropdown dropDown;

    public void StartGame()
    {
        gameObject.SetActive(false);
        SceneController.GameStart(dropDown.value);
    }

    public void Close()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
