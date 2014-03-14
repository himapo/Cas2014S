using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShopGUI : MyBehaviour {

	[HideInInspector]
	public bool isShopOpen = false;

	bool closeFlag = false;

	System.Action guiState;

	public GameObject normalGachaPrefab;

	public GameObject rareGachaPrefab;

	Gacha normalGacha;

	Gacha rareGacha;

	Gacha gacha;

	// Use this for initialization
	void Start () {
		normalGacha = (Instantiate(normalGachaPrefab) as GameObject).GetComponent<Gacha>();
		rareGacha = (Instantiate(rareGachaPrefab) as GameObject).GetComponent<Gacha>();

		guiState = StateRoot;
	}
	
	// Update is called once per frame
	void Update () {

		if(!isShopOpen)
		{
			return;
		}

		if(closeFlag)
		{
			CloseShop();
			closeFlag = false;
		}
	}

	public void OpenShop()
	{
		if(isShopOpen)
		{
			return;
		}

		isShopOpen = true;

		Player.SendMessage("OnShopOpen", SendMessageOptions.DontRequireReceiver);

		var enemys = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(var enemy in enemys)
		{
			enemy.SendMessage("OnShopOpen", SendMessageOptions.DontRequireReceiver);
		}

		Crosshair.Instance.OnShopOpen();
	}

	void CloseShop()
	{
		if(!isShopOpen)
		{
			return;
		}

		isShopOpen = false;

		Player.SendMessage("OnShopClose", SendMessageOptions.DontRequireReceiver);
		
		var enemys = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(var enemy in enemys)
		{
			enemy.SendMessage("OnShopClose", SendMessageOptions.DontRequireReceiver);
		}

		Crosshair.Instance.OnShopClose();
	}

	void OnGUI()
	{
		if(!isShopOpen)
		{
			return;
		}
		
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), " ");
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), " ");
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), " ");
		
		guiState();
	}

	void StateRoot()
	{
		RareGacha();

		NormalGacha();

		BackButton("店を出る", ()=> {
			closeFlag = true;
			return;
		},
		"");
	}

	void RareGacha()
	{
		var backupColor = GUI.color;

		GUI.color = Color.yellow;

		var topleft = Camera.main.ViewportToScreenPoint(
			new Vector3(0.3f, 0.1f, 0.0f));
		
		var size = Camera.main.ViewportToScreenPoint(
			new Vector3(0.4f, 0.35f, 0.0f));
		
		var area = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);
		
		GUILayout.BeginArea(area);
		
		GUILayout.FlexibleSpace();
		
		var guiStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize = 30;
		
		GUILayout.Label(
			"☆☆☆レアガチャ☆☆☆",
			guiStyle,
			GUILayout.MinHeight(60));
		
		//GUILayout.FlexibleSpace();

		GUI.color = Color.red;
		guiStyle.fontSize = 20;
		
		GUILayout.Label(
			"11連でSR1枚確定キャンペーン中！！！",
			guiStyle);

		GUI.color = Color.yellow;
		
		//GUILayout.FlexibleSpace();
		
		GUILayout.BeginHorizontal();
		
		GUILayout.FlexibleSpace();
		
		if(GUILayout.Button("レア1回\n(最大HP 10)", GUILayout.MinWidth(120), GUILayout.MinHeight(60)))
		{
			if(PlayerHealth.maxHealth >= 10)
			{
				PlayerHealth.AddMaxHealth(-10);
				rareGacha.Draw(1);
				gacha = rareGacha;
				guiState = StateGacha;
			}
			else
			{
				guiState = StateLack;
			}
		}
		
		GUILayout.FlexibleSpace();
		
		if(GUILayout.Button("レア11連\n(最大HP 100)", GUILayout.MinWidth(120), GUILayout.MinHeight(60)))
		{
			if(PlayerHealth.maxHealth >= 100)
			{
				PlayerHealth.AddMaxHealth(-100);
				rareGacha.Draw(11);
				gacha = rareGacha;
				guiState = StateGacha11;
			}
			else
			{
				guiState = StateLack;
			}
		}
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndArea();
	
		GUI.color = backupColor;
	}

	void NormalGacha()
	{
		var topleft = Camera.main.ViewportToScreenPoint(
			new Vector3(0.3f, 0.5f, 0.0f));
		
		var size = Camera.main.ViewportToScreenPoint(
			new Vector3(0.4f, 0.2f, 0.0f));
		
		var area = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);
		
		GUILayout.BeginArea(area);
		
		GUILayout.FlexibleSpace();
		
		var guiStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize = 20;
		
		GUILayout.Label(
			"ノーマルガチャ",
			guiStyle);
		
		GUILayout.FlexibleSpace();
		
		GUILayout.BeginHorizontal();
		
		GUILayout.FlexibleSpace();
		
		if(GUILayout.Button("ノーマル1回\n(HP 10)", GUILayout.MinWidth(120), GUILayout.MinHeight(40)))
		{
			if(PlayerHealth.health > 10)
			{
				PlayerHealth.AddHealth(-10);
				normalGacha.Draw(1);
				gacha = normalGacha;
				guiState = StateGacha;
			}
			else
			{
				guiState = StateLack;
			}
		}
		
		GUILayout.FlexibleSpace();
		
		if(GUILayout.Button("ノーマル11連\n(HP 100)", GUILayout.MinWidth(120), GUILayout.MinHeight(40)))
		{
			if(PlayerHealth.health > 100)
			{
				PlayerHealth.AddHealth(-100);
				normalGacha.Draw(11);
				gacha = normalGacha;
				guiState = StateGacha11;
			}
			else
			{
				guiState = StateLack;
			}
		}
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndArea();
	}

	void StateLack()
	{
		var guiStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize = 30;
		
		var windowRect = new Rect(0, 0, Screen.width, Screen.height * 0.7f);
		
		GUILayout.BeginArea(windowRect);
		GUILayout.FlexibleSpace();
		
		GUILayout.Label(
			"命は大事にしましょう！",
			guiStyle);
		
		GUILayout.FlexibleSpace();
		GUILayout.EndArea();

		BackButton("戻る", ()=> {
			guiState = StateRoot;
			return;
		},
		"");
	}

	void StateGacha()
	{
		AtariLabel ();

		ItemButton();
		
		BackButton("戻る", ()=> {
			guiState = StateRoot;
			return;
		},
		"残りのアイテムは全て破棄されます");
	}

	void StateGacha11()
	{
		AtariLabel ();

		ItemButton();

		BackButton("戻る", ()=> {
			guiState = StateRoot;
			return;
		},
		"残りのアイテムは全て破棄されます");
	}

	void AtariLabel()
	{
		var guiStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize = 30;
		
		var windowRect = new Rect(0, 0, Screen.width, Screen.height * 0.2f);
		
		GUILayout.BeginArea(windowRect);
		GUILayout.FlexibleSpace();
		
		GUILayout.Label(
			"以下のアイテムが当たりました！",
			guiStyle);
		
		GUILayout.FlexibleSpace();
		GUILayout.EndArea();
	}

	void ItemButton()
	{
		var topleft = Camera.main.ViewportToScreenPoint(
			new Vector3(0.35f, 0.2f, 0.0f));
		
		var size = Camera.main.ViewportToScreenPoint(
			new Vector3(0.3f, 0.6f, 0.0f));

		var area = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);

		var attachIndex = -1;

		GUILayout.BeginArea(area);

		foreach(var item in gacha.drawnItems.Select((val, index) => { return new { val, index }; }))
		{
			if(item.index != 0)
			{
				//GUILayout.FlexibleSpace();
			}

			GUILayout.BeginHorizontal("box", GUILayout.MaxHeight(40));

			GUILayout.BeginVertical();

			GUILayout.FlexibleSpace();

			if(GUILayout.Button("左手に装備"))
			{
				attachIndex = item.index;

				if(item.val.type == ItemType.Gun)
				{
					ChangeGun(0, item.val.gun.Type);
				}
				else if(item.val.type == ItemType.Skill)
				{
					AddSkill(0, item.val.skill.Type);
				}
			}

			GUILayout.FlexibleSpace();

			GUILayout.EndVertical();

			GUILayout.FlexibleSpace();

			if(item.val.type == ItemType.Gun)
			{
				IconDrawer.Instance.DrawGun(item.val.gun);
			}
			else if(item.val.type == ItemType.Skill)
			{
				IconDrawer.Instance.DrawShopSkill(item.val.skill, Vector3.zero, true);
			}

			GUILayout.FlexibleSpace();

			GUILayout.BeginVertical();
			
			GUILayout.FlexibleSpace();
			
			if(GUILayout.Button("右手に装備"))
			{
				attachIndex = item.index;

				if(item.val.type == ItemType.Gun)
				{
					ChangeGun(1, item.val.gun.Type);
				}
				else if(item.val.type == ItemType.Skill)
				{
					AddSkill(1, item.val.skill.Type);
				}
			}

			GUILayout.FlexibleSpace();
			
			GUILayout.EndVertical();
			
			GUILayout.EndHorizontal();
		}

		GUILayout.EndArea();

		if(attachIndex >= 0)
		{
			gacha.drawnItems.RemoveAt(attachIndex);
		}
	}

	void ChangeGun(int gunIndex, PlayerGunType type)
	{
		GunController.ChangeGun(gunIndex, type);
	}

	void AddSkill(int gunIndex, SkillType type)
	{
		GetGun(gunIndex).AddSkill(type);
	}

	void BackButton(string label, System.Action action, string notice)
	{
		var topleft = Camera.main.ViewportToScreenPoint(
			new Vector3(0.425f, 0.8f, 0.0f));
		
		var size = Camera.main.ViewportToScreenPoint(
			new Vector3(0.15f, 0.2f, 0.0f));
		
		var area = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);
		
		GUILayout.BeginArea(area);

		GUILayout.FlexibleSpace();

		if(GUILayout.Button(label, GUILayout.MinHeight(40.0f)))
		{
			action();
		}
		
		GUILayout.Label(notice);

		GUILayout.FlexibleSpace();
		
		GUILayout.EndArea();
	}
}
