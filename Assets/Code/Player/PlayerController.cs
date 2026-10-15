using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _runSpeed;
    [SerializeField] private float _walkSpeed;
    
    [SerializeField] private float _jumpForce;
    
    private float _currentSpeed;
    
    private Vector2 _moveInput;

    private void Start()
    {
        _currentSpeed = _walkSpeed;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void FixedUpdate()
    {
        var move = GetForward() * _moveInput.y + GetRight() * _moveInput.x;
        
        _rigidbody.AddForce(move.normalized * _currentSpeed, ForceMode.Impulse);
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    public void OnRun(InputValue value)
    {
        _currentSpeed = value.Get<float>() > 0.5f ? _runSpeed : _walkSpeed;
    }
        
    private Vector3 GetForward()
    {
        var forward = _cameraTransform.forward;
        forward.y = 0;
        
        return forward;
    }

    private Vector3 GetRight()
    {
        var right = _cameraTransform.right;
        right.y = 0;
        
        return right;
    }
    
    private bool IsGrounded()
    {
        var bounds = _capsuleCollider.bounds;
        var radius = _capsuleCollider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);

        return Physics.CheckSphere(
            new Vector3(bounds.center.x, bounds.min.y + radius, bounds.center.z),
            radius,
            LayerMask.GetMask("ground"),
            QueryTriggerInteraction.Ignore);
    }
}
