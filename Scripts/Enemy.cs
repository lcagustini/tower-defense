using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
    [Export] public float speed = 5.0f;

    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    [Export] public NavigationAgent3D agent;

    public override void _PhysicsProcess(double delta)
    {
        float dt = (float)delta;

        Vector3 position = GlobalPosition;

        Vector3 nextPosition = agent.GetNextPathPosition();
        Vector3 velocity = (nextPosition - position).Normalized() * speed * dt;

        if (!IsOnFloor()) velocity.Y -= gravity * dt;

        Velocity = Velocity.MoveToward(velocity, 0.25f);
        MoveAndSlide();
    }

    private void ReachedTarget()
    {
        QueueFree();
    }

    private void OnDamageTaken(float amount, bool died)
    {
        if (died) QueueFree();
    }
}
