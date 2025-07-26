using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoScaleTest : MonoBehaviour
{
    public GameObject target;
    public Button moveButton;
    public float scaleSize;
    
    void Start()
    {
        target = GameObject.Find("Target");
        moveButton = GetComponent<Button>();
        moveButton.onClick.AddListener(OnMoveButtonClick);
    }
    
    void OnMoveButtonClick()
    {
        Vector3 endScale = target.transform.localScale*scaleSize;
        target.transform.DoScale(endScale, 2);
    }
}
