using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
	public float speed;
	
	public TextMeshProUGUI countText;
	public GameObject winTextObject;
	
	public LayerMask groundLayers;
	public float jumpForce = 7;

	private float movementX;
	private float movementY;

	private SphereCollider col;
	private Rigidbody rb;
	private int count;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		count = 0;

		SetCountText();

		winTextObject.SetActive(false);

		col = GetComponent<SphereCollider>();
	}

	void FixedUpdate()
	{
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);

		if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);

			count = count + 1;

			SetCountText();
		}
	}

	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 12)
		{
			winTextObject.SetActive(true);
		}
	}
	private bool IsGrounded()
    {
		return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
			col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }
}