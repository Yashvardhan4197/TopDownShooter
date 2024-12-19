public class WeaponService
{
    private WeaponController weaponController;

    public WeaponService(WeaponView weaponView, WeaponDataSO weaponDataSO)
    {
        weaponController = new WeaponController(weaponView,weaponDataSO);
    }

    public WeaponController GetWeaponController() => weaponController;

}