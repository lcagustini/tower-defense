using Godot;
using System;
using System.Collections.Generic;

public partial class Tower : Node3D
{
    [Export] private GpuParticles3D attack;
    [Export] private float damageAmount = 10;

    private List<Health> enemiesInRange = new List<Health>();

    public void Attack()
    {
        if (enemiesInRange.Count == 0) return;

        Health enemyHealth = enemiesInRange[GD.RandRange(0, enemiesInRange.Count - 1)];
        Node3D enemy = enemyHealth.GetParent<Node3D>();

        enemyHealth.Damage(damageAmount);
        attack.LookAt(enemy.GlobalPosition);
        attack.EmitParticle(attack.Transform, Vector3.Zero, Color.Color8(0, 0, 0), Color.Color8(0, 0, 0), 0);
    }

    private void BodyEntered(Node3D body)
    {
        Health enemy = body.GetNode<Health>("Health");

        if (enemy == null) return;

        enemiesInRange.Add(enemy);
    }

    private void BodyExited(Node3D body)
    {
        Health enemy = body.GetNode<Health>("Health");

        if (enemy == null) return;

        enemiesInRange.Remove(enemy);
    }
}
