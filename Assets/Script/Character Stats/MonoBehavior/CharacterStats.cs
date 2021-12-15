using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO characterData;
    public AttackData_SO attackData;

    [HideInInspector]
    public bool isCritical;

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
         Debug.Log("damage: " + damage);
        if(isCritical){
             Debug.Log("Critical!");
            defender.GetComponent<Animator>().SetTrigger("Hit");
        }
        //TODO: Update UI
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
