using UnityEngine;
using UnityEngine.UI;

public class CoinsView : MonoBehaviour
{
	[SerializeField] private Text _coinsText;
	[SerializeField] private Wallet _wallet;

	private void OnEnable()
	{
		_wallet.CoinsBalanceChanged += CoinsBalanceChangedHandler;
	}
	private void CoinsBalanceChangedHandler(int coins)
	{
		_coinsText.text = $"Coins: {coins:000000}";
	}
	private void OnDisable()
	{
		_wallet.CoinsBalanceChanged -= CoinsBalanceChangedHandler;
	}
}
