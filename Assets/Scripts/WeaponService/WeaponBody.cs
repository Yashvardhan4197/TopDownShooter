using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBody : MonoBehaviour
{
    [SerializeField] Transform muzzleTransform;
    [SerializeField] Animator weaponAnimController;
    public Transform GetMuzzleTransform() => muzzleTransform;
    public Animator GetWeaponAnimController() => weaponAnimController; 
}
