using UnityEngine;

public class PlayOnCollision : MonoBehaviour
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
                _audioSource.Play();
        }
    }
}
