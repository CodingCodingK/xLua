using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class k_CSharpCallLua : MonoBehaviour
{
	private LuaEnv env = new LuaEnv();

	// Start is called before the first frame update
    void Start()
    {
	    env.DoString("require 'k_CSharpCallLua'");

        #region 用基本数据类型接

        var hp = env.Global.Get<int>("Hp");
        var sp = env.Global.Get<float>("Sp");
        var exp = env.Global.Get<int>("Exp");
        var name = env.Global.Get<string>("Name");
        var isDie = env.Global.Get<bool>("isDie");
        var test1 = env.Global.Get<int>("isDie");
        var test2 = env.Global.Get<string>("ASD");

        print(hp);
        print(sp);
        print(exp);
        print(name);
        print(isDie);
        print(test1);
        print(test2);

        #endregion

        #region 用类接

        print("============ 用class接 ============");

        var p = env.Global.Get<Person>("person");
        print(p.name + " " + p.hp + " " + p.sp + " " + p.Miss);//Name转name失败，hp转hp成功，不存在转Miss为默认值。如果Lua的100.1通过该框架转换成c#的int，会变成0而不是100。
        p.hp = 3000;
        print("修改后的c#类的Hp：" + p.hp + "，修改后的Lua虚拟机中的Hp：" + env.Global.Get<Person>("person").hp);

        #endregion

        #region 用接口接

        print("============ 用interface接 ============");
        var m = env.Global.Get<IMoney>("money");
        print(m.num + " " + m.type);
        m.Consume(30, 10);
        m.Show(30, 10);// => Lua下 money.Show(self,30,10) Lua这样的函数声明，第一位需要放入自己，然后依次放入参，多的参数扔掉，少的参数补nil

        #endregion

        #region 用Dictionary和List接

        print("============ 用Dictionary和List接 ============");

        // 用string可以接到所有带string型Key的
        var mDic = env.Global.Get<Dictionary<string, object>>("money");
        foreach (var item in mDic)
        {
	        print("字典Key用string接：" + item.Key + " " + item.Value);
        }

        // 用int可以接到所有带int型Key的、和不带Key的（索引默认也是int）
        var mDic2 = env.Global.Get<Dictionary<int, object>>("money");
        foreach (var item in mDic2)
        {
	        print("字典Key用int接：" + item.Key + " " + item.Value);
        }

        // 用List接只能接到所有不带Key的
        var mList = env.Global.Get<List<object>>("money");
        foreach (var item in mList)
        {
	        print("用List<object>接：" + item);
        }

        #endregion

        #region 用LuaTable接

        print("============ 用LuaTable接 ============");

        // LuaTable接到所有的，但是很慢，也没有类型检查
        var mLT = env.Global.Get<LuaTable>("money");
        foreach (var key in mLT.GetKeys())
        {
	        print("LuaTable用object接：" + mLT.Get<object,object>(key));// 等于 mLT[key]
        }

        #endregion

        #region 用Action接function

        print("============ 用Action接function ============");

        var add = env.Global.Get<Add>("add");
        print(add(15, 20));

        add = null;// for dispose

        #endregion

        //env.Dispose();
    }

    [CSharpCallLua]
    public delegate int Add(int a, int b);

    [CSharpCallLua]
    public interface IMoney
    {
	    public int num { get; set; }
	    public string type { get; set; }
	    public void Consume(int a,int b);
	    public void Show(int a,int b);
    }

    class Person
    {
	    public string name;
	    public int hp;
        public double sp;
	    public int Miss;
    }
}
