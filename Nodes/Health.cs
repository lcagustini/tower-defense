using Godot;
using System;

public partial class Health : Node
{
    [Signal] public delegate void OnDamageTakenEventHandler(float amount, bool died);

    [Export] public float maxHealth = 100;
    public float currentHealth;

    public override void _Ready()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;

        EmitSignal(SignalName.OnDamageTaken, amount, currentHealth <= 0);
    }
}
