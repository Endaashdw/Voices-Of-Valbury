using UnityEngine;

public class Background : MonoBehaviour
{
	[SerializeField] private float rotationSpeed = 5.0f;
    void Update()
    {
		transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }
}
