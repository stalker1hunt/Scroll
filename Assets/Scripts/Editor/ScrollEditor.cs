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
            myScript.SaveFlag(Guid.NewGuid().ToString());

        EditorGUILayout.BeginVertical();
        for (int i = 0; i < myTarget.Flags.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUI.color = new Color(1f, 0.5f, 0f);
            if (GUILayout.Button("ReBake"))
            {
                int index = i; List<FlagData> toremove = new List<FlagData>();
                //foreach (var el in myTarget.Flags[index])
                //{
                //    if (el == null || el.target == null)
                //    {
                //        toremove.Add(el);
                //        Debug.LogError("Must Del broken element!");
                //    }
                //    else
                //    {
                //        el.localSize = el.target.localScale;
                //        el.localPos = el.target.parent != null ? el.target.localPosition : el.target.position;
                //        el.localEulerEngles = el.target.parent != null ? el.target.localEulerAngles : el.target.eulerAngles;
                //    }
                //}
              //  myTarget.Flags[index].Elements.RemoveRange(toremove);
            }
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

        GUI.color = Color.green;
        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(myTarget);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
