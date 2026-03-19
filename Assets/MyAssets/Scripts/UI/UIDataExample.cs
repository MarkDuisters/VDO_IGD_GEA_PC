using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDataExample : MonoBehaviour
{
    [SerializeField] Slider getSlider;
    [SerializeField] TMP_Text getText;
    [SerializeField] Image getImage;


    public void OnInitializeSO(WeaponSO _weaponSO)
    {
        if (_weaponSO == null)
        {
            Debug.LogError("WeaponSO is null, cannot initialize UI values.");
            return;
        }

        UpdateUI(_weaponSO.ammoCount, _weaponSO.maxAmmo, _weaponSO.weaponSprite);
    }

    void UpdateUI(int _ammoCount, int _maxAmmo, Sprite _weaponSprite)
    {

        if (_weaponSprite != null) getImage.sprite = _weaponSprite;
        UpdateAmmoCountUI(_ammoCount, _maxAmmo);
    }

    public void UpdateAmmoCountUI(int _ammoCount, int _maxAmmo)
    {
        string combinedText = $"{_ammoCount:D2}/{_maxAmmo:D2}";
        getText.SetText(combinedText);
        getSlider.maxValue = _maxAmmo;
        getSlider.value = _ammoCount;
    }

}
