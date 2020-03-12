﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using CustomAnimators;
using DataModels.Interfaces;
using DataModels.MatchModels;
using UnityEngine;
using Utilities;
using Views;
using WebOperationUtilities;

namespace ViewModels
{
    public sealed partial class SlotsGameViewModel
    {
        /// <summary>
        /// Holds array of <see cref="SlotsView"/> Slot Icon resources, that are representing a group by their design,
        /// and gives functionality to get resource, that is corresponding to an ID  
        /// </summary>
        private class BoardIconsSetHolder
        {
            private readonly List<BoardIconData> _boardIcons = new List<BoardIconData>();
            private readonly int _rowsNumber, _columnsNumber;

            private BoardIconData[] BoardIcons
            {
                set
                {
                    _boardIcons.Clear();
                    _boardIcons.Capacity = value.Length;
                    _boardIcons.AddRange(value);
                }
            }

            public BoardIconsSetHolder(int rowsNumber, int columnsNumber)
            {
                _rowsNumber = rowsNumber;
                _columnsNumber = columnsNumber;
            }

            public void Refill(BoardIconData[] boardIconsData)
            {
                BoardIcons = boardIconsData;
            }

            // public void Refill(IReadOnlyList<IndexedTexture> indexedTextures)
            // {
            //     BoardIcons = SpritesAnimationResourcesCreator
            //         .CreateBoardIcons(indexedTextures, _rowsNumber, _columnsNumber).ToArray();
            // }


            private BoardIconData GetBordIconDataWithId(int index)
            {
                return _boardIcons.Find(icon => icon.Id == index);
            }

            public List<BoardIconData> GetBoardIconsDataWithIDs(IReadOnlyList<IIconIdentifier> identifiers)
            {
                var sprites = new List<BoardIconData>(identifiers.Count);
                for (int i = 0; i < identifiers.Count; i++)
                {
                    sprites.Add(GetBordIconDataWithId(identifiers[i].IconId));
                }

                return sprites;
            }
        }


        
    }
}