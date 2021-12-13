using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
	#region Singlton:Shop

	public static Shop Instance;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy (gameObject);
	}

	#endregion

	[System.Serializable] public class ShopItem
	{
		public Sprite Image;
		public int Price;
		public string Name;
		public int quantity;

		public bool IsPurchased = false;
	}

	public List<ShopItem> ShopItemsList;
 

	[SerializeField] GameObject ItemTemplate;
	GameObject g;
	[SerializeField] Transform ShopScrollView;
	[SerializeField] GameObject ShopPanel;
	Button buyBtn;

	void Start ()
	{
		int len = ShopItemsList.Count;
		for (int i = 0; i < len; i++)
		{
			g = Instantiate(ItemTemplate, ShopScrollView);
			g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
			g.transform.GetChild(1).GetComponent<Text>().text = ShopItemsList[i].Name.ToString();

			buyBtn = g.transform.GetChild(2).GetComponent<Button>();
			if (ShopItemsList[i].IsPurchased)
			{
				DisableBuyButton();
			}
			buyBtn.AddEventListener(i, OnShopItemBtnClicked);
		}
	}
	public void updateList(int id)
    {
		
			buyBtn = ShopScrollView.GetChild(id).GetChild(2).GetComponent<Button>();
		buyBtn.interactable = true;
		buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Добавить";

	}
	void OnShopItemBtnClicked (int itemIndex)
	{
		if (Game.Instance.HasEnoughCoins (ShopItemsList [itemIndex].Price)) {
			Game.Instance.UseCoins (ShopItemsList [itemIndex].Price);
			ShopItemsList [itemIndex].IsPurchased = true;

			buyBtn = ShopScrollView.GetChild (itemIndex).GetChild (2).GetComponent <Button> ();
			DisableBuyButton ();
			Game.Instance.UpdateAllCoinsUIText ();

			Profile.Instance.AddShop (ShopItemsList [itemIndex].Image, ShopItemsList[itemIndex].Name, ShopItemsList[itemIndex].Price, ShopItemsList[itemIndex].quantity);
		} 
	}
	void OpenBuyButton()
	{
		buyBtn.interactable = true;
		buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Добавить";
	}
	void DisableBuyButton ()
	{
		buyBtn.interactable = false;
		buyBtn.transform.GetChild (0).GetComponent <Text> ().text = "Добавлено";
	}
	public void OpenShop ()
	{
		ShopPanel.SetActive (true);
	}

	public void CloseShop ()
	{
		ShopPanel.SetActive (false);
	}

}
