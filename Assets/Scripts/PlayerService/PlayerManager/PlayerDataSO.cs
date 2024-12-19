using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData",menuName ="ScriptableObjects/PlayerData")]
public class PlayerDataSO:ScriptableObject
{
    [SerializeField] float movementSpeed;
    public float MovementSpeed {  get { return movementSpeed; } }
}