
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{

    private WeaponController weaponController;
    [SerializeField] SpriteRenderer weaponImage;
    public void SetController(WeaponController weaponController)
    {
        this.weaponController = weaponController;
    }

}
