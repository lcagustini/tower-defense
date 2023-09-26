using Godot;
using System;

public partial class Rotate : Node3D
{
	[Export] private Vector3 axis;
	[Export] private float speed;

	public override void _Process(double delta)
	{
        GlobalRotate(axis, (float)delta * speed);
    }
}
