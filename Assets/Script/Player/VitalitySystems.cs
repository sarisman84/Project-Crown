using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VitalitySystems : MonoBehaviour, IInteractable {
    [SerializeField] float maxHealth;
    float currentHealth;

    private void Awake () {
        currentHealth = maxHealth;
    }
    public void TakeDamage (float damage) {
        currentHealth -= damage;
        Debug.Log ($"{gameObject.name} took damage!");
        if (currentHealth <= 0) KillEntity ();
    }

    private void KillEntity () {
        gameObject.SetActive (false);
    }
}