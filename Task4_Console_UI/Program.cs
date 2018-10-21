// <copyright file="Program.cs" company="My company">
//     Copyright (c) My company". All rights reserved.
// </copyright>

namespace Task4_Console_UI
{
    using System;
    using System.IO;
    using Task4_Lib;

    /// <summary>
    /// Main class of program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point to program
        /// </summary>
        /// <param name="args">Command line args</param>
        private static void Main(string[] args)
        {
            ConsoleFileParserMenu.Menu();
        }
    }
}
