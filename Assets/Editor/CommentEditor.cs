using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Comment))]
public class CommentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Reference to the target object (the component)
        Comment commentComponent = (Comment)target;

        // Draw a multi-line text field for the comment
        commentComponent.commentString = EditorGUILayout.TextArea(commentComponent.commentString, GUILayout.Height(100));

        // Save changes to the component
        if (GUI.changed)
        {
            EditorUtility.SetDirty(commentComponent);
        }
    }
}