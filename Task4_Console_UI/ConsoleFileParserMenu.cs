// <copyright file="ConsoleFileParserMenu.cs" company="My company">
//     Copyright (c) My company". All rights reserved.
// </copyright>

namespace Task4_Console_UI
{
    using System;
    using Task4_Lib;

    /// <summary>
    /// Console menu class for fileParser opportunities demo
    /// </summary>
    public class ConsoleFileParserMenu
    {
        /// <summary>
        /// Console menu for fileParser opportunities demo
        /// </summary>
        /// <exception cref="SourceNotFoundException">Thrown when file not exist</exception>
        public void Menu()
        {
            string[] args = Environment.GetCommandLineArgs();
            FileParser fileParser = new FileParser(args[0]);
            switch (args.Length)
            {
                case 2:
                {
                    try
                    {
                        Console.Write("Count=" +
                            fileParser.GetNumberOfLineEntriesToFile(args[1]));
                    }
                    catch (SourceNotFoundException ex)
                    {
                        Console.Write(ex.Message);
                    }

                    break;
                }

                case 3:
                {
                    try
                    {
                        fileParser.ReplacingStringInFile(args[1], args[2]);
                    }
                    catch (SourceNotFoundException ex)
                    {
                        Console.Write(ex.Message);
                    }

                    break;
                }

                default:
                    this.ConsoleInstruction();
                    break;
            }
        }

        /// <summary>
        /// Instruction how to use programm. Writting to the console
        /// </summary>
        private void ConsoleInstruction()
        {
            Console.WriteLine("Program assignment" + Environment.NewLine +
            "Count the number of line entries in the text file" + Environment.NewLine +
            "Launch example: Task4_Console_UI.exe <filepath> <string_to_count_entries>" + Environment.NewLine +
            "Replace a string with another in the specified file" + Environment.NewLine +
            "Launch example: Task4_Console_UI.exe <filepath> <search_string> <replacement_string>");
        }
    }
}
