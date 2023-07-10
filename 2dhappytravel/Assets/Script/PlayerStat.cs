using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class PlayerStat : ScriptableObject
{
    [SerializeField]
    private float _hp;
    public float HP
    {
        get { return _hp; }
        set { _hp = value; }
    }

    [SerializeField]
    private float _maxHp;
    public float MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }

    [SerializeField]
    private int _lifecount;
    public int LifeCount
    {
        get { return _lifecount; }
        set { _lifecount = value; }
    }

    [SerializeField]
    private Vector3 _respawnPoint;
    public Vector3 RespawnPoint
    {
        get { return _respawnPoint; }
        set { _respawnPoint = value; }
    }

    [SerializeField]
    private bool _isNull;
    public bool IsNull
    {
        get { return _isNull; }
        set { _isNull = value; }
    }
}
