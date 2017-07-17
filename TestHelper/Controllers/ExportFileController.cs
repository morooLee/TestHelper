using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestHelper.Models;

namespace TestHelper.Controllers
{
    public class ExportFileController
    {
        public void ExportToCSV(ObservableCollection<GNBPageInfo> gnbPageInfoList)
        {
            string fileName = "GNBInfoList" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + @".csv";
            string path = Directory.GetCurrentDirectory() + @"\" + fileName;
            string category = string.Empty;
            string name = string.Empty;
            string url = string.Empty;
            string code = string.Empty;
            string hasGNB = string.Empty;
            string isPCHub = string.Empty;
            string isMyBanner = string.Empty;
            string isCheckedA2S = string.Empty;

            try
            {
                StreamWriter streamWriter = new StreamWriter(path, true, Encoding.UTF8);
                streamWriter.WriteLine("카테고리,페이지명,URL,페이지 코드,GNB 유무,PC방혜택 유무,맞춤혜택 유무,A2S 수집 여부");

                foreach (GNBPageInfo item in gnbPageInfoList)
                {
                    if(item.Category == Category.Common)
                    {
                        category = "공통 페이지";
                    }
                    else if(item.Category == Category.PCOnline)
                    {
                        category = "온라인 게임";
                    }
                    else if (item.Category == Category.Mobile)
                    {
                        category = "모바일 게임";
                    }
                    else
                    {
                        category = "";
                    }

                    name = item.Name == null ? "" : item.Name;
                    url = item.Url == null ? "" : item.Url;
                    code = item.Code == null ? "" : item.Code;
                    hasGNB = item.HasGNB == null ? "" : Convert.ToString((bool)item.HasGNB);
                    isPCHub = item.IsPCHub == null ? "" : Convert.ToString((bool)item.IsPCHub);
                    isMyBanner = item.IsMyBanner == null ? "" : Convert.ToString((bool)item.IsMyBanner);
                    isCheckedA2S = item.IsCheckedA2S == null ? "" : Convert.ToString((bool)item.IsCheckedA2S);

                    streamWriter.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", category, name.Replace(",", "/"), url, code, hasGNB, isPCHub, isMyBanner, isCheckedA2S);
                }
                streamWriter.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
