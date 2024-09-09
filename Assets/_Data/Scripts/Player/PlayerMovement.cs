using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] VariableJoystick joystick;
    [SerializeField] Character character;
    [SerializeField] Vector3 velocity;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float gravity = -9.81f;

    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {

        if (character.IsInDeadState())
        {
            velocity = Vector3.zero; 
            return; 
        }

        moveDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        if (characterController.isGrounded)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }

        if (moveDirection != Vector3.zero)
        {
            characterController.Move((moveDirection * moveSpeed * Time.fixedDeltaTime) + velocity * Time.fixedDeltaTime);

            transform.parent.rotation = Quaternion.LookRotation(moveDirection);
        }

        characterController.Move(velocity * Time.fixedDeltaTime);
    }
}


