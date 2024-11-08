using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0f;
    
    public float MoveSpeed => _moveSpeed;
    
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * _moveSpeed * Time.deltaTime);
    }
}
