using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using XLua;

public class k_Myloader_pathChanged : MonoBehaviour
{
	public LuaEnv env = new LuaEnv();

    // Start is called before the first frame update
    void Start()
    {
	    env.AddLoader(MyLoader);
		env.DoString("require 'k_helloworld'");
		env.Dispose();
    }

	private byte[] MyLoader(ref string filePath)
	{
		print(filePath);
		print(Application.streamingAssetsPath);

		var absPath = Path.Combine(Application.streamingAssetsPath, filePath) + ".lua.txt";
		return Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
	}
}
