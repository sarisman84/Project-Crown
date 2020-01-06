using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    public string weapon;
    private void OnCollisionEnter2D (Collision2D other) {
        OffensivePlayerSystems player = other.collider.GetComponent<OffensivePlayerSystems> ();
        if (player != null) {
            player.weaponHolder.AddWeaponToInventory = GameSystems.GetWeaponFromLibrary (weapon);
            TriggerPickupAnim ();
            gameObject.SetActive (false);
        }
    }

    private void TriggerPickupAnim () {
        Debug.Log ($"Pickup Anim triggered. Added {weapon} to the player's inventory.");
    }
}