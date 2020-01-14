/*
 Personal Log.
Этот хинт может пригодится для отладки AI, по крайней мере в моём случае это было так.
Смысл хинта совсем прост — просто чтобы не рыть огромный общий лог заводим каждому юниту персональный, в нашем случае это строка (string localLog).
Все важные события из жизни монстра пишем туда, а для просмотра надо просто выделить монстра в редакторе.
Код для того, чтобы отображался персональный лог в инспекторе: об
*/
using UnityEngine;
using System.Collections;
using UI;
using UnityEditor;

[CustomEditor(typeof(LoadScene))]
public class MonsterEditor : Editor {
    Vector2 scrollPos = new Vector2(0, Mathf.Infinity);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        LoadScene monster = (LoadScene)target;

        if (Application.isPlaying) {
            scrollPos = GUILayout.BeginScrollView (
                scrollPos, GUILayout.Height (250));
			
            //GUILayout.Label (LoadScene.LocalLog);
			
            GUILayout.EndScrollView ();

            if (GUILayout.Button("Clear"))
            {
                //LoadScene.LocalLog = "";
            }
                
        }

        serializedObject.ApplyModifiedProperties();
        DrawDefaultInspector();
    }
}