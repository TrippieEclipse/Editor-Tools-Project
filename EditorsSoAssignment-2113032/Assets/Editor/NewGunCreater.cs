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
    public BodyPartsEnum   bodyEnumChoice;
    public BarrelPartsEnum barrelEnumChoice;
    public HandlePartsEnum handleEnumChoice;


    public static List<GameObject> bodyList = new List<GameObject>();
    public static List<GameObject> barrelList = new List<GameObject>();
    public static List<GameObject> handleList = new List<GameObject>();


    static string clearBarrelArrayindex = "Index To Destroy";

    string weaponName = null;


    [MenuItem("My Tools/Room Maker")]
    public static void ShowMyWindow()
    {
        NewGunCreater wnd = GetWindow<NewGunCreater>();
        wnd.titleContent = new GUIContent("WeaponSmith");

        wnd.minSize = new Vector2(1080, 200);
        wnd.maxSize = new Vector2(1920, 720);
    }



    private void OnGUI()
    {

        #region Weapon Part Selection

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Weapon Name");
        weaponName = EditorGUILayout.TextField("", weaponName);

        EditorGUILayout.Space();

        bodyEnumChoice = (BodyPartsEnum)EditorGUILayout.EnumPopup("Choose Body Part -->", bodyEnumChoice);

        EditorGUILayout.Space();

        barrelEnumChoice = (BarrelPartsEnum)EditorGUILayout.EnumPopup("Choose Barrel Part -->", barrelEnumChoice);

        EditorGUILayout.Space();

        handleEnumChoice = (HandlePartsEnum)EditorGUILayout.EnumPopup("Choose Handle Part -->", handleEnumChoice);


        if (GUILayout.Button("Create"))
        {
            CreateWeaponSO();
        }



        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(40);

        #endregion


        #region Adding/Taking Away From List

        #region Body

        if (GUILayout.Button("Add To Body List"))
        {

            foreach (GameObject obj in Selection.gameObjects)
            {
                if (!bodyList.Contains(obj) && obj.CompareTag("Body"))
                {
                    bodyList.Add(obj);
                }
                else if (bodyList.Contains(obj))
                {
                    Debug.Log("Asset Already Added");
                }
                else if (!obj.CompareTag("Body"))
                {
                    Debug.Log("Wrong Asset Type");
                }


            }
        }

        if (GUILayout.Button("ListOffBody"))
        {
            if (bodyList.Count > 0)
            {
                for (int i = 0; i < bodyList.Count; i++)
                {
                    Debug.Log(bodyList[i] + " -- index - " + i);
                }
            }
            else
            {
                Debug.Log("No Items in list");
            }
        }

        if (GUILayout.Button("ClearBodylist"))
        {
            bodyList.Clear();
        }

        EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();
        #endregion

        #region Barrel
        if (GUILayout.Button("Add To Barrel List"))
        {

            foreach (GameObject obj in Selection.gameObjects)
            {
                if (!barrelList.Contains(obj) && obj.CompareTag("Barrel"))
                {
                    barrelList.Add(obj);
                }
                else if (barrelList.Contains(obj))
                {
                    Debug.Log("Asset Already Added");
                }
                else if (!obj.CompareTag("Barrel"))
                {
                    Debug.Log("Wrong Asset Type");
                }
            }
        }

        if (GUILayout.Button("ListOffBarrel"))
        {
            if (barrelList.Count > 0)
            {
                for (int i = 0; i < barrelList.Count; i++)
                {
                    Debug.Log(barrelList[i] + " -- index - " + i);

                }
            }
            else
            {
                Debug.Log("No Items in list");
            }
        }

        if (GUILayout.Button("ClearBarrellist"))
        {
            barrelList.Clear();
        }

        EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();
        #endregion

        #region Handle

        if (GUILayout.Button("Add To Handle List"))
        {

            foreach (GameObject obj in Selection.gameObjects)
            {
                if (!handleList.Contains(obj) && obj.CompareTag("Handle"))
                {
                    handleList.Add(obj);
                }
                else if (handleList.Contains(obj))
                {
                    Debug.Log("Asset Already Added");
                }
                else if (!obj.CompareTag("Handle"))
                {
                    Debug.Log("Wrong Asset Type");
                }
            }
        }

        if (GUILayout.Button("ListOffHandlel"))
        {
            if (handleList.Count > 0)
            {
                for (int i = 0; i < handleList.Count; i++)
                {
                    Debug.Log(handleList[i] + " -- index - " + i);

                }
            }
            else
            {
                Debug.Log("No Items in list");
            }
        }

        if (GUILayout.Button("ClearHandlelist"))
        {
            handleList.Clear();
        }

        #endregion

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

}