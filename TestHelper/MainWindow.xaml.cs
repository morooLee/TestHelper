using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestHelper.Controllers;
using TestHelper.Models;
using System.Collections.ObjectModel;
using TestHelper.Windows.Inspection;
using System.ComponentModel;
using System.Globalization;
using TestHelper.Windows.GNB;
using TestHelper.Windows.A2S;
using System.Threading;
using System.Windows.Threading;

namespace TestHelper
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        XMLFileController xmlController = new XMLFileController();
        WebDriverController webDriverController = new WebDriverController();
        ImportExportFileController importExportFileController = new ImportExportFileController();
        ObservableCollection<InspectionPageInfo> inspectionPageInfoList = null;
        ObservableCollection<GNBPageInfo> gnbPageInfoList = null;

        static A2SLogList a2sLogList = new A2SLogList();

        public MainWindow()
        {
            xmlController.FileCheck();
            InitializeComponent();
        }

        private void WebBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Inspection_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            InspectionPageInfo item = listView.SelectedItem as InspectionPageInfo;
            if (item != null)
            {
                try
                {
                    Inspection_WebBrowser.Source = new Uri(item.Url);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Inspection_WebBrowser.Source = null;
                }
            }
            else
            {
                Inspection_WebBrowser.Source = null;
            }
        }

        private void Inspection_WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            try
            {
                var doc = (Inspection_WebBrowser.Document as mshtml.HTMLDocument).getElementById("InspectionTime");

                if (doc == null)
                {
                    InspectionDate_TextBlock.Text = "점검시간 : ";
                }
                else
                {
                    InspectionDate_TextBlock.Text = "점검시간 : " + doc.innerText;
                    InspectionPageInfo item = Inspection_ListView.SelectedItem as InspectionPageInfo;

                    if (item != null)
                    {
                        item.InspectionDate = doc.innerText;
                    }
                }

                StatusBarItemChange(Inspection_WebBrowser.Source);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StatusBarItemChange(object obj)
        {
            if (Common_StatusBar.HasItems)
            {
                Common_StatusBar.Items.RemoveAt(0);
            }
            if (obj != null)
            {
                Common_StatusBar.Items.Add(obj);
            }
        }

        private void Common_Refresh_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            switch(Main_TabControl.SelectedIndex)
            {
                case 0:
                    {
                        Inspection_WebBrowser.Refresh();
                        break;
                    }
                case 1:
                    {
                        xmlController.GetGNBList(gnbPageInfoList);
                        GNBPageInfoList_ListView.ItemsSource = gnbPageInfoList;
                        Console.WriteLine("Reflash");
                        break;
                    }
            }
        }

        private void Inspection_ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem listViewItem = sender as ListViewItem;
            if (listViewItem.IsSelected)
            {
                InspectionPageInfo item = listViewItem.DataContext as InspectionPageInfo;
                InspectionDetailWindow subWindow = new InspectionDetailWindow(item, false);
                subWindow.Owner = Application.Current.MainWindow;
                subWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                subWindow.ShowDialog();
            }
        }

        private void Common_Setting_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            switch (Main_TabControl.SelectedIndex)
            {
                case 0:
                    {
                        ObservableCollection<InspectionPageInfo> tmp = new ObservableCollection<InspectionPageInfo>();
                        InspectionSettingWindow subWindow = new InspectionSettingWindow(inspectionPageInfoList);
                        subWindow.Owner = Application.Current.MainWindow;
                        subWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        subWindow.ShowDialog();

                        if (inspectionPageInfoList.Count > 0)
                        {
                            Inspection_WebBrowser.Source = new Uri(inspectionPageInfoList[0].Url);
                        }
                        break;
                    }
                case 1:
                    {
                        break;
                    }
            }
        }

        private void GNBPageInfoList_ListView_Loaded(object sender, RoutedEventArgs e)
        {
            if (gnbPageInfoList == null)
            {
                gnbPageInfoList = new ObservableCollection<GNBPageInfo>();

                xmlController.GetGNBList(gnbPageInfoList);
                GNBPageInfoList_ListView.ItemsSource = gnbPageInfoList;
            }
        }

        private void Inspection_ListView_Loaded(object sender, RoutedEventArgs e)
        {
            if (inspectionPageInfoList == null)
            {
                inspectionPageInfoList = new ObservableCollection<InspectionPageInfo>();

                xmlController.GetInspectionList(inspectionPageInfoList);
                Inspection_ListView.ItemsSource = inspectionPageInfoList;
            }
        }

        private void GNBListViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            GNBPageInfo item = ((ListViewItem)sender).DataContext as GNBPageInfo;
            GNBEditWindow gnbEditWindow = new GNBEditWindow(item);
            gnbEditWindow.Owner = Application.Current.MainWindow;
            gnbEditWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            gnbEditWindow.ShowDialog();

        }

        private void GNBListItem_MouseUp(object sender, RoutedEventArgs e)
        {
            ListViewItem listViewItem = sender as ListViewItem;
            if (listViewItem.IsSelected)
            {
                GNBPageInfo item = listViewItem.DataContext as GNBPageInfo;
            }
        }

        private void GNB_ListViewItem_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            GNBPageInfo item = ckb.DataContext as GNBPageInfo;
        }

        private void GNB_ListViewItem_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            GNBPageInfo item = ckb.DataContext as GNBPageInfo;
        }

        private void GNB_IsChecked_Header_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (gnbPageInfoList != null && gnbPageInfoList.Count > 0)
            {
                foreach (GNBPageInfo item in gnbPageInfoList)
                {
                    item.IsChecked = true;
                }
            }
        }

        private void GNB_IsChecked_Header_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (gnbPageInfoList != null && gnbPageInfoList.Count > 0)
            {
                foreach (GNBPageInfo item in gnbPageInfoList)
                {
                    item.IsChecked = false;
                }
            }
        }

        private async void Common_Action_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            switch (Main_TabControl.SelectedIndex)
            {
                case 0:
                    {
                        
                        break;
                    }
                case 1:
                    {
                        int errorCount = await webDriverController.GnbCheck(gnbPageInfoList);

                        if (errorCount > 0)
                        {
                            MessageBox.Show("GNB 체크 중에 에러가 발생하였습니다.\r\n자세한 사항은 상태 항목의 Tooltip을 확인하세요.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    }
            }
        }

        private void Main_TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(Main_TabControl.SelectedIndex)
            {
                case 0:
                    {
                        Common_Action_MenuItem.Visibility = Visibility.Hidden;
                        Common_Save_MenuItem.Visibility = Visibility.Hidden;
                        Common_Export_MenuItem.Visibility = Visibility.Hidden;
                        Common_Import_MenuItem.Visibility = Visibility.Hidden;
                        StatusBarItemChange(Inspection_WebBrowser.Source);
                        break;
                    }
                case 1:
                    {
                        Common_Action_MenuItem.Visibility = Visibility.Visible;
                        Common_Save_MenuItem.Visibility = Visibility.Visible;
                        Common_Export_MenuItem.Visibility = Visibility.Visible;
                        Common_Import_MenuItem.Visibility = Visibility.Visible;
                        GNBChangedStatusBar();
                        break;
                    }
            }
        }

        private void GNBChangedStatusBar()
        {
            GNBPageInfo item = GNBPageInfoList_ListView.SelectedItem as GNBPageInfo;
            string status = string.Empty;
            if (item != null)
            {
                status = item.Name + " | " + item.Url;
            }
            StatusBarItemChange(status);
        }

        private void Common_Save_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            bool isSaved = xmlController.SetGNBList(gnbPageInfoList);

            if (isSaved)
            {
                foreach (GNBPageInfo item in GNBPageInfoList_ListView.Items)
                {
                    if(item.IsChanged)
                    {
                        item.IsChanged = false;
                    }
                }
                MessageBox.Show("저장되었습니다.", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CategoryAll_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void Common_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void PCOnline_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Mobile_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void GNB_GameCode_Item_TextBlock_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            GNBPageInfo gnb = textBlock.DataContext as GNBPageInfo;
            gnb.IsChanged = true;

            if (textBlock.Text == string.Empty)
            {
                textBlock.ToolTip = textBlock.Tag + " > 비어있음" ;
                textBlock.Tag = "비어있음";
            }
            else
            {
                textBlock.ToolTip = textBlock.Tag + " > " + textBlock.Text;
                textBlock.Tag = textBlock.Text;
            }
            
        }

        private void GNB_GameCode_Item_TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            
            if (textBlock.Text == string.Empty)
            {
                textBlock.Tag = "비어있음";
                textBlock.ToolTip = "비어있음";
            }
            else
            {
                textBlock.Tag = textBlock.Text;
                textBlock.ToolTip = textBlock.Text;
            }
        }

        private void GNBPageInfoList_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GNBChangedStatusBar();
            e.Handled = true;
        }

        private void Common_Export_SubMenuItem_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            Common_ExportGlyphUpDown_Image.Source = new BitmapImage(new Uri(@"/TestHelper;component/Resources/GlyphUp_16x.png", UriKind.Relative));
        }

        private void Common_Export_SubMenuItem_MenuItemSubmenuClosed(object sender, RoutedEventArgs e)
        {
            Common_ExportGlyphUpDown_Image.Source = new BitmapImage(new Uri(@"/TestHelper;component/Resources/GlyphDown_16x.png", UriKind.Relative));
        }

        private void Common_ExportToCSV_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            importExportFileController.ExportToCSV(gnbPageInfoList);
        }

        private void Common_ExportToXLS_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            importExportFileController.ExportToXLS(gnbPageInfoList);
        }

        private void Common_ExportToTXT_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            importExportFileController.ExportToTXT(gnbPageInfoList);
        }

        private void Common_ImportGlyphUpDown_MenuItem_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            Common_ImportGlyphUpDown_Image.Source = new BitmapImage(new Uri(@"/TestHelper;component/Resources/GlyphUp_16x.png", UriKind.Relative));
        }

        private void Common_ImportGlyphUpDown_MenuItem_SubmenuClosed(object sender, RoutedEventArgs e)
        {
            Common_ImportGlyphUpDown_Image.Source = new BitmapImage(new Uri(@"/TestHelper;component/Resources/GlyphDown_16x.png", UriKind.Relative));
        }

        private void Common_ImportToCSV_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            importExportFileController.ImportToCSV(gnbPageInfoList);
        }

        private void Common_ImportToXLS_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            importExportFileController.ImportToXLS(gnbPageInfoList);
        }

        private void Common_ImportToTXT_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            importExportFileController.ImportToTXT(gnbPageInfoList);
        }

        private void A2S_Url_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string url;

                if (A2S_Url_TextBox.Text.StartsWith(@"http://") || A2S_Url_TextBox.Text.StartsWith(@"https://") != true)
                {
                    url = @"http://" + A2S_Url_TextBox.Text;
                }
                else
                {
                    url = A2S_Url_TextBox.Text;
                }

                A2S_WebBrowser.Navigate(new Uri(url));

                A2SLogWindow a2sLogWindow = new A2SLogWindow(a2sLogList);
                a2sLogWindow.Owner = Application.Current.MainWindow;
                a2sLogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                a2sLogWindow.Show();
            }
        }

        private void A2S_WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            StatusBarItemChange(A2S_WebBrowser.Source);

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(A2SCheck);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            dispatcherTimer.Start();
        }

        private void A2SCheck(object sender, EventArgs e)
        {
            try
            {
                webDriverController.A2SLogCheck(A2S_WebBrowser.Document, a2sLogList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
