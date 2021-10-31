using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        EditorGUILayout.TextField(data.cardName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Is Key Card");
        EditorGUILayout.Toggle(data.isKeyCard);
        EditorGUILayout.EndHorizontal();

        EditorUtility.SetDirty(data);
    }
}
