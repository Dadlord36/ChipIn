using System.Collections.Generic;
using Com.TheFallenGames.OSA.CustomParams;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    public class TextListItemData : ResizableItemData
    {
        public readonly string Text;

        private TextListItemData(string text)
        {
            Text = text;
        }

        public static IList<TextListItemData> MakeListFromStrings(string[] questionPredefinedAnswers)
        {
            var list = new List<TextListItemData>(questionPredefinedAnswers.Length);
            
            for (int i = 0; i < questionPredefinedAnswers.Length; i++)
            {
                list.Add(new TextListItemData(questionPredefinedAnswers[i]));
            }

            return list;
        }
    }

    public class SelectableTextListAdapter : SelectableListViewAdapter<BaseParamsWithPrefab, TextListItemData>
    {
    }
}