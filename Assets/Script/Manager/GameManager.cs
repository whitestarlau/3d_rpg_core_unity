using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CharacterStats playerStats;

    public void RegisterPlayer(CharacterStats characterStats)
    {
        playerStats = characterStats;
    }
}
