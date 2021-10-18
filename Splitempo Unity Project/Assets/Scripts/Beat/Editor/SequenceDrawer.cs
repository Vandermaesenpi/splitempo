using UnityEngine;
using UnityEditor;

// IngredientDrawer
[CustomPropertyDrawer(typeof(Sequence))]
public class SequenceDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects

        var notes = property.FindPropertyRelative("notes");

        for (int i = 0; i < notes.arraySize; i++)
        {
            Rect noteRect = new Rect(position.x + 20 * i, position.y, 20, position.height);    
            SerializedProperty note = notes.GetArrayElementAtIndex(i);
            note.boolValue = EditorGUI.Toggle(noteRect, note.boolValue);
        }

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}