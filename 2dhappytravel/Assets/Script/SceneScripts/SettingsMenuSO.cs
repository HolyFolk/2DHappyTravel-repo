using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class SettingsMenuSO : ScriptableObject
{
    [SerializeField]
    private float _volume;
    public float Volume
    {
        get { return _volume; }
        set { _volume = value; }
    }

    [SerializeField]
    private bool _isFullScreen;
    public bool IsFullScreen
    {
        get { return _isFullScreen; }
        set { _isFullScreen = value; }
    }

    [SerializeField]
    private int _resolutionIndex;
    public int ResolutionIndex
    {
        get { return _resolutionIndex; }
        set { _resolutionIndex = value; }
    }
}
    