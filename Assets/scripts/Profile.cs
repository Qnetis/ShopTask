using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Profile : MonoBehaviour
{
	#region Singlton:Profile

	public static Profile Instance;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy (gameObject);
	}

	#endregion

	public class Avatar
	{
		public Sprite Image;
	}

	public List<Avatar> AvatarsList;

	[SerializeField] GameObject ShopUITemplate;
	[SerializeField] Transform ShopScrollView;

	GameObject g;
	int newSelectedIndex, previousSelectedIndex;




	void Start ()
	{
		GetAvailableShop ();
		newSelectedIndex = previousSelectedIndex = 0;
	}

	void GetAvailableShop ()
	{
		for (int i = 0; i < Shop.Instance.ShopItemsList.Count; i++) {
			if (Shop.Instance.ShopItemsList [i].IsPurchased) {
				AddShop (Shop.Instance.ShopItemsList [i].Image, Shop.Instance.ShopItemsList[i].Name, Shop.Instance.ShopItemsList[i].Price, Shop.Instance.ShopItemsList[i].quantity);
				Game.Instance.UseCoins(Shop.Instance.ShopItemsList[i].Price);

			}
		}

	}


	public void AddShop (Sprite img,string midlName,int getPrice, int getNumber)
	{
		if (AvatarsList == null)
			AvatarsList = new List<Avatar> ();
		
		Avatar av = new Avatar (){ Image = img };
		AvatarsList.Add (av);
	
		g = Instantiate (ShopUITemplate, ShopScrollView);
		g.transform.GetChild (0).GetComponent <Image> ().sprite = av.Image;
		g.transform.GetChild(1).GetComponent<Text>().text = midlName.ToString();
		g.transform.GetChild(2).GetComponent<Text>().text = "$ " + (getPrice.ToString());
		g.transform.GetChild(3).GetComponent<Text>().text = getNumber.ToString();

			g.transform.GetChild(4).GetComponent <Button> ().AddEventListener (AvatarsList.Count - 1, OnRemoveClick);
			g.transform.GetChild(5).GetComponent <Button> ().AddEventListener (AvatarsList.Count - 1, OnRemoveClick);
			g.transform.GetChild(6).GetComponent <Button> ().AddEventListener (AvatarsList.Count - 1,OnAddClick);

	}
	void OnAddClick(int ShopIndex)
	{
		var nameClass = Shop.Instance.ShopItemsList[ShopIndex];
		if (nameClass.quantity < 99)
		{
			ShopScrollView.GetChild(ShopIndex).GetChild(3).GetComponent<Text>().text = (nameClass.quantity + 1).ToString();
			Shop.Instance.ShopItemsList[ShopIndex].quantity = nameClass.quantity + 1;
			Game.Instance.UseCoins(nameClass.Price);
		}
	}	
	void OnRemoveClick(int ShopIndex)
	{
		var nameClass = Shop.Instance.ShopItemsList[ShopIndex];
		if (nameClass.quantity > 0)
		{
			ShopScrollView.GetChild(ShopIndex).GetChild(3).GetComponent<Text>().text = (nameClass.quantity - 1).ToString();
			Shop.Instance.ShopItemsList[ShopIndex].quantity = nameClass.quantity - 1;
			Game.Instance.RemoveCoins(nameClass.Price);
		}

	}
	void OnDeliteClick(int ShopIndex)
	{
		print(ShopIndex);
		//	g.transform.GetChild(3).GetComponent<Text>().text = numberadd.ToString();
		Shop.Instance.ShopItemsList[ShopIndex].IsPurchased = false;
		Destroy(ShopScrollView.GetChild(ShopIndex).gameObject);
		 Shop.Instance.updateList(ShopIndex);
	}
}
