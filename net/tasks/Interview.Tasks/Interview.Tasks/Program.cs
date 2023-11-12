using Interview.Tasks.Async;
using System.Threading.Tasks;

namespace Interview.Tasks
{
	class Program
	{
		static async Task Main (string[] args)
		{
			await AsyncAwaitTaskOrdering.StartTasks();
		}
	}
}
