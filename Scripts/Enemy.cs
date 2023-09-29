using Godot;
using System;
using System.Threading.Tasks;

public partial class Enemy : CharacterBody3D
{
    [Export] public float damage = 10;
    [Export] public int killReward = 10;
    [Export] public float speed = 5.0f;

    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    [Export] public NavigationAgent3D agent;

    private Player player;

    public Action<Enemy> OnDespawnCallback;

    public override void _Ready()
    {
        player = GetTree().CurrentScene.GetNode<Player>("Player");
    }

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
        player.health.Damage(damage);
        Despawn();
    }

    private void OnDamageTaken(float currentHealth, float damageTaken)
    {
        if (currentHealth <= 0)
        {
            player.economy.CurrentMoney += killReward;
            Despawn();
        }
    }

    private void Despawn()
    {
        OnDespawnCallback?.Invoke(this);
        QueueFree();
    }
}
