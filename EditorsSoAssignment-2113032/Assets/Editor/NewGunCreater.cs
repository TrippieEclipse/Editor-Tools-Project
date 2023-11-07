using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


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



public class NewGunCreater : EditorWindow
{
    public CreatorSettings settingsWind = null;

    public BodyPartsEnum   bodyEnumChoice;
    public BarrelPartsEnum barrelEnumChoice;
    public HandlePartsEnum handleEnumChoice;


    public List<GameObject> bodyList = new List<GameObject>();
    public List<GameObject> barrelList = new List<GameObject>();
    public List<GameObject> handleList = new List<GameObject>();


    public string weaponName = "";

    

    [MenuItem("My Tools/Main Windows/Weapon Maker")]
    public static void ShowMyWindow()
    {
        NewGunCreater wnd = GetWindow<NewGunCreater>();


        

        wnd.titleContent = new GUIContent("WeaponSmith");

        wnd.minSize = new Vector2(500, 500);
        wnd.maxSize = new Vector2(1920, 720);
    }



    private void OnGUI()
    {
        #region Weapon Part Selection

        

        GUILayout.Label("Weapon Name");
        weaponName = EditorGUILayout.TextField("", weaponName);

        EditorGUILayout.Space();

        bodyEnumChoice = (BodyPartsEnum)EditorGUILayout.EnumPopup(new GUIContent("Choose Body Part -->","Select The Body You Would Like For Your Weapon"), bodyEnumChoice, GUILayout.MinWidth(250),GUILayout.MaxWidth(300));

        EditorGUILayout.Space();

        barrelEnumChoice = (BarrelPartsEnum)EditorGUILayout.EnumPopup(new GUIContent("Choose Barrel Part -->", "Select The Barrel You Would Like For Your Weapon"), barrelEnumChoice, GUILayout.MinWidth(250), GUILayout.MaxWidth(300));

        EditorGUILayout.Space();

        handleEnumChoice = (HandlePartsEnum)EditorGUILayout.EnumPopup(new GUIContent("Choose Handle Part -->", "Select The Handle You Would Like For Your Weapon"), handleEnumChoice, GUILayout.MinWidth(250), GUILayout.MaxWidth(300));


        if (GUILayout.Button("Create"))
        {
            CreateWeaponSO();
        }



        EditorGUILayout.Space(40);

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
