using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnData))]
public class SpawnEditor : Editor
{
    public SerializedProperty spawnDataList;

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

            /*SerializedProperty spawnXArray = element.FindPropertyRelative("spawnX_Value");
            SerializedProperty spawnYArray = element.FindPropertyRelative("spawnY_Value");
            SerializedProperty arrivePos = element.FindPropertyRelative("testVec");*/
            SerializedProperty spawnXArray = element.FindPropertyRelative("spawnX_Value");
            SerializedProperty arrivePos = element.FindPropertyRelative("ArrivePosition");

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.PropertyField(enemyData);

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(spawnEnemyCount);
            if (EditorGUI.EndChangeCheck())
            {
                int newCount = spawnEnemyCount.intValue;

                // 배열 길이 동기화
                /*ResizeArray(spawnXArray, newCount);
                ResizeArray(spawnYArray, newCount);
                ResizeArray(arrivePos, newCount);*/

                ResizeArray(arrivePos, newCount);
                ResizeArray(spawnXArray, newCount);
            }

            EditorGUILayout.PropertyField(waveDelay);
            EditorGUILayout.PropertyField(spawnDelay);
            EditorGUILayout.PropertyField(isCustomPosition);

            /*if (isCustomPosition.boolValue)
            {
                EditorGUILayout.LabelField("Custom Spawn Positions");
                for (int j = 0; j < spawnXArray.arraySize; j++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(spawnXArray.GetArrayElementAtIndex(j), new GUIContent($"X[{j}]"));
                    EditorGUILayout.PropertyField(spawnYArray.GetArrayElementAtIndex(j), new GUIContent($"Y[{j}]"));
                    EditorGUILayout.PropertyField(arrivePos.GetArrayElementAtIndex(j), new GUIContent($"Y[{j}]"));
                    EditorGUILayout.EndHorizontal();
                }
            }*/
            if (isCustomPosition.boolValue)
            {
                int count = spawnEnemyCount.intValue;
                ResizeArray(spawnXArray, count);
                ResizeArray(arrivePos, count);

                EditorGUILayout.LabelField("Custom Spawn Positions");

                /*for (int j = 0; j < spawnXArray.arraySize; j++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(arrivePos.GetArrayElementAtIndex(j), new GUIContent($"Vec[{j}]"));
                    EditorGUILayout.PropertyField(spawnXArray.GetArrayElementAtIndex(j), new GUIContent($"X[{j}]"));
                    EditorGUILayout.EndHorizontal();
                }*/
                for (int j = 0; j < arrivePos.arraySize; j++)
                {
                    SerializedProperty vecElement = arrivePos.GetArrayElementAtIndex(j);

                    EditorGUI.BeginChangeCheck();
                    EditorGUI.BeginProperty(EditorGUILayout.GetControlRect(), new GUIContent($"Vec[{j}]"), vecElement);

                    // 강제로 Vector2Field 그리기
                    Vector2 newVec = EditorGUILayout.Vector2Field($"arriveVec[{j}]", vecElement.vector2Value);

                    if (EditorGUI.EndChangeCheck())
                    {
                        vecElement.vector2Value = newVec;
                    }

                    EditorGUI.EndProperty();

                    // 함께 보여줄 X 배열 필드
                    //EditorGUILayout.PropertyField(spawnXArray.GetArrayElementAtIndex(j), new GUIContent($"moveX_Distance[{j}]"));
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
            spawnDataList.DeleteArrayElementAtIndex(spawnDataList.arraySize - 1);
        }

        serializedObject.ApplyModifiedProperties();
    }

    /*private void ResizeArray(SerializedProperty arrayProp, int newSize)
    {
        while (arrayProp.arraySize < newSize)
        {
            arrayProp.InsertArrayElementAtIndex(arrayProp.arraySize);
        }
        while (arrayProp.arraySize > newSize)
        {
            arrayProp.DeleteArrayElementAtIndex(arrayProp.arraySize - 1);
        }
    }*/

    private void ResizeArray(SerializedProperty arrayProp, int newSize)
    {
        while (arrayProp.arraySize < newSize)
        {
            arrayProp.InsertArrayElementAtIndex(arrayProp.arraySize);
            SerializedProperty newElement = arrayProp.GetArrayElementAtIndex(arrayProp.arraySize - 1);

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
