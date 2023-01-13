using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestController : AView 
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            string[] jName = Input.GetJoystickNames();
            string concat = null;
            foreach (var s in jName)
            {
                Debug.Log(s);
                concat += s + "\n";
            }

            if (concat == null)
            {
                Debug.Log("Concat est null");
                GetComponent<TextMeshProUGUI>().text = "Ya rien zebi";
            }
            else
            {
                GetComponent<TextMeshProUGUI>().text = concat;
            }
        }
    }
}