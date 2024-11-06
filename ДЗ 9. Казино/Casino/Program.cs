using CasinoGame.Casino;
using CasinoGame.SaveLoad;

namespace Casino
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Указываем путь для сохранения профиля
            string savePath = "C:\\GameData";

            // Создаем экземпляр службы сохранения/загрузки
            ISaveLoadService<string> saveLoadService = new FileSystemSaveLoadService(savePath);

            // Создаем объект казино, который реализует интерфейс IGame
            IGame casino = new CasinoGame.Casino.Casino(saveLoadService);

            // Запускаем игру, вызывая метод StartGame через интерфейс IGame
            casino.StartGame();
        }
    }
}
