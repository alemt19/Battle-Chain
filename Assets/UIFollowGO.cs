using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowGO : MonoBehaviour
{
    public Transform objectToFollow;
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (objectToFollow != null)
        {
            rectTransform.anchoredPosition = objectToFollow.localPosition + new Vector3(-0.58f, 0.55f);
        }
    }
}
