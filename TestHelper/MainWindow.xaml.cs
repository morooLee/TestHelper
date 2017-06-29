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
using System.Windows;
using TestHelper.Windows.Inspection;

namespace TestHelper
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        XMLFileController xmlController = new XMLFileController();
        ObservableCollection<InspectionPageInfo> inspectionPageInfoList = new ObservableCollection<InspectionPageInfo>();

        public MainWindow()
        {
            xmlController.FileCheck();
            xmlController.GetInspectionList(inspectionPageInfoList);

            InitializeComponent();
        }

        private void WebBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InspectionPageInfoList_ListView.ItemsSource = inspectionPageInfoList;
            
        }

        private void InspectionPageInfoList_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            InspectionPageInfo item = listView.SelectedItem as InspectionPageInfo;
            Inspection_WebBrowser.Source = new Uri(item.Url);
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

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Setting_Menu_Click(object sender, RoutedEventArgs e)
        {
            switch (Tab_Control.SelectedIndex)
            {
                case 0:
                    {
                        InspectionSettingWindow subWindow = new InspectionSettingWindow(inspectionPageInfoList);
                        subWindow.Owner = Application.Current.MainWindow;
                        subWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        subWindow.ShowDialog();
                        break;
                    }
            }
        }
    }
}
