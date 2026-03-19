using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponItem weaponItem;

    [SerializeField] WeaponAnimationController getWeaponAnimationController;
    PlayerController getPlayerController => GetComponent<PlayerController>();


    void LateUpdate()
    {
        if (getWeaponAnimationController == null) return;
        getWeaponAnimationController.SetMoveSpeed(getPlayerController.currentSpeed);
    }

    public void UpdateWeapon(WeaponItem weapon)
    {
        weaponItem = weapon;
        InitializeWeapon();
    }

    void InitializeWeapon()
    {
        if (weaponItem == null)
        {
            Debug.LogError("No reference found to weaponObject. Is it it set? Did the inventory set it correctly?");
            return;
        }

        getWeaponAnimationController = weaponItem.weaponGameObject.GetComponent<WeaponAnimationController>();
        return;
    }

    public void OnAttack(InputValue context)
    {
        if (getWeaponAnimationController == null)
        {
            Debug.LogError("No AnimationController found on weaponObject");
            return;
        }

        if (getWeaponAnimationController.GetFireState()) return;

        getWeaponAnimationController.FireWeapon(1);//1=true
    }



    /*public void OnMove(InputValue context)
    {
        getWeaponAnimationController.SetMoveSpeed(getPlayerController.currentSpeed);
    }*/
}
