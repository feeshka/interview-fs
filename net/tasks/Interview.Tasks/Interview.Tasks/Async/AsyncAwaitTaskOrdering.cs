using System;
using System.Threading.Tasks;

namespace Interview.Tasks.Async
{
    public static class AsyncAwaitTaskOrdering
    {
        public static async Task StartTasks()
		{
            var createSandwichTask = CreateSandwich();
            var turnOnTheKattleTask = TurnOnTheKettle();

            await Task.WhenAll(createSandwichTask, turnOnTheKattleTask);
        }

        private static async Task CreateSandwich()
		{
            Console.WriteLine("Начали готовить сендвич.");
            Task.Delay(1000);
            Console.WriteLine("Сэндвич готов.");
        }

        private static async Task TurnOnTheKettle()
		{
            Console.WriteLine("Чайник включен.");
            await Task.Delay(500);
            Console.WriteLine("Чайник закипел.");
        }
    }
}
