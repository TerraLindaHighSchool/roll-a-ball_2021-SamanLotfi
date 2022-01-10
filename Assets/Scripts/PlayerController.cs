using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpForce;
	
	public TextMeshProUGUI countText;
	public GameObject winTextObject;
	
	public LayerMask groundLayers;

	private float movementX;
	private float movementY;

	private Rigidbody rb;
	private int count;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		count = 0;

		SetCountText();

		winTextObject.SetActive(false);
	}

	void FixedUpdate()
	{
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);
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

	void OnJump()
    {
		rb.AddForce(Vector2.up * jumpForce);
    }

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 19)
		{
			winTextObject.SetActive(true);
		}
	}
}