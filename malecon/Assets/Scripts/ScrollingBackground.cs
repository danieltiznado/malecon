using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float elementXSize;

    private Transform cameraTransform;
    private Transform[] elements;
    private float viewZone = 10;
    private int leftIndex = 0;
    private int rightIndex;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        elements = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            elements[i] = transform.GetChild(i);
        }
        rightIndex = elements.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraTransform.position.x > (elements[rightIndex].transform.position.x - viewZone))
        {
            ScrollRight();
        }
    }

    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        elements[leftIndex].position = Vector3.right * (elements[rightIndex].position.x + elementXSize) + Vector3.up * elements[rightIndex].position.y;
        rightIndex = leftIndex;
        leftIndex++;
        if(leftIndex == elements.Length)
        {
            leftIndex = 0;
        }
    }
}
