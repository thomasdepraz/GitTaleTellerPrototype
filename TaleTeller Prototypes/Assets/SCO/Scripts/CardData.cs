using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardAttribute
{
    None,
    Exhaust, 
    Ethereal
}

public enum CardType
{
    Enemy, 
    Weapon, 
    Event
}

[CreateAssetMenu]
public class CardData : ScriptableObject
{
    public string cardName;
    public int weight;
    public CardAttribute attribute;
    public CardType type;
    public StoryEvent linkedEvent;

    [TextArea(2,3)]
    public string description;

    public bool canInteract;
}
