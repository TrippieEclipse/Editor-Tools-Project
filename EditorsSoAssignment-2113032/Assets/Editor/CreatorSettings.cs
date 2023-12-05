using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
public class CreatorSettings : EditorWindow
{
    public static event Action<CreatorSettings> ActiveCreatorCall;

    public NewGunCreater ngc;
    public bool ngcNull = true;
    public static Vector2 lastPos = Vector2.zero;

    [MenuItem("My Tools/Main Windows/Creator Settings")]
    public static void Init()
    {
        CreatorSettings wnd = GetWindow<CreatorSettings>();
        wnd.minSize = new Vector2(500, 500);
        wnd.maxSize = new Vector2(1920, 720);

        Rect currentPos = wnd.position;
        currentPos.position = lastPos;
    }

    private void Update()
    {
        if (ngc == null)
        {
            ngcNull = true;
        }
        else
        {
            ngcNull = false;
        }
    }

    private void OnGUI()
    {
        if (!ngcNull)
        {
            ngc.settingsWind = this;
            #region Display Lists
            #region Body List
            ScriptableObject targetBody = ngc;
            SerializedObject soBody = new SerializedObject(targetBody);
            SerializedProperty stringsPropertyBody = soBody.FindProperty("bodyList");

            EditorGUILayout.PropertyField(stringsPropertyBody, true, GUILayout.MinWidth(300), GUILayout.MaxWidth(700));
            soBody.ApplyModifiedProperties();

            if (GUILayout.Button("Add To Body List", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))
            {
                foreach (GameObject obj in Selection.gameObjects)
                {
                    if (!ngc.bodyList.Contains(obj) && obj.CompareTag("Body"))
                    {
                        ngc.bodyList.Add(obj);
                    }
                    else if (ngc.bodyList.Contains(obj))
                    {
                        Debug.Log("Asset Already Added");
                    }
                    else if (!obj.CompareTag("Body"))
                    {
                        Debug.Log("Wrong Asset Type");
                    }
                }
            }

            if (GUILayout.Button("ClearBodylist", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))
            {
                ngc.bodyList.Clear();
            }

            EditorGUILayout.Space(10);
            #endregion

            #region Barrel List
            ScriptableObject targetBarrel = ngc;
            SerializedObject soBarrel = new SerializedObject(targetBarrel);
            SerializedProperty stringsPropertyBarrel = soBarrel.FindProperty("barrelList");

            EditorGUILayout.PropertyField(stringsPropertyBarrel, true, GUILayout.MinWidth(300), GUILayout.MaxWidth(700));
            soBarrel.ApplyModifiedProperties();

            #region Buttons For Barrel
            if (GUILayout.Button("Add To Barrel List", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))
            {
                foreach (GameObject obj in Selection.gameObjects)
                {
                    if (!ngc.barrelList.Contains(obj) && obj.CompareTag("Barrel"))
                    {
                        ngc.barrelList.Add(obj);
                    }
                    else if (ngc.barrelList.Contains(obj))
                    {
                        Debug.Log("Asset Already Added");
                    }
                    else if (!obj.CompareTag("Barrel"))
                    {
                        Debug.Log("Wrong Asset Type");
                    }
                }
            }
            if (GUILayout.Button("Clear Barrel list", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))
            {
                ngc.barrelList.Clear();
            }

            EditorGUILayout.Space(5);
            #endregion

            #endregion

            EditorGUILayout.Space(5);

            #region Handle List
            ScriptableObject targetHandle = ngc;
            SerializedObject soHandle = new SerializedObject(targetHandle);
            SerializedProperty stringsPropertyHandle = soHandle.FindProperty("handleList");

            EditorGUILayout.PropertyField(stringsPropertyHandle, true, GUILayout.MinWidth(300), GUILayout.MaxWidth(700));
            soHandle.ApplyModifiedProperties();

            #region Buttons For Handle

            if (GUILayout.Button("Add To Handle List", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))
            {

                foreach (GameObject obj in Selection.gameObjects)
                {
                    if (!ngc.handleList.Contains(obj) && obj.CompareTag("Handle"))
                    {
                        ngc.handleList.Add(obj);
                    }
                    else if (ngc.handleList.Contains(obj))
                    {
                        Debug.Log("Asset Already Added");
                    }
                    else if (!obj.CompareTag("Handle"))
                    {
                        Debug.Log("Wrong Asset Type");
                    }


                }
            }


            if (GUILayout.Button("Clear Handle list", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))
            {
                ngc.handleList.Clear();
            }

            EditorGUILayout.Space(5);
            #endregion

            #endregion

            EditorGUILayout.Space(5);

            #region Scope List
            ScriptableObject targetScope = ngc;
            SerializedObject soScope = new SerializedObject(targetScope);
            SerializedProperty stringsPropertyScope = soScope.FindProperty("scopeList");

            EditorGUILayout.PropertyField(stringsPropertyScope, true, GUILayout.MinWidth(300), GUILayout.MaxWidth(700));
            soScope.ApplyModifiedProperties();

            #region Buttons For Handle
            if (GUILayout.Button("Add To Scope List", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))
            {
                foreach (GameObject obj in Selection.gameObjects)
                {
                    if (!ngc.scopeList.Contains(obj) && obj.CompareTag("Scope"))
                    {
                        ngc.scopeList.Add(obj);
                    }
                    else if (ngc.scopeList.Contains(obj))
                    {
                        Debug.Log("Asset Already Added");
                    }
                    else if (!obj.CompareTag("Scope"))
                    {
                        Debug.Log("Wrong Asset Type");
                    }
                }
            }

            if (GUILayout.Button("Clear Scope list", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))
            {
                ngc.scopeList.Clear();
            }

            EditorGUILayout.Space(10);
            #endregion
            #endregion
            #endregion
        }
        else
        { 
            EditorGUILayout.HelpBox("To access the creator settings you must open the Gun Creator First. Once the Gun Creator is open, press the button underneath to pair the windows", MessageType.Warning);

            if (GUILayout.Button("Pair Windows"))
            {
                CalltoCreatorWindow(this);
            }
        }
    }

    public void CalltoCreatorWindow(CreatorSettings csPass)
    {
        ActiveCreatorCall?.Invoke(csPass);
    }

    public void CreatorWindowReturn(NewGunCreater ngcPass)
    {
        ngc = ngcPass;
    }

}
