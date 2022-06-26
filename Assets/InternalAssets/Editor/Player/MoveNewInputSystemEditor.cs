using InternalAssets.Scripts.Characters.Hero;
using UnityEditor;

[CustomEditor(typeof(MoveNewInputSystem))]
public class MoveNewInputSystemEditor : Editor
{
    /*bool isUp = true;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.BeginHorizontal("box");
        GUILayout.BeginArea(new Rect(16,24,80,30));
        if (GUILayout.Button ("123", GUILayout.Width (80))) {

        }
        GUILayout.EndArea();
            GUILayout.FlexibleSpace();
            if (GUILayout.Toggle(isUp, "Up"))
            {
                isUp = !isUp;
                Debug.Log("123");
            }
            GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Toggle(false ,"Left");
            GUILayout.FlexibleSpace();
            GUILayout.Toggle(false ,"Right");
            GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Toggle(false ,"Down");
            GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

    }*/

}