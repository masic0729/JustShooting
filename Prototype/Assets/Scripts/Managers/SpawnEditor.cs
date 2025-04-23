using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnData))]
public class SpawnEditor : Editor
{
    private SerializedProperty spawnDataList;

    private void OnEnable()
    {
        spawnDataList = serializedObject.FindProperty("spawnDataList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Spawn Data List", EditorStyles.boldLabel);

        for (int i = 0; i < spawnDataList.arraySize; i++)
        {
            SerializedProperty element = spawnDataList.GetArrayElementAtIndex(i);

            SerializedProperty enemyData = element.FindPropertyRelative("enemyData");
            SerializedProperty spawnEnemyCount = element.FindPropertyRelative("spawnEnemyCount");
            SerializedProperty waveDelay = element.FindPropertyRelative("waveDelay");
            SerializedProperty spawnDelay = element.FindPropertyRelative("spawnDelay");
            SerializedProperty isCustomPosition = element.FindPropertyRelative("isCustomPosition");
            SerializedProperty spawnXArray = element.FindPropertyRelative("spawnX_Value");
            SerializedProperty arrivePos = element.FindPropertyRelative("ArrivePosition");

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.PropertyField(enemyData);
            EditorGUILayout.PropertyField(waveDelay);
            EditorGUILayout.PropertyField(spawnDelay);
            EditorGUILayout.PropertyField(isCustomPosition);

            // 사전에 배열 동기화만 따로 수행
            int count = Mathf.Max(1, spawnEnemyCount.intValue); // 최소 1 이상 보장
            ResizeArray(spawnXArray, count);
            ResizeArray(arrivePos, count);

            EditorGUILayout.PropertyField(spawnEnemyCount);

            // 수동 배열 그리기
            if (isCustomPosition.boolValue)
            {
                EditorGUILayout.LabelField("Custom Spawn Positions");

                for (int j = 0; j < count; j++)
                {
                    SerializedProperty vecElement = arrivePos.GetArrayElementAtIndex(j);
                    SerializedProperty xOffset = spawnXArray.GetArrayElementAtIndex(j);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    Vector2 newVec = EditorGUILayout.Vector2Field($"Pos[{j}]", vecElement.vector2Value);
                    if (EditorGUI.EndChangeCheck())
                        vecElement.vector2Value = newVec;

                    EditorGUILayout.PropertyField(xOffset, GUIContent.none);
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }

        if (GUILayout.Button("Add new Spawn Data"))
        {
            spawnDataList.InsertArrayElementAtIndex(spawnDataList.arraySize);
        }
        if (GUILayout.Button("Delete Last Spawn Data"))
        {
            if (spawnDataList.arraySize > 0)
                spawnDataList.DeleteArrayElementAtIndex(spawnDataList.arraySize - 1);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ResizeArray(SerializedProperty arrayProp, int newSize)
    {
        while (arrayProp.arraySize < newSize)
        {
            arrayProp.InsertArrayElementAtIndex(arrayProp.arraySize);
            var newElement = arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1);

            if (newElement.propertyType == SerializedPropertyType.Float)
                newElement.floatValue = 0f;
            else if (newElement.propertyType == SerializedPropertyType.Vector2)
                newElement.vector2Value = Vector2.zero;
        }

        while (arrayProp.arraySize > newSize)
        {
            arrayProp.DeleteArrayElementAtIndex(arrayProp.arraySize - 1);
        }
    }
}
