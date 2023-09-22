using Godot;
using System;
using System.Collections.Generic;

public partial class EnemySpawn : Node3D
{
	[Export] private PackedScene enemyPrefab;
    [Export] private Node3D enemyTarget;

    private void TimerTick()
    {
        Enemy enemy = enemyPrefab.Instantiate<Enemy>();
        GetTree().CurrentScene.AddChild(enemy);
        enemy.GlobalPosition = GlobalPosition;
        enemy.agent.TargetPosition = enemyTarget.GlobalPosition;
    }
}
