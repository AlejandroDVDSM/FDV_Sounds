using UnityEngine;
using Random = System.Random;

public class PlayerMovementAudio : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0f;

    [SerializeField] private AudioClip[] _audioClips;
    
    private AudioSource _audioSource;
    private Random _random;
    
    public float MoveSpeed => _moveSpeed;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _random = new Random();
    }

    void Update()
    {
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");

        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = _audioClips[_random.Next(0, _audioClips.Length - 1)];
                _audioSource.Play();
            }
            transform.Translate(Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime);
        }
        
    }
}
