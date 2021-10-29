using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [Header("Stats")]
    [HideInInspector] public int chapterCount;
    public List<List<StoryEvent>> steps = new List<List<StoryEvent>>();
    public float heroMovingSpeed;

    [Header("Events")]
    public StoryEvent combatEvent;

    [Header("References")]
    public List<RectTransform> stepsUI;
    public RectTransform heroUITransform;
    public Image heroGraph;

    //Private variables
    private int currentStepIndex = 0;
    private int currentStepEventListIndex = 0;


    private void Start()
    {
        Debug.LogError("Opening the console");
        steps = new List<List<StoryEvent>>();
        //Init list
        for (int i = 0; i < 4; i++)
        {
            steps.Add(new List<StoryEvent>());
        }

        //Initialize first chapter
        InitializeChapter();
    }

    private void InitializeChapter()
    {
        //Init one monster for the time being
        int r = Random.Range(1,4);
        steps[r].Add(combatEvent);
        steps[r][0].InitializeEvent();//init event
        Debug.LogError($"Added monster on step {r + 1}");

        //Init graphics

    }
    public void StartStory()
    {
        StartCoroutine(ReadStory());
    }
    public IEnumerator ReadStory()
    {
        //Visually move the player
        Debug.LogError($"Moving to step {currentStepIndex + 1}");

        heroUITransform.position = stepsUI[currentStepIndex + 1].position;

        //Make the hero go through every events and trigger enter and exit on every event
        yield return new WaitForSeconds(1);
        if (steps[currentStepIndex].Count > 0)
        {
            steps[currentStepIndex][currentStepEventListIndex].OnTriggerEnterEvent();
        }
        else
        {
            MoveToNextEvent();
        }
        yield return null;
    }
    public void MoveToNextEvent()
    {
        if(currentStepEventListIndex < steps[currentStepIndex].Count - 1)
        {
            currentStepEventListIndex++;
            //launch readstory
            StartCoroutine(ReadStory());
        }
        else
        {
            if(currentStepIndex < 3)
            {
                currentStepEventListIndex = 0;
                currentStepIndex++;
                //launch readstory
                StartCoroutine(ReadStory());
            }
            else
            {
                Debug.LogError("Fin du chapitre");
                chapterCount++;
                currentStepIndex = 0;
                currentStepEventListIndex = 0;

                //Start new Chapter
                StartNewChapter();
            }
        }
    }
    public void StartNewChapter()
    {
        //Clear lists
        for (int i = 0; i < steps.Count; i++)
        {
            steps[i].Clear();
        }
        InitializeChapter();

        //Clear player temporary values
        GameManager.Instance.currentHero.bonusDamage = 0;

        //Deal Cards
        CardManager.Instance.cardDeck.DealCards(CardManager.Instance.cardHand.maxHandSize);

        //Give creativity
        GameManager.Instance.creativityManager.creativity += 5;
    }
    public void StartEventCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
