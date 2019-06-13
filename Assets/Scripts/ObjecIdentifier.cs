using System;

namespace Worlds
{
    [Serializable]
    public abstract class ObjecIdentifier
    {
        public int Id;
        public string Name;
        [NonSerialized]
        private string displayName;

        public string DisplayName
        {
            get
            {
                if (!string.IsNullOrEmpty(displayName))
                {
                    return displayName;
                }
                displayName = GetName();
                return displayName;
            }
            set { displayName = value; }
        }

        protected virtual string GetName()
        {
            return Name;
        }
        [NonSerialized]
        private string description;

        public string Description
        {
            get
            {
                if (!string.IsNullOrEmpty(description))
                {
                    return description;
                }
                description = GetDescription();
                return description;
            }
            set { description = value; }
        }

        protected virtual string GetDescription()
        {
            return "";
        }
    }
}