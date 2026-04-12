using UnityEngine;

public class ShieldPowerUp : PowerUp
{
    protected override void ApplyPowerUp(GameObject player)
    {
        player.GetComponent<PowerUpController>().ActivateShield(duration);
    }
}