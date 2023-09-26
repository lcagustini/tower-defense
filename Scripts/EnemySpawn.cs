using Godot;
using Godot.Collections;
using System;

public partial class EnemySpawn : Node3D
{
    [Export] private Array<WaveConfig> waveConfigs;
    private int currentWave;

    [Export] private PackedScene enemyPrefab;
    [Export] private Node3D enemyTarget;

    private void SpawnEnemy()
    {
        Enemy enemy = enemyPrefab.Instantiate<Enemy>();
        GetTree().CurrentScene.AddChild(enemy);
        enemy.GlobalPosition = GlobalPosition;
        enemy.agent.TargetPosition = enemyTarget.GlobalPosition;
    }
}
