using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShopGUI : MyBehaviour {

	[HideInInspector]
	public bool isShopOpen = false;

	bool closeFlag = false;

	System.Action guiState;

	Gacha gacha;

	void Awake()
	{
		gacha = GetComponent<Gacha>();
	}

	// Use this for initialization
	void Start () {
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

		Screen.lockCursor = false;
		Screen.showCursor = true;

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

		Screen.lockCursor = true;
		Screen.showCursor = false;

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
		BackButton("店を出る", ()=>
		{
			closeFlag = true;
			return;
		},
		"");

		var guiStyle = new GUIStyle(GUI.skin.GetStyle("button"));

		guiStyle.alignment = TextAnchor.MiddleCenter;

		var buttonSize = new Vector2(120.0f, 40.0f);

		var screenPosition = Camera.main.ViewportToScreenPoint(
			new Vector3(0.4f, 0.2f, 0.0f));
		
		if(GUI.Button(
			new Rect(
				screenPosition.x - buttonSize.x * 0.5f,
				screenPosition.y - buttonSize.y * 0.5f,
				buttonSize.x,
				buttonSize.y),
			"ガチャを回す"))
		{
			gacha.Draw(1);
			guiState = StateGacha;
		}

		screenPosition = Camera.main.ViewportToScreenPoint(
			new Vector3(0.6f, 0.2f, 0.0f));

		if(GUI.Button(
			new Rect(
				screenPosition.x - buttonSize.x * 0.5f,
				screenPosition.y - buttonSize.y * 0.5f,
				buttonSize.x,
				buttonSize.y),
			"11連ガチャ"))
		{
			gacha.Draw(11);
			guiState = StateGacha11;
		}
	}

	void StateBuy()
	{
	}

	void StateGacha()
	{
		AtariLabel ();

		ItemButton();
		
		BackButton("戻る", ()=>
		{
			guiState = StateRoot;
			return;
		},
		"残りのアイテムは全て破棄されます");
	}

	void StateGacha11()
	{
		AtariLabel ();

		ItemButton();

		BackButton("戻る", ()=>
		           {
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

		var guiStyle = new GUIStyle(GUI.skin.GetStyle("button"));

		var attachIndex = -1;

		GUILayout.BeginArea(area);

		foreach(var item in gacha.drawnItems.Select((val, index) => { return new { val, index }; }))
		{
			if(item.index != 0)
			{
				//GUILayout.FlexibleSpace();
			}

			GUILayout.BeginHorizontal("box");

			if(GUILayout.Button("左手に装備", guiStyle))
			{
				attachIndex = item.index;

				if(item.val.type == ItemType.Skill)
				{
					AddSkill(0, item.val.skill.Type);
				}
			}

			GUILayout.FlexibleSpace();

			if(item.val.type == ItemType.Skill)
			{
				IconDrawer.Instance.DrawShopSkill(item.val.skill, Vector3.zero, true);
			}

			GUILayout.FlexibleSpace();
			
			if(GUILayout.Button("右手に装備", guiStyle))
			{
				attachIndex = item.index;

				if(item.val.type == ItemType.Skill)
				{
					AddSkill(1, item.val.skill.Type);
				}
			}
			
			GUILayout.EndHorizontal();
		}

		GUILayout.EndArea();

		if(attachIndex >= 0)
		{
			gacha.drawnItems.RemoveAt(attachIndex);
		}
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
		
		if(GUILayout.Button(label, GUILayout.MinHeight(40.0f)))
		{
			action();
		}
		
		GUILayout.Label(notice);
		
		GUILayout.EndArea();
	}
}
