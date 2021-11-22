using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInterfaces 
{
    public CharacterStats stats { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public CharacterBehaviour behaviour { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int useCount { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Fight()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateUseCount()
    {
        throw new System.NotImplementedException();
    }
}

public interface ICharacterCard
{
    /// <summary>
    /// The base stats of the character
    /// </summary>
    public CharacterStats stats { get; set; }

    /// <summary>
    /// The behaviour of the character (Allied or enemy)
    /// </summary>
    public CharacterBehaviour behaviour { get; set; }

    /// <summary>
    /// How many times the player is able to use the card.
    /// </summary>
    public int useCount { get; set; }

    /// <summary>
    /// Fight logic between characters /with players. 
    /// </summary>
    void Fight();

    /// <summary>
    /// Update method of the useCount.
    /// </summary>
    void UpdateUseCount();
}


public interface IObjectCard
{

}

public interface ILocationCard
{

}
