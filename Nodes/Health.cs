using Godot;
using System;

public partial class Health : Node
{
    [Signal] public delegate void OnHealthChangeEventHandler(float currentHealth, float damageTaken);

    [Export] private float maxHealth = 100;

    private float currentHealth;
    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            EmitSignal(SignalName.OnHealthChange, value, value - currentHealth);
            currentHealth = value;
        }
    }

    public override void _Ready()
    {
        CurrentHealth = maxHealth;
    }

    public void Damage(float amount)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= amount;
    }
}
