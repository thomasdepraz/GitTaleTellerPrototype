using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [Header("Stats")]
    public int chapterCount;
    public List<List<StoryEvent>> steps = new List<List<StoryEvent>>();

    [Header("Events")]
    public StoryEvent combatEvent;

    //Private variables
    private int currentStepIndex = 0;
    private int currentStepEventListIndex = 0;


    private void Start()
    {
        //Init list
        for (int i = 0; i < steps.Count; i++)
        {
            steps[i] = new List<StoryEvent>();
        }

        //Initialize first chapter
        InitializeChapter();
    }

    private void InitializeChapter()
    {
        //Init one monster for the time being
        int r = Random.Range(1,4);
        steps[r].Add(combatEvent);

        //Init graphics

    }

    public IEnumerator ReadStory()
    {
        //Visually move the player

        //Make the hero go through every events and trigger enter and exit on every event
        if (steps[currentStepIndex][currentStepEventListIndex] != null)
            steps[currentStepIndex][currentStepEventListIndex].OnTriggerEnterEvent();

        yield return null;
    }
    public void MoveToNextEvent()
    {
        if(currentStepEventListIndex < steps[currentStepIndex].Count - 1)
        {
            currentStepEventListIndex++;
            //launch readstory
        }
        else
        {
            if(currentStepIndex < 3)
            {
                currentStepEventListIndex = 0;
                currentStepIndex++;
                //launch readstory
            }
            else
            {
                Debug.Log("Fn du chapitre");
                currentStepIndex = 0;
            }
        }
    }

    public void StartEventCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

}
