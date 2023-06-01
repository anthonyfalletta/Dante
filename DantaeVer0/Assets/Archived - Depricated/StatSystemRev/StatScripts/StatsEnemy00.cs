using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StatSystem/Enemies/Enemy00")]
public class StatsEnemy00 : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float wanderMinDistance;
    [SerializeField] private float wanderMaxDistance;

    public int Health => _health;
    public float Speed => _speed;
    public float WanderMinDistance => wanderMinDistance;
    public float WanderMaxDistance => wanderMaxDistance;
}
