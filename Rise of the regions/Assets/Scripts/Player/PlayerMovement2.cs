using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{
    Vector2 moveVector;
    public float moveSpeed = 0f;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void InputPlayer(InputAction.CallbackContext _context)
    {
        animator.SetBool("isWalking", true);

        if(_context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveVector.x);
            animator.SetFloat("LastInputY", moveVector.y);
        }

        moveVector = _context.ReadValue<Vector2>();

        animator.SetFloat("InputX", moveVector.x);
        animator.SetFloat("InputY", moveVector.y);
    }


    private void FixedUpdate()
    {
        rb.linearVelocity = moveVector * moveSpeed;
    }

}
