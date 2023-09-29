using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class EnemySpawn : Node3D
{
    [Export] private Array<WaveConfig> waveConfigs;
    private int currentWave = -1;

    [Export] private Node3D enemyTarget;

    private List<Enemy> spawnedEnemies = new List<Enemy>();

    public override void _Process(double delta)
    {
        if (spawnedEnemies.Count == 0)
        {
            currentWave++;

            if (currentWave >= waveConfigs.Count) GetTree().Quit();
            else SpawnWave();
        }
    }

    private void SpawnWave()
    {
        Node scene = GetTree().CurrentScene;
        foreach (EnemyConfig config in waveConfigs[currentWave].spawns)
        {
            Enemy enemy = config.prefab.Instantiate<Enemy>();

            scene.AddChild(enemy);
            spawnedEnemies.Add(enemy);

            enemy.GlobalPosition = GlobalPosition;
            enemy.agent.TargetPosition = enemyTarget.GlobalPosition;
            enemy.OnDespawnCallback = (e) => spawnedEnemies.Remove(e);
        }
    }
}
