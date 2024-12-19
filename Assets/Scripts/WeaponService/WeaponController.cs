public class WeaponController
{
    private WeaponView weaponView;
    private WeaponDataSO weaponDataSO;
    public WeaponController(WeaponView weaponView,WeaponDataSO weaponDataSO)
    {
        this.weaponView = weaponView;
        this.weaponView.SetController(this);
    }


}