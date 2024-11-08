using UnityEngine;

public class MovingAudio : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10.0f;

    [SerializeField] private AudioSource _audioSource;

    private bool _isMovementEnabled;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _isMovementEnabled = !_isMovementEnabled;

            if (!_audioSource.isPlaying)
            {
                Debug.Log($"Key <P> pressed. Playing audio: {_audioSource.clip.name}");
                _audioSource.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (_audioSource.isPlaying)
            {
                Debug.Log($"Key <S> pressed. Stopping audio: {_audioSource.clip.name}");
                _audioSource.Stop();
            }
        }

        if (_isMovementEnabled)
            transform.Translate(_moveSpeed * Time.deltaTime, 0, 0);
    }
}