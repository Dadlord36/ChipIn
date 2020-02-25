using System;
using System.Threading.Tasks;
using DataModels.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using WebOperationUtilities;

namespace DataModels.MatchModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SlotsBoardData
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

        public MatchBoardElementData[] Elements =>
            new[] {First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eighth, Ninth};

        public SlotsBoardData(MatchBoardElementData first, MatchBoardElementData second, MatchBoardElementData third,
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
        
        public ISlotIconBaseData[] GetIconsData()
        {
            return Array.ConvertAll(Elements, item => (ISlotIconBaseData) item);
        }
    }
    
    public interface IActive
    {
        [JsonProperty("active")] bool Active { get; set; }
    }

    public interface IIconIdentifier
    {
        [JsonProperty("icon_id")] int IconId { get; set; }
    }

    public interface ISlotIconBaseData : IActive, IIconIdentifier
    {
    }

    public struct MatchBoardElementData : ISlotIconBaseData
    {
        public bool Active { get; set; }
        public int IconId { get; set; }
    }
}