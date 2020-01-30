using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(ScrollMove))]
public class ScrollEditor : Editor
{
    private ScrollMove myTarget;

    private void OnEnable()
    {
        myTarget = (ScrollMove)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ScrollMove myScript = (ScrollMove)target;

        if (GUILayout.Button("Save Flag"))
            myScript.SaveFlag($"Flag_{myTarget.Flags.Count}");

        EditorGUILayout.BeginVertical();
        for (int i = 0; i < myTarget.Flags.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            GUI.color = Color.red;
            if (GUILayout.Button("Del"))
            {
                myTarget.Flags.RemoveAt(i);
                return;
            }
            GUI.color = Color.white;
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        //GUI.color = Color.green;
        //if (GUILayout.Button("Save"))
        //{
        //    EditorUtility.SetDirty(myTarget);
        //    AssetDatabase.SaveAssets();
        //    AssetDatabase.Refresh();
        //}
    }
}
