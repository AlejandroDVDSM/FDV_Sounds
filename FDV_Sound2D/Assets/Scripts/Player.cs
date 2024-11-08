using System;
using Cinemachine;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private int _playerHP;
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private float _jumpForce = 7.5f;

    [Header("UI")]
    [SerializeField] private TMP_Text _scoreTxt;
    [SerializeField] private TMP_Text _hpTxt;

    [Header("SFX")]
    [SerializeField] private AudioClip[] _stepSounds; 
    [SerializeField] private AudioClip _landingSound;
    [SerializeField] private AudioClip _dmgSound;

    private float _horizontalMovement;
    private Vector3 _direction;
    private bool _isJumping;
    private bool _isGrounded;

    private int _collectedItems;
    private bool _receivingDamage;
    
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;
    private CinemachineImpulseSource _impulseSource;
    private Random _random;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _random = new Random();
        
        _hpTxt.text = $"HP: {_playerHP}"; 
    }

    private void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (_horizontalMovement != 0 & !_isJumping)
        {
            _animator.SetBool(IsWalking, true);
            _spriteRenderer.flipX = _horizontalMovement > 0;
            
            _direction = new Vector3(_horizontalMovement, 0, 0).normalized;
        }
        else
            _animator.SetBool(IsWalking, false);
        
        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
            _isJumping = true;
        
    }

    private void FixedUpdate()
    {
        if (_horizontalMovement != 0 && _isGrounded)
        {
            _rigidbody2D.MovePosition(transform.position + _direction * (_moveSpeed * Time.fixedDeltaTime));

            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = _stepSounds[_random.Next(0, _stepSounds.Length - 1)];
                _audioSource.Play();
                
            }
        }

        if (_isJumping && _isGrounded)
        {
            _isGrounded = false;
            _rigidbody2D.AddForce(new Vector2(_horizontalMovement / 2, 1) * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log($"<{gameObject.name}> has collided with <{other.gameObject.name}>");

        var otherGO = other.gameObject;
        
        if (otherGO.CompareTag("Ground"))
        {
            _isJumping = false;
            _isGrounded = true;
            _rigidbody2D.velocity = Vector2.zero; // Cancel any remaining speed
            
            _audioSource.clip = _landingSound;
            _audioSource.Play();
        }

        if (otherGO.CompareTag("Item"))
        {
            if (otherGO.GetComponent<Item>().ItemType == EItemType.Reward)
            {
                _collectedItems++;
                _scoreTxt.text = $"Score: {_collectedItems}";
            }

            if (otherGO.GetComponent<Item>().ItemType == EItemType.Healing)
            {
                _playerHP++;
                _hpTxt.text = $"HP: {_playerHP}";
            }
        }

        if (otherGO.CompareTag("Enemy"))
        {
            _impulseSource.GenerateImpulse();
            _audioSource.clip = _dmgSound;
            _audioSource.Play();
            _playerHP--;
            _hpTxt.text = $"HP: {_playerHP}";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SpecialWalls"))
        {
            
        }
    }
}