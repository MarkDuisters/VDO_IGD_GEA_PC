using System;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class FakeInventory : MonoBehaviour
{

    [Header("Invenotry")]
    [SerializeField] WeaponItem[] weapons;
    [SerializeField] WeaponItem selectedWeapon;
    [SerializeField] int index;

    [Header("UI Reference")]
    [SerializeField] UIDataExample uIDataExample;

    WeaponController getWeaponController => GetComponent<WeaponController>();
    PlayerController getPlayerController => GetComponent<PlayerController>();
    bool initalized = false;

    static public FakeInventory instance;

    void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    void Start()
    {
        index = 0;
        selectedWeapon = weapons[index];
        InitializeInventoryItems();//Initialize altijd eerst anders kunnen de regels hier onder niet bij hun data.
        initalized = true;
    }

    //Haal onze scroll richting op.
    void OnScrollWheel(InputValue value)
    {
        if (!initalized) return;
        //haal onze scrollrichting op en pas deze toe op onze index. 
        //Scrollen naar boven telt de index op, scrollen naar beneden trekt de index af.
        float scrollDirection = value.Get<float>();
        index += (int)scrollDirection;

        //via een modulo zorgen we dat we niet out of bounds gaan van onze lijst. 
        //Wanneer we boven het maximum komen, gaan we terug naar 0. 
        // Wanneer we onder 0 komen, gaan we naar het maximum via onze ? operator. 
        index = index % weapons.Length;
        index = index < 0 ? weapons.Length - 1 : index;

        //Als we tenslot onze index weten geven we de referentie van onze lijst door aan selectedWeapon.
        // Vervolgens kunnen we deze referentie gebruiken om onze UI te updaten.
        selectedWeapon = weapons[index];
        InitializeInventoryItems();
    }

    //Zet onze selected item aan en alle rest uit.
    void InitializeInventoryItems()
    {
        selectedWeapon.weaponGameObject.SetActive(false);
        print(selectedWeapon.weaponGameObject.name);

        foreach (WeaponItem weaponItem in weapons)
        {
            if (weaponItem == selectedWeapon)
            {
                if (selectedWeapon.weaponInfo.pickedUp)
                {
                    weaponItem.weaponGameObject.SetActive(true);
                    continue;
                }
            }
            weaponItem.weaponGameObject.SetActive(false);
        }
        uIDataExample.OnInitializeSO(selectedWeapon.weaponInfo);
        getWeaponController.UpdateWeapon(selectedWeapon);
        getPlayerController.UpdateWeapon(selectedWeapon);
    }
    public void PickUpItem(WeaponSO item)
    {
        int _index = 0;
        foreach (WeaponItem weaponItem in weapons)
        {
            if (weaponItem.weaponInfo == item)
            {
                weaponItem.weaponInfo.pickedUp = true;
                selectedWeapon = weaponItem;
                index = _index;
                break;
            }
            _index++;
        }
        InitializeInventoryItems();
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


#region ScriptObject + GameObject wrapper
[Serializable]
public class WeaponItem
{
    public WeaponSO weaponInfo;
    public GameObject weaponGameObject;
}
#endregion