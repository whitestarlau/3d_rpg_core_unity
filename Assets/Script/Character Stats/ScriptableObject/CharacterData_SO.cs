using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("Stats Info")]
    public int maxHealth;
    public int currentHealth;
    public int baseDefence;
    public int curretnDefence;

    [Header("Kill")]
    public int killPoint;

    [Header("Level")]
    public int currentLevel;
    public int maxLevel;
    public int baseExp;
    public int currentExp;
    public float levelBuff;

    public float LevelUpMultiplier
    {
        get
        {
            return 1 + (currentLevel - 1) * levelBuff;
        }
    }
    public void UpdateExp(int point)
    {
        Debug.Log("UpdateExp" + point);
        currentExp += point;
        if (currentExp >= baseExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
        baseExp += (int)(baseExp * LevelUpMultiplier);

        maxHealth = (int)(maxHealth * LevelUpMultiplier);
        currentHealth = maxHealth;

        Debug.Log("Level up!" + currentLevel + "MaxHealth:" + maxHealth);
    }
}
