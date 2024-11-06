using System;
using CasinoGame.Games;
using CasinoGame.SaveLoad;

namespace CasinoGame.Casino
{
    public class Casino : IGame
    {
        private readonly ISaveLoadService<string> _saveLoadService;
        private PlayerProfile _playerProfile;

        public Casino(ISaveLoadService<string> saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void StartGame()
        {
            LoadPlayerProfile();

            while (true)
            {
                Console.WriteLine("Choose game: 1 - Blackjack, 2 - Dice Game");
                int choice = int.Parse(Console.ReadLine());

                CasinoGameBase game = choice == 1 ? (CasinoGameBase)new BlackjackGame(52) : new DiceGame(2, 1, 6);
                game.OnWin += () => Console.WriteLine("You Win!");
                game.OnLose += () => Console.WriteLine("You Lose!");
                game.OnDraw += () => Console.WriteLine("Draw!");

                Console.WriteLine("Enter your bet:");
                int bet = int.Parse(Console.ReadLine());

                game.PlayGame();

                Console.WriteLine("Play again? (yes/no)");
                if (Console.ReadLine().ToLower() != "yes") break;
            }

            SavePlayerProfile();
        }

        private void LoadPlayerProfile()
        {
            // Загрузка или создание профиля игрока
        }

        private void SavePlayerProfile()
        {
            // Сохранение профиля игрока
        }
    }
}
