using UnityEngine;

public class AudioSpeed : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.volume = other.gameObject.GetComponent<PlayerMovement>().MoveSpeed / 100;
                _audioSource.Play();
            }
        }
    }
}
