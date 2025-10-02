using System;
using System.Collections;
using System.Collections.Generic;
using MindlessSDK;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour, ICrateBarcode
{

    // Ammo
    private float nextTimeToFire = 0;
    public GunData gun = new();
    public AmmoData ammo = new();
    public UltEvent ShootEvent = new ();
    public UltEvent ReloadEvent = new();
    public UltEvent<RaycastHit> HitEvent = new();
    public bool firing = false;

    public void Fire(bool fire) => firing = fire;
    private void OnEnable()
    {
        ammo.Reloading = false;

        Validate();
    }

    public void Validate()
    {
        if (gun.MuzzleFlash.Crate)
            gun.MuzzleFlash.Barcode = gun.MuzzleFlash.Crate.Barcode;
        AssetWarehouse.Instance.TryGetCrate<Crate>(gun.MuzzleFlash.Barcode, out var crate);
    }

    public void Reload()
    {
        StartCoroutine(ReloadCallback());
    }

    void Update() {
        if (ammo.Reloading) {
            return;
        }
        if (ammo.CurrentAmmo <= 0) {
            Reload();
            return;
        }
        if (gun.Mode == GunData.GunMode.Auto) {
            if (firing && Time.time >= nextTimeToFire) {
                nextTimeToFire = Time.time + 1f / gun.FireRate;
                Shoot();
            }
        }
        else {
            if (firing && Time.time >= nextTimeToFire) {
                nextTimeToFire = Time.time + 0.2f / gun.FireRate;
                Shoot();
            }
        }     
        AmmoText.text = $"{ammo.CurrentAmmo}/{ammo.MaxAmmo}";
    }

    IEnumerator ReloadCallback() {
        Debug.Log("Reloading..");
        AmmoText.text = $"{ammo.CurrentAmmo}/{ammo.MaxAmmo}";
        gun.Reloading = true;
        yield return new WaitForSeconds(gun.ReloadTime);
        gun.Reloading = false;
        ammo.CurrentAmmo = ammo.MaxAmmo;
    }

    private void Shoot() {
        ShootEvent.Invoke();
        if (gun.MuzzleFlash.Crate != null)
        {
            GameObject muzzleFlashGO = AssetSpawner.Instance.Spawn(gun.MuzzleFlash.Crate, gun.Muzzle.position, gun.Muzzle.rotation);
        }
        ammo.CurrentAmmo--;
        for (int x = 0; x < ammo.RoundsPerFire; x++) {
            RaycastHit hit;
            Vector3 fwd = gun.Muzzle.forward;
            float calculatedSpread = UnityEngine.Random.Range(-ammo.Spread, ammo.Spread);
            fwd = fwd + gun.Muzzle.TransformDirection(new Vector3(calculatedSpread, calculatedSpread));
            if (Physics.Raycast(gun.Muzzle.position, fwd, out hit, ammo.Range)) {
                if (hit.collider != null)
                {
                    Debug.LogError(hit.transform.name);
                    AssetSpawner.Instance.Spawn(ammo.ImpactEffect.Crate, hit.point, Quaternion.LookRotation(hit.normal));
                    IDamagable weaponTarget = hit.transform.GetComponent<IDamagable>();
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * ammo.ImpactForce);
                    }
                    if (weaponTarget != null && ammo.ImpactForce != 0)
                    {
                        weaponTarget.Damage(new(ammo.Damage));
                    }
                    HitEvent.Invoke(hit);
                }
            }           
        }
        GetComponentInParent<Rigidbody>().AddForce(-gun.Muzzle.forward * ammo.ImpactForce * 1.5f);
    }

    public void Start()
    {
        Validate();
    }
}

[Serializable]
public struct AmmoData
{
    [Range(1, 100)]
    public int MaxAmmo;
    [Range(1, 100)]
    public int CurrentAmmo;
    [Range(1, 100)]
    public int RoundsPerFire;
    public float ReloadTime;
    public float Spread;
    public float Range;
    public float ImpactForce;
    public float Damage;
    public bool Reloading;
    public CrateBarcode<SpawnableCrate> MuzzleFlash;
    public CrateBarcode<SpawnableCrate> Smoke;
    public CrateBarcode<SpawnableCrate> ImpactEffect;

    public AmmoData(int maxAmmo = 15, float reloadTime = 1f)
    {
        Spread = 0;
        Range = 100;
        RoundsPerFire = 1;
        Damage = 5;
        ImpactForce = 4;
        MaxAmmo = maxAmmo;
        ReloadTime = 1f;
        CurrentAmmo = MaxAmmo;
        Reloading = false;
        MuzzleFlash = new();
        Smoke = new();
        ImpactEffect = new();
    }

    public AmmoData(int currentAmmo, int maxAmmo = 15, float reloadTime = 1f)
    {
        Spread = 0;
        Range = 100;
        RoundsPerFire = 1;
        Damage = 5;
        ImpactForce = 4;
        CurrentAmmo = currentAmmo;
        MaxAmmo = maxAmmo;
        ReloadTime = 1f;
        Reloading = false;
        MuzzleFlash = new();
        Smoke = new();
        ImpactEffect = new();
    }
}

[Serializable]
public struct GunData
{

    public enum GunMode
    {
        SemiAuto,
        Auto
    }
    public GunMode Mode;
    public float ReloadTime;
    public float FireRate;
    public bool Reloading;
    public Transform Muzzle;
    public CrateBarcode<SpawnableCrate> MuzzleFlash;
    public CrateBarcode<SpawnableCrate> Smoke;

    public GunData(GunMode mode = GunMode.SemiAuto)
    {
        Mode = mode;
        FireRate = 5;
        ReloadTime = 1f;
        Reloading = false;
        Muzzle = null;
        MuzzleFlash = new();
        Smoke = new();
    }
}