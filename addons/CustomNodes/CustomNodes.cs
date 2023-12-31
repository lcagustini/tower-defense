#if TOOLS
using Godot;
using System;

[Tool]
public partial class CustomNodes : EditorPlugin
{
    public override void _EnterTree()
    {
        Script script = GD.Load<Script>("Nodes/Health.cs");
        Texture2D texture = GD.Load<Texture2D>("Nodes/Health.png");
        AddCustomType("Health", "Node", script, texture);

        script = GD.Load<Script>("Scripts/Resources/TowerConfig.cs");
        AddCustomType("TowerConfig", "Resource", script, null);

        script = GD.Load<Script>("Scripts/Resources/WaveConfig.cs");
        AddCustomType("WaveConfig", "Resource", script, null);

        script = GD.Load<Script>("Scripts/Resources/EnemyConfig.cs");
        AddCustomType("EnemyConfig", "Resource", script, null);
    }

    public override void _ExitTree()
    {
        RemoveCustomType("Health");
        RemoveCustomType("TowerConfig");
        RemoveCustomType("WaveConfig");
        RemoveCustomType("EnemyConfig");
    }
}
#endif
