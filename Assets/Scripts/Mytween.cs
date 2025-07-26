using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public static class Mytween
{
    public static string DOMOVE_ID = "DoMove";
    public static string DOSCALE_ID = "DoScale";
    
    // 创建一个GameObject放TweenRunner.
    static private GameObject core;
    // static private Tweenr _runner;
    private static Dictionary<String, Tweenr> runningCoroutines = new Dictionary<String, Tweenr>();

    static private void CheckCore()
    {
        if (!core)
        {
            core = new GameObject("TweenManager");
        }
    }

    public static Tweenr GetTweenrById(string id)
    {
        runningCoroutines.TryGetValue(id, out Tweenr tweenr);
        return tweenr;
    }

    // 检查某个tweener。加入哈希表
    private static void CheckTweener(String name)
    {
        if (!runningCoroutines.ContainsKey(name))
        {
            Tweenr newTweener = core.AddComponent<Tweenr>();
            newTweener.name = name;
            runningCoroutines[name] = newTweener;
        }
    }
    
    // 扩展方法
    public static Tweenr DoMove(this Transform t, Vector3 target, float duration)
    {
        CheckCore();
        CheckTweener(DOMOVE_ID);
        if (runningCoroutines.TryGetValue(DOMOVE_ID, out Tweenr tweenr) && tweenr != null)
        {
            // 同一个协程类型只允许存一个, 有多个就先消除前一个。
            if (tweenr.IsCorouTineExist())
            {
                tweenr.StopRTCouroutine();   
            }
            tweenr.StartRTCorouTine(MoveCoroutine(t, target, duration, tweenr));
            return tweenr;
        }
        return null;
    }
    
    
    // 协程实现 移动
    private static IEnumerator MoveCoroutine(Transform t, Vector3 target, float duration, Tweenr tweenr)
    {
        Vector3 start = t.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (tweenr.isKill)
            {
                if (tweenr.isComplete)
                {
                    t.position = target;
                }
                tweenr.isKill = false;
                tweenr.isComplete = false;
                yield break;
            }
               
            t.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        t.position = target;
    }
    
    public static Tweenr DoScale(this Transform t, Vector3 target, float duration)
    {
        CheckCore();
        CheckTweener(DOSCALE_ID);
        if (runningCoroutines.TryGetValue(DOSCALE_ID, out Tweenr tweenr) && tweenr != null)
        {
            // 同一个协程类型只允许存一个, 有多个就先消除前一个。
            if (tweenr.IsCorouTineExist())
            {
                tweenr.StopRTCouroutine();   
            }
            tweenr.StartRTCorouTine(ScaleCoroutine(t, target, duration, tweenr));
            return tweenr;
        }
        return null;
    }
    
    
    // 协程实现 移动
    private static IEnumerator ScaleCoroutine(Transform t, Vector3 target, float duration, Tweenr tweenr)
    {
        Vector3 start = t.localScale;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (tweenr.isKill)
            {
                if (tweenr.isComplete)
                {
                    t.localScale = target;
                }
                tweenr.isKill = false;
                tweenr.isComplete = false;
                yield break;
            }
               
            t.localScale = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        t.localScale = target;
    }
    
}
