using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	#region SIngleton:Game

	public static Game Instance;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	#endregion

	[SerializeField] Text[] allCoinsUIText;

	public int Coins;

	void Start ()
	{
		UpdateAllCoinsUIText ();
	}

	public void UseCoins (int amount)
	{
		Coins += amount;
		UpdateAllCoinsUIText();
	}
	public void RemoveCoins(int amount)
	{
		Coins -= amount;
		UpdateAllCoinsUIText();
	}
	public bool HasEnoughCoins (int amount)
	{
		return true;
	}

	public void UpdateAllCoinsUIText ()
	{
		for (int i = 0; i < allCoinsUIText.Length; i++) {
			allCoinsUIText [i].text ="$ "+ Coins.ToString ();
		}
	}

}
