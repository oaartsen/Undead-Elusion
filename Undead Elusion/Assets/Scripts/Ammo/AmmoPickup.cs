using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour {

    // Configuration parameters
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;

    // State variables
    bool playerIsTouchingPickup = false;

    // Cached references
    Ammo playerAmmo = null;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        HandlePickupInput();
    }

    private void HandlePickupInput() {
        if (playerIsTouchingPickup && Input.GetKeyDown(KeyCode.E) && playerAmmo != null) {
            playerAmmo.IncreaseCurrentAmmoAmount(ammoType, ammoAmount);
            Debug.Log("Player has picked up box");
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            playerAmmo = other.gameObject.GetComponent<Ammo>();
            playerIsTouchingPickup = true;
        }
    }


    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            playerIsTouchingPickup = false;
        }
    }


}
