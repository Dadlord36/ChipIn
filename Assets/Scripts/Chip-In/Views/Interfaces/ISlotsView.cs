using System.Collections.Generic;
using DataModels.MatchModels;

namespace Views.Interfaces
{
    public interface ISlotsView
    {
        void SetSlotsIcons(List<BoardIconData> boardIconsData);
        void StartSlotsAnimation();
    }
}