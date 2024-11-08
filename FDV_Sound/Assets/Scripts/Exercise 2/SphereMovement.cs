using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0f;
    
    private bool _isMovementEnabled;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            _isMovementEnabled = !_isMovementEnabled;
            
        if (_isMovementEnabled)
            transform.Translate(_moveSpeed * Time.deltaTime, 0, 0);
    }
}
