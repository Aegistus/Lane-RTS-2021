using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForward : MonoBehaviour
{
    [Range(0, 5)]
    public float TimeScale = 1.0f;

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = TimeScale;
        fastForwardKey();

    }

    void fastForwardKey()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            TimeScale = 0f;
        }

        if (Input.GetKey(KeyCode.F1))
        {
            TimeScale = 1f;
        }

        if(Input.GetKey(KeyCode.F2))
        {
            TimeScale = 2f;
        }

        if(Input.GetKey(KeyCode.F3))
        {
            TimeScale = 3f;
        }

        if (Input.GetKey(KeyCode.F4))
        {
            TimeScale = 4f;
        }

        if (Input.GetKey(KeyCode.F5))
        {
            TimeScale = .5f;
        }

    }


}
