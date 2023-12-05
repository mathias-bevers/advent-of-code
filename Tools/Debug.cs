using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Tools
{
	/// <summary>
	///     A helper class to make logging information to the console easier.
	/// </summary>
	//todo: add null checks
	public static class Debug
	{
		/// <summary>
		///     Log a <paramref name="message" /> to the console with a red "[ERROR]" tag in front of it.
		/// </summary>
		/// <param name="message">The text that has to be displayed in the console</param>
		public static void LogError(object message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string callerPath = null)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			string fileName = callerPath.Split(Path.DirectorySeparatorChar).Last();
			Console.Write($"[ERROR {fileName}:{lineNumber}] ");
			Console.ResetColor();
			Console.WriteLine(message.ToString());
		}

		/// <summary>
		///     Log a <paramref name="message" /> to the console with a gray-ish "[INFO]" tag in front of it.
		/// </summary>
		/// <param name="message">The text that has to be displayed in the console</param>
		public static void Log(object message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string callerPath = null)
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			string fileName = callerPath.Split(Path.DirectorySeparatorChar).Last();
			Console.Write($"[INFO {fileName}:{lineNumber}] ");
			Console.ResetColor();
			Console.WriteLine(message.ToString());
		}

		/// <summary>
		///     Log a <paramref name="message" /> to the console with a yellow "[WARNING]" tag in front of it.
		/// </summary>
		/// <param name="message">The text that has to be displayed in the console</param>
		public static void LogWaring(object message, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string callerPath = null)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			string fileName = callerPath.Split(Path.DirectorySeparatorChar).Last();
			Console.Write($"[WARNING {fileName}:{lineNumber}] ");
			Console.ResetColor();
			Console.WriteLine(message.ToString());
		}
	}
}