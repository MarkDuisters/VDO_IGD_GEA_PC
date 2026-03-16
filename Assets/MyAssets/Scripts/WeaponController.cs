using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject weaponObject;
    [SerializeField] WeaponAnimationController getWeaponController;
    [SerializeField] PlayerController getPlayerController;

    void Start()
    {
        InitializeWeapon();
    }

    public void UpdateWeapon(GameObject weapon)
    {
        weaponObject = weapon;
        InitializeWeapon();
    }

    void InitializeWeapon()
    {
        if (weaponObject == null)
        {
            Debug.LogError("No reference found to weaponObject. Is it it set? Did the inventory set it correctly?");
            return;
        }

        getWeaponController = weaponObject.GetComponent<WeaponAnimationController>();
        return;
    }

    public void OnAttack(InputValue context)
    {
        if (getWeaponController == null)
        {
            Debug.LogError("No AnimationController found on weaponObject");
            return;
        }

        if (getWeaponController.GetFireState()) return;
        getWeaponController.FireWeapon(1);//1=true
    }

    public void OnMove(InputValue context)
    {
        getWeaponController.SetMoveSpeed(getPlayerController.currentSpeed);
    }
}
