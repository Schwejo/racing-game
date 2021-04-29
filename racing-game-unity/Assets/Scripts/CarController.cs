using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    public float accelerationPower = 50f;

    [SerializeField]
    public float steeringPower = 5f;

    [SerializeField]
    public float friction = 5f;

    [SerializeField]
    public float maxSpeed = 100f;

    private Vector2 currentSpeed;

    private Rigidbody2D rb;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        this.currentSpeed = new Vector2(this.rb.velocity.x, this.rb.velocity.y);

        // Debug.Log(transform.up);

        // clamp to max speed
        if (this.currentSpeed.magnitude > this.maxSpeed) {
            this.currentSpeed = this.currentSpeed.normalized;
            this.currentSpeed *= this.maxSpeed;
        }

        if (Input.GetKey(KeyCode.W)) {
            this.rb.AddForce(transform.up * this.accelerationPower);
            this.rb.drag = this.friction;
        }

        if (Input.GetKey(KeyCode.S)) {
            this.rb.AddForce(-transform.up * this.accelerationPower);
            this.rb.drag = this.friction;
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * this.steeringPower);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * this.steeringPower);            
        }
    }
}
