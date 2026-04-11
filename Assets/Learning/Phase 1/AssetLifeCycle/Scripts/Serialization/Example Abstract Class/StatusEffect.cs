// TRUOC Unity 2019.3: Khong the serialize polymorphic data
// Unity chi serialize CONCRETE type, khong serialize derived type qua base reference
// SAU Unity 2019.3: [SerializeReference] cho phep serialize polymorphism
using System;

[Serializable]
public abstract class StatusEffect // : ScriptableObject
{
    public float duration;
    public abstract void Apply();
}