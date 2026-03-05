using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDataExample : MonoBehaviour
{
    [SerializeField] Slider getSlider;
    [SerializeField] TMP_Text getText;
    [SerializeField] Image getImage;

    /*
    IEnumerator AmmoTest()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireDelay);
            ammoCount--;
            ammoCount = Mathf.Clamp(ammoCount, 0, 30);
            getSlider.value = ammoCount;
            //met de $ notatie kunnen we veriables meteen als string interpreteren door ze te encapsuleren {}
            //:D2 is het zelfde als int.ToString("00") en geeft ons dus controle over het aantal decimalen.
            string combinedText = $"{ammoCount:D2}/{maxAmmoTxt}";
            getText.SetText(combinedText);

            yield return null;
        }

    }*/

    /*  public void InitializeUIValues(int _maxAmmo, int _ammoCount, Sprite _weaponSprite, float _fireDelay)
      {
          maxAmmo = _maxAmmo;
          ammoCount = ammoCount == 0 ? maxAmmo : _ammoCount;
          getSlider.maxValue = maxAmmo;
          getSlider.value = ammoCount;
          getImage.sprite = _weaponSprite;
          fireDelay = _fireDelay;
      }*/

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
