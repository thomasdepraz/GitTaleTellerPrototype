using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Effect : ScriptableObject
{
    public string effectName;
}

public class CardMalus : Effect
{

}

public class DCEffect : CardMalus
{
    public string discardValue;
}

public class ExhaustEffect : CardMalus
{

}

public class BonusEffect : Effect
{
    public string bonusValue;
}

public class MalusEffect : Effect
{
    public string malusValue;
}

public class GoldMalusEffect : MalusEffect
{
    public string goldMalusValue;
}
public static class EffectManager
{
    public static List<Type> GetSubClasses(Type type)
    {
        List<Type> result = new List<Type>();
        Type[] temp = Assembly.GetAssembly(type).GetTypes();

        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].IsSubclassOf(type) && temp[i].BaseType == type)//get only if direct child type (remove base type check if all childs needed)
            {
                result.Add(temp[i]);
            }
        }
        return result;
    }

    public static bool HasSubClasses(Type type)
    {
        Type[] temp = Assembly.GetAssembly(type).GetTypes();
        
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].IsSubclassOf(type))
            {
                return true;
            }
        }
        return false;
    }
}
