using UnityEngine;

/*[CreateAssetMenu(
    fileName = "SlowEffect",
    menuName = "Phase1/Serialization/Polymorphism/SlowEffect"
)]*/
public class SlowEffect : StatusEffect
{
    public float speedMultiplier;
    public override void Apply() { /* ... */ }
}