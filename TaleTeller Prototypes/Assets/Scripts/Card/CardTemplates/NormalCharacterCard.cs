using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Normal Card", menuName = "Data/NormalCard")]
public class NormalCharacterCard : CardData, ICharacterCard
{
    [Header("Character")]
    [SerializeField]
    private CharacterStats _stats;
    public CharacterStats stats { get =>_stats; set {_stats = value; } }

    [SerializeField]
    private CharacterBehaviour _behaviour;
    public CharacterBehaviour behaviour { get => _behaviour; set { _behaviour = value; } }

    [SerializeField]
    private int _useCount;
    public int useCount { get => _useCount; set { _useCount = value; } }



    public void Fight()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateUseCount()
    {
        throw new System.NotImplementedException();
    }
}
