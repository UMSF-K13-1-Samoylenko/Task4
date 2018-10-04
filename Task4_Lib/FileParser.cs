// <copyright file="FileParser.cs" company="My company">
//     Copyright (c) My company". All rights reserved.
// </copyright>

namespace Task4_Lib
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// FileParser class for specified work with files
    /// </summary>
    public static class FileParser
    {
        /// <summary>
        /// Getting numbers of line entries to file. Can throw default file exceptions like FileNotFoundEx
        /// </summary>
        /// <param name="filepath">Path to file.</param>
        /// <param name="entriedString">String to count entries</param>
        /// <returns>Returns count of line entries</returns>
        public static int GetNumberOfLineEntriesToFile(string filepath, string entriedString)
        {
            StreamReader streamReader = new StreamReader(filepath);
            int result = 0;
            result = streamReader.ReadToEnd().Split(new string[] { entriedString }, StringSplitOptions.None).Count() - 1;
            streamReader.Close();
            return result;
        }

        /// <summary>
        /// Replacing searchingString-s in file to replacementString-s. Can throw default file exceptions like FileNotFoundEx
        /// </summary>
        /// <param name="filepath">Path to file.</param>
        /// <param name="searchString">String to replace</param>
        /// <param name="replacementString">Replacement string</param>
        public static void ReplacingStringInFile(string filepath, string searchString, string replacementString)
        {
            StreamReader sourceFile = new StreamReader(filepath);
            string tempFileFilepath = DateTime.Now.ToString("dd_MMM_yyyy");
            StreamWriter destFile = new StreamWriter(tempFileFilepath);
            string stringFromSource;
            while (!sourceFile.EndOfStream)
            {
                stringFromSource = sourceFile.ReadLine();
                stringFromSource = stringFromSource.Replace(searchString, replacementString);
                destFile.WriteLine(stringFromSource);
            }

            sourceFile.Close();
            destFile.Close();
            sourceFile = new StreamReader(tempFileFilepath);
            destFile = new StreamWriter(filepath);
            while (!sourceFile.EndOfStream)
            {
                destFile.WriteLine(sourceFile.ReadLine());
            }

            sourceFile.Close();
            destFile.Close();
            File.Delete(tempFileFilepath);
        }
    }
}
