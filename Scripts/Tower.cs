using Godot;
using System;
using System.Collections.Generic;

public partial class Tower : Node3D
{
    [Export] private float damageAmount = 10;

    private List<Health> enemiesInRange = new List<Health>();

    public void Attack()
    {
        if (enemiesInRange.Count == 0) return;

        Health randomEnemy = enemiesInRange[GD.RandRange(0, enemiesInRange.Count - 1)];
        randomEnemy.Damage(damageAmount);
    }

    private void BodyEntered(Node3D body)
    {
        Health enemy = body.GetNode<Health>(body.GetPath() + "/Health");

        if (enemy == null) return;

        enemiesInRange.Add(enemy);
    }

    private void BodyExited(Node3D body)
    {
        Health enemy = body.GetNode<Health>(body.GetPath() + "/Health");

        if (enemy == null) return;

        enemiesInRange.Remove(enemy);
    }
}
