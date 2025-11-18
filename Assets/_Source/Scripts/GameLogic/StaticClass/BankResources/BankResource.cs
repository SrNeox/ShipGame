namespace _Source.Scripts.GamePlay.StaticClass.BankResources
{
    public static class BankResource
    {
        public static int Score { get; private set; }
        public static int Coin { get; private set; }

        public static void ChangeScore(int countScore)
        {
            Score += countScore;
        }

        public static void AddCoin(int coin)
        {
            Coin += coin;
        }
    }
}