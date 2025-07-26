using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoMoveTest : MonoBehaviour
{
    public GameObject target;
    public Button moveButton;
    public int moveDistance;
    public int duration = 2;
    
    void Start()
    {
        target = GameObject.Find("Target");
        moveButton = GetComponent<Button>();
        moveButton.onClick.AddListener(OnMoveButtonClick);
    }
    
    void OnMoveButtonClick()
    {
        Vector3 endPos = target.transform.position + new Vector3(moveDistance, 0, 0);
        target.transform.DoMove(endPos, duration);
    }
}
