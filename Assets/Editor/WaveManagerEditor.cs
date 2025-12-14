#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveManager))]
public class WaveManagerEditor : Editor
{
    private WaveManager waveManager;
    private bool showDebugTools = false;
    private EnemyProfile selectedEnemyProfile;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        waveManager = (WaveManager)target;

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Custom Wave Tools", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox($"Enemies Remaining: {waveManager.enemiesRemaining}", MessageType.Info);

        if (GUILayout.Button("Start Next Wave", GUILayout.Height(30)))
        {
            waveManager.StartNextWave();
        }

        if (GUILayout.Button("Reset Waves", GUILayout.Height(25)))
        {
            waveManager.currentWaveIndex = 0;
            waveManager.enemiesRemaining = 0;
            Debug.Log("Waves reset to beginning");
        }

        EditorGUILayout.Space(10);

        showDebugTools = EditorGUILayout.Foldout(showDebugTools, "Debug Tools");

        if (showDebugTools)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Quick Enemy Spawn", EditorStyles.miniBoldLabel);

            selectedEnemyProfile = (EnemyProfile)EditorGUILayout.ObjectField(
                "Enemy Profile",
                selectedEnemyProfile,
                typeof(EnemyProfile),
                false
            );

            EditorGUI.BeginDisabledGroup(selectedEnemyProfile == null);
            if (GUILayout.Button("Spawn Test Enemy"))
            {
                waveManager.TestSpawnSingleEnemy(selectedEnemyProfile);
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.HelpBox(
            "Use these tools to test waves without playing the full game.",
            MessageType.None
        );
    }
}
#endif