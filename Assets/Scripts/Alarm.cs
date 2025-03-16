using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
	private const float MaxVolume = 1;
	private const float MinVolume = 0;

	[SerializeField][Min(0)] private float _volumeRate;
	[SerializeField] private ThiefDetector _thiefDetector;

	private AudioSource _audioSource;
	private Coroutine _soundRoutine;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		_audioSource.volume = 0;
	}

	private void OnEnable()
	{
		_thiefDetector.BustedIn += ActiveVolume;
		_thiefDetector.BustedOut += DeactiveVolume;
	}

	private void OnDisable()
	{
		_thiefDetector.BustedIn -= ActiveVolume;
		_thiefDetector.BustedOut -= DeactiveVolume;
	}

	private void ActiveVolume()
	{
		SetVolume(MaxVolume);
	}

	private void DeactiveVolume()
	{
		SetVolume(MinVolume);
	}

	private void SetVolume(float targetVolume)
	{
		if (_soundRoutine != null)
		{
			StopCoroutine(_soundRoutine);
		}

		_soundRoutine = StartCoroutine(ChangeVolume(targetVolume));
	}

	private IEnumerator ChangeVolume(float targetVolume)
	{
		float step = (targetVolume > _audioSource.volume) ? _volumeRate : -_volumeRate;

		while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
		{
			_audioSource.volume = Mathf.Clamp(_audioSource.volume + step * Time.deltaTime, 0, 1);

			yield return null;
		}
	}
}
