using Godot;
using Godot.Collections;
using System;

public partial class WaveConfig : Resource
{
    [Export] public Array<EnemyConfig> spawns;
}
