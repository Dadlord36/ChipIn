using System;
using System.Collections.Generic;
using DataModels.Interfaces;
using Newtonsoft.Json;
using UnityEngine.Assertions;

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

        public int[] GetIdenticalActiveSlotsIndexes()
        {
            var elements = Elements;
            var activeElements = GetActiveElementsIndexes();

            Assert.IsTrue(activeElements.Count>0);
            
            var indexesArraysList = new List<int[]>();

            int[] CollectIndexesOfIdenticalItems(int controlIconId)
            {
                var indexes = new List<int>();
                
                for (int i = 0; i < activeElements.Count; i++)
                {
                    var activeElementId = activeElements[i];
                    
                    if (elements[activeElementId].IconId == controlIconId)
                        indexes.Add(activeElementId);
                }

                return indexes.ToArray();
            }

            for (int i = 0; i < activeElements.Count; i++)
            {
                indexesArraysList.Add(CollectIndexesOfIdenticalItems(elements[activeElements[i]].IconId));
            }

            if (indexesArraysList.Count == 0)
                return null;

            int arrayToReturnIndex = 0;
            int largestSetItemsNum = 0;

            for (int i = 0; i < indexesArraysList.Count; i++)
            {
                var length = indexesArraysList[i].Length;
                
                if (length > largestSetItemsNum)
                {
                    largestSetItemsNum = length;
                    arrayToReturnIndex = i;
                }
            }

            return indexesArraysList[arrayToReturnIndex];
        }

        private List<int> GetActiveElementsIndexes()
        {
            var elements = Elements;
            var indexes = new List<int>();
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].Active)
                    indexes.Add(i);
            }

            return indexes;
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

    public class MatchBoardElementData : ISlotIconBaseData, IPosterImageUri
    {
        public bool Active { get; set; }
        public int IconId { get; set; }
        public string PosterUri { get; set; }
    }
}