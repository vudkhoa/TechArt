using System.Collections.Generic;
using UnityEngine;

namespace Learning.Phase1
{
    public class StatusManager : MonoBehaviour
    {
        // KHONG dung [SerializeReference] -> Unity chi serialize StatusEffect fields
        // va MAT het derived data (damagePerSecond, speedMultiplier)
        // public List<StatusEffect> effects;  // SAI

        // DUNG [SerializeReference] -> Unity ghi lai TYPE THAT SU va data day du
        [SerializeReference]
        private List<StatusEffect> effects = new List<StatusEffect>
        {
            new SlowEffect(),
            new PoisonEffect()
        };
    }
}