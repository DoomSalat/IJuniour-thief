using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
	[SerializeField][Min(0)] private float _volumeRate;

	private AudioSource _audioSource;
	private int _thiefCount = 0;
	private Coroutine _soundRoutine;
	private bool _isOn;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		_audioSource.volume = 0;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Thief>(out _))
		{
			_thiefCount++;
			ActiveVolumeRoutine(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Thief>(out _))
		{
			_thiefCount--;

			if (_thiefCount == 0)
			{
				ActiveVolumeRoutine(false);
			}
		}
	}

	private void ActiveVolumeRoutine(bool activeOn)
	{
		if (activeOn != _isOn)
		{
			if (_soundRoutine != null)
			{
				StopCoroutine(_soundRoutine);
			}

			_isOn = activeOn;

			if (activeOn)
			{
				_soundRoutine = StartCoroutine(SoundOn());
			}
			else
			{
				_soundRoutine = StartCoroutine(SoundOff());
			}
		}
	}

	private IEnumerator SoundOn()
	{
		while (_audioSource.volume < 1)
		{
			_audioSource.volume += _volumeRate * Time.deltaTime;

			yield return null;
		}
	}

	private IEnumerator SoundOff()
	{
		while (_audioSource.volume > 0)
		{
			_audioSource.volume -= _volumeRate * Time.deltaTime;

			yield return null;
		}
	}
}
