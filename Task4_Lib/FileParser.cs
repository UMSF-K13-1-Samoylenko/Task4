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
    public class FileParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileParser" /> class
        /// </summary>
        /// <param name="filepath">Path to source file</param>
        public FileParser(string filepath)
        {
            this.Filepath = filepath;
        }

        /// <summary>
        /// Gets or sets path to source file
        /// </summary>
        public string Filepath { get; set; }

        /// <summary>
        /// Getting numbers of line entries to file.
        /// </summary>
        /// <param name="entriedString">String to count entries</param>
        /// <returns>Returns count of line entries</returns>
        /// <exception cref="SourceNotFoundException">Thrown when file not exist</exception>
        public int GetNumberOfLineEntriesToFile(string entriedString)
        {
            int result = 0;
            try
            {
                using (StreamReader streamReader = new StreamReader(this.Filepath))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string stringFromSource = streamReader.ReadLine();
                        int oldLength = stringFromSource.Length;
                        stringFromSource = stringFromSource.Replace(entriedString, string.Empty);
                        int newLength = stringFromSource.Length;
                        result += (oldLength - newLength) / entriedString.Length;
                    }
                }
            }
            catch (FileNotFoundException ex)
            { 
                throw new SourceNotFoundException("Source not found!", ex);
            }

            return result;
        }

        /// <summary>
        /// Replacing searchingString-s in file to replacementString-s.
        /// </summary>
        /// <param name="searchString">String to replace</param>
        /// <param name="replacementString">Replacement string</param>
        /// <exception cref="SourceNotFoundException">Thrown when file not exist</exception>
        public void ReplacingStringInFile(string searchString, string replacementString)
        {
            string tempFileFilepath = Path.GetTempFileName();
            try
            {
                using (StreamReader sourceFile = new StreamReader(this.Filepath))
                {
                    using (StreamWriter destFile = new StreamWriter(tempFileFilepath))
                    {
                        while (!sourceFile.EndOfStream)
                        {
                            string stringFromSource = sourceFile.ReadLine();
                            stringFromSource = stringFromSource.Replace(searchString, replacementString);
                            destFile.Write(stringFromSource + Environment.NewLine);
                        }
                    }
                }

                using (StreamReader sourceFile = new StreamReader(tempFileFilepath))
                {
                    using (StreamWriter destFile = new StreamWriter(this.Filepath))
                    {
                        while (!sourceFile.EndOfStream)
                        {
                            destFile.Write(sourceFile.ReadLine() + Environment.NewLine);
                        }
                    }
                }

                File.Delete(tempFileFilepath);
            }
            catch (FileNotFoundException ex)
            {
                throw new SourceNotFoundException("Source not found!", ex);
            }
        }
    }
}
