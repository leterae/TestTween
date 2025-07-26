using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweenr : MonoBehaviour
{
    
    public 
    
    public bool isComplete;
    // 记录Tweener类型
    public string name;
    
    // 用来做管理协程做计时器
    private Coroutine runtimeCoroutine;
    
    public Coroutine RTCoroutine
    {
        get => runtimeCoroutine;
        set => runtimeCoroutine = value;
    }

    public bool IsCorouTineExist()
    {
        return RTCoroutine != null;
    }

    public void StopRTCouroutine()
    {
        StopCoroutine(RTCoroutine);
    }

    public void StartRTCorouTine(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }
}
