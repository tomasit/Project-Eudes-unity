
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescaleCanva : MonoBehaviour
{
    private void OnRectTransformDimensionsChange()
    {
        Camera.main.GetComponent<FitWithWorldSize>().RescaleCamera();
    }
}