using Aspose.Zip;
using Aspose.Zip.Saving;
using GameCommons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChineseChess_Avalonia
{
    public static class UtilOps
    {
        public static Side RandomStart()
        {
            Random playerStart = new Random();
            //create random number between 0 and 10
            int whoStart = playerStart.Next(10);
            //red starts if smaller than 5, black starts if greater
            if (whoStart <= 5)
            {
                return Side.Red;
            }
            else
            {
                return Side.Black;
            }

        }

        public static void DeleteTempFilesAfterThisTurn(int currentTurn)
        {
            UtilOps.DeleteTempFilesAfterTurn(currentTurn);
        }
        public static void CheckSaveDirectory()
        {
            if (!Directory.Exists(FilePaths.rootTempFilePath))
            {
                Directory.CreateDirectory(FilePaths.rootTempFilePath);
            }
        }
        public static IEnumerable<Turn> LoadSaveFile(string saveFilePath)
        {
            using (FileStream fs = File.OpenRead(saveFilePath))
            {
                using (Archive archive = new Archive(fs))
                {
                    foreach (var t in archive.Entries)
                    {
                        t.Extract(FilePaths.rootTempFilePath + t.Name);
                    }
                }
            }
            return LoadAllTempFiles();
        }
        public static IEnumerable<Turn> LoadAllTempFiles()
        {
            string[] tempFilePaths = Directory.GetFiles(FilePaths.rootTempFilePath);
            foreach (string filePath in tempFilePaths)
            {
                yield return CreateTurnFromFile(filePath);
            }
        }
        public static void DeleteTempFilesAfterTurn(int turnNumber)
        {
            string[] tempFilePaths = Directory.GetFiles(FilePaths.rootTempFilePath);
            var pathToDelete = tempFilePaths.Where(x => Convert.ToInt32(Path.GetFileNameWithoutExtension(x)) > turnNumber);
            foreach (string filePath in pathToDelete)
            {
                File.Delete(filePath);
            }
        }
        public static void ClearTempFolder()
        {
            string[] tempFilePaths = Directory.GetFiles(FilePaths.rootTempFilePath);
            foreach (string filePath in tempFilePaths)
            {
                File.Delete(filePath);
            }
        }
        private static string CheckExistingSaveFile(string fileName, int? counter = null)
        {
            var allFiles = Directory.GetFiles(FilePaths.rootSaveFilePath);
            var allExistingSaves = allFiles.Where(x => Path.GetExtension(x) == ".sav").Select(f => Path.GetFileNameWithoutExtension(f)).ToArray();
            if (allExistingSaves.Contains(fileName))
            {
                if (counter is null)
                {
                    counter = 2;
                    fileName = $"{fileName} ({counter})";
                }
                else
                {
                    counter++;
                    var name = fileName.Split(' ');
                    var orgName = string.Concat(name.Take(name.Length - 1));
                    fileName = $"{orgName} ({counter})";
                }
                return CheckExistingSaveFile(fileName, counter);
            }
            else
            {
                return fileName;
            }
        }
        public static void SaveFile(string fileName, bool overWriteSave)
        {
            var tempDir = new DirectoryInfo(FilePaths.rootTempFilePath);
            var tempfiles = tempDir.GetFiles();
            string realFileName;
            if (overWriteSave)
            {
                realFileName = fileName;
            }
            else
            {
                realFileName = CheckExistingSaveFile(fileName);
            }
            string saveFile = FilePaths.rootSaveFilePath + realFileName + ".sav";
            using (FileStream zipFile = File.Open(saveFile, FileMode.Create))
            {
                using (var archive = new Archive())
                {
                    // Add files to the archive
                    foreach (var temp in tempfiles)
                    {
                        archive.CreateEntry(temp.Name, temp);
                        // Create ZIP archive
                        archive.Save(zipFile, new ArchiveSaveOptions() { Encoding = Encoding.ASCII });
                    }

                }
            }

        }
        public static Turn CreateTurnFromFile(string filePath)
        {
            int turn = Convert.ToInt32(Path.GetFileNameWithoutExtension(filePath));
            Side side;
            List<string> boardState = new List<string>();
            string[] allLines = File.ReadAllLines(filePath);
            side = (Side)Enum.Parse(typeof(Side), allLines[0]);
            for (int i = 1; i < allLines.Length; i++)
            {
                boardState.Add(allLines[i]);
            }
            return new Turn(turn, side, boardState);
        }

    }
}
