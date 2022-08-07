using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace kl
{
    [CustomEditor(typeof(CharacterControl))]
    public class MaterialChanger : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            CharacterControl control = (CharacterControl)target;

            if(GUILayout.Button("Change Material"))
            {
                control.ChangeMaterial();
            }
        }
    }
}
