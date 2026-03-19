using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    WeaponSO weaponInfo;
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.GetComponent<IDamagable>() == null) return;

        coll.gameObject.GetComponent<IDamagable>().DoDamage(weaponInfo.projectileDamage);
    }

    public void SetWeaponInfo(WeaponSO info)
    {
        weaponInfo = info;
    }

}
