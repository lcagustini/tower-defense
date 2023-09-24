using Godot;
using System;

public enum TowerType
{
    Basic,
}

public partial class TowerConfig : Resource
{
    [Export] public TowerType type;
    [Export] public Texture2D icon;
    [Export] public PackedScene prefab;
    [Export] public int cost;
}
