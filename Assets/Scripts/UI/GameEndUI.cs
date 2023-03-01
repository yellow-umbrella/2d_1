using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
    private const float SHOW_DELAY = 1f;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private TextMeshProUGUI gameEndText;
    [SerializeField] private Color victoryColor;
    [SerializeField] private Color gameOverColor;
    [SerializeField] private Image background;

    private void Awake()
    {
        restartButton.onClick.AddListener(() => { SceneManager.LoadScene(1); });
        returnToMenuButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
    }

    private void Start()
    {
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        GameManager.Instance.OnVictory += GameManager_OnVictory;
        Hide();
    }

    private void GameManager_OnGameOver()
    {
        background.color = gameOverColor;
        gameEndText.text = "Game Over";
        Invoke(nameof(Show), SHOW_DELAY);
    }

    private void GameManager_OnVictory()
    {
        background.color = victoryColor;
        gameEndText.text = "VICTORY";
        Invoke(nameof(Show), SHOW_DELAY);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
