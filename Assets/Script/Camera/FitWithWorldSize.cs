using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitWithWorldSize : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    public void RescaleCamera()
    {
        FitLevelSize(_sprite.bounds.size.x, _sprite.bounds.size.y);   
    }

    private void FitLevelSize(float width, float height)
    {
        GetComponent<Camera>().orthographicSize = ((width > height * GetComponent<Camera>().aspect) ? (float)width / (float)GetComponent<Camera>().pixelWidth * GetComponent<Camera>().pixelHeight : height) / 2;
    }
}