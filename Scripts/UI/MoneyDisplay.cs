using Godot;
using System;

public partial class MoneyDisplay : Node
{
	[Export] private Label moneyAmount;

	public void OnMoneyChange(int amount, int difference)
	{
		moneyAmount.Text = $"${amount}";
	}
}
