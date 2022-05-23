using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(Command))]
public class CommandPropertyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 34.0f;
    }
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
            var spawnRect = new Rect(rect.x + 20, rect.y, 80, rect.height / 2);
            EditorGUI.PropertyField(spawnRect, property.FindPropertyRelative("spawnObject"), GUIContent.none);

            // New rect for next element
            var vectorRect = new Rect(rect.x + 110, rect.y, 130, rect.height / 2);
            EditorGUI.PropertyField(vectorRect, property.FindPropertyRelative("vector3"), GUIContent.none);
        }
        //if command type is wait
        //if (property.FindPropertyRelative("commandType").enumValueIndex == 2)
        //{
        //    // New rect for next element
        //    var timeRect = new Rect(rect.x + 20, rect.y, 80, rect.height);
        //    EditorGUI.PropertyField(timeRect, property.FindPropertyRelative("time"), GUIContent.none);
        //}
        //if command type is speed
        if (property.FindPropertyRelative("commandType").enumValueIndex == 2)
        {
            // New rect for next element
            var speedRect = new Rect(rect.x + 20, rect.y, 30, rect.height / 2);
            EditorGUI.PropertyField(speedRect, property.FindPropertyRelative("speed"), GUIContent.none);
        }



        //if command type is audio
        if (property.FindPropertyRelative("commandType").enumValueIndex == 5)
        {
            // New rect for next element
            var audioClipRect = new Rect(rect.x + 20, rect.y, 69, rect.height / 2);
            EditorGUI.PropertyField(audioClipRect, property.FindPropertyRelative("audioClip"), GUIContent.none);

            var audioLabelRect = new Rect(audioClipRect.x + 71, rect.y, 50, rect.height / 2);
            EditorGUI.LabelField(audioLabelRect, "Duration");

            var audioDurationRect = new Rect(audioLabelRect.x + 50, audioLabelRect.y, 25, rect.height / 2);
            EditorGUI.PropertyField(audioDurationRect, property.FindPropertyRelative("audioDuration"), GUIContent.none);

            audioLabelRect = new Rect(audioDurationRect.x + 27, rect.y, 45, rect.height / 2);
            EditorGUI.LabelField(audioLabelRect, "Volume");

            var audioVolumeRect = new Rect(audioLabelRect.x + 47, audioLabelRect.y, 25, rect.height / 2);
            EditorGUI.PropertyField(audioVolumeRect, property.FindPropertyRelative("audioVolume"), GUIContent.none);
        }

        var labelRect = new Rect(80, rect.y + rect.height / 2, 159, rect.height / 2);
        var timeRect = labelRect;
        timeRect.x += labelRect.width + 20;
        timeRect.width = 30;
        EditorGUI.LabelField(labelRect, "Delay before next command");
        EditorGUI.PropertyField(timeRect, property.FindPropertyRelative("time"), GUIContent.none);
        // indent back to where it was
        EditorGUI.indentLevel = indent;
    }
}
    