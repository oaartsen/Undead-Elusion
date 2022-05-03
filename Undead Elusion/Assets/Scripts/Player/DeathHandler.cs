using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour {

    // Configuration parameters
    [SerializeField] Canvas gameOverCanvas;

    // Cached references
    WeaponSwitcher myWeaponSwitcher = null;


    // Start is called before the first frame update
    void Start() {
        gameOverCanvas.enabled = false;
        myWeaponSwitcher = GetComponentInChildren<WeaponSwitcher>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void HandleDeath() {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        myWeaponSwitcher.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
