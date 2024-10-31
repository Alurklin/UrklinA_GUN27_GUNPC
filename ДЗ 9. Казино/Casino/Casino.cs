using System;

namespace Casino
{
    public class Casino : IGame
    {
        private string _playerName;
        private double _bankroll;
        private FileSystemSaveLoadService _saveLoadService;

        public Casino(string savePath)
        {
            _saveLoadService = new FileSystemSaveLoadService(savePath);
            LoadPlayerProfile();
        }

        public void StartGame()
        {
            Console.WriteLine("Welcome to the Casino!");
            ChooseGame();
        }

        private void LoadPlayerProfile()
        {
            Console.Write("Enter your name: ");
            _playerName = Console.ReadLine();
            // Загружаем профиль (необходима реализация)
        }

        private void ChooseGame()
        {
            Console.WriteLine("Choose a game: 1 - Blackjack, 2 - Dice Game");
            string choice = Console.ReadLine();
            CasinoGameBase game = null;

            if (choice == "1")
            {
                game = new Blackjack();
            }
            else if (choice == "2")
            {
                game = new DiceGame(2, 1, 6); // Например, 2 кости от 1 до 6
            }

            if (game != null)
            {
                game.OnWin += () => Console.WriteLine("You win!");
                game.OnLoose += () => Console.WriteLine("You lose!");
                game.OnDraw += () => Console.WriteLine("It's a draw!");
                game.PlayGame();
            }

            // Сохранение профиля (необходима реализация)
        }
    }
}
