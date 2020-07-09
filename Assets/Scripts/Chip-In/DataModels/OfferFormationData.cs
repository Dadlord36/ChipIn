using System;

namespace DataModels
{
    public class OfferFormationData
    {
        public string InitialCategory { get; set; }
        public string Info { get; set; }
        public string Budget { get; set; }
        public string Where { get; set; }
        public string Choice { get; set; }
        public string When { get; set; }

        public string this[string fieldName]
        {
            get
            {
                if (AreEqual(fieldName, nameof(InitialCategory)))
                    return InitialCategory;
                if (AreEqual(fieldName, nameof(Info)))
                    return Info;
                if (AreEqual(fieldName, nameof(Budget)))
                    return Budget;
                if (AreEqual(fieldName, nameof(Where)))
                    return Where;
                if (AreEqual(fieldName, nameof(Choice)))
                    return Choice;
                if (AreEqual(fieldName, nameof(When)))
                    return When;

                throw new ArgumentOutOfRangeException();
            }

            set
            {
                if (AreEqual(fieldName, nameof(InitialCategory)))
                {
                    InitialCategory = value;
                    return;
                }

                if (AreEqual(fieldName, nameof(Info)))
                {
                    Info = value;
                    return;
                }

                if (AreEqual(fieldName, nameof(Budget)))
                {
                    Budget = value;
                    return;
                }

                if (AreEqual(fieldName, nameof(Where)))
                {
                    Where = value;
                    return;
                }

                if (AreEqual(fieldName, nameof(Choice)))
                {
                    Choice = value;
                    return;
                }

                if (AreEqual(fieldName, nameof(When)))
                {
                    When = value;
                    return;
                }
            }
        }

        private static bool AreEqual(in string a, in string b)
        {
            var leftPart = a.Split(' ')[0];
            return string.Equals(b, leftPart, StringComparison.OrdinalIgnoreCase);
        }
    }
}