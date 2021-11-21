using System;
using System.Collections.Generic;
using UnityEngine;

public interface IShape { }

[Serializable]
public class Cube : IShape
{
    public Vector3 size;
}

[Serializable]
public class Thing
{
    public int weight;
}

[ExecuteInEditMode]
public class BuildingBlocks : MonoBehaviour
{


    [SerializeReference]
    public List<Effect> bins = new List<Effect>();

    void OnEnable()
    {
        if (bins != null)
        {
            //bins = new List<Effect>();
            bins.Add(new GoldMalusEffect());
            bins.Add(new Effect());
            bins.Add(new MalusEffect());
        }
    }
}