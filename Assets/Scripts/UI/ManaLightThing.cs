using UnityEngine;
using UnityEngine.UI;

public class ManaLightThing : MonoBehaviour
{
    [SerializeField] private Image targetImg;
    [SerializeField] private AutoShooter autoShooter;

    private float speed = 3f;
    private float time;

    void Update()
    {
        if (autoShooter == null) return;

        float energyPercent =
            autoShooter.GetCurrentEnergy() / 5f;

        bool lowMana = energyPercent < 0.25f;

        if (lowMana)
        {
            time += Time.deltaTime * speed;

            float pulse = (Mathf.Sin(time) + 1f) / 2f;

            targetImg.color =
                Color.Lerp(Color.white, Color.red, pulse);
        }
        else
        {
            time = 0f;
            targetImg.color = Color.white;
        }
    }
}