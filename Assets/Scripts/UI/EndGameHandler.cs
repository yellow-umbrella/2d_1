using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject uiToShow;
    [SerializeField] private GameObject uiToHide;

    public void EndGame()
    {
        uiToShow.SetActive(true);
        uiToHide.SetActive(false);
        player.SetActive(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
