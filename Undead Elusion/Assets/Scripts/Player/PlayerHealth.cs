using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    // Configuration parameters
    [SerializeField] float maxHealth = 100f;

    // State variables
    float currentHealth;

    // Cached references
    DeathHandler myDeathHandler = null;

    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
        myDeathHandler = GetComponent<DeathHandler>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void DamagePlayer(float damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            //Debug.Log("Player has died");
            myDeathHandler.HandleDeath();
        }
    }

}
