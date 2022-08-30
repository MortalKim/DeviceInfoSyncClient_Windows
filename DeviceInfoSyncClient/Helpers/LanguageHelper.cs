using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeviceInfoSyncClient.Helpers
{
    static public class LanguageHelper
    {
        /// <summary>
        /// Change Language Runtime
        /// </summary>
        /// <param name="newLang">Support zh_cn and en_us Now</param>
        public static void ChangeLanguage(string newLang)
        {
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri(@"Resources\zh_cn.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries[0] = dict;
        }
    }
}
