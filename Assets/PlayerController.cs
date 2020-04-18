using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float speed = 10;
    public float jumpForce = 11;

    public float gravity = -20;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public Animator animator;
    public Transform modelTransform;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        animator.SetFloat("speed", Mathf.Abs(hInput) + Mathf.Abs(vInput));

        direction.x = hInput * speed;
        direction.z = vInput * speed;

        if(hInput != 0 || vInput != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(hInput, 0, vInput));
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, newRotation, 0.7f);
        }

        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded)
        {
            direction.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }

        controller.Move(direction * Time.deltaTime);
    }
}
