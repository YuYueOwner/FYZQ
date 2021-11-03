using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Tools
{
    #region Bool
    /// <summary>
    /// 更新链表中的某一个值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_list"></param>
    /// <param name="index"></param>
    /// <param name="_t"></param>
    /// <returns></returns>
    public static bool UpdateListByItem<T>(this List<T> _list, int index, T _t)
    {
        if (_list.Count <= index)
            return false;

        _list[index] = _t;
        return true;
    }
    #endregion

    #region Array
    /// <summary>
    /// 数组随机顺序
    /// </summary>
    /// <returns>The sort.</returns>
    /// <param name="array">Array.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static T[] RandomSort<T>(T[] array)
    {
        int len = array.Length;
        List<int> list = new List<int>();
        T[] ret = new T[len];
        System.Random rand = new System.Random();
        int i = 0;
        while (list.Count < len)
        {
            int iter = rand.Next(0, len);
            if (!list.Contains(iter))
            {
                list.Add(iter);
                ret[i] = array[iter];
                i++;
            }
        }
        return ret;
    }
    #endregion

    #region List
    /// <summary>
    /// 将数组转换为list 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
    public static List<T> ToList<T>(this T[] array)
    {
        List<T> temp = new List<T>();
        for (int i = 0, count = array.Length; i < count; i++)
        {
            temp.Add(array[i]);
        }
        return temp;
    }

    /// <summary>
    /// 返回字典中的 key
    /// </summary>
    /// <param name="tempDict"></param>
    /// <returns></returns>
    public static List<T> ToList<T, K>(this Dictionary<T, K> tempDict)
    {
        return tempDict.Keys.ToList();
    }


    public static bool ListEquals<T>(this IEnumerable<T> one, IEnumerable<T> another)
    {
        if (one.Count() != another.Count()) return false;
        return (one.Except(another)).Count() == 0;
    }


    #endregion

    #region Dictionary
    /// <summary>
    /// 增加或者更新值
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="dict"></param>
    /// <param name="k"></param>
    /// <param name="v"></param>
    public static void AddOrUpdate<K, V>(this IDictionary<K, V> dict, K k, V v)
    {
        if (dict.ContainsKey(k))
        {
            dict[k] = v;
        }
        else
        {
            dict.Add(k, v);
        }
    }
    /// <summary>
    /// 将字典中的值重写。
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="dict"></param>
    /// <param name="copy"></param>
    /// <param name="overwrite">该值决定是否重写</param>
    public static void AddRange<K, V>(this Dictionary<K, V> dict, Dictionary<K, V> copy, bool overwrite)
    {
        if (copy == null)
        {
            return;
        }

        foreach (KeyValuePair<K, V> pair in copy)
        {
            if (dict.ContainsKey(pair.Key) && overwrite)
            {
                dict[pair.Key] = pair.Value;
            }
            else
            {
                dict.Add(pair.Key, pair.Value);
            }
        }
    }
    /// <summary>
    /// 尝试根据key得到value，得到了话直接返回value，没有得到直接返回null
    /// </summary>
    public static Tvalue TryGet<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
    {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }

    #endregion

    #region Tranform
    //使用 静态关键字进行修饰便于外部进行修饰
    public static GameObject GetChild(Transform trans, string childName)
    {
        //得到当前脚本挂在的对象
        Transform child = trans.Find(childName);
        //找到的时候
        if (child != null)
        {
            //返回一个游戏对象
            return child.gameObject;
        }
        //得到所有的子物体个数
        int count = trans.childCount;
        //定义一个新的对象
        GameObject go = null;
        //遍历所有的对象
        for (int i = 0; i < count; ++i)
        {
            //拿到当前对象下面所有的子对象
            child = trans.GetChild(i);
            //获取对象并赋值
            go = GetChild(child, childName);
            //不为空的时候返回出去
            if (go != null)
            {
                return go;
            }
        }
        //Debug.LogWarning("Select Object null");
        return null;
    }
    //使用 泛型 进行修饰，可以是任意类型，约束为必须是 Component类型，所有组件的基类
    public static T GetChild<T>(Transform trans, string childName) where T : Component
    {
        //赋值给对象
        GameObject go = GetChild(trans, childName);
        if (go == null)
        {
            Debug.LogWarning("Select " + typeof(T).Name + " Object null");
            return null;
        }
        return go.GetComponent<T>();
    }
    /// <summary>
    /// 获得父对象下所有的子物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parent"></param>
    /// <param name="list"></param>
    public static void FindAllChildToList<T>(this Transform parent, List<T> list)
    {
        foreach (Transform child in parent)
        {
            T go = child.GetComponent<T>();
            if (go != null)
            {
                list.Add(child.GetComponent<T>());
            }
            FindAllChildToList(child, list);
        }
    }


    /// <summary>
    /// 得到自身下一个子物体
    /// </summary>
    /// <param name="trans"></param>
    /// <returns></returns>
    public static T GetObject<T>(Transform trans) where T : Component
    {
        T temp_Object = trans.GetChild(0).gameObject.GetComponent<T>();
        if (temp_Object == null)
            temp_Object = trans.GetChild(0).gameObject.AddComponent<T>();
        return temp_Object;
    }

    /// <summary>
    /// 得到自身下方所有子物体
    /// </summary>
    /// <param name="trans"></param>
    /// <returns></returns>
    public static T[] GetArray<T>(Transform trans) where T : Component
    {
        int trans_Length = trans.childCount;
        T[] temp_Object = new T[trans_Length];

        for (int i = 0; i < temp_Object.Length; i++)
        {
            T temp_t = trans.GetChild(i).gameObject.GetComponent<T>();

            if (temp_t == null)
                temp_t = trans.GetChild(i).gameObject.AddComponent<T>();

            temp_Object[i] = temp_t;
        }
        return temp_Object;
    }
    #endregion

    #region Function
    /// <summary>限制角度</summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float ClampAngle(float angle)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return angle;
    }

    /// <summary>限制角度</summary>
    /// <param name="angle"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }

    /// <summary>限制角度</summary>
    /// <param name="angle"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float CheckAngleInspector(float value)
    {
        float angle = value - 180;

        if (angle > 0) return angle - 180;

        return angle + 180;
    }

    #endregion
}
