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
    [SerializeField] private float _flipAngle = -90f;
    [SerializeField] private float _flipSpeed = 30f;
    [SerializeField] private float _walkSpeed = 10f;
    private AimSystem aimSystem;
    [SerializeField] private LaserSystem laserSystem;
    private Rigidbody _rigidbody;
    public bool _facingRight = true;//public para debug
    public bool _isFliping = false;//public para debug
    public float _horizontalAxis = 0;//public para debug
    public float _verticallAxis = 0;//public para debug
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
        laserSystem.Player = this;
    }
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        IsGrounded();

        _animator.SetBool("isGrounded", isGrounded);
        if (Input.GetButtonDown("Fire2"))
        {
            IsActiveAim = !IsActiveAim;
            _animator.SetBool("isAim", IsActiveAim);
        }
        debugAimPosition = _aim.position - _player.transform.position;//debug
        _aimPositionX = _aim.position.x - _player.transform.position.x;
        _horizontalAxis = Input.GetAxisRaw("Horizontal");
        _verticallAxis = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump"))
        {
            //StartCoroutine(JumpingUp());
        }

    }

    private void IsGrounded()
    {
        RaycastHit raycastHit;

        Color rayColor;
        if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, 0.5f))
        {
            isGrounded = false;
            rayColor = Color.green;
        }
        else
        {
            isGrounded = true;
            rayColor = Color.red;
        }
        Debug.DrawRay(transform.position, raycastHit.point);
    }


    private void FixedUpdate()
    {
        //if (IsFlipToLeft())
        //    Flip(_flipSpeed);
        //if (IsFlipToRight())
        //    Flip(-_flipSpeed);
        //if (!_isFliping && isGrounded && _flipDirection != 0f)
        //    Walk();

    }
    private bool IsFlipToRight()
    {
        if (_horizontalAxis != 0)
            _flipDirection = _horizontalAxis;
        if (((IsActiveAim && _aimPositionX > 0) || (!IsActiveAim && _flipDirection > 0)) && _flipAngle > -90f)
        {
            _isFliping = true;
            return true;
        }
        return false;
    }
    private bool IsFlipToLeft()
    {
        if (_horizontalAxis != 0)
            _flipDirection = _horizontalAxis;
        if (((IsActiveAim && _aimPositionX < 0) || (!IsActiveAim && _flipDirection < 0)) && _flipAngle < 90f)
        {
            _isFliping = true;
            return true;
        }
        return false;
    }
    private IEnumerator JumpingUp()
    {
        _animator.SetBool("Jump", true);
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("Jump", false);
    }
    private void Walk()
    {
        _animator.SetFloat("H_Move", _horizontalAxis);
    }
    private void Flip(float speed)
    {
        _flipAngle += speed;
        _player.transform.Rotate(0, speed, 0);
        _facingRight = (_flipAngle == -90f);
        if (_flipAngle == -90f || _flipAngle == 90f)
        {
            _flipDirection = 0;
            _isFliping = false;
        }
        //_rigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(_aim.position.x - transform.position.x), 0)));
    }
}
//TODO:
//ver sistema de movimento com cardano
//corrigir pulo
//pular erro conhecido player fica parado no ar
//mirar com a arma
//agachar
//andar para traz quando estiver mirando
//Procurar animações melhores
//Colocar clamp na mira com o braço pq fica estranho quando gira de um lada para o outro se a mira estiver muito proximo a cabeça
//CORRIGIR RESOLUÇÃO DO GAME PLAY
//problema conhecido: player para no meio do Flip se clicar com direto, corrigir fazendo condição a mira só é ativada ou desativada se IsFliping == false