using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float _amplitude = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, MathF.Sin(Time.time) * _amplitude, 0);
    }
}
