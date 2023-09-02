using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShearScaleExample : MonoBehaviour
{
    public Transform targetTransform; // Reference to the Transform component of the GameObject to shear scale
    public Vector3 shearAmount = new Vector3(0.0f, 0.0f, 0.0f); // Shear amount in each axis

    private Matrix4x4 shearMatrix;

    private bool isHoldingF = false;

    private void Start()
    {
        // Initialize the shear matrix
        shearMatrix = Matrix4x4.identity;
    }

    private void Update()
    {
        // Check for holding the "F" key
        if (Input.GetKey(KeyCode.F))
        {
            isHoldingF = true;
        }
        else
        {
            isHoldingF = false;
        }

        // Update the shear matrix based on the shear amount
        UpdateShearMatrix();

        // Apply shear scaling to the target Transform when "C" key is held
        if (isHoldingF)
        {
            ApplyShearScale();
        }

        else if (!isHoldingF)
        {
            shearMatrix.m01 = 0f;
            shearMatrix.m02 = 0f;
            shearMatrix.m10 = 0f;
            
            ApplyShearScale();
        }
    }

    private void UpdateShearMatrix()
    {
        // Update the shear matrix based on the shear amount
        shearMatrix.m01 = 1f;
        shearMatrix.m02 = 1f;
        shearMatrix.m10 = -0.5f;
        
    }

    private void ApplyShearScale()
    {
        // Apply the shear matrix to the local scale of the target Transform
        targetTransform.localScale = shearMatrix.MultiplyPoint(Vector3.one / 10);
    }
}


