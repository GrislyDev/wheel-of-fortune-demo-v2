using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
	public event Action<int> CoinsBalanceChanged;
	public int Coins => _coins;

	[SerializeField] private RewardManager _rewardManager;

	private int _coins;

	private void OnEnable()
	{
		_rewardManager.CoinsAwarded += CoinsAwardedHandler;
	}

	private void Start()
	{
		_coins = PlayerPrefs.GetInt("Coins", 0);
		CoinsBalanceChanged?.Invoke(_coins);
	}

	private void CoinsAwardedHandler(int coins)
	{
		_coins += coins;
		PlayerPrefs.SetInt("Coins", _coins);
		CoinsBalanceChanged?.Invoke(_coins);
	}

	private void OnDisable()
	{
		_rewardManager.CoinsAwarded -= CoinsAwardedHandler;
	}
}
