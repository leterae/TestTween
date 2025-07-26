using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public static class Mytween
{
    private class TweenState
    {
        public bool Killed = false;
        public bool Complete = false;
    }

    private static string DOMOVE_ID = "DoMove";
    
    // 创建一个GameObject放TweenRunner.
    static private GameObject core;
    // static private Tweenr _runner;
    private static Dictionary<String, Tweenr> runningCoroutines = new Dictionary<String, Tweenr>();
    // private static Dictionary<string, TweenState> runningStates = new Dictionary<string, TweenState>();

    static private void CheckCore()
    {
        if (!core)
        {
            core = new GameObject("TweenManager");
        }
    }

    public static void Kill(this Transform t, String str)
    {
        CheckCore();
        // 同一个协程类型只允许存一个, 有多个就先消除前一个。
        if (runningCoroutines.TryGetValue(str, out Tweenr tweenr) && tweenr != null)
        {
            if (tweenr.IsCorouTineExist())
            {
                tweenr.isComplete = true;
                tweenr.StopRTCouroutine();
                tweenr.isComplete = false;
            }
        }
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
    
    // 扩展方法示例
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
            if (tweenr.isComplete)
            {
                Debug.LogError("IsCompelete");
                yield break;
            }   
            t.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        t.position = target;
    }
    
    
}
