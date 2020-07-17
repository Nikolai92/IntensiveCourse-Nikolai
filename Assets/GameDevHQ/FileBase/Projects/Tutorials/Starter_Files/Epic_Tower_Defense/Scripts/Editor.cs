using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor : EditorWindow
{
    bool groupEnabled;
    [SerializeField] private Wave Mechs;




    [MenuItem("Window/Editor")]

    static void Init()
    {
        Editor window = (Editor)EditorWindow.GetWindow(typeof(Editor));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Test Wave", EditorStyles.boldLabel);

        //[TooltipAttribute]("Add scriptable wave obj here")
        GUILayout.Label("Scriptable Test Wave");

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional settings", groupEnabled);
        EditorGUILayout.EndToggleGroup();

        if (GUILayout.Button("Start Wave"))
        {
            var enemy = PoolManager.Instance.RequestEnemy();
        }
    }
}
