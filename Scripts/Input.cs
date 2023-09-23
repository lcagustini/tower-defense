using Godot;
using System;

public partial class Input : Node
{
	[Export] private PackedScene towerPrefab;
	[Export] private float maxMouseRayDistance = 100;

    private Camera3D camera;
    private PhysicsDirectSpaceState3D space;
    private PhysicsRayQueryParameters3D query;

    public static readonly StringName QueryPosition = "position";

    public override void _Ready()
    {
        camera = GetViewport().GetCamera3D();
        space = camera.GetWorld3D().DirectSpaceState;

        query = new PhysicsRayQueryParameters3D
        {
            CollideWithBodies = true,
            CollisionMask = 4
        };
    }

    public override void _Input(InputEvent inputEvent)
    {
        switch (inputEvent)
        {
            case InputEventMouseButton iemb:
                if (iemb.IsReleased() && iemb.ButtonIndex == MouseButton.Left)
                {
                    Tower tower = towerPrefab.Instantiate<Tower>();
                    GetTree().CurrentScene.AddChild(tower);

                    query.From = camera.ProjectRayOrigin(iemb.Position);
                    query.To = query.From + camera.ProjectRayNormal(iemb.Position) * maxMouseRayDistance;
                    Godot.Collections.Dictionary queryResult = space.IntersectRay(query);

                    tower.GlobalPosition = queryResult[QueryPosition].AsVector3();
                }
                break;
        }
    }
}
