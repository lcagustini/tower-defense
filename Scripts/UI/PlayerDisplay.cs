using Godot;
using System;

public partial class PlayerDisplay : Node
{
	[Export] private Label moneyAmount;
	[Export] private Label healthAmount;

	public void OnHealthChange(float amount, float difference)
	{
		healthAmount.Text = $"{amount}";
	}

	public void OnMoneyChange(int amount, int difference)
	{
		moneyAmount.Text = $"${amount}";
	}
}
