/** Program: SodukuSolverCMD
 *  Description: Solves a soduku game using recursive backtracking and prints it to screen.
 *  Author: Jordan Mryyan
 */

using System;
using System.IO;
using System.Text;

namespace SodukuSolverCMD
{

    class Program
    {
        static void Main(string[] args)
        {
            Board gameBoard = new Board(9, 9);

            string filePath = getValidFile("Enter the path to a unsolved Sudoku board: ", "File not valid!\n");

            // the board before we slipt into individual characters
            string input;
            input = File.ReadAllText(filePath, Encoding.UTF8);

            gameBoard.createBoard(input);

            gameBoard.printBoard();

            solveGame(ref gameBoard);

            gameBoard.printBoard();

            Console.ReadLine();
        }

        /// <summary>
        ///  Prompts the user for a file path until the one they provide is a valid file path.
        /// </summary>
        /// <param name="prompt">The message to display to the user when asking for a file path.</param>
        /// <param name="error">The message tp display to the user if the provided file path is invalid.</param>
        /// <returns>string</returns>
        private static string getValidFile(string prompt, string error)
        {
            string filePath = "";

            while (true)
            {
                Console.WriteLine(prompt);
                filePath = Console.ReadLine();
                if (File.Exists(filePath))
                {
                    return filePath;
                }
                else
                {
                    Console.WriteLine(error);
                }
            }
        }

        /// <summary>
        /// Recursively solves the sudoku board.
        /// </summary>
        /// <param name="gameBoard">A refrence to a precreated soduku Board.</param>
        /// <returns>True if the recursion succeeded, false otherwise.</returns>
        private static bool solveGame(ref Board gameBoard)
        {
            // Initialize our row and columns so we can manipulate them through 
            // refrence later.
            int row = -1;
            int column = -1;

            // BASECASE: There are spaces still left to place a number
            if (!gameBoard.setToEmptySpace(ref row, ref column))
            {
                return true;
            }

            // Trys to place numbers 1-9 into the empty cell set by setToEmptySpace.
            for (int curNum = 1; curNum <= 9; curNum++)
            {
                // Check if it is safe to place a number in row, cloumn
                if (gameBoard.isSafeSpace(curNum, row, column))
                {
                    gameBoard.setCell(curNum, row, column);

                    // Go one stack frame deeper until setEmptySpace returns false
                    if (solveGame(ref gameBoard)) return true;

                    // Undo a wrong decision
                    gameBoard.unassignCell(row, column);
                }
            }

            return false;
        }
    }
}
