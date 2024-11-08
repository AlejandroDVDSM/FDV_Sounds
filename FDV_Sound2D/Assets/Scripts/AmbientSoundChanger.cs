using UnityEngine;

public class AmbientSoundChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _ambientAudioSource;
    [SerializeField] private AudioClip _birdsClip;
    [SerializeField] private AudioClip _cafeteriaClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _ambientAudioSource.clip = _cafeteriaClip;
            _ambientAudioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _ambientAudioSource.clip = _birdsClip;
            _ambientAudioSource.Play();
        }
    }
}
