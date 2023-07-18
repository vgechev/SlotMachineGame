using SlotMachine.Models;
using SlotMachine.Modules;
using SlotMachine.Modules.Helpers;

namespace SlotMachine
{
	public class SlotMachine
	{
		private const int rows = 4;
		private const int columns = 3;
		private Symbol[,] grid = new Symbol[rows, columns];
		private decimal playerBalance;

		public void PlayGame()
		{
			Console.WriteLine("Welcome to the Slot Machine Game!");
			Console.WriteLine("Please enter the initial deposit amount: ");

			playerBalance += InputHelper.ReadInputSafely();

			while (playerBalance > 0)
			{
				Console.WriteLine($"Current Balance: {playerBalance}");
				Console.WriteLine("Enter the stake amount for the next spin (0 to quit): ");

				decimal stakeAmount = InputHelper.ReadInputSafely();

				if (stakeAmount == 0)
				{
					Console.WriteLine("Thank you for playing! Goodbye!");
					return;
				}

				if (stakeAmount > playerBalance)
				{
					Console.WriteLine("Insufficient balance. Please enter a lower stake amount.");
					continue;
				}

				decimal winnings = ExecuteSpin(stakeAmount);
				DisplaySpinResult(winnings);
				Calculator.UpdateBalance(ref playerBalance, winnings, stakeAmount);
			}

			Console.WriteLine("Game over! Your balance reached 0.");
		}


		private void DisplaySpinResult(decimal winnings)
		{
			Console.WriteLine("Spin Result:");

			for (int row = 0; row < grid.GetLength(0); row++)
			{
				Console.WriteLine("-----------------------------");

				for (int col = 0; col < grid.GetLength(1); col++)
					Console.Write($"{grid[row, col].Name} | ");

				Console.WriteLine();
			}

			Console.WriteLine("-----------------------------");
			Console.WriteLine($"Winnings: {winnings}");
		}

		private decimal ExecuteSpin(decimal stakeAmount)
		{
			for (int row = 0; row < grid.GetLength(0); row++)
			{
				Symbol[] arr = Symbol.GetRandomSymbolsForRow(columns);

				for (int col = 0; col < grid.GetLength(1); col++)
					grid[row, col] = arr[col];
			}

			return Calculator.CalculateWinnings(grid, stakeAmount, columns);
		}
	}
}