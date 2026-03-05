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
    public float fireDelay;

    [Header("Weapon graphics and object")]
    public Sprite weaponSprite;
    public GameObject weaponPrefab;
    
    [Header("Projectile settings")]
    public int projectileDamage;
    public enum ProjectileType { Raycast, PhysicalProjectile }
    public ProjectileType projectileType = ProjectileType.Raycast;
    public float maxRayDistance = Mathf.Infinity;
    public GameObject physicalProjectile;
}
