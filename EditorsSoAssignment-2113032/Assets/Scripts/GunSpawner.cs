using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunSpawner : MonoBehaviour
{
    


    public ScriptableWeapon weaponSO;

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            SpawnGun();
        }
    }

    public void SpawnGun() 
    {
        GameObject bodyIns = Instantiate(weaponSO.body, Vector3.zero, Quaternion.identity);
        BodyConectionNodes bodyNodes = bodyIns.GetComponent<BodyConectionNodes>();

        GameObject barrelIns = Instantiate(weaponSO.barrel, bodyNodes.barrelNode.position, Quaternion.identity);
        barrelIns.transform.parent = bodyNodes.barrelNode;
    
        GameObject handleIns = Instantiate(weaponSO.handle, bodyNodes.handleNode.position, Quaternion.identity);
        handleIns.transform.parent = bodyNodes.handleNode;


    }
}
