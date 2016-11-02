using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour {
	private bool isGrounded;
	private bool isMoving;
	public float walkSpeed = 2.0f;
	public float jumpSpeed = 5.0f;
	private const float COOLDOWN = 1.0f;
	private float jumpCooldown;
	private const float ROTATION_LIMIT = 30.0f;
	public enum Direction {LEFT = -1, RIGHT = 1};
	public Direction defaultDirection = Direction.LEFT;
	private int currentDirection = (int)Direction.LEFT;
	private Animator animator;
	[SerializeField]
	private LayerMask groundLayer;
	[SerializeField]
	private Transform[] groundDetectors;

	void Awake() {
		animator = this.GetComponent<Animator>();
		StartCoroutine(StateUpdate());
	}

	void Start() {
		GetComponent<Rigidbody2D>().centerOfMass = Vector2.zero;
		jumpCooldown = 0.0f;
		ResetCurrentDirection();
	}

	void OnDestroy() {
		StopCoroutine(StateUpdate());
	}

	private IEnumerator StateUpdate() {
		while (true) {
			GroundCheck();
			RotationControll();
			if (jumpCooldown > 0.0f) {
				jumpCooldown -= Time.fixedDeltaTime;
			}
			if (animator != null) {
				animator.SetBool("isGrounded", isGrounded);
				animator.SetBool("isMoving", isMoving);
				animator.SetFloat("vy", GetComponent<Rigidbody2D>().velocity.y);
			}
			yield return new WaitForFixedUpdate();
		}
	}

	void Update() {
		Move();
	}

	public Direction GetCurrentDirection() {
		return (Direction)currentDirection;
	}

	public void ResetCurrentDirection() {
		if ((Direction)currentDirection != defaultDirection) {
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
			currentDirection = (int)defaultDirection;
		}
	}

	public bool GroundCheck() {
		isGrounded = false;
		foreach (Transform groundDetector in groundDetectors) {
			isGrounded |= Physics2D.Linecast(transform.position, groundDetector.position, groundLayer);
		}
		return isGrounded;
	}

	private void Move() {
		isMoving = false;
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			Walk(Direction.LEFT);
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			Walk(Direction.RIGHT);
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			Jump();
	}

	public void Walk(Direction direction) {
		if (!GroundCheck())
			return;
		Vector3 scale = transform.localScale;
		Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
		scale.x *= currentDirection * (int)direction;
		currentDirection = (int)direction;
		velocity.x = currentDirection * Vector3.Project(Vector3.right * walkSpeed, transform.right).x;
		transform.localScale = scale;
		GetComponent<Rigidbody2D>().velocity = velocity;
		isMoving = true;
	}

	public void Jump() {
		if (jumpCooldown > 0.0f)
			return;
		if (!GroundCheck())
			return;
		Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
		velocity.y = jumpSpeed;
		jumpCooldown = COOLDOWN;
		if (animator != null) 
			animator.SetTrigger("jump");
		GetComponent<Rigidbody2D>().velocity = velocity;
	}

	public void Halt() {
		isMoving = false;
		if (!GroundCheck())
			return;
		Vector3 velocity = GetComponent<Rigidbody2D>().velocity;
		velocity.x = 0.0f;
		GetComponent<Rigidbody2D>().velocity = velocity;
	}

	private void RotationControll() {
		Vector3 rotation = transform.eulerAngles;
		if (rotation.z > ROTATION_LIMIT && rotation.z <= 180.0f) {
			rotation.z = ROTATION_LIMIT;
		}
		if (rotation.z < 360.0f - ROTATION_LIMIT && rotation.z > 180.0f) {
			rotation.z = 360.0f - ROTATION_LIMIT;
		}
		transform.eulerAngles = rotation;
	}
}
