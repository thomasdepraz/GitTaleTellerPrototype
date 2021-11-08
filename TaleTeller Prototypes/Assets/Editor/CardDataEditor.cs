using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardData))]
[CanEditMultipleObjects]
public class CardDataEditor : Editor
{
    public CardData data;

    private void OnEnable()
    {
        data = (CardData)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Card Name");
        data.cardName = EditorGUILayout.TextField(data.cardName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Is Key Card");
        data.isKeyCard = EditorGUILayout.Toggle(data.isKeyCard);
        EditorGUILayout.EndHorizontal();

        if(data.isKeyCard)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Interest Cooldown");
            data.interestCooldown = EditorGUILayout.IntField(data.interestCooldown);
            GUILayout.Label("Creativity Burn");
            data.creativityBurn = EditorGUILayout.IntField(data.creativityBurn);
            EditorGUILayout.EndHorizontal();
        }

        EditorUtility.SetDirty(data);
    }
}
