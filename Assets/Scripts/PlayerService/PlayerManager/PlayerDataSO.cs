using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData",menuName ="ScriptableObjects/PlayerData")]
public class PlayerDataSO:ScriptableObject
{
    [SerializeField] float movementSpeed;
    [SerializeField] float health;
    [SerializeField] List<Transform> startPostionsForEachLevel=new List<Transform>();
    public float MovementSpeed {  get { return movementSpeed; } }
    public float Health { get { return health; } }
    public List<Transform> StartPositionsForEachLevel { get { return startPostionsForEachLevel; } }
}