using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterBehaviour
{
    None,
    Peaceful, 
    Agressive
}
public enum CardEventTrigger
{
    None, 
    OnEncounter,
    OnDeath, 
    OnAppear
}

public enum CardType
{
    Character, 
    Object, 
    Location
}

[CreateAssetMenu(fileName = "Card", menuName = "Data/Card", order = 0)]
public class CardData : ScriptableObject
{
    public string cardName;
    public bool isKeyCard;
    public int interestCooldown;
    public int creativityBurn;
    public CardType type;
    public CharacterBehaviour characterBehaviour;
    public CharacterStats characterStats;

    public CardEventTrigger trigger;
    public int creativityCost;
    public CardEffect effect;
    public Sprite cardGraph;
    [HideInInspector] public CardFeedback feedback;

    [TextArea(2, 3)]
    public string description;

}