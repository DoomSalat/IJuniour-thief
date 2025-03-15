using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovableController : MonoBehaviour
{
	[SerializeField][Min(0)] private float _speed = 5;

	private Rigidbody2D _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		_rigidbody.linearVelocityX = Input.GetAxis("Horizontal") * _speed;
	}
}
