using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class k_LuaCallCSharp : MonoBehaviour
{
	private LuaEnv env = new LuaEnv();

	// Start is called before the first frame update
	void Start()
	{
		env.DoString("require 'k_LuaCallCSharp'");
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnDestroy()
	{
		env.Dispose();
	}

	[LuaCallCSharp]
	public class ChooseMoney
	{
		public void GetMoney(MoneyEnum lang)
		{
			switch (lang)
			{
				case MoneyEnum.golden:
					Debug.Log("golden!");
					break;
				case MoneyEnum.silver:
					Debug.Log("silver!");
					break;
				case MoneyEnum.bronze:
					Debug.Log("bronze!");
					break;
			}
		}
	}
}




public enum MoneyEnum
{
	golden = 1,
	silver = 2,
	bronze = 3
}

[LuaCallCSharp]
public enum PeopleEnum
{
	Male,
	Female,
}


