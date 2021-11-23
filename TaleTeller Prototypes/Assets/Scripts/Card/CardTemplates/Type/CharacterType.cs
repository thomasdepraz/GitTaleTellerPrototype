using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterType : CardTypes
{
    public enum CharacterFightingRange
    {
        None, Left, Right, LeftAndRight
    }

    /// <summary>
    /// The base stats of the character
    /// </summary>
    public CharacterStats stats;

    /// <summary>
    /// The behaviour of the character (Allied or enemy)
    /// </summary>
    public CharacterBehaviour behaviour;

    public CharacterFightingRange fightingRange;

    /// <summary>
    /// How many times the player is able to use the card.
    /// </summary>
    public int useCount;
    private int maxUseCount;

    /// <summary>
    /// Fight logic between characters /with players. 
    /// </summary>
    public void Fight()
    {

    }

    void GetFightingTargets()
    {

    }

    /// <summary>
    /// Update method of the useCount.
    /// </summary>
    public void UpdateUseCount()
    {
        useCount--;
        if (useCount == 0)
        {
            //Discard

            //resest useCount;
            useCount = maxUseCount;
        }
    }

    public override void InitType(CardData data)
    {
        maxUseCount = useCount;
        data.onStartEvent += OnStart;
    }

    public void OnStart()
    {
        //add to OnStartQueue
        CardManager.Instance.board.onStartQueue.Add(OnStartRoutine());
    }

    public IEnumerator OnStartRoutine()
    {
        Debug.Log("Il");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("se passe des trucs");


        yield return new WaitForSeconds(0.5f);

        //Unqueue
        CardManager.Instance.board.UpdateOnStartQueue();
    }

}
