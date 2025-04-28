/*using Unity.VisualScripting;
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
*/
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

            // Draw positions if custom position is enabled
            if (isCustomPosition.boolValue)
            {
                EditorGUILayout.LabelField("Custom Spawn Positions");

                for (int j = 0; j < count; j++)
                {
                    SerializedProperty vecElement = arrivePos.GetArrayElementAtIndex(j);
                    SerializedProperty xOffset = spawnXArray.GetArrayElementAtIndex(j);

                    // Instead of Vector2Field, manually handle X and Y values
                    EditorGUILayout.BeginHorizontal();
                    SerializedProperty xValue = vecElement.FindPropertyRelative("x");
                    SerializedProperty yValue = vecElement.FindPropertyRelative("y");

                    EditorGUI.BeginChangeCheck();
                    float newX = EditorGUILayout.FloatField($"Pos[{j}] X", xValue.floatValue);
                    if (EditorGUI.EndChangeCheck())
                        xValue.floatValue = newX;

                    EditorGUI.BeginChangeCheck();
                    float newY = EditorGUILayout.FloatField($"Pos[{j}] Y", yValue.floatValue);
                    if (EditorGUI.EndChangeCheck())
                        yValue.floatValue = newY;

                    // Make sure xOffset is editable
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
