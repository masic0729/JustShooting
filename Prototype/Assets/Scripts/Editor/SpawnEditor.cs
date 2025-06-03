using UnityEditor;
using UnityEngine;

// SpawnData 클래스에 커스텀 인스펙터를 적용하기 위한 에디터 클래스
[CustomEditor(typeof(SpawnData))]
public class SpawnEditor : Editor
{
    // SpawnData 내의 waveGroups 리스트를 SerializedProperty로 참조
    private SerializedProperty waveGroups;

    // 에디터가 활성화될 때 SerializedProperty 초기화
    private void OnEnable()
    {
        waveGroups = serializedObject.FindProperty("waveGroups");
    }

    // 커스텀 인스펙터 UI 정의
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("Wave Groups", EditorStyles.boldLabel);

        // 각 WaveGroup 단위로 반복 처리
        for (int i = 0; i < waveGroups.arraySize; i++)
        {
            SerializedProperty group = waveGroups.GetArrayElementAtIndex(i);
            SerializedProperty nextDelay = group.FindPropertyRelative("nextWaveDelay");
            SerializedProperty waveList = group.FindPropertyRelative("wavesInGroup");

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Group {i + 1}", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(nextDelay);

            // Group 내의 각 SpawnInfomation 처리
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

                // spawnCount에 따라 배열 크기를 조절
                int count = Mathf.Max(1, spawnCount.intValue);
                ResizeArray(spawnX, count);
                ResizeArray(arrivePos, count);

                // 커스텀 위치 지정일 경우, X/Y 입력 필드 제공
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

            // Group 내 Spawn 정보 추가/삭제 버튼
            if (GUILayout.Button("Add Spawn to Group"))
                waveList.InsertArrayElementAtIndex(waveList.arraySize);
            if (waveList.arraySize > 0 && GUILayout.Button("Delete Last Spawn from Group"))
                waveList.DeleteArrayElementAtIndex(waveList.arraySize - 1);

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }

        // WaveGroup 자체 추가/삭제 버튼
        if (GUILayout.Button("Add new Wave Group"))
            waveGroups.InsertArrayElementAtIndex(waveGroups.arraySize);

        if (waveGroups.arraySize > 0 && GUILayout.Button("Delete Last Wave Group"))
            waveGroups.DeleteArrayElementAtIndex(waveGroups.arraySize - 1);

        serializedObject.ApplyModifiedProperties();
    }

    // 배열 크기 자동 조절 메서드
    void ResizeArray(SerializedProperty array, int size)
    {
        while (array.arraySize < size)
            array.InsertArrayElementAtIndex(array.arraySize);
        while (array.arraySize > size)
            array.DeleteArrayElementAtIndex(array.arraySize - 1);
    }
}
