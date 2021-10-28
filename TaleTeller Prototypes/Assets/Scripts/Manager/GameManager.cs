using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    public Hero currentHero;
    public StoryManager storyManager;
    public CreativityManager creativityManager;
    public GameObject goButton;

    public void Awake()
    {
        CreateSingleton(true);
    }

}
