using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIView: MonoBehaviour
{
    private WeaponUIController weaponUIController;
    [SerializeField] TextMeshProUGUI currentWeaponBullets;
    [SerializeField] Image[] weaponImageCollection;

    public void SetController(WeaponUIController weaponUIController)
    {
        this.weaponUIController = weaponUIController;
    }

    public TextMeshProUGUI GetCurrentWeaponBullets() => currentWeaponBullets;

    public Image[] GetWeaponImageCollection()=> weaponImageCollection;

}