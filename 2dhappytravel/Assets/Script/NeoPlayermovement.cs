using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoPlayermovement : MonoBehaviour
{
    public PlayerData Data; //Scriptable Object which holds players movement params

    #region COMPONETS
    public Rigidbody2D RB { get; private set; }

    public Animator AnimHandler { get; private set; }
    #endregion

    #region STATE PARAMETERS
    //boolean variables which control if player could do certain movement
    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsWalljumping { get; private set; }
    public bool IsDashing { get; private set; }
    public bool IsSliding { get; private set; }

    //Timers for state
    public float LastOnGroundTime { get; private set; }
    public float LastOnWallTIme { get; private set; }
    public float LastOnWallRightTime { get; private set; }
    public float LastOnWallLeftTime { get; private set; }

    //Jump
    private bool _isJumpCut;
    private bool _isJumpFalling;

    //Wall Jump
    private float _wallJumpStartTime;
    private int _lastWallJumpDir;

    //Dash
    private int _dashsLeft;
    private bool _dashRefilling;
    private Vector2 _lastDashDir;
    private bool _isDashAttacking;
    #endregion

    #region INPUT PARAMETERS
    private Vector2 _moveInput;

    public float LastPresedJumpTime { get; private set; }
    public float LastPressedDashTime { get; private set; }
    #endregion

    #region CHECK PARAMTERS
    [Header("Checks")]
    //groundCheck
    [SerializeField] private Transform _groundCheckPoint;

    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    //WallCheck
    [Space(5)]
    [SerializeField] private Transform _frontWallCheckPoint;
    [SerializeField] private Transform _backWallCheckPoint;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
    #endregion

    #region LAYERS & TAGS
    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;
    #endregion

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        AnimHandler = GetComponent<Animator>();
    }

    #region INPUT CALLBACKS
    //Methods which handle input detected in Update()

    public void OnJumpInput()
    {
        LastPressedDashTime = Data.jumpInputBufferTime;
    }

    public void OnJumpUpInput()
    {
        if(CanJumpCut() || CanWallJumpCut())
        {
            _isJumpCut = true;
        }
    }

    public void OnDashInput()
    {
        LastPressedDashTime = Data.dashInputBufferTime;
    }
    #endregion

    #region GENERAL METHODS

    public void SetGravityScale(float scale)
    {
        RB.gravityScale = scale;
    }

    private void Sleep(float duration)
    {
        StartCoroutine(nameof(PerformSleep), duration);
    }

    private IEnumerator PerformSleep(float duration)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration); //Must be Realtime since timeScale will be 0
        Time.timeScale = 1;
    }
    #endregion

    #region RUN METHODS
    private void Run() { 

    }
    #endregion


    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }

    private bool CanJump()
    {
        return LastOnGroundTime > 0 && !IsJumping;
    }

    private bool CanWallJump()
    {
        return LastPressedJumpTime > 0 && LastOnWallTime > 0 && LastOnGroundTime <= 0 && (!IsWallJumping ||
             (LastOnWallRightTime > 0 && _lastWallJumpDir == 1) || (LastOnWallLeftTime > 0 && _lastWallJumpDir == -1));
    }

    private bool CanJumpCut()
    {
        return IsJumping && RB.velocity.y > 0;
    }

    private bool CanWallJumpCut()
    {
        return IsWallJumping && RB.velocity.y > 0;
    }

    private bool CanDash()
    {
        if (!IsDashing && _dashesLeft < Data.dashAmount && LastOnGroundTime > 0 && !_dashRefilling)
        {
            StartCoroutine(nameof(RefillDash), 1);
        }

        return _dashesLeft > 0;
    }

    public bool CanSlide()
    {
        if (LastOnWallTime > 0 && !IsJumping && !IsWallJumping && !IsDashing && LastOnGroundTime <= 0)
            return true;
        else
            return false;
    }
    #endregion


    #region EDITOR METHODS
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_frontWallCheckPoint.position, _wallCheckSize);
        Gizmos.DrawWireCube(_backWallCheckPoint.position, _wallCheckSize);
    }
    #endregion
}