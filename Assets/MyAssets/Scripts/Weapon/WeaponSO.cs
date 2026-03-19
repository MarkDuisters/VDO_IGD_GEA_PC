using System;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponObject", menuName = "Inventory/create new Weapon")]
public class WeaponSO : ScriptableObject
{
    public enum WeaponType { ScopeRifle, Shotgun, PlasmaRifle, Grenadelauncher }
    [Header("Weapon Type")]
    public WeaponType weaponType = WeaponType.ScopeRifle;

    [Header("Weapon Info")]
    public int maxAmmo;
    public int ammoCount;
    public int ammoCost;
    public float fireDelay;

    [Header("Weapon graphics and object")]
    public Sprite weaponSprite;

    public enum ProjectileType { Raycast, PhysicalProjectile }
    [Header("Projectile settings")]
    public ProjectileType projectileType = ProjectileType.Raycast;

    public int projectileDamage;
    public LayerMask hitLayers;
    public float maxRayDistance = Mathf.Infinity;
    public GameObject physicalProjectile;
    public float projectileVelocity;
}
