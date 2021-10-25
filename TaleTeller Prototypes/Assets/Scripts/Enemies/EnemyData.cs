using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public int baseLifePoints;
    public int baseAttackDamage;
}
