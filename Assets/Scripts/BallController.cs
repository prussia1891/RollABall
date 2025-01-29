using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody sphereRigidbody;
    [SerializeField] private float ballSpeed = 2f;
    [SerializeField] private float jumpForce = 5f;

    private bool isGrounded; // check if the ball is on the ground
    private float jumpCharge = 0f; // the force to jump

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void MoveBall(Vector2 input)
    {
        Vector3 inputXZPlane = new(input.x, 0, input.y);
        sphereRigidbody.AddForce(inputXZPlane * ballSpeed);
    }

    public void JumpBall()
    {
        if (isGrounded)
        {
            jumpCharge += Time.deltaTime * 10; // add the force
            jumpCharge = Mathf.Clamp(jumpCharge, 0, jumpForce * 2); // limit the force
        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            sphereRigidbody.AddForce(Vector3.up * jumpCharge, ForceMode.Impulse);
            isGrounded = false; // not on the ground
            jumpCharge = 0f; // reset the force
        }
    }

    // check if the ball is on the ground
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            isGrounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        JumpBall();
    }
}
