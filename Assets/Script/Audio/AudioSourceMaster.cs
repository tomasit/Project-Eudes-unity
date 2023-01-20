using UnityEngine;
using System.Collections;

public class AudioSourceMaster : MonoBehaviour
{
    public enum AudioName {
        click_on_button
    }

    [SerializeField] private AudioSource[] _sources;

    public AudioSource GetAudioSource(AudioName name)
    {
        if (name == AudioName.click_on_button)
            return _sources[0];
        return null;
    }
}