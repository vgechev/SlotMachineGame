namespace SlotMachine.Modules.Helpers
{
    public static class InputHelper
    {
        public static decimal ReadInputSafely()
        {
            decimal value;

            while (!decimal.TryParse(Console.ReadLine(), out value))
                Console.WriteLine("Invalid input. Please enter a valid decimal number.");

            return value;
        }
    }
}