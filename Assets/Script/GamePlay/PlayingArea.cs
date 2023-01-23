using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingArea : MonoBehaviour
{
    [SerializeField] private GameObject _area;
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _pedals;
    [SerializeField] private GameObject _pitchAndRoll;
    [SerializeField] private GameObject _gaz;

    private void Start()
    {
        RestartSession();
    }

    public void EnableArea(bool enable)
    {
        _target.SetActive(enable);
        _pedals.SetActive(enable);
        _pitchAndRoll.SetActive(enable);
        _gaz.SetActive(enable);
    }

    public Vector2 GetAreaBounds()
    {
        return _area.GetComponent<SpriteRenderer>().bounds.size;
    }

    public Vector3 GetAreaPosition()
    {
        return _area.transform.position;
    }

    public void RestartSession()
    {
        var bounds = GetAreaBounds();
        _target.transform.position = new Vector3(_area.transform.position.x, _area.transform.position.y, _target.transform.position.z);
        _pedals.transform.position = new Vector3(_area.transform.position.x, _area.transform.position.y, _pedals.transform.position.z);
        _pitchAndRoll.transform.position = new Vector3(_area.transform.position.x, _area.transform.position.y - _pitchAndRoll.GetComponent<PitchAndRoll>().GetSpriteBounds().size.y * 0.5f, _pitchAndRoll.transform.position.z);
    }
}