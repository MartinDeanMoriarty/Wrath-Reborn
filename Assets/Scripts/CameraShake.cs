using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float positionAmplitude = 0.1f;
    public float rotationAmplitude = 1.0f;
    public float noiseFrequency = 1.0f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        float time = Time.time * noiseFrequency;

        Vector3 randomPositionOffset = new Vector3(
            Mathf.PerlinNoise(time, 0) * 2 - 1,
            Mathf.PerlinNoise(time, 100) * 2 - 1,
            Mathf.PerlinNoise(time, 200) * 2 - 1) * positionAmplitude;

        Vector3 randomRotationOffset = new Vector3(
            Mathf.PerlinNoise(time, 300) * 2 - 1,
            Mathf.PerlinNoise(time, 400) * 2 - 1,
            Mathf.PerlinNoise(time, 500) * 2 - 1) * rotationAmplitude;

        transform.position = originalPosition + randomPositionOffset;
        transform.rotation = Quaternion.Euler(originalRotation.eulerAngles + randomRotationOffset);
    }
}
