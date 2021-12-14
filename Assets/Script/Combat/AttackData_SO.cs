using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data",menuName ="Attack Data/Attack")]
public class AttackData_SO : ScriptableObject
{
    public float attackRange;
    public int skillRange;
    public float coolDown;
    public int minDamage;
    public int maxDamage;
    public float criticalMultiplier;
    public float criticalChance;

}
