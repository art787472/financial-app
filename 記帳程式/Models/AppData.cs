using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳程式.Models
{
    internal class AppData
    {
        public static readonly List<KeyValuePair<string, string>> typeList =
           new List<KeyValuePair<string, string>>
           {
                new KeyValuePair<string, string>("食", "食"),
                new KeyValuePair<string, string>("衣", "衣"),
                new KeyValuePair<string, string>("住", "住"),
                new KeyValuePair<string, string>("行", "行")
           };

        public static readonly List<KeyValuePair<string, string>> foodList =
            new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("便當", "便當"),
                new KeyValuePair<string, string>("麵", "麵"),
                new KeyValuePair<string, string>("美式速食", "美式速食")
            };

        public static readonly List<KeyValuePair<string, string>> clothesList =
            new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Tshort", "Tshort"),
                new KeyValuePair<string, string>("外套", "外套"),
                new KeyValuePair<string, string>("長褲", "長褲"),
                new KeyValuePair<string, string>("短褲", "短褲")
            };

        public static readonly List<KeyValuePair<string, string>> livingList =
            new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("房租", "房租"),
                new KeyValuePair<string, string>("水電費", "水電費")
            };

        public static readonly List<KeyValuePair<string, string>> transportationList =
            new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("油錢", "油錢"),
                new KeyValuePair<string, string>("公車", "公車"),
                new KeyValuePair<string, string>("火車", "火車"),
                new KeyValuePair<string, string>("捷運", "捷運")
            };

        public static readonly Dictionary<string, List<KeyValuePair<string, string>>> typeDictionary =
            new Dictionary<string, List<KeyValuePair<string, string>>>
            {
                { "食", foodList },
                { "衣", clothesList },
                { "住", livingList },
                { "行", transportationList }
            };

    }
}
