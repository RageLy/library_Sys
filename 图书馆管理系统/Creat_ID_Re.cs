using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 图书馆管理系统
{
    
    class Creat_ID_Re
    {
        #region 生成单据号
/// <summary>
/// 生成单据号
/// </summary>
/// <param name="pFromType"></param>
/// <returns></returns>
public  string GetFormCode(int m,int n)
{
    string formcode = "";
    switch(m)
    {
    case 1:
    {
        formcode = "";
        break;
    }
    case 2:
    {
        formcode = "JS";
        break;
    }
    case 3:
    {
        formcode = "HS";
        break;
    }
    }
    formcode += DateTime.Now.Year.ToString();
    formcode += DateTime.Now.Month.ToString().Length == 1?"0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
    formcode += DateTime.Now.Day.ToString().Length == 1?"0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
    formcode += DateTime.Now.Hour.ToString().Length == 1?"0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
    formcode += DateTime.Now.Minute.ToString().Length == 1?"0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
    formcode += DateTime.Now.Second.ToString().Length == 1?"0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString();
    if(DateTime.Now.Millisecond.ToString().Length == 1)
    {
        formcode += "00" + DateTime.Now.Millisecond.ToString();
    }
    else if (DateTime.Now.Millisecond.ToString().Length == 2)
    {
        formcode += "0" + DateTime.Now.Millisecond.ToString();
    }
    else
    {
        formcode += DateTime.Now.Millisecond.ToString();
    }
    formcode = Random_Char(formcode, n);
    return formcode;
    }
#endregion

        #region 产生随机的字符串
        /// <summary>
        /// 产生指定长度的字符串
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
public string Random_Char(string m,int n)
    {
        char[] chars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'R', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        string time = DateTime.Now.Hour.ToString();
        time += DateTime.Now.Minute.ToString();
        time += DateTime.Now.Second.ToString();
        int seed = int.Parse(time);
        int position = 0;
        for(int i=0;i<n;i++)
        {
            Random rand1 = new Random(seed);
            position = rand1.Next(26);
            m = m + chars[position].ToString();
            seed = seed*2;
        }
        return m;
    }
#endregion
    }
}
