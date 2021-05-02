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

    private Rigidbody2D rigidbody2d;
    private Animator animator;

    private void Start()
    {
        this.rigidbody2d = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        this.currentSpeed = new Vector2(this.rigidbody2d.velocity.x, this.rigidbody2d.velocity.y);

        // Debug.Log(transform.up);

        // clamp to max speed
        if (this.currentSpeed.magnitude > this.maxSpeed) {
            this.currentSpeed = this.currentSpeed.normalized;
            this.currentSpeed *= this.maxSpeed;
        }

        if (Input.GetKey(KeyCode.W)) {
            this.rigidbody2d.AddForce(transform.up * this.accelerationPower);
            this.rigidbody2d.drag = this.friction;

            this.animator.Play("car_idle");
        }

        if (Input.GetKey(KeyCode.S)) {
            this.rigidbody2d.AddForce(-transform.up * this.accelerationPower);
            this.rigidbody2d.drag = this.friction;

            this.animator.Play("car_idle");
        }

        if (Input.GetKey(KeyCode.A) && this.currentSpeed.magnitude > 0.1) {
            transform.Rotate(Vector3.forward * this.steeringPower);

            this.animator.Play("car_left");
        }

        if (Input.GetKey(KeyCode.D) && this.currentSpeed.magnitude > 0.1) {
            transform.Rotate(-Vector3.forward * this.steeringPower);       
                        
            this.animator.Play("car_right");
        }
    }
}
