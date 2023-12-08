using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using PopupWindow = UnityEditor.PopupWindow;

public enum BodyPartsEnum
{
    LongBody,
    ShortBody
}
public enum BarrelPartsEnum
{
    LongBarrel,
    ShortBarrel
}
public enum HandlePartsEnum
{
    LongHandle,
    ShortHandle
}
public enum ScopePartsEnum
{
    CircleScope,
    SquareScope
}



public class NewGunCreater : EditorWindow
{
    public CreatorSettings settingsWind = null;
    public BodyPartsEnum   bodyEnumChoice;
    public BarrelPartsEnum barrelEnumChoice;
    public HandlePartsEnum handleEnumChoice;
    public ScopePartsEnum  scopeEnumChoice;
    public List<GameObject> bodyList = new List<GameObject>();
    public List<GameObject> barrelList = new List<GameObject>();
    public List<GameObject> handleList = new List<GameObject>();
    public List<GameObject> scopeList = new List<GameObject>();
    public List<String> createdWeaponsList = new List<String>();
    public string weaponName = "";
    public bool scopeEnabled;

    [MenuItem("My Tools/Main Windows/Weapon Maker")]
    public static void ShowMyWindow()
    {
        NewGunCreater wnd = GetWindow<NewGunCreater>();
        wnd.titleContent = new GUIContent("WeaponSmith");
        wnd.minSize = new Vector2(500, 500);
        wnd.maxSize = new Vector2(1920, 720);
    }

    /// <summary>
    ///  ONGUI Summary
    ///  
    /// All UI and Main interactions Run through the OnGUI()
    /// </summary>

    private void OnGUI()
    {
        #region Weapon Part Selection
        weaponName = EditorGUILayout.TextField("Weapon Name -->", weaponName, GUILayout.MinWidth(300), GUILayout.MaxWidth(550));
        EditorGUILayout.Space();
        bodyEnumChoice = (BodyPartsEnum)EditorGUILayout.EnumPopup(new GUIContent("Choose Body Part -->","Select The Body You Would Like For Your Weapon"), bodyEnumChoice, GUILayout.MinWidth(300),GUILayout.MaxWidth(550)); //Creates the DropDown For the weapon body selection
        EditorGUILayout.Space();
        barrelEnumChoice = (BarrelPartsEnum)EditorGUILayout.EnumPopup(new GUIContent("Choose Barrel Part -->", "Select The Barrel You Would Like For Your Weapon"), barrelEnumChoice, GUILayout.MinWidth(300), GUILayout.MaxWidth(550)); //Creates the DropDown For the weapon barrel selection
        EditorGUILayout.Space();
        handleEnumChoice = (HandlePartsEnum)EditorGUILayout.EnumPopup(new GUIContent("Choose Handle Part -->", "Select The Handle You Would Like For Your Weapon"), handleEnumChoice, GUILayout.MinWidth(300), GUILayout.MaxWidth(550)); //Creates the DropDown For the weapon handle selection
        EditorGUILayout.Space();
        scopeEnabled = EditorGUILayout.Toggle(new GUIContent("Enable sight on weapon -->", "Select if you want a scope or not"),scopeEnabled, GUILayout.MinWidth(300), GUILayout.MaxWidth(550)); //Allows user to decide if they want a scope using a toggle box

        if (scopeEnabled) //If the scope is enabled then the it Creates the DropDown For the scope body selection
        {
            scopeEnumChoice = (ScopePartsEnum)EditorGUILayout.EnumPopup(new GUIContent("Choose Scope Part -->", "Select The Scope You Would Like For Your Weapon"), scopeEnumChoice, GUILayout.MinWidth(300), GUILayout.MaxWidth(550));
        }

        if (GUILayout.Button("Create", GUILayout.MinWidth(300), GUILayout.MaxWidth(550)))
        {
            bool weaponPartsNotEmpty = ListIsNotEmptyCheck(barrelList, handleList, bodyList, scopeList);
            if (weaponPartsNotEmpty) 
            {
                bool hasWeaponAlreadyBeenCreated = CreatedWeaponListCheck();
                if (!hasWeaponAlreadyBeenCreated)
                {
                    CreateWeaponSO();
                    createdWeaponsList.Add(weaponName);
                }
                else
                {
                    if (EditorUtility.DisplayDialog("WeaponOverride", "There is already a weapon with the name " + weaponName, "Override", "Cancel")) { CreateWeaponSO(); }
                    else { return; }
                }
            }
            else
            {
                if (EditorUtility.DisplayDialog("WeaponListEmpty", "There is a List Missing a gameobject, Make sure all lists in the Creator settings are full with the right objects ", "Okay", "Cancel")) { return; }
                else { return; }
            }
        }
        #endregion
    }
    public void CreateWeaponSO() 
    {
        if(weaponName != null && barrelList.Count > 0) 
        {
            ScriptableWeapon newScriptableObject = ScriptableObject.CreateInstance<ScriptableWeapon>();
            newScriptableObject.weaponName = weaponName;
            newScriptableObject.body = bodyList[(int)bodyEnumChoice];
            newScriptableObject.barrel = barrelList[(int)barrelEnumChoice];
            newScriptableObject.handle = handleList[(int)handleEnumChoice];
            newScriptableObject.scopeEnabled = scopeEnabled;
            newScriptableObject.scope = scopeList[(int)scopeEnumChoice];
            string path = "Assets/WeaponSO/" + weaponName + ".asset";
            UnityEditor.AssetDatabase.CreateAsset(newScriptableObject, path);
        }
        else if (weaponName == null)
        {
            Debug.Log("Please Input Weapon Name");
        }
        else if (barrelList.Count == 0)
        { 
                Debug.Log("No Items in Barrel list");
        }
    }

    bool ListIsNotEmptyCheck(List<GameObject> barrelList,List<GameObject> handleList,List<GameObject> bodyList,List<GameObject> scopeList) 
    {
        foreach(GameObject obj in barrelList) 
        { 
            if(obj == null) 
            {
                return false;
            }
        }
        foreach(GameObject obj in handleList) 
        { 
            if(obj == null) 
            {
                return false;
            }
        }
        foreach(GameObject obj in bodyList) 
        { 
            if(obj == null) 
            {
                return false;
            }
        }
        foreach(GameObject obj in scopeList) 
        { 
            if(obj == null) 
            {
                return false;
            }
        }
        return true;    
    }

    bool CreatedWeaponListCheck()
    {
        for (int i = 0; i < createdWeaponsList.Count; i++)
        {
            if (createdWeaponsList[i] == weaponName) 
            {
                return true;
            }
        }
        return false;
    }
    private void OnEnable()
    {
        CreatorSettings.ActiveCreatorCall += CallToSettings;
        string bodyListData = EditorPrefs.GetString("bodyList", JsonUtility.ToJson(this, false));
        JsonUtility.FromJsonOverwrite(bodyListData, this);
    }
    private void OnDisable()
    {
        if (settingsWind != null) 
        {
            settingsWind.Close();
        }
        string data = JsonUtility.ToJson(this, false);
        EditorPrefs.SetString("bodyList", data);
    }
    public void CallToSettings(CreatorSettings csMainPass) 
    {
        settingsWind = csMainPass;
        settingsWind.ngc = this;
    }
}

