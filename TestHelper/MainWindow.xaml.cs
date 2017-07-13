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

namespace TestHelper
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        XMLFileController xmlController = new XMLFileController();
        WebDriverController webDriverController = new WebDriverController();
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
            statusBar.Items.Add(obj);
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
            Console.WriteLine("클릭 ");
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Action_Menu_Click(object sender, RoutedEventArgs e)
        {
            switch (Tab_Control.SelectedIndex)
            {
                case 0:
                    {
                        
                        break;
                    }
                case 1:
                    {
                        int errorCount = webDriverController.GnbCheck(gnbPageInfoList);

                        if (errorCount > 0)
                        {
                            MessageBox.Show("GNB 체크 중에 에러가 발생하였습니다.\r\n자세한 사항은 Status의 Tooltip을 확인하세요.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        break;
                    }
                case 1:
                    {
                        Action_MenuItem.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }
    }
}
