using System;
using UnityEngine;

public class WeaponAnimationController : MonoBehaviour
{
    Animator animator => GetComponent<Animator>();
    [SerializeField] PlayerController getPlayerController;//refactor zodat ook dit communiceert via WeaponController.cs
    bool initialized = false;

    void Start()
    {

        if (animator == null)
        {
            Debug.LogError("Animator missing. Please add an Animator to this object first!");
            return;
        }

        if (getPlayerController == null)
        {
            Debug.LogError("PlayerController reference not set. Please assign one.");
            return;
        }

        initialized = true;

    }


    public void SetMoveSpeed(float speed)
    {
        if (!initialized) return;
        animator.SetFloat("_moveSpeed", speed);
    }

    public void FireWeapon(int inputValue)
    {
        bool fireGun = IntBasedBool(inputValue);
        animator.SetBool("_fire", fireGun);
    }

    public bool GetFireState()
    {
        return animator.GetBool("_fire");
    }
    public void SetJumpState(bool state)
    {
        animator.SetBool("_isGrounded", state);
    }
    public void SetJumpDirection(float value)
    {
        animator.SetFloat("_jumpDirection", value); ;
    }

    bool IntBasedBool(int _value)
    {
        return _value == 1 ? true : false;
    }
}
