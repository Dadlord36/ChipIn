using System.Collections.Generic;
using Utilities;

namespace Common
{
    public sealed class ChangedPropertiesCollector
    {
        public Dictionary<string, string> ChangedPropertiesCollection { get; } = new Dictionary<string, string>();
        public Dictionary<string, string> ChangedPropertiesWithFileDataCollection { get; } = new Dictionary<string, string>();
        
        public void AddChangedField(in string value, string propertyName)
        {
            if (string.IsNullOrEmpty(value)) return;
            ChangedPropertiesCollection.AddOrUpdate(value, propertyName);
        }

        public void AddChangedFieldWithFileData(in string pathToFile, string propertyName)
        {
            if (string.IsNullOrEmpty(pathToFile)) return;
            ChangedPropertiesWithFileDataCollection.AddOrUpdate(pathToFile, propertyName);
        }

        public void ClearCollectedFields()
        {
            ChangedPropertiesCollection.Clear();
            ChangedPropertiesWithFileDataCollection.Clear();
        }
    }
}