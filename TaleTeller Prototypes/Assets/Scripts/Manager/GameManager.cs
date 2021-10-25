using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    public Hero currentHero;

    public void Awake()
    {
        CreateSingleton(true);
    }

}
