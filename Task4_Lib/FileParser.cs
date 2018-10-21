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
        /// String for search
        /// </summary>
        private string searchString;

        /// <summary>
        /// String for replace
        /// </summary>
        private string replacementString;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileParser" /> class
        /// </summary>
        /// <param name="filepath">Path to source file</param>
        public FileParser(string filepath)
        {
            this.Filepath = filepath;
        }

        /// <summary>
        /// Delegate for methods which will be called inside <see cref="ReadFromSourceAndWriteToDest"/>
        /// </summary>
        /// <param name="sourceFile">Source file stream</param>
        /// <param name="destFile">Destination file stream</param>
        private delegate void ReadWriteOption(StreamReader sourceFile, StreamWriter destFile);

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
            this.searchString = searchString;
            this.replacementString = replacementString;
            string tempFileFilepath = Path.GetTempFileName();
            try
            {
               this.ReadFromSourceAndWriteToDest(this.Filepath, tempFileFilepath, this.CopyFromOneToOtherAndReplaceString);
               this.ReadFromSourceAndWriteToDest(tempFileFilepath, this.Filepath, this.CopyFromOneToOther);
                File.Delete(tempFileFilepath);
            }
            catch (FileNotFoundException ex)
            {
                throw new SourceNotFoundException("Source not found!", ex);
            }
        }

        /// <summary>
        /// Read From One File And Writing To Other
        /// </summary>
        /// <param name="sourceFilepath">Source file path</param>
        /// <param name="destFilepath">Destination file path</param>
        /// <param name="readWriteOption">Delegate witch will be called ass a body of read-write</param>
        private void ReadFromSourceAndWriteToDest(string sourceFilepath, string destFilepath, ReadWriteOption readWriteOption)
        {
            using (StreamReader sourceFile = new StreamReader(sourceFilepath))
            {
                using (StreamWriter destFile = new StreamWriter(destFilepath))
                {
                    readWriteOption.Invoke(sourceFile, destFile);
                }
            }
        }

        /// <summary>
        /// Copy From One To Other And Replace String
        /// </summary>
        /// <param name="sourceFile">Source File</param>
        /// <param name="destFile">Destination file</param>
        private void CopyFromOneToOtherAndReplaceString(StreamReader sourceFile, StreamWriter destFile)
        {
            while (!sourceFile.EndOfStream)
            {
                string stringFromSource = sourceFile.ReadLine();
                stringFromSource = stringFromSource.Replace(this.searchString, this.replacementString);
                destFile.Write(stringFromSource + Environment.NewLine);
            }
        }

        /// <summary>
        /// Copy From One To Other
        /// </summary>
        /// <param name="sourceFile">Source file</param>
        /// <param name="destFile">Destination file</param>
        private void CopyFromOneToOther(StreamReader sourceFile, StreamWriter destFile)
        {
            while (!sourceFile.EndOfStream)
            {
                destFile.Write(sourceFile.ReadLine() + Environment.NewLine);
            }
        }
    }
}
