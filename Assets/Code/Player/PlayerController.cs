using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _runSpeed = 18.75f;
    [SerializeField] private float _walkSpeed = 12.5f;
    
    [SerializeField] private float _jumpForce = 50f;
    [SerializeField] private LayerMask _groundLayer;
    
    [SerializeField] private Vector3 _crouchCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 _standCenter  = new Vector3(0, 0, 0);
    
    [SerializeField] private float _crouchHeight = 1f;
    [SerializeField] private float _standHeight  = 2f;
    
    private Vector2 _moveInput;
    
    private float _currentSpeed;

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

    public void OnJump(InputValue value)
    {
        if (IsGrounded() && value.isPressed)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Acceleration);
        }
    }

    public void OnRun(InputValue value)
    {
        _currentSpeed = value.isPressed ? _runSpeed : _walkSpeed;
    }

    public void OnCrouch(InputValue value)
    {
        if (value.isPressed)
        {
            _capsuleCollider.center = _crouchCenter;
            _capsuleCollider.height = _crouchHeight;
        }

        else
        {
            _capsuleCollider.center = _standCenter;
            _capsuleCollider.height = _standHeight;
        }
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
            new Vector3(bounds.center.x, bounds.min.y + radius * 0.90f, bounds.center.z),
            radius,
            _groundLayer,
            QueryTriggerInteraction.Ignore);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (_capsuleCollider == null) _capsuleCollider = GetComponent<CapsuleCollider>();
        if (_capsuleCollider == null) return;

        var bounds = _capsuleCollider.bounds;
        var radius = _capsuleCollider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);
        Vector3 sphereCenter = new Vector3(bounds.center.x, bounds.min.y + radius * 0.90f, bounds.center.z);

        Gizmos.color = Color.green; // Цвет сферы в окне Scene
        Gizmos.DrawWireSphere(sphereCenter, radius);
    }

}
