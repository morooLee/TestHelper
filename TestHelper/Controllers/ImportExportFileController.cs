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
using System.Data;
using System.Data.OleDb;
using System.Reflection;

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
            bool isSucceed = false;
            string separator = string.Empty;
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

            #region Using OleDB
            //string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
            //string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
            //string fileName = fileName = "GNBInfoList" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + @".xls";
            //string path = Directory.GetCurrentDirectory() + @"\" + fileName;
            //string fileContents = string.Empty;

            //int index = 1;
            //string category = string.Empty;
            //string name = string.Empty;
            //string url = string.Empty;
            //string code = string.Empty;
            //string hasGNB = string.Empty;
            //string isPCHub = string.Empty;
            //string isMyBanner = string.Empty;
            //string isCheckedA2S = string.Empty;

            //if (Environment.Is64BitOperatingSystem == true)
            //{
            //    fileContents = string.Format(Excel07ConString, path, false);
            //}
            //else
            //{
            //    fileContents = string.Format(Excel03ConString, path, false);
            //}

            //using (OleDbConnection con = new OleDbConnection(fileContents))
            //{
            //    using (OleDbCommand cmd = new OleDbCommand())
            //    {
            //        cmd.Connection = con;
            //        con.Open();

            //        cmd.CommandText = "CREATE TABLE [table1] (id INT, Category NVARCHAR, PageName NVARCHAR, URL VARCHAR, GameCode VARCHAR, HasGNB VARCHAR, IsPcCafe VARCHAR, IsMyBanner VARCHAR, IsCheckedA2S VARCHAR);";
            //        cmd.ExecuteNonQuery();

            //        foreach (GNBPageInfo item in gnbPageInfoList)
            //        {
            //            switch (item.Category)
            //            {
            //                case (Category.Common):
            //                    {
            //                        category = "공통 페이지";
            //                        break;
            //                    }
            //                case (Category.PCOnline):
            //                    {
            //                        category = "온라인 게임";
            //                        break;
            //                    }
            //                case (Category.Mobile):
            //                    {
            //                        category = "모바일 게임";
            //                        break;
            //                    }
            //                case (Category.None):
            //                    {
            //                        category = "";
            //                        break;
            //                    }
            //            }

            //            name = item.Name == null ? "" : item.Name;
            //            url = item.Url == null ? "" : item.Url;
            //            code = item.Code == null ? "" : item.Code;
            //            hasGNB = item.HasGNB == null ? "" : Convert.ToString((bool)item.HasGNB);
            //            isPCHub = item.IsPCHub == null ? "" : Convert.ToString((bool)item.IsPCHub);
            //            isMyBanner = item.IsMyBanner == null ? "" : Convert.ToString((bool)item.IsMyBanner);
            //            isCheckedA2S = item.IsCheckedA2S == null ? "" : Convert.ToString((bool)item.IsCheckedA2S);

            //            cmd.CommandText = "INSERT INTO [table1] (id, Category, PageName, URL, GameCode, HasGNB, IsPcCafe, IsMyBanner, IsCheckedA2S) VALUES(" + index + ", '" + category + "', '" + name + "', '" + url + "', '" + code + "', '" + hasGNB + "', '" + isPCHub + "', '" + isMyBanner + "', '" + isCheckedA2S + "');";
            //            cmd.ExecuteNonQuery();

            //            index++;
            //        }

            //        con.Close();
            //    }
            //} 
            #endregion

            #region Using Excel
            Excel.Application excelApp = null;
            Excel.Workbook workBook = null;
            Excel.Worksheet workSheet = null;

            try
            {
                excelApp = new Excel.Application();
                workBook = excelApp.Workbooks.Add(System.Reflection.Missing.Value);
                workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);

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

                MessageBox.Show("XLS 포맷으로 저장이 완료되었습니다.\r\n" + fileName, "Export To XLS", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (COMException)
            {
                isSucceed = false;
                MessageBox.Show("Excel이 설치되어 있지 않아 저장에 실패했습니다.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            #endregion

            return isSucceed;
        }

        public bool ImportToCSV(ObservableCollection<GNBPageInfo> gnbPageInfoList)
        {
            bool isSucceed = false;
            isSucceed = DoImport(gnbPageInfoList, "csv");

            return isSucceed;
        }

        public bool ImportToTXT(ObservableCollection<GNBPageInfo> gnbPageInfoList)
        {
            bool isSucceed = false;
            isSucceed = DoImport(gnbPageInfoList, "txt");

            return isSucceed;
        }

        private bool DoImport(ObservableCollection<GNBPageInfo> gnbPageInfoList, string fileExtension)
        {
            bool isSucceed = false;
            string separator = string.Empty;
            string fileName = string.Empty;
            string path = string.Empty;

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

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                fileName = dialog.SafeFileName;
                path = dialog.FileName;

                try
                {
                    FileStream fileStream = new FileStream(path, FileMode.Open);
                    StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, false);

                    string fileContents = string.Empty;
                    string[] list = null;
                    string[] properties = null;

                    fileContents = streamReader.ReadToEnd();

                    if (string.IsNullOrEmpty(fileContents) == false)
                    {
                        list = fileContents.Split('\n');

                        for (int i = 1; i < list.Length - 1; i++)
                        {
                            list[i] = list[i].Replace("\r", "");
                            GNBPageInfo tmp = new GNBPageInfo();
                            properties = list[i].Split(Convert.ToChar(separator));

                            if (properties[1] == "공통 페이지")
                            {
                                tmp.Category = Category.Common;
                            }
                            else if (properties[1] == "온라인 게임")
                            {
                                tmp.Category = Category.PCOnline;
                            }
                            else if (properties[1] == "모바일 게임")
                            {
                                tmp.Category = Category.Mobile;
                            }
                            else
                            {
                                tmp.Category = Category.Common;
                            }

                            tmp.Name = properties[2];
                            tmp.Url = properties[3];
                            tmp.Code = properties[4];
                            tmp.HasGNB = properties[5] == "" ? (bool?)null : Convert.ToBoolean(properties[5]);
                            tmp.IsPCHub = properties[6] == "" ? (bool?)null : Convert.ToBoolean(properties[6]);
                            tmp.IsMyBanner = properties[7] == "" ? (bool?)null : Convert.ToBoolean(properties[7]);
                            tmp.IsCheckedA2S = properties[8] == "" ? (bool?)null : Convert.ToBoolean(properties[8]);

                            gnbPageInfoList.Add(tmp);
                        }

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return isSucceed;
        }

        public bool ImportToXLS(ObservableCollection<GNBPageInfo> gnbPageInfoList)
        {
            bool isSucceed = false;

            #region Using OleDB
            //string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
            //string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
            //string fileName = string.Empty;
            //string path = string.Empty;
            //string fileExtension = string.Empty;
            //string fileContents = string.Empty;
            //string sheetName = string.Empty;

            //System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            //dialog.Title = "XSL로 가져오기";
            //dialog.Filter = "XLS, XLSX (*.xls, *.xlsx) | *.xls;*.xlsx; | 모든 파일 (*.*) | *.*";

            //try
            //{
            //    System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            //    if (result == System.Windows.Forms.DialogResult.OK)
            //    {
            //        fileName = dialog.SafeFileName;
            //        path = dialog.FileName;
            //        fileExtension = Path.GetExtension(path);

            //        //switch (fileExtension)
            //        //{
            //        //    case ".xls":    //Excel 97-03
            //        //        {
            //        //            fileContents = string.Format(Excel07ConString, path, false);
            //        //            break;
            //        //        }
            //        //    case ".xlsx":  //Excel 07
            //        //        {
            //        //            fileContents = string.Format(Excel07ConString, path, false);
            //        //            break;
            //        //        }
            //        //}

            //        if (Environment.Is64BitOperatingSystem == true)
            //        {
            //            fileContents = string.Format(Excel07ConString, path, false);
            //        }
            //        else
            //        {
            //            fileContents = string.Format(Excel03ConString, path, false);
            //        }

            //        using (OleDbConnection con = new OleDbConnection(fileContents))
            //        {
            //            using (OleDbCommand cmd = new OleDbCommand())
            //            {
            //                cmd.Connection = con;
            //                con.Open();
            //                DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //                sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            //                con.Close();
            //            }
            //        }

            //        using (OleDbConnection con = new OleDbConnection(fileContents))
            //        {
            //            using (OleDbCommand cmd = new OleDbCommand())
            //            {
            //                using (OleDbDataAdapter oda = new OleDbDataAdapter())
            //                {
            //                    DataTable dt = new DataTable();
            //                    cmd.CommandText = "SELECT * From [" + sheetName + "]";
            //                    cmd.Connection = con;
            //                    con.Open();
            //                    oda.SelectCommand = cmd;
            //                    oda.Fill(dt);
            //                    con.Close();

            //                    int columnCount = dt.Columns.Count;

            //                    foreach (DataRow row in dt.Rows)
            //                    {
            //                        GNBPageInfo tmp = new GNBPageInfo();

            //                        for (int i = 0; i < columnCount; i++)
            //                        {
            //                            switch (i)
            //                            {
            //                                case (1):
            //                                    {
            //                                        switch (row.ItemArray[1].ToString())
            //                                        {
            //                                            case ("0"):
            //                                                {
            //                                                    tmp.Category = Category.Common;
            //                                                    break;
            //                                                }
            //                                            case ("1"):
            //                                                {
            //                                                    tmp.Category = Category.PCOnline;
            //                                                    break;
            //                                                }
            //                                            case ("2"):
            //                                                {
            //                                                    tmp.Category = Category.Mobile;
            //                                                    break;
            //                                                }

            //                                        }
            //                                        break;
            //                                    }
            //                                case (2):
            //                                    {
            //                                        tmp.Name = row.ItemArray[2].ToString();
            //                                        break;
            //                                    }
            //                                case (3):
            //                                    {
            //                                        tmp.Url = row.ItemArray[3].ToString();
            //                                        break;
            //                                    }
            //                                case (4):
            //                                    {
            //                                        tmp.Code = row.ItemArray[4].ToString();
            //                                        break;
            //                                    }
            //                                case (5):
            //                                    {
            //                                        tmp.HasGNB = row.ItemArray[5].ToString() == "" ? (bool?)null : Convert.ToBoolean(row.ItemArray[5].ToString());
            //                                        break;
            //                                    }
            //                                case (6):
            //                                    {
            //                                        tmp.IsPCHub = row.ItemArray[6].ToString() == "" ? (bool?)null : Convert.ToBoolean(row.ItemArray[6].ToString());
            //                                        break;
            //                                    }
            //                                case (7):
            //                                    {
            //                                        tmp.IsMyBanner = row.ItemArray[7].ToString() == "" ? (bool?)null : Convert.ToBoolean(row.ItemArray[7].ToString());
            //                                        break;
            //                                    }
            //                                case (8):
            //                                    {
            //                                        tmp.IsCheckedA2S = row.ItemArray[8].ToString() == "" ? (bool?)null : Convert.ToBoolean(row.ItemArray[8].ToString());
            //                                        break;
            //                                    }
            //                            }
            //                        }

            //                        gnbPageInfoList.Add(tmp);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            #endregion

            #region Using Excel
            GNBPageInfo tmp = new GNBPageInfo();
            string fileName = "GNBInfoList" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + @".xls";
            string path = Directory.GetCurrentDirectory() + @"\" + fileName;

            Excel.Application excelApp = null;
            Excel.Workbook workBook = null;
            Excel.Worksheet workSheet = null;
            Excel.Range range = null;

            int rCnt = 0; // 열 갯수
            int cCnt = 0; // 행 갯수

            try
            {
                excelApp = new Excel.Application();
                workBook = excelApp.Workbooks.Open(path, 0, true, 5, Missing.Value, Missing.Value, false, Missing.Value, Missing.Value, true, false, Missing.Value, false, false, false);
                workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
                range = workSheet.UsedRange;

                for (rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                {
                    for (cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                    {
                        string value = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2; ;

                        switch (cCnt)
                        {
                            case (2):
                                {
                                    switch ((string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2)
                                    {
                                        case ("공통 페이지"):
                                            {
                                                tmp.Category = Category.Common;
                                                break;
                                            }
                                        case ("온라인 게임"):
                                            {
                                                tmp.Category = Category.PCOnline;
                                                break;
                                            }
                                        case ("모바일 게임"):
                                            {
                                                tmp.Category = Category.Mobile;
                                                break;
                                            }
                                        case (""):
                                            {
                                                tmp.Category = Category.None;
                                                break;
                                            }
                                    }
                                    break;
                                }
                            case (3):
                                {
                                    tmp.Name = value;
                                    break;
                                }
                            case (4):
                                {
                                    tmp.Url = value;
                                    break;
                                }
                            case (5):
                                {
                                    tmp.Code = value;
                                    break;
                                }
                            case (6):
                                {
                                    tmp.HasGNB = value == null ? (bool?)null : Convert.ToBoolean(value);
                                    break;
                                }
                            case (7):
                                {
                                    tmp.IsPCHub = value == null ? (bool?)null : Convert.ToBoolean(value);
                                    break;
                                }
                            case (8):
                                {
                                    tmp.IsMyBanner = value == null ? (bool?)null : Convert.ToBoolean(value);
                                    break;
                                }
                            case (9):
                                {
                                    tmp.IsCheckedA2S = value == null ? (bool?)null : Convert.ToBoolean(value);
                                    break;
                                }
                        }
                    }

                    gnbPageInfoList.Add(tmp);
                }

                isSucceed = true;

                MessageBox.Show("불러오기에 성공하였습니다.", "Import To XLS", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (COMException)
            {
                isSucceed = false;
                MessageBox.Show("Excel이 설치되어 있지 않아 불러오기에 실패했습니다.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            #endregion

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
    }
}
