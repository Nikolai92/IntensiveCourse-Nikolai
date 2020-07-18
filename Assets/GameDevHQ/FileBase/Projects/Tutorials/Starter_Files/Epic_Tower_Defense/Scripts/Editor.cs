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
    private int _currentWave;

    [SerializeField] private List<Wave> _waves;
    [SerializeField] private GameObject _startPos;

    [MenuItem("Window/WaveTester")]

    static void Init()
    {
        Editor window = (Editor)EditorWindow.GetWindow(typeof(Editor));
        window.Show();
    }

    private void OnGUI()
    {
        var so = new SerializedObject(this);

        var property = so.FindProperty("_waves");
        EditorGUILayout.PropertyField(property, true);

        property = so.FindProperty("_startPos");
        EditorGUILayout.PropertyField(property, true);

        so.ApplyModifiedProperties();

        if (GUILayout.Button("Start Wave"))
        {
            EditorCoroutines.EditorCoroutineExtensions.StartCoroutine(this, WaveTest());
        }
    }

    IEnumerator WaveTest()
    {
        while (true)
        {
            var currentWave = _waves[_currentWave].sequence;

            foreach (var obj in currentWave)
            {
                Instantiate(obj, _startPos.transform);
                yield return new WaitForSeconds(1.0f);
            }

            yield return new WaitForSeconds(5.0f);

            _currentWave++;

            if (_currentWave == _waves.Count)
            {
                Debug.Log("WAVE TEST FINISHED!");
                break;
            }
        }
    } 
}

        /*
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

        */
