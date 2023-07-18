namespace SlotMachine.Models
{
	public record Symbol(string Name, decimal Coefficient, decimal Probability)
	{
		private static readonly Random random = new();

		public static Symbol[] Symbols => new Symbol[]
		{
			new Symbol("Apple", 0.4m, 0.45m),
			new Symbol("Banana", 0.6m, 0.35m),
			new Symbol("Pineapple", 0.8m, 0.15m),
			new Symbol("Wildcard", 0, 0.05m)
		};

		public static Symbol[] GetRandomSymbolsForRow(int columns)
		{
			Symbol[] rowSymbols = new Symbol[columns];

			for (int column = 0; column < columns; column++)
			{
				decimal randomValue = (decimal)random.NextDouble();
				decimal cumulativeProbability = 0;

				foreach (Symbol symbol in Symbols)
				{
					cumulativeProbability += symbol.Probability;

					if (randomValue <= cumulativeProbability)
					{
						rowSymbols[column] = symbol;
						break;
					}
				}
			}

			return rowSymbols;
		}
	};
}