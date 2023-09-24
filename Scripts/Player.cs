using Godot;
using System;

public partial class Player : Node3D
{
    [Export] public Economy economy;
    [Export] public Health health;

    private void OnDamageTaken(float currentHealth, float damageTaken)
    {
        if (currentHealth <= 0)
        {
            GetTree().Quit();
        }
    }
}
