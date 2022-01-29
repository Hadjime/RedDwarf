using System;
using System.Reflection;

namespace Utils.StringInEnum
{
    public enum StringEnum
    {
        [StringValue("1")]
        Qwerty,
        [StringValue("2")]
        Asdf,
        [StringValue("Place")]
        Place,
    }
    public class StringValueAttribute : System.Attribute
    {
        public string StringValue { get; protected set;}


        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

        public static string GetStringValue(Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];
            
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
        
    }

    public class DictionaryKey
	{
		public const string RESOURCES_PROVIDER = "ResourcesProvider";
		public const string CURRENT_HOUSE_INDEX = "CurrentHouseInd";
        public const string CHEAT_PANEL_HOUSE = "CheatPanelHouse";
        public const string Player2 = "player_22222222";
        public const string Player3 = "player_33333333";
        public const string Place = "Place";
    }

    
}
