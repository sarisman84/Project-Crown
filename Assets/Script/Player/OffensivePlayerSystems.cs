using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OffensivePlayerSystems : MonoBehaviour {
    //What does this class do?

    //TODO  Basic projectile shooting
    //TODO  Weapon Collection/Assignment
    //TODO  Projectile definitions (like who fired it, what behaivour does the projectile do, etc.)

    public GameObject indicatorPrefab;

    public WeaponHolder weaponHolder;

    AimingLogic aim;

    private void Awake () {
        weaponHolder = new WeaponHolder (this);
        aim = new AimingLogic ();
        aim.AffectGameObject (GameObject.Find (indicatorPrefab.name));

    }

    private void Update () {
        indicatorPrefab.gameObject.SetActive (weaponHolder.WeaponInventory.Count != 0);
        if (indicatorPrefab.activeInHierarchy)
            aim.TowardsCursor (Camera.main.ScreenToWorldPoint (Input.mousePosition), transform.position);
        if (Input.GetMouseButton (0))
            weaponHolder.TriggerAttackFunction (gameObject, indicatorPrefab.transform.GetChild (0));

    }

}

public class AimingLogic {

    List<GameObject> listOfGameObjects = new List<GameObject> ();

    public bool SystemDisabled { private get; set; }

    public void AffectGameObject (GameObject gameObject) {
        if (listOfGameObjects.Contains (gameObject)) return;
        listOfGameObjects.Add (gameObject);
    }

    public void ResetGameObject (GameObject gameObject) {
        listOfGameObjects.Remove (gameObject);
    }

    public Vector2 TowardsCursor (Vector2 mousePosition, Vector2 ownerPosition) {
        if (listOfGameObjects.Count == 0 || !Array.Find (GameObject.FindObjectsOfType<GameObject> (), p => p == listOfGameObjects[UnityEngine.Random.Range (0, listOfGameObjects.Count)])) throw new NullReferenceException ("Prefab missing in the scene.");
        Vector2 direction = -(ownerPosition - mousePosition);
        if (SystemDisabled) return direction;
        for (int i = 0; i < listOfGameObjects.Count; i++) {
            GameObject obj = listOfGameObjects[i];
            obj.transform.rotation = Quaternion.LookRotation (Vector3.forward, direction);
        }

        return direction;
    }

}