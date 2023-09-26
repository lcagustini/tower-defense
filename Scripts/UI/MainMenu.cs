using Godot;
using System;

public partial class MainMenu : Control
{
    private void StartGame()
    {
        GetTree().ChangeSceneToFile("Scenes/game.tscn");
    }
}
