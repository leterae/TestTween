using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillMoveTest : MonoBehaviour
{
    public GameObject target;
    public Button moveButton;
    
    void Start()
    {
        target = GameObject.Find("Target");
        moveButton = GetComponent<Button>();
        moveButton.onClick.AddListener(OnMoveButtonClick);
    }
    
    void OnMoveButtonClick()
    {
        target.transform.Kill("DoMove");
    }
}