using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using WebOperationUtilities;

namespace DataModels.MatchModels
{
    public class SlotsBoard
    {
        [JsonProperty("0")] public readonly MatchBoardElementData First;
        [JsonProperty("1")] public readonly MatchBoardElementData Second;
        [JsonProperty("2")] public readonly MatchBoardElementData Third;
        [JsonProperty("3")] public readonly MatchBoardElementData Fourth;
        [JsonProperty("4")] public readonly MatchBoardElementData Fifth;
        [JsonProperty("5")] public readonly MatchBoardElementData Sixth;
        [JsonProperty("6")] public readonly MatchBoardElementData Seventh;
        [JsonProperty("7")] public readonly MatchBoardElementData Eighth;
        [JsonProperty("8")] public readonly MatchBoardElementData Ninth;

        public SlotsBoard(MatchBoardElementData first, MatchBoardElementData second, MatchBoardElementData third,
            MatchBoardElementData fourth, MatchBoardElementData fifth, MatchBoardElementData sixth,
            MatchBoardElementData seventh, MatchBoardElementData eighth, MatchBoardElementData ninth)
        {
            First = first;
            Second = second;
            Third = third;
            Fourth = fourth;
            Fifth = fifth;
            Sixth = sixth;
            Seventh = seventh;
            Eighth = eighth;
            Ninth = ninth;
        }

        public async Task<Texture2D[]> GetSlotsIconsTextures()
        {
            var iconsUrls = new[]
            {
                First.PosterUrl, Second.PosterUrl, Third.PosterUrl, Fourth.PosterUrl,Fifth.PosterUrl, Sixth.PosterUrl,
                Seventh.PosterUrl, Eighth.PosterUrl, Ninth.PosterUrl
            };

            return await ImagesDownloadingUtility.LoadImagesArray(iconsUrls);
        }
    }

    public struct MatchBoardElementData
    {
        [JsonProperty("active")] public bool Activity;
        [JsonProperty("poster")] public string PosterUrl;
        [JsonProperty("icon_id")] public int IconId;
    }
}