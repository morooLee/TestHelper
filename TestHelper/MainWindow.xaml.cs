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
            Url_TextBlock.Text = item.Url;
            Inspection_WebBrowser.Source = new Uri(item.Url);
        }

        private void Inspection_WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            try
            {
                var doc = (Inspection_WebBrowser.Document as mshtml.HTMLDocument).getElementById("InspectionTime");
                Date_TextBlock.Text = doc.innerText;
                InspectionPageInfo item = InspectionPageInfoList_ListView.SelectedItem as InspectionPageInfo;
                if (item != null)
                {
                    item.InspectionDate = doc.innerText;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
