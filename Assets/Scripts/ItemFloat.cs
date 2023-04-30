using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    public float floatAmplitude = 0.1f;
    public float floatSpeed = 1f;
    public float rotateSpeed = 50.0f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Float up and down
        Vector3 pos = startPos;
        pos.y += Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = pos;

        // Rotate around own axis
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }
}
