using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 15f;

    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontal, 0f, vertical).normalized;
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * speed, ForceMode.Force);
    }
}