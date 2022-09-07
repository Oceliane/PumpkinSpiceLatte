using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    [SerializeField] GameObject childArea;

    private void Start()
    {
        if (transform.childCount > 0)
            childArea = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (childArea != null)
            childArea.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (childArea != null)
            childArea.SetActive(true);
    }
}
