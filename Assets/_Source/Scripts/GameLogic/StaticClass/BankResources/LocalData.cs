using YG;

namespace _Source.Scripts.GameLogic.StaticClass.BankResources
{
    public static class LocalData
    {
        public static int Score { get; private set; }
        
        public static void AddScore(int countScore) => Score += countScore;
        
        public static void TryChangeData()
        {
            if (Score > YG2.saves.Score)
            {
                YG2.saves.Score = Score;
                YG2.SaveProgress();
                YG2.SetLeaderboard("LeaderBoard", YG2.saves.Score);
            }

            ResetScore();
        }

        private static int ResetScore() => Score = 0;
    }
}