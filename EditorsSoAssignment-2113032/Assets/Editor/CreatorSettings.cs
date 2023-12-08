using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
public class CreatorSettings : EditorWindow
{
    public static event Action<CreatorSettings> ActiveCreatorCall;              //Action event to call to the open NewGunCreator window 
    public NewGunCreater ngc;                                                   //Link to active NewGunCreator instance
    public bool ngcNull = true;                                                 //bool for if the ngc variable is null or not
    public static Vector2 lastPos = Vector2.zero;                               //Gets set to the last undocked position from when it was last open

    [MenuItem("My Tools/Main Windows/Creator Settings")]
    public static void Init()         //Initiates and opens window
    {
        CreatorSettings wnd = GetWindow<CreatorSettings>();
        wnd.minSize = new Vector2(500, 500);
        wnd.maxSize = new Vector2(1920, 720);

        Rect currentPos = wnd.position;
        currentPos.position = lastPos;
    }

    private void Update()
    {
        //When ngc is linked to main window it sets the main bool to true 
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
        if (!ngcNull)   //if the NewGunCreator open window is linked to the current instance
        {
            ngc.settingsWind = this;      //Sets the variable for the Settings window in the current instance of the NewGunCreator window as this current instance of the CreatorSettings
            #region Body List
            ScriptableObject targetBody = ngc;                                                                                                  ///Summary<>
            SerializedObject soBody = new SerializedObject(targetBody);                                                                         ///Seralizes the body list
            SerializedProperty stringsPropertyBody = soBody.FindProperty("bodyList");                                                           ///Then displays the list in a property field
            EditorGUILayout.PropertyField(stringsPropertyBody, true, GUILayout.MinWidth(300), GUILayout.MaxWidth(700));                         ///
            soBody.ApplyModifiedProperties();                                                                                                   ///End<>

            if (GUILayout.Button("Add To Body List", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))     //If the Add To Body List Button is pressed 
            {
                foreach (GameObject obj in Selection.gameObjects)         //Loops through all the gameobjects that the user has selected
                {
                    if (!ngc.bodyList.Contains(obj) && obj.CompareTag("Body"))            //Makes sure it has the tag 'Body' and isn't already added
                    {
                        ngc.bodyList.Add(obj);
                    }
                    else if (ngc.bodyList.Contains(obj))                                  //if the gameobject is already in the list it doesn't add to the list and tells the user it's already there
                    {
                        Debug.Log("Asset Already Added");
                    }
                    else if (!obj.CompareTag("Body"))                                     //if the tag isn't 'Body' is tells the user it's the wrong asset type
                    {
                        Debug.Log("Wrong Asset Type");
                    }
                }
            }

            if (GUILayout.Button("ClearBodylist", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))    //Clears the whole Body List
            {
                ngc.bodyList.Clear();
            }

            EditorGUILayout.Space(10);
            #endregion

            #region Barrel List
            ScriptableObject targetBarrel = ngc;                                                                                                  ///Summary<>
            SerializedObject soBarrel = new SerializedObject(targetBarrel);                                                                       ///Seralizes the Barrel list
            SerializedProperty stringsPropertyBarrel = soBarrel.FindProperty("barrelList");                                                       ///Then displays the list in a property field
            EditorGUILayout.PropertyField(stringsPropertyBarrel, true, GUILayout.MinWidth(300), GUILayout.MaxWidth(700));                         ///
            soBarrel.ApplyModifiedProperties();                                                                                                   ///End<>

            if (GUILayout.Button("Add To Barrel List", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))   //If the Add To Barrel List Button is pressed 
            {
                foreach (GameObject obj in Selection.gameObjects)         //Loops through all the gameobjects that the user has selected
                {
                    if (!ngc.barrelList.Contains(obj) && obj.CompareTag("Barrel"))            //Makes sure it has the tag 'Barrel' and isn't already added
                    {
                        ngc.barrelList.Add(obj);
                    }
                    else if (ngc.barrelList.Contains(obj))                                   //if the gameobject is already in the list it doesn't add to the list and tells the user it's already there
                    {
                        Debug.Log("Asset Already Added");                                   
                    }
                    else if (!obj.CompareTag("Barrel"))                                      //if the tag isn't 'Barrel' is tells the user it's the wrong asset type
                    {
                        Debug.Log("Wrong Asset Type");
                    }
                }
            }
            if (GUILayout.Button("Clear Barrel list", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))    //Clears the whole Barrel List
            {
                ngc.barrelList.Clear();
            }
            EditorGUILayout.Space(10);
            #endregion

            #region Handle List
            ScriptableObject targetHandle = ngc;                                                                                                  ///Summary<>
            SerializedObject soHandle = new SerializedObject(targetHandle);                                                                       ///Seralizes the Handle list
            SerializedProperty stringsPropertyHandle = soHandle.FindProperty("handleList");                                                       ///Then displays the list in a property field
            EditorGUILayout.PropertyField(stringsPropertyHandle, true, GUILayout.MinWidth(300), GUILayout.MaxWidth(700));                         ///
            soHandle.ApplyModifiedProperties();                                                                                                   ///End<>

            if (GUILayout.Button("Add To Handle List", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))   //If the Add To Handle List Button is pressed 
            {
                foreach (GameObject obj in Selection.gameObjects)         //Loops through all the gameobjects that the user has selected
                {
                    if (!ngc.handleList.Contains(obj) && obj.CompareTag("Handle"))            //Makes sure it has the tag 'Handle' and isn't already added
                    {
                        ngc.handleList.Add(obj);
                    }
                    else if (ngc.handleList.Contains(obj))                                   //if the gameobject is already in the list it doesn't add to the list and tells the user it's already there
                    {
                        Debug.Log("Asset Already Added");
                    }
                    else if (!obj.CompareTag("Handle"))                                      //if the tag isn't 'Handle' is tells the user it's the wrong asset type
                    {
                        Debug.Log("Wrong Asset Type");
                    }
                }
            }

            if (GUILayout.Button("Clear Handle list", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))    //Clears the whole Handle List
            {
                ngc.handleList.Clear();
            }

            EditorGUILayout.Space(10);
            #endregion

            #region Scope List
            ScriptableObject targetScope = ngc;                                                                                                  ///Summary<>
            SerializedObject soScope = new SerializedObject(targetScope);                                                                        ///Seralizes the Scope list
            SerializedProperty stringsPropertyScope = soScope.FindProperty("scopeList");                                                         ///Then displays the list in a property field
            EditorGUILayout.PropertyField(stringsPropertyScope, true, GUILayout.MinWidth(300), GUILayout.MaxWidth(700));                         ///
            soScope.ApplyModifiedProperties();                                                                                                   ///End<>

            if (GUILayout.Button("Add To Scope List", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))   //If the Add To Scope List Button is pressed 
            {
                foreach (GameObject obj in Selection.gameObjects)         //Loops through all the gameobjects that the user has selected
                {
                    if (!ngc.scopeList.Contains(obj) && obj.CompareTag("Scope"))            //Makes sure it has the tag 'Scope' and isn't already added
                    {
                        ngc.scopeList.Add(obj);
                    }
                    else if (ngc.scopeList.Contains(obj))                                   //if the gameobject is already in the list it doesn't add to the list and tells the user it's already there
                    {
                        Debug.Log("Asset Already Added");
                    }
                    else if (!obj.CompareTag("Scope"))                                      //if the tag isn't 'Scope' is tells the user it's the wrong asset type
                    {
                        Debug.Log("Wrong Asset Type");
                    }
                }
            }

            if (GUILayout.Button("Clear Scope list", GUILayout.MinWidth(300), GUILayout.MaxWidth(400)))    //Clears the whole Scope List
            {
                ngc.scopeList.Clear();
            }

            EditorGUILayout.Space(10);
            #endregion
            
        }
        else
        { 
            EditorGUILayout.HelpBox("To access the creator settings you must open the Gun Creator First. Once the Gun Creator is open, press the button underneath to pair the windows", MessageType.Warning);   //Displays warning text telling the user to open the main weaponSmith window and pair the windows

            if (GUILayout.Button("Pair Windows"))   //If the Pair Windows button is pressed this instance calls to the open instance for the NewGunCreator class
            {
                CalltoCreatorWindow(this);
            }
        }
    }

    public void CalltoCreatorWindow(CreatorSettings csPass)  //calls to the open instance for the NewGunCreator class
    {
        ActiveCreatorCall?.Invoke(csPass);
    }

    public void CreatorWindowReturn(NewGunCreater ngcPass)  //NewGunCreator can call this function to pair the two windows
    {
        ngc = ngcPass;
    }

}
