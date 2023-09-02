using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompressingEffect : MonoBehaviour
{
    public Transform objectToTransform;
    public float holdDurationThreshold = 0.5f; // Time threshold for considering the key held down
    public float scaleMultiplier = 2f; // Multiplier for scaling the object

    private bool isHolding = false;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        // Check for single click and hold of the "C" key
        if (Input.GetKeyDown(KeyCode.C))
        {
            startTime = Time.time;
            isHolding = true;
        }

        if (Input.GetKey(KeyCode.C) && isHolding)
        {
            float elapsedTime = Time.time - startTime;
            if (elapsedTime >= holdDurationThreshold)
            {
                ApplyTransformation();
            }
        }

        // Release the "C" key
        if (Input.GetKeyUp(KeyCode.C))
        {
            isHolding = false;
        }
    }

    private void ApplyTransformation()
    {
        Vector3 scaleChange = new Vector3(scaleMultiplier, 1f, 1f);
        objectToTransform.localScale = Vector3.Scale(objectToTransform.localScale, scaleChange);
    }
}



