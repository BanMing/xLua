/******************************************************************
** 文件名:  LuaProtoBufTest.cs
** 创建人:  BanMing 
** 日  期:  2020/1/21 下午6:10:35
** 描  述:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
*******************************************************************/
using UnityEngine;
using System;
using System.IO;
using XLua;

public class LuaProtoBufTest : MonoBehaviour
{
    void Start()
    {
        readPbFile();
    }
    // 读取一个，如果是ab读取需要把pb格式的转换成bytes文件才能读
    void readPbFile()
    {
        LuaEnv luaenv = new LuaEnv();
        luaenv.DoString(@"
            CS.UnityEngine.Debug.Log('hello world')
            local pb = require 'pb'
            local bytes =  CS.System.IO.File.ReadAllBytes(CS.System.IO.Path.GetFullPath([[Assets/XLua/Examples/14_LuaProtoBuf/test.pb]]))
            print(#bytes)
            assert(pb.load(bytes))

            -- lua table data
            local data = {
               name = 'ilse',
               age  = 18,
               contacts = {
                  { name = 'alice', phonenumber = 12312341234 },
                  { name = 'bob',   phonenumber = 45645674567 }
               }
            }

            -- encode lua table data into binary format in lua string and return
            local bytes = assert(pb.encode('test.Person', data))
            print(pb.tohex(bytes))

            -- and decode the binary data back into lua table
            local data2 = assert(pb.decode('test.Person', bytes))
            print(data2.name)
            -- serpent为打印table的工具
            print(require 'serpent'.block(data2))
        ");
        luaenv.Dispose();
    }

}