using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData",menuName ="ScriptableObjects/PlayerData")]
public class PlayerDataSO:ScriptableObject
{
    [SerializeField] float movementSpeed;
    [SerializeField] float health;
    public float MovementSpeed {  get { return movementSpeed; } }
    public float Health { get { return health; } }
}