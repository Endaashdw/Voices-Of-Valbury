using UnityEngine;

public class Spin3 : MonoBehaviour
{
    public Vector3 rotationAxis = new Vector3(1, 1, 0);
    public float rotationSpeed = -30f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime, Space.Self);

        float bob = Mathf.Sin(Time.time) * 0.1f;
        transform.position = startPos + new Vector3(0, bob, 0);
    }
}
