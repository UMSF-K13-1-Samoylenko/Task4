﻿// <copyright file="FileParser_TestClass.cs" company="My company">
//     Copyright (c) My company". All rights reserved.
// </copyright>

namespace Task4_UnitTests
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task4_Lib;

    /// <summary>
    /// File parser test class for testing methods inside FileParser
    /// </summary>
    [TestClass]
    public class FileParser_TestClass
    {
        /// <summary>
        /// Path to testing file
        /// </summary>
        private readonly string filepath;

        /// <summary>
        /// String to search by default
        /// </summary>
        private readonly string stringToSearch;

        /// <summary>
        /// String with some text to fill test file
        /// </summary>
        private readonly string trashString;

        /// <summary>
        /// File parser object to works with files
        /// </summary>
        private readonly FileParser fileParser;

        /// <summary>
        /// Initializes a new instance of the FileParser_TestClass class
        /// Constructor of class to initialize string fields
        /// </summary>
        public FileParser_TestClass()
        {
            this.filepath = "temp.txt";
            this.stringToSearch = "String to search";
            this.trashString = "Hello world!";
            this.fileParser = new FileParser(this.filepath);
        }

        /// <summary>
        /// Methods, which will launch before each test.
        /// Removing temp file
        /// </summary>
        [TestInitialize]
        public void RemoveFile()
        {
            if (File.Exists(this.filepath))
            {
                File.Delete(this.filepath);
            }
        }

        /// <summary>
        /// Test FileParser.GetNumberOfLineEntriesToFile in case of empty file
        /// </summary>
        [TestMethod]
        public void GetNumberOfLineEntriesToFile_EmptyFile()
        {
            // Arrange
            StreamWriter streamWriter = new StreamWriter(this.filepath);
            streamWriter.Close();

            // Actual
            int actual = this.fileParser.GetNumberOfLineEntriesToFile(this.stringToSearch);

            // Assert
            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test FileParser.GetNumberOfLineEntriesToFile in a case of null entries of line
        /// </summary>
        [TestMethod]
        public void GetNumberOfLineEntriesToFile_NullEntries()
        {
            // Arrange
            StreamWriter streamWriter = new StreamWriter(this.filepath);
            for (int i = 0; i < 3; ++i)
            {
                for (int j = i; j < 3; ++j)
                {
                    streamWriter.Write(this.trashString);
                }

                streamWriter.WriteLine();
            }

            streamWriter.Close();

            // Actual
            int actual = this.fileParser.GetNumberOfLineEntriesToFile(this.stringToSearch);
            Assert.AreEqual(0, actual);
        }

        /// <summary>
        /// Test FileParser.GetNumberOfLineEntriesToFile in a case of three entries
        /// In:
        /// Begin of the file and the line
        /// Mid of the file and the line
        /// End of the file and the line
        /// </summary>
        [TestMethod]
        public void GetNumberOfLineEntriesToFile_ThreeEntries()
        {
            // Arrange
            StreamWriter streamWriter = new StreamWriter(this.filepath);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i != j)
                    {
                        streamWriter.Write(this.trashString);
                    }
                    else
                    {
                        streamWriter.Write(this.stringToSearch);
                    }
                }

                streamWriter.WriteLine();
            }

            streamWriter.Close();

            int actual = this.fileParser.GetNumberOfLineEntriesToFile(this.stringToSearch);
            Assert.AreEqual(3, actual);
        }

        /// <summary>
        /// Test FileParser.GetNumberOfLineEntriesToFile in a case of file not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SourceNotFoundException))]
        public void GetNumberOfLineEntriesToFile_FileNotExist()
        {
            this.fileParser.GetNumberOfLineEntriesToFile(this.stringToSearch);
        }

        /// <summary>
        /// Testing of FileParser.ReplacingStringInFile (empty file)
        /// </summary>
        [TestMethod]
        public void ReplacingStringInFile_EmptyFile()
        {
            // Arrange
            StreamWriter streamWriter = new StreamWriter(this.filepath);
            streamWriter.Close();
            this.fileParser.ReplacingStringInFile(this.stringToSearch, this.trashString);

            // Actual
            string actual = this.GetFileBody();

            // Assert
            Assert.AreEqual(string.Empty, actual);
        }

        /// <summary>
        /// Testing of FileParser.ReplacingStringInFile (0 entries)
        /// </summary>
        [TestMethod]
        public void ReplacingStringInFile_NullEntries()
        {
            // Arrange
            StreamWriter streamWriter = new StreamWriter(this.filepath);
            for (int i = 0; i < 3; ++i)
            {
                for (int j = i; j < 3; ++j)
                {
                    streamWriter.Write(this.trashString);
                }

                streamWriter.WriteLine();
            }

            streamWriter.Close();
            string expected = this.GetFileBody();
            this.fileParser.ReplacingStringInFile(this.stringToSearch, this.trashString);

            // Actual
            string actual = this.GetFileBody();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Testing of FileParser.ReplacingStringInFile (three entries)
        /// In:
        /// Begin of the file and the line
        /// Mid of the file and the line
        /// End of the file and the line
        /// </summary>
        [TestMethod]
        public void ReplacingStringInFile_ThreeEntries()
        {
            // Arrange
            StreamWriter streamWriter = new StreamWriter(this.filepath);
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (i != j)
                    {
                        streamWriter.Write(this.trashString);
                    }
                    else
                    {
                        streamWriter.Write(this.stringToSearch);
                    }
                }

                streamWriter.WriteLine();
            }

            streamWriter.Close();
            string expected = string.Empty;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    expected += this.trashString;
                }

                expected += Environment.NewLine;
            }

            this.fileParser.ReplacingStringInFile(this.stringToSearch, this.trashString);

            // Actual
            string actual = this.GetFileBody();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///  Testing of FileParser.ReplacingStringInFile (file not exist)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SourceNotFoundException))]
        public void ReplacingStringInFile_FileNotExist()
        {
            this.fileParser.ReplacingStringInFile(this.stringToSearch, this.trashString);
        }

        /// <summary>
        /// Getting whole lines from temp file. Unsafe if file is really big.
        /// </summary>
        /// <returns>Line with whole file body</returns>
        private string GetFileBody()
        {
            StreamReader streamReader = new StreamReader(this.filepath);
            string result = streamReader.ReadToEnd();
            streamReader.Close();
            return result;
        }
    }
}
