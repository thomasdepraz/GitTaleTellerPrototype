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

    public override void InitType()
    {
        maxUseCount = useCount;
    }
}
