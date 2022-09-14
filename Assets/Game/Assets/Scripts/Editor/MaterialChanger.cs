using UnityEngine;
using UnityEditor;

namespace kl
{
    [CustomEditor(typeof(ChangeSkin))]
    public class MaterialChanger : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ChangeSkin control = (ChangeSkin)target;

            if(GUILayout.Button("Change Material"))
            {
                control.ChangeMaterial();
            }
        }
    }
}
