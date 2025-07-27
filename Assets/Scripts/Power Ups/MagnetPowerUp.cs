using UnityEngine;

public class MagnetPowerUp : PowerUp
{
    [SerializeField] private float magnetRadius = 10f;
    [SerializeField] private float attractSpeed = 15f;

    protected override void ApplyPowerUp(GameObject player)
    {
        player.GetComponent<PowerUpController>().ActivateMagnet(duration, magnetRadius, attractSpeed);
    }
}