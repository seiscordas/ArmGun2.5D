using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DController : MonoBehaviour
{
    private Animator _animator;
    public bool isGrounded;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _aim;
    [SerializeField] private float _flipAngle = -90f;// serializedfieldo só para debug
    [SerializeField] private float _flipSpeed = 30f;
    [SerializeField] private float _walkSpeed = 10f;
    AimSystem aimSystem = new AimSystem();
    private Rigidbody _rigidbody;
    public bool facingRight = true;
    public bool _isFliping = false;
    public float touchRun = 0;//public para debug
    private float _aimPositionX;
    [SerializeField] private bool IsActiveAim = false;// serializedfieldo só para debug


    //debug inspector
    [Header("Debug Inspector")]
    public Vector3 debugAimPosition;
    private float _flipDirection;

    private void Awake()
    {
        aimSystem = _player.GetComponent(typeof(AimSystem)) as AimSystem;
        aimSystem.Player = _player;
    }
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.Linecast(transform.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        _animator.SetBool("isGrounded", isGrounded);
        if (Input.GetButtonDown("Fire2"))
        {
            IsActiveAim = !IsActiveAim;
        }
        debugAimPosition = _aim.position - _player.transform.position;//debug
        _aimPositionX = _aim.position.x - _player.transform.position.x;
        touchRun = Input.GetAxisRaw("Horizontal");

    }
    private bool IsFlipToRight()
    {
        if (touchRun != 0)
            _flipDirection = touchRun;
        if (((IsActiveAim && _aimPositionX > 0) || (!IsActiveAim && _flipDirection > 0)) && _flipAngle > -90f)
        {
            _isFliping = true;
            return true;
        }
        return false;
    }
    private bool IsFlipToLeft()
    {
        if (touchRun != 0)
            _flipDirection = touchRun;
        if (((IsActiveAim && _aimPositionX < 0) || (!IsActiveAim && _flipDirection < 0)) && _flipAngle < 90f)
        {
            _isFliping = true;
            return true;
        }
        return false;
    }
    private void FixedUpdate()
    {
        if (IsFlipToLeft())
            Flip(_flipSpeed);
        if (IsFlipToRight())
            Flip(-_flipSpeed);
        if (!_isFliping && isGrounded && _flipDirection != 0f)
            Walk();
    }
    private void Walk()
    {
        _animator.SetFloat("H_Walk", touchRun);
    }
    private void Flip(float speed)
    {
        _flipAngle += speed;
        _player.transform.Rotate(0, speed, 0);
        facingRight = (_flipAngle == -90f);
        if (_flipAngle == -90f || _flipAngle == 90f)
        {
            _flipDirection = 0;
            _isFliping = false;
        }
    }
}
//TODO:
//MUDA O PLAYER PRA UM HUMANO NORMAL DO SITE MIXANO
//player não está virarndo usando as setas quando IsActiveAim == false
//animação do player andando
//pular
//mirar com o braço