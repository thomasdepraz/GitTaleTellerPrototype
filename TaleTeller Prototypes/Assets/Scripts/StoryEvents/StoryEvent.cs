using UnityEngine;

public enum StoryEventType
{
    None, 
    Combat,
    Location, 
    Object, 
    Npc
}
public abstract class StoryEvent : ScriptableObject
{
    public string eventName;
    public string eventDescription;
    public StoryEventType eventType;

    public abstract void InitializeEvent();

    public abstract void OnTriggerEnterEvent();

    public abstract void OnTriggerExitEvent();

    //Extend this class if needs be

}
