using NaughtyAttributes;
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
    [HideInInspector] public bool keyCardActivated;
    public int interestCooldown;
    [HideInInspector] public int currentInterestCooldown;
    public int creativityBurn;
    public CardType type;
    public CharacterBehaviour characterBehaviour;
    public CharacterStats characterStats;

    public CardEventTrigger trigger;
    public int creativityCost;
    public CardEffect effect;
    public CardEffect deadCardEffect;
    public Sprite cardGraph;
    [HideInInspector] public CardFeedback feedback;
    [HideInInspector] public CardContainer currentContainer;

    [TextArea(2, 3)]
    public string description;

    //TEMP
    [Expandable]
    public List<Effect> effects = new List<Effect>();

    [Expandable]
    public CardTypes cardType;


    //Events specification
    public delegate void BoardEvent();
    public BoardEvent onStartEvent;
    public BoardEvent onEndEvent;
    public BoardEvent onEnterEvent;

    public CardData InitializeData(CardData data)
    {
        data = Instantiate(data);//make data an instance of itself

        //Instantiate other scriptables objects
        if(data.cardType!= null) data.cardType = Instantiate(data.cardType);

        for (int i = 0; i < data.effects.Count; i++)
        {
            if(effects[i]!=null) data.effects[i] = Instantiate(data.effects[i]);
        }


        //Write logic to determine how the card subscribe to the events

        //Subscribe to onEnterEvent
        data.onEnterEvent += OnEnter;

        return data;
    }

    public void OnEnter()
    {
        //add effects to board manager list
        for (int i = 0; i < effects.Count; i++)
        {
            //Init effect that adds a routine to the manager list

            CardManager.Instance.board.cardEffectQueue.Add(tempEffect());//THIS IS TEMPORARY
        }
    }

    IEnumerator tempEffect()
    {
        Debug.Log("Trigger Effect");
        yield return null;
        CardManager.Instance.board.UpdateStoryQueue();
    }

    public void ResetCharacterStats()
    {
        characterStats.Reset();
    }

}