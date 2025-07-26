using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMoveEaseTest : MonoBehaviour
{
    public GameObject target;
    public Button moveButton;
    public Mytween.EaseType easeType;
    void Start()
    {
        target = GameObject.Find("Target");
        moveButton = GetComponent<Button>();
        moveButton.onClick.AddListener(OnMoveButtonClick);
    }
    
    void OnMoveButtonClick()
    {
        Tweenr tween = Mytween.GetTweenrById(Mytween.DOMOVE_ID); 
        tween.SetEase(easeType);
    }
}
