using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoryEventType
{
    None, 
    Combat,
    Location, 
    Object, 
    Npc
}
public abstract class StoryEvent : MonoBehaviour
{
    public string eventName;
    public string eventDescription;
    public StoryEventType eventType;

    public abstract void InitializeEvent();

    public abstract void OnTriggerEnterEvent();

    public abstract void OnTriggerExitEvent();

    //Extend this class if needs be

}
