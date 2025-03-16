using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ThiefDetector : MonoBehaviour
{
	private const int First = 1;
	private const int None = 0;
	private int _thiefCount = None;

	public event Action BustedIn;
	public event Action BustedOut;

	private void Awake()
	{
		if (GetComponent<Collider2D>().isTrigger == false)
		{
			Debug.LogWarning($"{gameObject.name} trigger is disabled.");
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Thief>(out _))
		{
			_thiefCount++;

			if (_thiefCount == First)
			{
				BustedIn?.Invoke();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Thief>(out _))
		{
			_thiefCount--;

			if (_thiefCount == None)
			{
				BustedOut?.Invoke();
			}
		}
	}
}
