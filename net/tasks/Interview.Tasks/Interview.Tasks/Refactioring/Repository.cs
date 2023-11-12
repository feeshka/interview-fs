using System;
using System.Threading.Tasks;

namespace Interview.Tasks.Refactioring
{
	class Repository
	{
		public DbContext context = new DbContext();

		public Repository ()
		{
		}

		public async Task CreateUser (User newUser)
		{
			var user = context.Users.GetUserById(newUser.UserId);

			if (user != null)
			{
				throw new Exception();
			}

			if (string.IsNullOrEmpty(newUser.UserUserName))
			{
				throw new Exception();
			}

			await context.Users.Insert(newUser);
			await context.SaveAsync();
		}

		public async Task Delete (User user)
		{
			await context.Users.Delete(user.UserId);
			await context.SaveAsync();
		}
	}

	record User (int UserId, string UserName, string UserUserName);





	// Вспомогательные классы


	class DbContext
	{
		public UserTable Users { get; set; }
		public Task SaveAsync ()
		{
			Console.WriteLine("Saved.");
			return Task.CompletedTask;
		}
	}

	interface IUserTable
	{
		Task<User> GetUserById (int userId);
		Task Insert (User user);
		Task Delete (int userId);
	}

	class UserTable : IUserTable
	{
		public Task Delete (int userId)
		{
			throw new NotImplementedException();
		}

		public Task<User> GetUserById (int userId)
		{
			throw new NotImplementedException();
		}

		public Task Insert (User user)
		{
			throw new NotImplementedException();
		}
	}
}
