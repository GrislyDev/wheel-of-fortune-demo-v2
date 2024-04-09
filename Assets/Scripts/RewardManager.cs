using System;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
	public event Action<int> CoinsAwarded;

	public void DistributeReward(int coinsAwarded)
	{
		CoinsAwarded?.Invoke(coinsAwarded);
	}
}