using AdventOfCode.Interfaces;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Services
{
	public class InputStorage : IInputStorage
	{
		public async Task Write(int year, int day, string content, CancellationToken cancellationToken = default)
		{
			var path = GetInputPath(year, day);

			CreateDirectoryIfMissing(path);

			await File.WriteAllTextAsync(path, content, cancellationToken);
		}

		public async Task<string> Read(int year, int day, CancellationToken cancellationToken)
		{
			return await File.ReadAllTextAsync(GetInputPath(year, day), cancellationToken);
		}

		private static string GetInputPath(int year, int day) => $"Inputs/{year}/Day{day:00}.txt";

		private void CreateDirectoryIfMissing(string path)
		{
			var file = new FileInfo(path);
			if (!file.Directory.Exists)
			{
				file.Directory.Create();
			}
		}
	}
}
