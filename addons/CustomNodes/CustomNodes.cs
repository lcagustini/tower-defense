#if TOOLS
using Godot;
using System;

[Tool]
public partial class CustomNodes : EditorPlugin
{
    public override void _EnterTree()
    {
        // Initialization of the plugin goes here.
        // Add the new type with a name, a parent type, a script and an icon.
        Script script = GD.Load<Script>("Nodes/Health.cs");
        Texture2D texture = GD.Load<Texture2D>("Nodes/Health.png");
        AddCustomType("Health", "Node", script, texture);
    }

    public override void _ExitTree()
    {
        // Clean-up of the plugin goes here.
        // Always remember to remove it from the engine when deactivated.
        RemoveCustomType("Health");
    }
}
#endif
