using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO characterData;

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

}
