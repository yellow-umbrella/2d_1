using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image heartTemplate;
    [SerializeField] private Transform heartContainer;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private void Start()
    {
        Player.Instance.OnHealed += Player_OnHealed;
        Player.Instance.OnHurt += Player_OnHurt;

        heartTemplate.gameObject.SetActive(false);
        UpdateHealthUI();
    }

    private void Player_OnHurt()
    {
        UpdateHealthUI();
    }

    private void Player_OnHealed()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        int currentHealth = Player.Instance.Health;
        int maxHealth = Player.Instance.MaxHealth;

        foreach (Transform child in heartContainer)
        {
            if (child.GetComponent<Image>() != heartTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < maxHealth; i++)
        {
            Image heart = Instantiate(heartTemplate, heartContainer);
            if (i < currentHealth)
            {
                heart.sprite = fullHeart;
            }
            else
            {
                heart.sprite = emptyHeart;
            }
            heart.gameObject.SetActive(true);
        }
    }
}
