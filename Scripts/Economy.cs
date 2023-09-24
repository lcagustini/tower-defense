using Godot;
using System;

public partial class Economy : Node
{
    [Signal] public delegate void OnMoneyChangeEventHandler(int amount, int difference);

    [Export] private int startingMoney;

    private int currentMoney;
    public int CurrentMoney
    {
        get => currentMoney;
        set
        {
            EmitSignal(SignalName.OnMoneyChange, value, value - currentMoney);
            currentMoney = value;
        }
    }

    public override void _Ready()
    {
        CurrentMoney = startingMoney;
    }
}
