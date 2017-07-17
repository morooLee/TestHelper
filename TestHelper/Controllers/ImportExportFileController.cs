using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestHelper.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace TestHelper.Controllers
{
    public class ImportExportFileController
    {
        public bool ExportToCSV(ObservableCollection<GNBPageInfo> gnbPageInfoList)
        {
            bool isSucceed = false;
            isSucceed = DoExport(gnbPageInfoList, "csv");

            return isSucceed;
        }

        public bool ExportToTXT(ObservableCollection<GNBPageInfo> gnbPageInfoList)
        {
            bool isSucceed = false;
            isSucceed = DoExport(gnbPageInfoList, "txt");

            return isSucceed;
        }

        private bool DoExport(ObservableCollection<GNBPageInfo> gnbPageInfoList, string fileExtension)
        {
            string separator = string.Empty;
            bool isSucceed = false;
            string fileName = "GNBInfoList" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + @"." + fileExtension;
            string path = Directory.GetCurrentDirectory() + @"\" + fileName;
            int index = 1;
            string category = string.Empty;
            string name = string.Empty;
            string url = string.Empty;
            string code = string.Empty;
            string hasGNB = string.Empty;
            string isPCHub = string.Empty;
            string isMyBanner = string.Empty;
            string isCheckedA2S = string.Empty;

            if (fileExtension == "txt")
            {
                separator = "\t";
            }
            else if (fileExtension == "csv")
            {
                separator = ",";
            }
            else
            {
                separator = ";";
            }
            try
            {
                StreamWriter streamWriter = new StreamWriter(path, true, Encoding.UTF8);
                streamWriter.WriteLine("#"+ separator + "카테고리" + separator + "페이지명" + separator + "URL" + separator + "게임 코드" + separator + "GNB 유무" + separator + "PC방 혜택" + separator + "맞춤 혜택" + separator + "A2S 수집");

                foreach (GNBPageInfo item in gnbPageInfoList)
                {
                    if (item.Category == Category.Common)
                    {
                        category = "공통 페이지";
                    }
                    else if (item.Category == Category.PCOnline)
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

                    streamWriter.WriteLine(index + separator + category + separator + name.Replace(separator, " ") + separator + url.Replace(separator, " ") + separator + code + separator + hasGNB + separator + isPCHub + separator + isMyBanner + separator + isCheckedA2S);
                    index++;
                }
                streamWriter.Close();
                isSucceed = true;
                MessageBox.Show(fileExtension.ToUpper() + " 포맷으로 저장이 완료되었습니다.\r\n" + fileName, "Export To " + fileExtension.ToUpper(), MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                isSucceed = false;
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return isSucceed;
        }

        public bool ExportToXLS(ObservableCollection<GNBPageInfo> gnbPageInfoList)
        {
            bool isSucceed = false;
            Excel.Application excelApp = null;
            Excel.Workbook workBook = null;
            Excel.Worksheet workSheet = null;

            try
            {
                excelApp = new Excel.Application();
                workBook = excelApp.Workbooks.Add(System.Reflection.Missing.Value);
                workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
                //workBook = excelApp.Workbooks.Add();
                //workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

                workSheet.Cells[1, 1] = "#";
                workSheet.Cells[1, 2] = "카테고리";
                workSheet.Cells[1, 3] = "페이지명";
                workSheet.Cells[1, 4] = "URL";
                workSheet.Cells[1, 5] = "게임 코드";
                workSheet.Cells[1, 6] = "GNB 유무";
                workSheet.Cells[1, 7] = "PC방 혜택";
                workSheet.Cells[1, 8] = "맞춤 혜택";
                workSheet.Cells[1, 9] = "A2S 수집";

                int row = 2;

                foreach (GNBPageInfo item in gnbPageInfoList)
                {
                    workSheet.Cells[row, 1] = row - 1;
                    workSheet.Cells[row, 2] = item.Category;
                    workSheet.Cells[row, 3] = item.Name;
                    workSheet.Cells[row, 4] = item.Url;
                    workSheet.Cells[row, 5] = item.Code;
                    workSheet.Cells[row, 6] = item.HasGNB;
                    workSheet.Cells[row, 7] = item.IsPCHub;
                    workSheet.Cells[row, 8] = item.IsMyBanner;
                    workSheet.Cells[row, 9] = item.IsCheckedA2S;

                    row++;
                }

                string fileName = "GNBInfoList" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + @".xls";
                string path = Directory.GetCurrentDirectory() + @"\" + fileName;

                workBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal);
                workBook.Close(true);
                excelApp.Quit();

                isSucceed = true;
            }
            catch (COMException e)
            {
                isSucceed = false;
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                isSucceed = false;
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Clean up
                ReleaseExcelObject(workSheet);
                ReleaseExcelObject(workBook);
                ReleaseExcelObject(excelApp);
            }

            return isSucceed;
        }

        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception e)
            {
                obj = null;
                throw e;
            }
            finally
            {
                GC.Collect();
            }
        }

        public ObservableCollection<GNBPageInfo> ImportToCSV()
        {
            ObservableCollection<GNBPageInfo> gnbPageInfoList = new ObservableCollection<GNBPageInfo>();

            DoImport("csv");

            return gnbPageInfoList;
        }

        public ObservableCollection<GNBPageInfo> ImportToTXT()
        {
            ObservableCollection<GNBPageInfo> gnbPageInfoList = new ObservableCollection<GNBPageInfo>();

            DoImport("txt");

            return gnbPageInfoList;
        }

        private ObservableCollection<GNBPageInfo> DoImport(string fileExtension)
        {
            ObservableCollection<GNBPageInfo> gnbPageInfoList = new ObservableCollection<GNBPageInfo>();

            string separator = string.Empty;

            if (fileExtension == "txt")
            {
                separator = "\t";
            }
            else if (fileExtension == "csv")
            {
                separator = ",";
            }
            else
            {
                separator = ";";
            }

            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Title = fileExtension.ToUpper() + "로 가져오기";
            dialog.Filter = fileExtension.ToUpper() + " (*." + fileExtension + ") | *." + fileExtension + "; | 모든 파일 (*.*) | *.*";

            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            return gnbPageInfoList;
        }

        public ObservableCollection<GNBPageInfo> ImportToXLS()
        {
            ObservableCollection<GNBPageInfo> gnbPageInfoList = new ObservableCollection<GNBPageInfo>();

            return gnbPageInfoList;
        }
    }
}
