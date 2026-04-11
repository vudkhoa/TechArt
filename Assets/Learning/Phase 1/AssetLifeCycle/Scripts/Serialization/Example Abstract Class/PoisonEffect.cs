using UnityEngine;

/*[CreateAssetMenu(
    fileName = "PoisonEffect",
    menuName = "Phase1/Serialization/Polymorphism/PoisonEffect"
)]*/
public class PoisonEffect : StatusEffect
{
    public float damagePerSecond;
    public override void Apply() { /* ... */ }
}