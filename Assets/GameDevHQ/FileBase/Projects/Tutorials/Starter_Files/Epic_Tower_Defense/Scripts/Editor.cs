using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Editor : EditorWindow
{
    bool groupEnabled;
    public Object[] mech;
    public int amountOfMechs;
    bool addedEnemies = false;
    
    [MenuItem("Window/Editor")]

    static void Init()
    {
        Editor window = (Editor)EditorWindow.GetWindow(typeof(Editor));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Test Wave", EditorStyles.boldLabel);

        amountOfMechs = EditorGUILayout.IntField("Number of mechs", amountOfMechs);


        if (GUILayout.Button("Add Enemies"))
        {
            mech = new Object[amountOfMechs];
            addedEnemies = true;
        }

        if (amountOfMechs > 0 && addedEnemies)
        {
            for(int i = 0; i < amountOfMechs; i++)
            {
                mech[i] = EditorGUILayout.ObjectField("Mech", mech[i], typeof(Object), true);
            }
        }

        
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional settings", groupEnabled);
        //Add advanced settings here.
        EditorGUILayout.EndToggleGroup();

        if (GUILayout.Button("Start Wave"))
        {
            var enemy = PoolManager.Instance.RequestEnemy();
        }
    }
}

public class ListWrapper<T> : UnityEngine.Object
{
    public List<T> objects = new List<T>();
}
