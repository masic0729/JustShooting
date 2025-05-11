using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnData))]
public class SpawnEditor : Editor
{
    private SerializedProperty waveGroups;

    private void OnEnable()
    {
        waveGroups = serializedObject.FindProperty("waveGroups");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("Wave Groups", EditorStyles.boldLabel);

        for (int i = 0; i < waveGroups.arraySize; i++)
        {
            SerializedProperty group = waveGroups.GetArrayElementAtIndex(i);
            SerializedProperty nextDelay = group.FindPropertyRelative("nextWaveDelay");
            SerializedProperty waveList = group.FindPropertyRelative("wavesInGroup");

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Group {i + 1}", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(nextDelay);

            for (int j = 0; j < waveList.arraySize; j++)
            {
                SerializedProperty wave = waveList.GetArrayElementAtIndex(j);
                SerializedProperty enemyData = wave.FindPropertyRelative("enemyData");
                SerializedProperty spawnCount = wave.FindPropertyRelative("spawnEnemyCount");
                SerializedProperty spawnDelay = wave.FindPropertyRelative("spawnDelay");
                SerializedProperty isCustom = wave.FindPropertyRelative("isCustomPosition");
                SerializedProperty isRandY = wave.FindPropertyRelative("isRandPositionY");
                SerializedProperty spawnX = wave.FindPropertyRelative("spawnX_Value");
                SerializedProperty arrivePos = wave.FindPropertyRelative("ArrivePosition");

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.PropertyField(enemyData);
                EditorGUILayout.PropertyField(spawnCount);
                EditorGUILayout.PropertyField(spawnDelay);
                EditorGUILayout.PropertyField(isCustom);
                EditorGUILayout.PropertyField(isRandY);

                // 배열 동기화
                int count = Mathf.Max(1, spawnCount.intValue);
                ResizeArray(spawnX, count);
                ResizeArray(arrivePos, count);

                if (isCustom.boolValue)
                {
                    EditorGUILayout.LabelField("Custom Positions");
                    for (int k = 0; k < count; k++)
                    {
                        SerializedProperty vec = arrivePos.GetArrayElementAtIndex(k);
                        SerializedProperty x = vec.FindPropertyRelative("x");
                        SerializedProperty y = vec.FindPropertyRelative("y");

                        EditorGUILayout.BeginHorizontal();
                        x.floatValue = EditorGUILayout.FloatField($"X [{k}]", x.floatValue);
                        if (!isRandY.boolValue)
                            y.floatValue = EditorGUILayout.FloatField("Y", y.floatValue);
                        else
                            EditorGUILayout.LabelField("Y = 랜덤", GUILayout.Width(70));
                        EditorGUILayout.EndHorizontal();
                    }
                }

                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("Add Spawn to Group"))
                waveList.InsertArrayElementAtIndex(waveList.arraySize);
            if (waveList.arraySize > 0 && GUILayout.Button("Delete Last Spawn from Group"))
                waveList.DeleteArrayElementAtIndex(waveList.arraySize - 1);

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }

        if (GUILayout.Button("Add new Wave Group"))
            waveGroups.InsertArrayElementAtIndex(waveGroups.arraySize);

        if (waveGroups.arraySize > 0 && GUILayout.Button("Delete Last Wave Group"))
            waveGroups.DeleteArrayElementAtIndex(waveGroups.arraySize - 1);

        serializedObject.ApplyModifiedProperties();
    }

    void ResizeArray(SerializedProperty array, int size)
    {
        while (array.arraySize < size)
            array.InsertArrayElementAtIndex(array.arraySize);
        while (array.arraySize > size)
            array.DeleteArrayElementAtIndex(array.arraySize - 1);
    }
}
