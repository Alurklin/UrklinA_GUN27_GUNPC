using System;
using System.IO;

namespace CasinoGame.SaveLoad
{
    public class FileSystemSaveLoadService : ISaveLoadService<string>
    {
        private readonly string _path;

        public FileSystemSaveLoadService(string path)
        {
            _path = path;
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        public void SaveData(string data, string id)
        {
            File.WriteAllText(Path.Combine(_path, $"{id}.txt"), data);
        }

        public string LoadData(string id)
        {
            var filePath = Path.Combine(_path, $"{id}.txt");
            return File.Exists(filePath) ? File.ReadAllText(filePath) : null;
        }
    }
}
