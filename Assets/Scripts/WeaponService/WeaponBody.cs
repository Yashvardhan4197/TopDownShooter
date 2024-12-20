using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBody : MonoBehaviour
{
    [SerializeField] Transform muzzleTransform;

    public Transform GetMuzzleTransform() => muzzleTransform;
}
