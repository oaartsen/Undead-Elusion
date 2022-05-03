using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    // Configuration parameters
    [SerializeField] Camera FPCamera = null;
    [SerializeField] ParticleSystem muzzleFlash = null;
    [SerializeField] GameObject terrainHitEffect = null;
    [SerializeField] float shootingRange = 100f;
    [SerializeField] float weaponDamage = 50f;
    [SerializeField] float delayDestroyTerrainHitEffect = 0.1f;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;

    // State variables
    bool canShoot = true;

    // Cached references
    WeaponSwitcher myWeaponSwitcher = null;

    void Start() {
        myWeaponSwitcher = GetComponentInParent<WeaponSwitcher>();
    }

    //Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(0) && ammoSlot.GetCurrentAmmoAmount(ammoType) > 0 && canShoot) {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot() {
        canShoot = false;
        myWeaponSwitcher.SetBoolCanSwitch(false);

        ammoSlot.ReduceCurrentAmmoAmount(ammoType);
        PlayMuzzleFlash();
        ProcessRayCast();

        yield return new WaitForSeconds(timeBetweenShots);

        myWeaponSwitcher.SetBoolCanSwitch(true);
        canShoot = true;
    }

    void PlayMuzzleFlash() {
        muzzleFlash.Play();
    }

    void ProcessRayCast() {
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, shootingRange)) {

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) {
                CreateTerrainHitEffect(hit);
            }
            else {
                target.TakeDamage(weaponDamage);
                // TO DO: create and display hit effect for zombie
            }

        }

        else {
            return;
        }
    }

    void CreateTerrainHitEffect(RaycastHit hit) {
        GameObject terrainHitEffectObject = Instantiate(terrainHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(terrainHitEffectObject, delayDestroyTerrainHitEffect);
    }

}
