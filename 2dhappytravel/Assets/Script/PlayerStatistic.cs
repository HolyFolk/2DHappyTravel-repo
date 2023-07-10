using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class PlayerStatistic : ScriptableObject
{
    [SerializeField]
    private float _hp;
    public float HP
    {
        get { return _hp; }
        set { _hp = value; }
    }

    [SerializeField]
    private int _lifecount;
    public int LifeCount
    {
        get { return _lifecount; }
        set { _lifecount = value; }
    }
}
