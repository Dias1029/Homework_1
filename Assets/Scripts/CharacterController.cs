using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _speedMultiplyer = 2;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A and D
        float verticalInput = Input.GetAxis("Vertical");  // W and S
        

        Vector3 movement = new Vector3 (horizontalInput, 0, verticalInput);
        movement.Normalize();

        transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, Time.deltaTime * _moveSpeed);
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0, verticalInput));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);

        float calculatedSpeed = Mathf.Clamp(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput), 0, 1);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            calculatedSpeed *= _speedMultiplyer;
            _moveSpeed = Mathf.Clamp(_moveSpeed, 2, _moveSpeed * _speedMultiplyer);
        }

        _animator.SetFloat("MovementSpeed", calculatedSpeed);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _animator.SetBool("Walking", true);
        }
        else
        {
            _animator.SetBool("Walking", false);
        }
    }

    public void JumpButton()
    {
        _animator.SetTrigger("Jump");  
    }
}
