using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CanEditMultipleObjects]
[CustomEditor(typeof(tk2dUIBaseItemControl))]
public class tk2dUIBaseItemControlEditor : Editor
{
    protected bool hasBtnCheckBeenDone = false;
    public override void OnInspectorGUI()
    {
        EditorGUIUtility.LookLikeInspector();
        tk2dUIBaseItemControl baseButtonControl = (tk2dUIBaseItemControl)target;

        baseButtonControl.uiItem = tk2dUICustomEditorGUILayout.SceneObjectField("UIItem", baseButtonControl.uiItem,target);

        if (baseButtonControl.uiItem == null)
        {
            if (!hasBtnCheckBeenDone)
            {
                hasBtnCheckBeenDone = true;
                baseButtonControl.uiItem = tk2dUIItemEditor.FindAppropriateButtonInHierarchy(baseButtonControl.gameObject);
                GUI.changed = true;
            }
        }
        else if (hasBtnCheckBeenDone)
        {
            hasBtnCheckBeenDone = false;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(baseButtonControl);
        }
    }
}
