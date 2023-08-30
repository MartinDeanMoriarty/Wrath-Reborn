using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class MuseHelper : EditorWindow
{
    public Texture aTexture;

    [@MenuItem("Muse/Muse")]
    static void Init()
    {
        EditorWindow window = EditorWindow.GetWindowWithRect(typeof(MuseHelper), new Rect(0, 0, 250, 150));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();


        if (!aTexture)
        {
            Debug.LogError("Assign a Texture in the inspector.");
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, 250, 140), aTexture);


        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Open!"))
        {
            
                    Help.BrowseURL("https://muse.unity.com/");
                
           
        }
    }
}
