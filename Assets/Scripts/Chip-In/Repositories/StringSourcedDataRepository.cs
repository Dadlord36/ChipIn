﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Repositories.Remote;
using UnityEngine;
using Utilities;

namespace Repositories
{
    public abstract class StringSourcedDataRepository<TDataType> : RemoteRepositoryBase, IPaginatedItemsListRepository<TDataType>
        where TDataType : class
    {
        public class TempRepoList<T>
        {
            [JsonProperty("list")] public List<T> ListOfItems;
        }

        [SerializeField] private string sourceString;
        private List<TDataType> _dataList = new List<TDataType>();

        public bool IsInitialized { get; }
        public bool IsBusy { get; }

        public uint GetCorrespondingToIndexPage(uint pageItemIndex)
        {
            throw new NotImplementedException();
        }

        public int ItemsPerPage { get; }
        public int TotalPages { get; }

        public uint TotalItemsNumber
        {
            get => (uint) _dataList.Count;
        }

        public uint LastPageItemsNumber { get; }


        public Task<IReadOnlyList<TDataType>> CreateGetPageItemsTask(uint pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TDataType>> CreateGetItemsRangeTask(uint startIndex, uint length)
        {
            return Task.FromResult<IReadOnlyList<TDataType>>(_dataList.GetRange((int) startIndex, (int) length));
        }

        public Task<TDataType> CreateGetItemWithIndexTask(uint itemIndex)
        {
            throw new NotImplementedException();
        }


        public override Task LoadDataFromServer()
        {
            if (string.IsNullOrEmpty(sourceString)) return Task.CompletedTask;
            var parsedData = JsonConverterUtility.ConvertJsonString<TempRepoList<TDataType>>(sourceString);
            _dataList.Clear();
            _dataList = parsedData.ListOfItems;
            return Task.CompletedTask;
        }

        public override Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
    }
}