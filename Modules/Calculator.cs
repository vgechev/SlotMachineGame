using SlotMachine.Models;

namespace SlotMachine
{
	public static class Calculator
	{
		public static decimal CalculateRowWinnings(Symbol[] rowSymbols, decimal stakeAmount)
		{
			decimal rowWinnings = 0;

			if (rowSymbols.All(s => s == rowSymbols.FirstOrDefault(x => x.Name != "Wildcard") || s.Name == "Wildcard"))
				rowWinnings += rowSymbols.Where(s => s.Name != "Wildcard").Sum(s => s.Coefficient);

			return rowWinnings * stakeAmount;
		}

		public static decimal CalculateWinnings(Symbol[,] grid, decimal stakeAmount, int columns)
		{
			decimal totalWinnings = 0;
			Symbol[] rowSymbols = new Symbol[columns];

			for (int row = 0; row < grid.GetLength(0); row++)
			{
				for (int col = 0; col < grid.GetLength(1); col++)
					rowSymbols[col] = grid[row, col];

				totalWinnings += CalculateRowWinnings(rowSymbols, stakeAmount);
			}

			return totalWinnings;
		}

		public static void UpdateBalance(ref decimal playerBalance, decimal winnings, decimal stakeAmount) =>
			playerBalance = playerBalance - stakeAmount + winnings;
	}
}