using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
	[SerializeField][Min(0)] private float _volumeRate;

	private AudioSource _audioSource;
	private bool _isCollide;
	private int _thiefCount = 0;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		_audioSource.volume = 0;
	}

	private void Update()
	{
		if (_isCollide)
		{
			_audioSource.volume += _volumeRate * Time.deltaTime;
		}
		else
		{
			_audioSource.volume -= _volumeRate * Time.deltaTime;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Thief>(out _))
		{
			_thiefCount++;
			_isCollide = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Thief>(out _))
		{
			_thiefCount--;
			_isCollide = _thiefCount > 0;
		}
	}
}
