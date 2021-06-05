using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.Text;

public class k_MyLoader : MonoBehaviour
{
	public LuaEnv luaenv = new LuaEnv();

    // Start is called before the first frame update
    void Start()
    {
	    luaenv.AddLoader(MyLoader);

	    luaenv.DoString("require 'xxxxxxx'");

	    luaenv.Dispose();
    }

    private byte[] MyLoader(ref string filePath)
    {
	    print(filePath);
	    string result = "print(123)";

		//return null;
	    return Encoding.UTF8.GetBytes(result);
    }
}
