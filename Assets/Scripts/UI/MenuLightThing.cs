using UnityEngine;
using UnityEngine.UI;

public class MenuLightThing : MonoBehaviour
{
    [SerializeField] private Image targetImg;
    private float speed = 2f;
    private float minAlpha = 0.2f;
    private float maxAlpha = 0.7f;
    private float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * speed;
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(time) + 1f) / 2f);

        Color current = targetImg.color;
        current.a = alpha;
        targetImg.color = current;
    }
}
