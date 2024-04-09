using UnityEngine;
using System;
using DG.Tweening;

public class Roulette : MonoBehaviour
{
	public event Action OnWheelSectionHit;

	[SerializeField] private int[] _sectorCoinsReward;
	[SerializeField] private RewardManager _rewardManager;

	private const float ROTATION_TIME_PER_SECTION = 0.1f;
	private const float FULL_ROTATION = 360f;
	private const float TWO_FULL_ROTATION = -720f;
	private const float ARROW_ANGLE_OFFSET = 4f;

	private Tween _tween;
	private Vector3 _spinAngle;
	private int _winSection;
	private float _rotateDuration;
	private float _winSectionAngle;
	private float _sectionAngle;
	private float _newAngle;
	private float _nextSectionAngle;

	#region UNITY_ENGINE
	private void Awake()
	{
		_sectionAngle = FULL_ROTATION / _sectorCoinsReward.Length;
		_spinAngle = Vector3.zero;
		_nextSectionAngle = FULL_ROTATION - (_sectionAngle - ARROW_ANGLE_OFFSET);
	}
	#endregion
	#region PUBLIC_METHODS
	public void Spin()
	{
		if (_tween != null && _tween.IsActive())
			return;

		CalculateWinSection();

		_tween.Kill();
		_tween = transform.DORotate(_spinAngle, _rotateDuration, RotateMode.FastBeyond360)
			.OnComplete(() =>
			{
				_rewardManager.DistributeReward(_sectorCoinsReward[_winSection]);
			})
			.OnUpdate(() =>
			{
				CheckSectionHit();
			});
	}
	#endregion
	#region PRIVATE_METHODS
	private void CalculateWinSection()
	{
		_winSection = UnityEngine.Random.Range(0, _sectorCoinsReward.Length);
		_winSectionAngle = _winSection * _sectionAngle;
		_spinAngle.z = TWO_FULL_ROTATION - _winSectionAngle;
		_rotateDuration = Mathf.Abs(_spinAngle.z / _sectionAngle * ROTATION_TIME_PER_SECTION);
	}
	private void CheckSectionHit()
	{
		_newAngle = transform.rotation.eulerAngles.z;

		if (_newAngle < _nextSectionAngle && !(_nextSectionAngle -_newAngle > _sectionAngle))
		{
			_nextSectionAngle -= _sectionAngle;

			if (_nextSectionAngle < 0)
				_nextSectionAngle = FULL_ROTATION - (_sectionAngle - ARROW_ANGLE_OFFSET);
			OnWheelSectionHit?.Invoke();
		}
	}
	#endregion
}
