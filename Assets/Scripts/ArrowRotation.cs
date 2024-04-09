using DG.Tweening;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
	[SerializeField] private Roulette _roulette;

	private const float DURATION = 0.15f;
	private const float MAX_START_ANGLE = 40f;

	private Tween _tween;
	private Vector3 _targetAngle= new Vector3(0,0,65f);

	private void OnEnable()
	{
		_roulette.OnWheelSectionHit += OnWheelSectionHitHandler;
	}
	private void OnWheelSectionHitHandler()
	{
		if (transform.rotation.eulerAngles.z > MAX_START_ANGLE)
			return;

		_tween.Kill();
		_tween = transform.DORotate(_targetAngle, DURATION)
			.OnComplete(() =>
			{
				_tween = transform.DORotate(Vector3.zero, DURATION);
			});
	}
	private void OnDisable()
	{
		_roulette.OnWheelSectionHit -= OnWheelSectionHitHandler;
	}
}
