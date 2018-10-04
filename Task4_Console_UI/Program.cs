// <copyright file="Program.cs" company="My company">
//     Copyright (c) My company". All rights reserved.
// </copyright>

namespace Task4_Console_UI
{
    using System;
    using System.IO;
    using Task4_Lib;

    /// <summary>
    /// Main class of programm
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Instruction how to use programm. Writting to the console
        /// </summary>
        private static void Instruction()
        {
            Console.WriteLine("Program assignment\n" +
            "Count the number of line entries in the text file\n" +
            "Launch example: Task4_Console_UI.exe <filepath> <string_to_count_entries>\n" +
            "Replace a string with another in the specified file\n" +
            "Launch example: Task4_Console_UI.exe <filepath> <search_string> <replacement_string>");
        }

        /// <summary>
        /// Entry point to programm
        /// </summary>
        /// <param name="args">Command line args</param>
        private static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 2:
                    {
                        try
                        {
                            Console.Write("Count=" +
                                FileParser.GetNumberOfLineEntriesToFile(args[0], args[1]));
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.Write(ex.Message);
                        }

                        break;
                    }

                case 3:
                    {
                        try
                        {
                            FileParser.ReplacingStringInFile(args[0], args[1], args[2]);
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.Write(ex.Message);
                        }

                        break;
                    }

                default:
                    Instruction();
                    break;
            }
        }
    }
}
