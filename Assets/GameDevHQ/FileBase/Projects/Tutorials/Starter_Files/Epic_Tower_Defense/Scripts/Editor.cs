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
    private GameObject testWave;

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

        if (GUILayout.Button("Clear Wave"))
        {
            EditorCoroutines.EditorCoroutineExtensions.StopCoroutine(this, WaveTest());
            Destroy(testWave);
        }
    }

    IEnumerator WaveTest()
    {
        while (true)
        {
            var currentWave = _waves[_currentWave].sequence;
            testWave = new GameObject("TestWave");
            testWave.transform.position = _startPos.transform.position;

            foreach (var obj in currentWave)
            {
                Instantiate(obj, testWave.transform);
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