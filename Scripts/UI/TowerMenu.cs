using Godot;
using System;

public partial class TowerMenu : Control
{
    [Export] private Godot.Collections.Array<TowerConfig> towerConfigs;
    [Export] private PackedScene buttonPrefab;

    [Export] private Economy economy;
    [Export] private Container buttonContainer;

    [Export] private float maxMouseRayDistance = 100;

    private Camera3D camera;
    private PhysicsDirectSpaceState3D space;
    private PhysicsRayQueryParameters3D query;

    private TowerConfig currentConfig;

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

        foreach (TowerConfig config in towerConfigs)
        {
            Node buttonInstance = buttonPrefab.Instantiate<Node>();

            buttonInstance.GetNode<Label>("Cost").Text = $"${config.cost}";
            Button button = buttonInstance.GetNode<Button>("Button");
            button.Icon = config.icon;
            button.Pressed += () => SelectedTower(config.type);

            buttonContainer.AddChild(buttonInstance);
        }
    }

    public void SelectedTower(TowerType tower)
    {
        TowerConfig selectedConfig = towerConfigs[(int)tower];
        if (currentConfig == selectedConfig) currentConfig = null;
        else currentConfig = selectedConfig;
    }

    public override void _Input(InputEvent inputEvent)
    {
        switch (inputEvent)
        {
            case InputEventMouseButton iemb:
                if (currentConfig == null) break;
                if (economy.CurrentMoney < currentConfig.cost) break;

                if (iemb.IsReleased() && iemb.ButtonIndex == MouseButton.Left)
                {
                    query.From = camera.ProjectRayOrigin(iemb.Position);
                    query.To = query.From + camera.ProjectRayNormal(iemb.Position) * maxMouseRayDistance;
                    Godot.Collections.Dictionary queryResult = space.IntersectRay(query);

                    if (queryResult.Count <= 0) break;

                    economy.CurrentMoney -= currentConfig.cost;
                    Tower tower = currentConfig.prefab.Instantiate<Tower>();
                    GetTree().CurrentScene.AddChild(tower);
                    tower.GlobalPosition = queryResult[QueryPosition].AsVector3();

                    currentConfig = null;
                }
                break;
        }
    }
}
