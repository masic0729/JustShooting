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
            SerializedProperty nextWaveDelay = element.FindPropertyRelative("nextWaveDelay");
            SerializedProperty spawnDelay = element.FindPropertyRelative("spawnDelay");
            SerializedProperty isCustomPosition = element.FindPropertyRelative("isCustomPosition");
            SerializedProperty spawnXArray = element.FindPropertyRelative("spawnX_Value");
            SerializedProperty arrivePos = element.FindPropertyRelative("ArrivePosition");

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.PropertyField(enemyData);
            EditorGUILayout.PropertyField(nextWaveDelay);
            EditorGUILayout.PropertyField(spawnDelay);
            EditorGUILayout.PropertyField(isCustomPosition);

            // Synchronize array sizes
            int count = Mathf.Max(1, spawnEnemyCount.intValue); // Ensure at least 1 element
            ResizeArray(spawnXArray, count);
            ResizeArray(arrivePos, count);

            EditorGUILayout.PropertyField(spawnEnemyCount);

            SerializedProperty isRandY = element.FindPropertyRelative("isRandPositionY");
            EditorGUILayout.PropertyField(isRandY);
            // Draw positions if custom position is enabled
            if (isCustomPosition.boolValue)
            {
                EditorGUILayout.LabelField("Custom Spawn Positions");

                for (int j = 0; j < count; j++)
                {
                    SerializedProperty vecElement = arrivePos.GetArrayElementAtIndex(j);
                    SerializedProperty xOffset = spawnXArray.GetArrayElementAtIndex(j);

                    EditorGUILayout.BeginHorizontal();

                    SerializedProperty xValue = vecElement.FindPropertyRelative("x");
                    SerializedProperty yValue = vecElement.FindPropertyRelative("y");

                    // X 입력은 항상 가능
                    EditorGUI.BeginChangeCheck();
                    float newX = EditorGUILayout.FloatField($"Pos[{j}] X", xValue.floatValue);
                    if (EditorGUI.EndChangeCheck()) xValue.floatValue = newX;

                    // Y 입력은 isRandPositionY가 false일 때만 가능
                    if (!isRandY.boolValue)
                    {
                        EditorGUI.BeginChangeCheck();
                        float newY = EditorGUILayout.FloatField($"Y", yValue.floatValue);
                        if (EditorGUI.EndChangeCheck()) yValue.floatValue = newY;
                    }
                    else
                    {
                        EditorGUILayout.LabelField("Y = 랜덤", GUILayout.Width(80));
                    }

                    EditorGUILayout.PropertyField(xOffset, GUIContent.none);
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }

        // Buttons to add or remove spawn data
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
