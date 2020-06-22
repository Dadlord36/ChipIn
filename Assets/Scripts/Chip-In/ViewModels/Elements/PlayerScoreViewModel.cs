﻿using ViewModels.Basic;
using Views.ViewElements;

namespace ViewModels.Elements
{
    public class PlayerScoreViewModel : BaseViewModel
    {
        public uint ScoreNumber
        {
            set => ((PlayerScoreView) View).Score = value;
        }

        public PlayerScoreViewModel() : base(nameof(PlayerScoreViewModel))
        {
        }

        public void SetUserScore(uint score)
        {
            ScoreNumber = score;
        }
    }
}