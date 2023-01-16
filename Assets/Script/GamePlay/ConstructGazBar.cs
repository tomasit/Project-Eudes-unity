using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ConstructGazBar : MonoBehaviour
{
    [Range(0.05f, 0.3f)][SerializeField] private float _toleranceHeight;
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject[] _composite;
    [SerializeField] private Color[] _colors;
    private GameObject _toleranceGO;
    public float _sideSize = 0.1f;
    [SerializeField] private GameObject _arrow;

    private void Awake()
    {
        GameObject bg = (GameObject)Instantiate(_composite[0], _parent.transform) as GameObject;
        bg.transform.localScale = new Vector3(1, ScaleWithScreen.GetScreenToWorldHeight, 1);
        bg.GetComponent<SpriteRenderer>().color = _colors[0];

        var bounds = bg.GetComponent<SpriteRenderer>().bounds.size;

        _toleranceGO = (GameObject)Instantiate(_composite[1], _parent.transform) as GameObject;
        _toleranceGO.transform.localScale = new Vector3(bg.transform.localScale.x - 2 * _sideSize
        , bg.transform.localScale.y * _toleranceHeight, 1);
        _toleranceGO.transform.localPosition = Vector3.zero;
        _toleranceGO.GetComponent<SpriteRenderer>().color = _colors[1];
        _toleranceGO.GetComponent<SpriteRenderer>().sortingOrder = 1;

        var b2 = _toleranceGO.GetComponent<SpriteRenderer>().bounds.size;

        GameObject inter1 = (GameObject)Instantiate(_composite[1], _parent.transform) as GameObject;
        inter1.transform.localScale = new Vector3(bg.transform.localScale.x - 2 * _sideSize
        , (bg.transform.localScale.y - _toleranceGO.transform.localScale.y) * 0.5f - 2 * _sideSize, 1);
        inter1.transform.localPosition = new Vector3(0,
            (bounds.y * 0.5f - inter1.GetComponent<SpriteRenderer>().bounds.size.y * 0.5f) - _sideSize, 0);
        inter1.GetComponent<SpriteRenderer>().color = _colors[1];
        inter1.GetComponent<SpriteRenderer>().sortingOrder = 1;

        GameObject inter2 = (GameObject)Instantiate(_composite[1], _parent.transform) as GameObject;
        inter2.transform.localScale = new Vector3(bg.transform.localScale.x - 2 * _sideSize
        , (bg.transform.localScale.y - _toleranceGO.transform.localScale.y) * 0.5f - 2 * _sideSize, 1);
        inter2.transform.localPosition = new Vector3(0, 
            (-bounds.y * 0.5f + inter2.GetComponent<SpriteRenderer>().bounds.size.y * 0.5f) + _sideSize, 0);
        inter2.GetComponent<SpriteRenderer>().color = _colors[1];
        inter2.GetComponent<SpriteRenderer>().sortingOrder = 1;

        var b4 = _arrow.GetComponent<SpriteRenderer>().bounds.size;
        _arrow.transform.localScale = new Vector3(b2.x / b4.x, b2.y / b4.y, b2.z / b4.z);
        ResetArrowPosition();
    }

    public void ResetArrowPosition()
    {
        _arrow.transform.position = this.transform.position;
    }

    public float GetTolerance()
    {
        return _toleranceHeight;
    }

    public GameObject GetToleranceGO()
    {
        return _toleranceGO;
    }
}