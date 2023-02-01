
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescaleCanva : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void OnRectTransformDimensionsChange()
    {
        if (_camera != null)
            _camera.GetComponent<FitWithWorldSize>().RescaleCamera();
    }
}