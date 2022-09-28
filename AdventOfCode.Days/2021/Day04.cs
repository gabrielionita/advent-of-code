using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days2021
{
    public class Day04 : DayBase<DataDay04, int>
    {
        public override DataDay04 MapInput(string input)
        {
            var lines = input.Split('\n');
            var data = new DataDay04
            {
                Numbers = lines[0].Split(',').Select(int.Parse).ToArray()
            };
            var boards = new List<int[][]>();
            for (var i = 2; i < lines.Length; i++)
            {
                var board = new int[5][];
                if (string.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }

                for (var j = 0; j < 5; j++)
                {
                    board[j] = lines[i + j].Split(' ', System.StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                }
                i += 5;
                boards.Add(board);
            }
            data.Boards = boards.ToArray();
            return data;
        }

        public override int SolvePart1(DataDay04 input)
        {
            foreach (var number in input.Numbers)
            {
                foreach (var board in input.Boards)
                {
                    for (var i = 0; i < board.Length; i++)
                    {
                        for (var j = 0; j < board.GetLength(0); j++)
                        {
                            if (board[i,j] == number)
                            {
                                board[i,j] = 0;
                            }
                        }
                    }

                    var win = true;
                    for (var i = 0; i < board.Length; i++)
                    {
                        win = true;
                        for (var j = 0; j < board.GetLength(1); j++)
                            if (board[i,j] != 0)
                            {
                                win = false;
                                break;
                            }


                        if (win)
                            break;
                    }

                    if (!win)
                    {
                        for (var i = 0; i < board.GetLength(1); i++)
                        {
                            win = true;
                            for (var j = 0; j < board.GetLength(0); j++)
                                if (board[j,i] != 0)
                                {
                                    win = false;
                                    break;
                                }

                            if (win)
                                break;
                        }
                    }

                    if (win)
                    {
                        var sum = 0;
                        for(var i = 0; i < board.GetLength(0); i++)
                            for(var j = 0; j < board.GetLength(1); j++)
                                sum += board[i,j];

                        return sum * number;
                    }
                }
            }

            return 0;
        }

        public override int SolvePart2(DataDay04 input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class DataDay04
    {
        public int[] Numbers { get; set; }
        public int[][,] Boards { get; set; }
    }
}
