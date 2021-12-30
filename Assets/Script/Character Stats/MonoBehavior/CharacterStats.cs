using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public event Action<int, int> UpdateHealthBarOnAttack;
    public CharacterData_SO templeData;
    public CharacterData_SO characterData;
    public AttackData_SO attackData;

    [HideInInspector]
    public bool isCritical;

    private void Awake()
    {
        if (templeData != null)
        {
            characterData = Instantiate(templeData);
        }
    }

    #region Read from Data_SO
    public int MaxHealth
    {
        get
        {
            if (characterData != null)
                return characterData.maxHealth;
            else
                return 0;
        }
        set
        {
            if (characterData != null)
                characterData.maxHealth = value;
        }
    }

    public int CurrentHealth
    {
        get
        {
            if (characterData != null)
                return characterData.currentHealth;
            else
                return 0;
        }
        set
        {
            if (characterData != null)
                characterData.currentHealth = value;
        }
    }

    public int BaseDefence
    {
        get
        {
            if (characterData != null)
                return characterData.baseDefence;
            else
                return 0;
        }
        set
        {
            if (characterData != null)
                characterData.baseDefence = value;
        }
    }

    public int CurretnDefence
    {
        get
        {
            if (characterData != null)
                return characterData.curretnDefence;
            else
                return 0;
        }
        set
        {
            if (characterData != null)
                characterData.curretnDefence = value;
        }
    }
    #endregion

    #region Character Combat
    public void TakeDamage(CharacterStats attcker, CharacterStats defender)
    {
        int damage = Mathf.Max(attcker.CurrentDamage() - defender.CurretnDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        // Debug.Log("TakeDamage damage: " + damage+",isCritical:"+isCritical+",status:"+this.name);
        if (attcker.isCritical)
        {
            Debug.Log("TakeDamage Critical!");
            defender.GetComponent<Animator>().SetTrigger("Hit");
        }
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);
        //TODO: 经验Update
    }


    public void TakeDamage(int damage, CharacterStats defender)
    {
        int filterDamage = Mathf.Max(damage - defender.CurretnDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - filterDamage, 0);
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);
        //TODO: 经验Update
    }

    private int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);
        if (isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;
        }
        return (int)coreDamage;
    }
    #endregion
}
