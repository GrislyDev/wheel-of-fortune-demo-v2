using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AwardMessage : MonoBehaviour
{
	[SerializeField] private Text _awardText;
	[SerializeField] private RectTransform _rectTransform;

	private const float SCALE_DURATION = 2f;
	private const float STANDARD_SCALE = 1f;

	private Tween _tween;

	public void ShowAwardMessage(int award)
	{
		_awardText.text = $"You won {award} coins!";
		_rectTransform.localScale = Vector3.zero;
		gameObject.SetActive(true);

		_tween.Kill();
		_tween = _rectTransform.DOScale(STANDARD_SCALE, SCALE_DURATION).
			OnComplete(() =>
			{
				gameObject.SetActive(false);
			});
	}
}
