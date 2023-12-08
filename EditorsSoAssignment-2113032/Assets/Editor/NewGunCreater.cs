using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using PopupWindow = UnityEditor.PopupWindow;

public enum BodyPartsEnum                          //Holds the name for each available body piece
{
    LongBody,
    ShortBody
}
public enum BarrelPartsEnum                        //Holds the name for each available barrel piece
{
    LongBarrel,
    ShortBarrel
}
public enum HandlePartsEnum                        //Holds the name for each available handle piece
{
    LongHandle,
    ShortHandle
}
public enum ScopePartsEnum                         //Holds the name for each available scope piece
{
    CircleScope,
    SquareScope
}



public class NewGunCreater : EditorWindow
{
    public CreatorSettings settingsWind = null;                                         //Link to creator settings window
    public BodyPartsEnum   bodyEnumChoice;                                              //Value for chosen body   piece
    public BarrelPartsEnum barrelEnumChoice;                                            //Value for chosen barrel piece
    public HandlePartsEnum handleEnumChoice;                                            //Value for chosen handle piece
    public ScopePartsEnum  scopeEnumChoice;                                             //Value for chosen scope  piece
    public List<GameObject> bodyList = new List<GameObject>();                          //Holds the gameobject for each available body   piece
    public List<GameObject> barrelList = new List<GameObject>();                        //Holds the gameobject for each available barrel piece
    public List<GameObject> handleList = new List<GameObject>();                        //Holds the gameobject for each available handle piece
    public List<GameObject> scopeList = new List<GameObject>();                         //Holds the gameobject for each available scope  piece
    public List<String> createdWeaponsList = new List<String>();                        //Holds the name for every created weapon                
    public string weaponName = "";                                                      //Name for the weapon being created
    public bool scopeEnabled;                                                           //Bool for if the weapon needs a scope or not

    [MenuItem("My Tools/Main Windows/Weapon Maker")]
    public static void ShowMyWindow()                                                   //Opens the window
    {
        NewGunCreater wnd = GetWindow<NewGunCreater>();
        wnd.titleContent = new GUIContent("WeaponSmith");
        wnd.minSize = new Vector2(500, 500);
        wnd.maxSize = new Vector2(1920, 720);
    }

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
            bool weaponPartsNotEmpty = ListIsNotEmptyCheck(barrelList, handleList, bodyList, scopeList);            //Checks to see if any of the lists are empty
            if (weaponPartsNotEmpty) 
            {
                bool hasWeaponAlreadyBeenCreated = CreatedWeaponListCheck();            //Checks if the name they want already exists or not
                if (!hasWeaponAlreadyBeenCreated)
                {
                    CreateWeaponSO();
                    createdWeaponsList.Add(weaponName);            //Creates the weapon then adds the weapons name to the list of existing weapons
                }
                else
                {
                    if (EditorUtility.DisplayDialog("WeaponOverride", "There is already a weapon with the name " + weaponName, "Override", "Cancel")) { CreateWeaponSO(); }            //Gives the user a warning if there is a weapon that already has the name that they are trying to use and if they click overide it will overwrite the old WeaponSO with the current data
                    else { return; }
                }
            }
            else
            {
                EditorUtility.DisplayDialog("WeaponListEmpty", "There is a List Missing a gameobject, Make sure all lists in the Creator settings are full with the right objects ", "Okay", "Cancel");            //Gives the user a warning if there are any lists empty in the creator settings when trying to save their new weapon
            }
        }
        #endregion
    }
    public void CreateWeaponSO() 
    {
        if(weaponName != null) 
        {
            ///Summary
            ///This Function saves all the data that the user entered and puts it into a newly created ScriptableWeapon and saves it to the folder Named WeaponSO
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
        else if (weaponName == null)  //if the user hasn't input the name of the weapon
        {
            EditorUtility.DisplayDialog("WeaponNameEmpty", "There is no weapon name. Make sure to give the weapon a name before trying to save it", "Okay", "Cancel");                     //Gives the user a warning if they haven't put in the weapons name
        }
    }

    bool ListIsNotEmptyCheck(List<GameObject> barrelList,List<GameObject> handleList,List<GameObject> bodyList,List<GameObject> scopeList)           
    {
        //Checks all the lists of game objects to make sure there isn't anything missing
        foreach (GameObject obj in barrelList) 
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

    bool CreatedWeaponListCheck()                                                                           //Checks the list of every weapon already created to see if the name's already taken and returns true if the name is taken
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
        JsonUtility.FromJsonOverwrite(bodyListData, this);                                                  //Loads the data from the last opened window
    }
    private void OnDisable()
    {
        if (settingsWind != null) 
        {
            settingsWind.Close();                                                                           //closes settings window
        }
        string data = JsonUtility.ToJson(this, false);
        EditorPrefs.SetString("bodyList", data);                                                            //Saves the data from the current window
    }
    public void CallToSettings(CreatorSettings csMainPass)   //Calls to the Settings window so it can link up with it
    {
        settingsWind = csMainPass;
        settingsWind.ngc = this;      //Sets the NewGunCreator Variable in CreatorSettings to this instance
    }
}

