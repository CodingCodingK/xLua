using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class k_helloworld : MonoBehaviour
{
	public LuaEnv luaenv = new LuaEnv();

    // Start is called before the first frame update
    void Start()
    {
        // method 1
	    luaenv.DoString("print('print by lua codes')");

        // method 2
        luaenv.DoString("CS.UnityEngine.Debug.Log('print by lua called c# codes')");//Debug.Log('s');

		// method 3
		TextAsset ta = Resources.Load<TextAsset>("k_helloworld.lua");// load Resources/k_helloworld.lua.txt
		luaenv.DoString(ta.text);

	    // method 4
	    luaenv.DoString("require 'k_helloworld'");// load Resources/k_helloworld.lua
    }

    // Update is called once per frame
    void Update()
    {
	    
    }

	private void OnDestroy()
	{
		luaenv.Dispose();
    }
}
