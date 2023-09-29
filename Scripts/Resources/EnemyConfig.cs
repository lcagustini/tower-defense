using Godot;
using System;

public enum EnemyType
{
    Basic,
}

public partial class EnemyConfig : Resource
{
    [Export] public EnemyType type;
    [Export] public PackedScene prefab;
}
