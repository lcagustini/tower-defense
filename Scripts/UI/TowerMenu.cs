using Godot;
using System;

public partial class TowerMenu : Control
{
	[Export] private PackedScene towerPrefab;
	[Export] private int towerCost = 40;
	[Export] private float maxMouseRayDistance = 100;

	[Export] private Economy economy;

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
                    if (economy.CurrentMoney < towerCost) break;

                    query.From = camera.ProjectRayOrigin(iemb.Position);
                    query.To = query.From + camera.ProjectRayNormal(iemb.Position) * maxMouseRayDistance;
                    Godot.Collections.Dictionary queryResult = space.IntersectRay(query);

                    if (queryResult.Count <= 0) break;

                    economy.CurrentMoney -= towerCost;
                    Tower tower = towerPrefab.Instantiate<Tower>();
                    GetTree().CurrentScene.AddChild(tower);
                    tower.GlobalPosition = queryResult[QueryPosition].AsVector3();
                }
                break;
        }
    }
}
