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

    [SerializeField]
    public Animator smokeAnimator;

    private Vector2 currentSpeed;

    private Rigidbody2D rigidbody2d;
    private Animator carAnimator;

    private void Start()
    {
        this.rigidbody2d = GetComponent<Rigidbody2D>();
        this.carAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        this.currentSpeed = new Vector2(this.rigidbody2d.velocity.x, this.rigidbody2d.velocity.y);

        // Debug.Log(transform.up);

        if (this.currentSpeed.magnitude > 0.1) {
            this.smokeAnimator.Play("car_smoke");
        } else {
            this.smokeAnimator.Play("car_smoke_idle");
        }

        // clamp to max speed
        if (this.currentSpeed.magnitude > this.maxSpeed) {
            this.currentSpeed = this.currentSpeed.normalized;
            this.currentSpeed *= this.maxSpeed;
        }

        if (Input.GetKey(KeyCode.W)) {
            this.rigidbody2d.AddForce(transform.up * this.accelerationPower);
            this.rigidbody2d.drag = this.friction;

            this.carAnimator.Play("car_idle");
        }

        if (Input.GetKey(KeyCode.S)) {
            this.rigidbody2d.AddForce(-transform.up * this.accelerationPower);
            this.rigidbody2d.drag = this.friction;

            this.carAnimator.Play("car_idle");
        }

        if (Input.GetKey(KeyCode.A) && this.currentSpeed.magnitude > 0.1) {
            transform.Rotate(Vector3.forward * this.steeringPower);

            this.carAnimator.Play("car_left");
        }

        if (Input.GetKey(KeyCode.D) && this.currentSpeed.magnitude > 0.1) {
            transform.Rotate(-Vector3.forward * this.steeringPower);       
                        
            this.carAnimator.Play("car_right");
        }
    }
}
