using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class test : MonoBehaviour
{
    private void Update()
    {
        float rubberInput = 0.0f;
//        rubberInput = Input.GetAxis("Rubber");
        float pitchAndRoll = 0.0f;
        pitchAndRoll = Input.GetAxis("PitchAndRollX");
        float pitchAndRollY = 0.0f;
        pitchAndRollY = Input.GetAxis("PitchAndRollY");
        // float throttle = 0.0f;
        // throttle = Input.GetAxis("Throttle");

//        Debug.Log("Rubber = " + rubberInput);
        Debug.Log("Pitch and Roll x = " + pitchAndRoll);
        Debug.Log("Pitch and Roll y = " + pitchAndRollY);
//        Debug.Log("throttle = " + throttle);
    }
}
