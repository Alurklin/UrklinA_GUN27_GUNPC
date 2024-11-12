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
            try
            {
                // Загрузка профиля игрока из файла
                string profileData = _saveLoadService.LoadData("PlayerProfile.txt");
                if (!string.IsNullOrEmpty(profileData))
                {
                    string[] data = profileData.Split(',');
                    string name = data[0];
                    int bank = int.Parse(data[1]);
                    _playerProfile = new PlayerProfile(name, bank);
                    Console.WriteLine($"Welcome back, {_playerProfile.Name}!");
                }
            }
            catch (Exception)
            {
                // Если возникла ошибка, создаем новый профиль
                Console.WriteLine("No profile found. Please enter your name:");
                string name = Console.ReadLine();
                _playerProfile = new PlayerProfile(name, 1000); // Начальный банк
                Console.WriteLine($"Profile created for {_playerProfile.Name} with bank: {_playerProfile.Bank}");
            }
        }

        private void SavePlayerProfile()
        {
            // Сохранение профиля игрока в файл
            string profileData = $"{_playerProfile.Name},{_playerProfile.Bank}";
            _saveLoadService.SaveData(profileData, "PlayerProfile.txt");
            Console.WriteLine("Profile saved.");
        }
    }
}
