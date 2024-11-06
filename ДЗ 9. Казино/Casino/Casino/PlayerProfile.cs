namespace CasinoGame.Casino
{
    public class PlayerProfile
    {
        public string Name { get; set; }
        public int Bank { get; set; }

        public PlayerProfile(string name, int bank)
        {
            Name = name;
            Bank = bank;
        }
    }
}
