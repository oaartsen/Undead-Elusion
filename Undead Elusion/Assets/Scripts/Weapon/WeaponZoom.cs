using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour {

    // Configuration parameters
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 35f;
    [SerializeField] float mouseSensitivityZoomedOut = 2f;

    // State variables
    bool isZoomedIn = false;
    float currentMouseSensitivity;

    // Cached references
    Camera fpsCamera = null;
    RigidbodyFirstPersonController myRigidbodyFirstPersonController = null;

    // Start is called before the first frame update
    void Start() {
        fpsCamera = GetComponentInParent<Camera>();
        myRigidbodyFirstPersonController = GetComponentInParent<RigidbodyFirstPersonController>();
        currentMouseSensitivity = mouseSensitivityZoomedOut;
    }

    // Update is called once per frame
    void Update() {
        
        if (Input.GetMouseButtonDown(1)) {
            if (isZoomedIn) {
                ZoomOut();
            }
            else {
                ZoomIn();
            }

            myRigidbodyFirstPersonController.mouseLook.XSensitivity = currentMouseSensitivity;
            myRigidbodyFirstPersonController.mouseLook.YSensitivity = currentMouseSensitivity;

        }
    }

    private void ZoomOut() {
        isZoomedIn = false;
        fpsCamera.fieldOfView = zoomedOutFOV;
        currentMouseSensitivity = mouseSensitivityZoomedOut;
    }

    private void ZoomIn() {
        isZoomedIn = true;
        fpsCamera.fieldOfView = zoomedInFOV;
        currentMouseSensitivity = mouseSensitivityZoomedOut * zoomedInFOV / zoomedOutFOV;
    }

    private void OnDisable() {
        if (isZoomedIn) {
            ZoomOut();
        }
    }
}
