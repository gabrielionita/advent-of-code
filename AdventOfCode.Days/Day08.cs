using AdventOfCode.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace AdventOfCode.Days
{
	public class Day08 : DayBase<List<Instruction>, int>
	{
		private const string nopInstruction = "nop";
		private const string accInstruction = "acc";
		private const string jmpInstruction = "jmp";

		public Day08(HttpClient httpClient, ILogger<Day08> logger) : base(httpClient, logger)
		{
		}

		protected override List<Instruction> MapInput(string input)
		{
			return input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(line =>
			{
				var split = line.Split(" ");
				return new Instruction(split[0], int.Parse(split[1]));
			}).ToList();
		}

		protected override int SolvePart1(List<Instruction> code) => RunCode(code);

		protected override int SolvePart2(List<Instruction> code)
		{
			var linesChanged = new List<int>();
			while (true)
			{
				var lineNumber = RunCode(code, true, linesChanged);
				var instruction = code[lineNumber];
				linesChanged.Add(lineNumber);
				SwitchCode(instruction);
				try
				{
					return RunCode(code, true);
				}
				catch(StackOverflowException)
				{
					SwitchCode(instruction);
				}
			}
		}

		private void SwitchCode(Instruction instruction)
		{
			if (instruction.Code == nopInstruction)
			{
				instruction.Code = jmpInstruction;
			}
			else if (instruction.Code == jmpInstruction)
			{
				instruction.Code = nopInstruction;
			}
		}

		private int RunCode(List<Instruction> code, bool throwIfLoop = false, List<int> linesChanged = null)
		{
			code.ForEach(instruction => instruction.Executed = false);
			var accumulator = 0;
			var lineNumber = 0;
			var instruction = code[lineNumber];
			while (!instruction.Executed)
			{
				if ((instruction.Code == jmpInstruction || instruction.Code == nopInstruction) && linesChanged != null && !linesChanged.Contains(lineNumber))
				{
					return lineNumber;
				}

				if (instruction.Code == accInstruction)
				{
					accumulator += instruction.Argument;
					lineNumber++;
				}
				else if (instruction.Code == jmpInstruction)
				{
					lineNumber += instruction.Argument;
				}
				else if (instruction.Code == nopInstruction)
				{
					lineNumber++;
				}

				instruction.Executed = true;
				if (lineNumber >= code.Count)
				{
					break;
				}
				instruction = code[lineNumber];
				if (throwIfLoop && instruction.Executed)
				{
					throw new StackOverflowException();
				}
			}

			return accumulator;
		}
	}

	public class Instruction
	{
		public string Code { get; set; }
		public int Argument { get; set; }
		public bool Executed { get; set; }

		public Instruction(string code, int argument)
		{
			Code = code;
			Argument = argument;
			Executed = false;
		}
	}
}
