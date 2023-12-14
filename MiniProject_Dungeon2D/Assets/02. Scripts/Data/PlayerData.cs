
using System;

[Serializable]
public class PlayerData
{
    public int Level { get; set; } = 1;
    public float HpCurrent { get; set; } = 1000f;
    public float HpMax { get; set; } = 1000f;
    public float StaminaCurrent { get; set; } = 100f;
    public float StaminaMax { get; set; } = 100f;
    public float ExpCurrent { get; set; } = 0f;
    public float ExpMax { get; set; } = 200f;
    public float BaseDamage { get; set; } = 50f;
    public float AttackDamage { get; set; } = 50f;
    public float BaseArmor { get; set; } = 5f;
    public float DefenseArmor { get; set; } = 5f;
    public float MovementSpeed { get; set; } = 10f;

    public int Gold { get; set; } = 30000;
    public int Gem { get; set; } = 0;
}
