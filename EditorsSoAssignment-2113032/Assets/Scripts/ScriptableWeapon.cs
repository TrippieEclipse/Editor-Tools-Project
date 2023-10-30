using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "ScriptableObjects/Create New Weapon")]
public class ScriptableWeapon : ScriptableObject
{
    public string weaponName;
    public GameObject body;
    public GameObject barrel;
    public GameObject handle;
}
