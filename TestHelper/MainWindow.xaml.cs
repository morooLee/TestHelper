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

        private void InspectionPageInfoList_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    Date_TextBlock.Text = "점검시간 : ";
                }
                else
                {
                    Date_TextBlock.Text = "점검시간 : " + doc.innerText;
                    InspectionPageInfo item = InspectionPageInfoList_ListView.SelectedItem as InspectionPageInfo;

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
            if (statusBar.HasItems)
            {
                statusBar.Items.RemoveAt(0);
            }
            if (obj != null)
            {
                statusBar.Items.Add(obj);
            }
        }

        private void Refresh_Menu_Click(object sender, RoutedEventArgs e)
        {
            switch(Tab_Control.SelectedIndex)
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

        private void InspectionPageInfoList_ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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

        private void Inspection_TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            //xmlController.GetInspectionList(inspectionPageInfoList);
            //InspectionPageInfoList_ListView.ItemsSource = inspectionPageInfoList;
        }

        private void Setting_Menu_Click(object sender, RoutedEventArgs e)
        {
            switch (Tab_Control.SelectedIndex)
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

        private void GNB_TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            //xmlController.GetGNBList(gnbPageInfoList);
            //GNBPageInfoList_ListView.ItemsSource = gnbPageInfoList;
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

        private void InspectionPageInfoList_ListView_Loaded(object sender, RoutedEventArgs e)
        {
            if (inspectionPageInfoList == null)
            {
                inspectionPageInfoList = new ObservableCollection<InspectionPageInfo>();

                xmlController.GetInspectionList(inspectionPageInfoList);
                InspectionPageInfoList_ListView.ItemsSource = inspectionPageInfoList;
            }
        }

        private void GNBListItem_MouseDoubleClick(object sender, RoutedEventArgs e)
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            GNBPageInfo item = ckb.DataContext as GNBPageInfo;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox ckb = sender as CheckBox;
            GNBPageInfo item = ckb.DataContext as GNBPageInfo;
        }

        private void Header_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (gnbPageInfoList != null && gnbPageInfoList.Count > 0)
            {
                foreach (GNBPageInfo item in gnbPageInfoList)
                {
                    item.IsChecked = true;
                }
            }
        }

        private void Header_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (gnbPageInfoList != null && gnbPageInfoList.Count > 0)
            {
                foreach (GNBPageInfo item in gnbPageInfoList)
                {
                    item.IsChecked = false;
                }
            }
        }

        private async void Action_Menu_Click(object sender, RoutedEventArgs e)
        {
            switch (Tab_Control.SelectedIndex)
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

        private void Tab_Control_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(Tab_Control.SelectedIndex)
            {
                case 0:
                    {
                        Action_MenuItem.Visibility = Visibility.Hidden;
                        Save_MenuItem.Visibility = Visibility.Hidden;
                        Export_MenuItem.Visibility = Visibility.Hidden;
                        Import_MenuItem.Visibility = Visibility.Hidden;
                        StatusBarItemChange(Inspection_WebBrowser.Source);
                        break;
                    }
                case 1:
                    {
                        Action_MenuItem.Visibility = Visibility.Visible;
                        Save_MenuItem.Visibility = Visibility.Visible;
                        Export_MenuItem.Visibility = Visibility.Visible;
                        Import_MenuItem.Visibility = Visibility.Visible;
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

        private void Save_MenuItem_Click(object sender, RoutedEventArgs e)
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

        private void Category_Header_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Category_ContextMenu.IsOpen = true;
        }

        private void Category_ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            Category_TextBlock.Text = "카테고리 ▼";
        }

        private void Category_ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            Category_TextBlock.Text = "카테고리 ▲";
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

        private void GameCode_TextBlock_TargetUpdated(object sender, DataTransferEventArgs e)
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

        private void GameCode_TextBlock_Loaded(object sender, RoutedEventArgs e)
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

        private void Export_Menu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportGlyph_MenuItem_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            ExportGlyph_Image.Source = new BitmapImage(new Uri(@"/TestHelper;component/Resources/GlyphUp_16x.png", UriKind.Relative));
        }

        private void ExportGlyph_MenuItem_SubmenuClosed(object sender, RoutedEventArgs e)
        {
            ExportGlyph_Image.Source = new BitmapImage(new Uri(@"/TestHelper;component/Resources/GlyphDown_16x.png", UriKind.Relative));
        }

        private void ExportCSV_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            importExportFileController.ExportToCSV(gnbPageInfoList);
        }

        private void ExportXLS_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            importExportFileController.ExportToXLS(gnbPageInfoList);
        }

        private void ExportTXT_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            importExportFileController.ExportToTXT(gnbPageInfoList);
        }

        private void Import_Menu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ImportGlyph_MenuItem_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            ImportGlyph_Image.Source = new BitmapImage(new Uri(@"/TestHelper;component/Resources/GlyphUp_16x.png", UriKind.Relative));
        }

        private void ImportGlyph_MenuItem_SubmenuClosed(object sender, RoutedEventArgs e)
        {
            ImportGlyph_Image.Source = new BitmapImage(new Uri(@"/TestHelper;component/Resources/GlyphDown_16x.png", UriKind.Relative));
        }

        private void ImportCSV_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            importExportFileController.ImportToCSV();
        }

        private void ImportXLS_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            importExportFileController.ImportToXLS();
        }

        private void ImportTXT_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            importExportFileController.ImportToTXT();
        }
    }
}
