using DataModels.MatchModels;
using ViewModels.Basic;
using Views.ViewElements;

namespace ViewModels.Elements
{
    public class PlayerScoreViewModel : BaseViewModel
    {
        public uint ScoreNumber
        {
            set => ((PlayerScoreView) View).Score =value;
        }

        public void SetUserScore(MatchUserData userData)
        {
            ScoreNumber = userData.Score;
        }
    }
}