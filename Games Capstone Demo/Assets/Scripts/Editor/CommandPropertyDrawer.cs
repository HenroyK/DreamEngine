using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(Command))]
public class CommandPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        // draw label, it returns modified rect
        rect = EditorGUI.PrefixLabel(rect, new GUIContent("Command:"));

        // reset indentation
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // New rect for next element
        var commandRect = new Rect(rect.x-70, rect.y, 80, rect.height);

        // draw command selector field using command rect
        EditorGUI.PropertyField(commandRect, property.FindPropertyRelative("commandType"), GUIContent.none);
        //if command type is spawn
        if (property.FindPropertyRelative("commandType").enumValueIndex == 1)
        {
            // New rect for next element
            var spawnRect = new Rect(rect.x + 20, rect.y, 80, rect.height);
            EditorGUI.PropertyField(spawnRect, property.FindPropertyRelative("spawnObject"), GUIContent.none);

            // New rect for next element
            var vectorRect = new Rect(rect.x + 110, rect.y, 130, rect.height);
            EditorGUI.PropertyField(vectorRect, property.FindPropertyRelative("vector3"), GUIContent.none);
        }
        //if command type is wait
        if (property.FindPropertyRelative("commandType").enumValueIndex == 2)
        {
            // New rect for next element
            var timeRect = new Rect(rect.x + 20, rect.y, 80, rect.height);
            EditorGUI.PropertyField(timeRect, property.FindPropertyRelative("time"), GUIContent.none);
        }
        //if command type is speed
        if (property.FindPropertyRelative("commandType").enumValueIndex == 3)
        {
            // New rect for next element
            var timeRect = new Rect(rect.x + 20, rect.y, 80, rect.height);
            EditorGUI.PropertyField(timeRect, property.FindPropertyRelative("speed"), GUIContent.none);
        }

        // indent back to where it was
        EditorGUI.indentLevel = indent;
    }
}
    