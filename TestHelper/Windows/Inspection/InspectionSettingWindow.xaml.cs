using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestHelper.Controllers;
using TestHelper.Models;

namespace TestHelper.Windows.Inspection
{
    /// <summary>
    /// InspectionSettingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InspectionSettingWindow : Window
    {
        XMLFileController xmlController = new XMLFileController();
        ObservableCollection<InspectionPageInfo> inspectionPageInfoList = new ObservableCollection<InspectionPageInfo>();
        public InspectionSettingWindow()
        {
            xmlController.GetInspectionList(inspectionPageInfoList);
            InitializeComponent();
        }

        private void InspectionPageInfoList_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InspectionPageInfoList_ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ListViewItem listViewItem = sender as ListViewItem;
            //if (listViewItem.IsSelected)
            //{
            //    InspectionPageInfo item = listViewItem.DataContext as InspectionPageInfo;
            //    InspectionDetailWindow subWindow = new InspectionDetailWindow(item);
            //    subWindow.Owner = Application.Current.MainWindow;
            //    subWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //    subWindow.ShowDialog();
            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InspectionPageInfoList_ListView.ItemsSource = inspectionPageInfoList;
        }
    }
}
