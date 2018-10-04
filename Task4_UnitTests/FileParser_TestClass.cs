using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task4_Lib;

namespace Task4_UnitTests
{
    [TestClass]
    public class FileParser_TestClass
    {
        private string filepath;
        private string stringToSearch;
        private string trashString;
        public FileParser_TestClass()
        {
            filepath = "temp.txt";
            stringToSearch = "String to search";
            trashString = "Hello world!";
        }
        [TestInitialize]
        private void RemoveFile()
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }

        [TestMethod]
        public void GetNumberOfLineEntriesToFile_EmptyFile()
        {
            StreamWriter streamWriter = new StreamWriter(filepath);
            streamWriter.Close();
            Assert.AreEqual(0, FileParser.GetNumberOfLineEntriesToFile(filepath, stringToSearch));
        }
        
        [TestMethod]
        public void GetNumberOfLineEntriesToFile_NullEntries()
        {
            StreamWriter streamWriter = new StreamWriter(filepath);
            for (int i = 0; i < 3; ++i)
            {
                for (int j = i; j < 3; ++j)
                {
                    streamWriter.Write(trashString);
                }
                streamWriter.WriteLine();
            }
            streamWriter.Close();
            Assert.AreEqual(0, FileParser.GetNumberOfLineEntriesToFile(filepath, stringToSearch));
        }

        [TestMethod]
        public void GetNumberOfLineEntriesToFile_ThreeEntries()
        {
            StreamWriter streamWriter = new StreamWriter(filepath);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i != j)
                    {
                        streamWriter.Write(trashString);
                    }
                    else
                    {
                        streamWriter.Write(stringToSearch);
                    }
                }
                streamWriter.WriteLine();
            }
            streamWriter.Close();
            Assert.AreEqual(3, FileParser.GetNumberOfLineEntriesToFile(filepath, stringToSearch));
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetNumberOfLineEntriesToFile_FileNotExist()
        {
            FileParser.GetNumberOfLineEntriesToFile(filepath, string.Empty);
        }
    }
}
