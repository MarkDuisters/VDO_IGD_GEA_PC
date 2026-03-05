using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FakeInventory : MonoBehaviour
{
    // [SerializeField] WeaponUI[] weapon;
    //  WeaponUIStruct[] weaponUIStructs;
    [SerializeField] WeaponSO[] weaponSO;
    [SerializeField] WeaponSO selectedWeapon;
    [SerializeField] int index;

    [Header("UI Reference")]
    [SerializeField] UIDataExample uIDataExample;

    void Start()
    {
        index = 0;
        selectedWeapon = weaponSO[index];
        uIDataExample.OnInitializeSO(selectedWeapon);
    }

    //Haal onze scroll richting op.
    void OnScrollWheel(InputValue value)
    {
        //haal onze scrollrichting op en pas deze toe op onze index. 
        //Scrollen naar boven telt de index op, scrollen naar beneden trekt de index af.
        float scrollDirection = value.Get<float>();
        index += (int)scrollDirection;

        //via een modulo zorgen we dat we niet out of bounds gaan van onze lijst. 
        //Wanneer we boven het maximum komen, gaan we terug naar 0. 
        // Wanneer we onder 0 komen, gaan we naar het maximum via onze ? operator. 
        index = index % weaponSO.Length;
        index = index < 0 ? weaponSO.Length - 1 : index;

        //Als we tenslot onze index weten geven we de referentie van onze lijst door aan selectedWeapon.
        // Vervolgens kunnen we deze referentie gebruiken om onze UI te updaten.
        selectedWeapon = weaponSO[index];
        uIDataExample.OnInitializeSO(selectedWeapon);
    }

}

#region class & struct
//We maken een eige class, oftewel "object" aan. Hierin geven we properties/eigenschappen die dit object
//beschrijft mee. Dit object kan nu als een reference type gebruikt worden.
[Serializable]
public class WeaponUI //class is een reference type. Wanneer nieuwe variables van dezelfde type WeaponUI gemaakt 
//worden en we deze instellen met =. Dan point de nieuwe variable altijd naar het origineel.
{
    public int maxAmmo;
    public int ammoCount;
    public Sprite weaponSprite;
    public float fireDelay;
}

[Serializable]
public struct WeaponUIStruct //struct is een value type. Wanneer we nieuwe variables van dezelde struct WeaponUIstruct
//gemaakt worden en we deze instellen met =. Dan point deze NIET naar het origineel. Dit word echter een unieke
//copie waar de data naar overgeschreven word en beiden worden hun eigen entiteit in memory.
{
    public int maxAmmo;
    public int ammoCount;
    public Sprite weaponSprite;
    public float fireDelay;
}
#endregion