using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    Text levelText;
    Image healthSlider;
    Image expSlider;

    private void Awake()
    {
        levelText = transform.GetChild(2).GetComponent<Text>();
        healthSlider = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        expSlider = transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        CharacterStats playerStatus = GameManager.Instance.playerStats;
        UpdateHealthBar(playerStatus.CurrentHealth, playerStatus.MaxHealth);

        CharacterData_SO playerData = playerStatus.characterData;
        UpdateExpBar(playerData.currentExp, playerData.baseExp);

        levelText.text = "Level :" + playerData.currentLevel.ToString("00");
    }

    private void UpdateHealthBar(int CurrentHealth, int MaxHealth)
    {
        float sliderPercent = (float)CurrentHealth / MaxHealth;
        healthSlider.fillAmount = sliderPercent;
    }

    private void UpdateExpBar(int CurrentExp, int LevelUpExp)
    {
        float sliderPercent = (float)CurrentExp / LevelUpExp;
        expSlider.fillAmount = sliderPercent;
    }
}
