using UnityEngine;

public class Background : MonoBehaviour
{
	[SerializeField] private float rotationSpeed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
		transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }
}
