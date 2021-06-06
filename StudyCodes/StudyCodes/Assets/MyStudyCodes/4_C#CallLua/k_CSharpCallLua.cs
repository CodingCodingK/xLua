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

        #region �û����������ͽ�

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

        #region �����

        print("============ ��class�� ============");

        var p = env.Global.Get<Person>("person");
        print(p.name + " " + p.hp + " " + p.sp + " " + p.Miss);//Nameתnameʧ�ܣ�hpתhp�ɹ���������תMissΪĬ��ֵ�����Lua��100.1ͨ���ÿ��ת����c#��int������0������100��
        p.hp = 3000;
        print("�޸ĺ��c#���Hp��" + p.hp + "���޸ĺ��Lua������е�Hp��" + env.Global.Get<Person>("person").hp);

        #endregion

        #region �ýӿڽ�

        print("============ ��interface�� ============");
        var m = env.Global.Get<IMoney>("money");
        print(m.num + " " + m.type);
        m.Consume(30, 10);
        m.Show(30, 10);// => Lua�� money.Show(self,30,10) Lua�����ĺ�����������һλ��Ҫ�����Լ���Ȼ�����η���Σ���Ĳ����ӵ����ٵĲ�����nil

        #endregion

        #region ��Dictionary��List��

        print("============ ��Dictionary��List�� ============");

        // ��string���Խӵ����д�string��Key��
        var mDic = env.Global.Get<Dictionary<string, object>>("money");
        foreach (var item in mDic)
        {
	        print("�ֵ�Key��string�ӣ�" + item.Key + " " + item.Value);
        }

        // ��int���Խӵ����д�int��Key�ġ��Ͳ���Key�ģ�����Ĭ��Ҳ��int��
        var mDic2 = env.Global.Get<Dictionary<int, object>>("money");
        foreach (var item in mDic2)
        {
	        print("�ֵ�Key��int�ӣ�" + item.Key + " " + item.Value);
        }

        // ��List��ֻ�ܽӵ����в���Key��
        var mList = env.Global.Get<List<object>>("money");
        foreach (var item in mList)
        {
	        print("��List<object>�ӣ�" + item);
        }

        #endregion

        #region ��LuaTable��

        print("============ ��LuaTable�� ============");

        // LuaTable�ӵ����еģ����Ǻ�����Ҳû�����ͼ��
        var mLT = env.Global.Get<LuaTable>("money");
        foreach (var key in mLT.GetKeys())
        {
	        print("LuaTable��object�ӣ�" + mLT.Get<object,object>(key));// ���� mLT[key]
        }

        #endregion

        #region ��Action��function

        print("============ ��Action��function ============");

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
