using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    //Bevat alle data die aangepast moet worden op ons wapen. Hoeft de info niet van het inventory
    //te krijgen. Want dit is het object zelf al. Zorg wel dat onze weapon de juiste WeaponSO heeft goegewezen.
    [Header("Required")]
    [SerializeField] WeaponSO weaponInfo;
    [SerializeField] Transform projectileOrigin;


    [Header("UI Reference")]
    [SerializeField] UIDataExample uIDataExample;

    void UpdateUI()
    {
        uIDataExample.UpdateAmmoCountUI(weaponInfo.ammoCount, weaponInfo.maxAmmo);
    }

    public void FireBullet()
    {
        switch (weaponInfo.projectileType)
        {
            case WeaponSO.ProjectileType.PhysicalProjectile:
                FireProjectile(weaponInfo.physicalProjectile);
                break;

            case WeaponSO.ProjectileType.Raycast:
                FireHitScan(weaponInfo.maxRayDistance, weaponInfo.hitLayers);
                break;
        }
    }

    public void FireProjectile(GameObject projectile)//Straks niet vergeten damage interface.
    {
        //implement super coole projectile code later. Die werkt!
        GameObject projectileClone = Instantiate(projectile, projectileOrigin.position, projectileOrigin.rotation);
        projectileClone.GetComponent<ProjectileMovement>().SetVelocity(weaponInfo.projectileVelocity);
        print("Coole projectile goes pfeeeeeew");
    }

    public void FireHitScan(float distance, LayerMask hitLayers)//Straks niet vergeten damage interface.
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(projectileOrigin.position, projectileOrigin.forward);

        if (Physics.Raycast(ray, out hit, distance, hitLayers))
        {
            Debug.DrawRay(projectileOrigin.position, projectileOrigin.forward * 1000f, Color.red, 3f);
            print(hit.collider.name);
        }
        //implement hyper accurate raycast.
        print("rifle goes brrrrrr!");
    }

    public void SubtractAmmo()
    {

        int tempAmmo = weaponInfo.ammoCount;
        tempAmmo -= weaponInfo.ammoCost;
        tempAmmo = Mathf.Clamp(tempAmmo, 0, weaponInfo.maxAmmo);
        weaponInfo.ammoCount = tempAmmo;
        UpdateUI();
    }
}