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
        bool dataContextChanged = false;

        public InspectionSettingWindow(ObservableCollection<InspectionPageInfo> _inspectionPageInfoList)
        {
            xmlController.GetInspectionList(inspectionPageInfoList);
            inspectionPageInfoList.CollectionChanged += InspectionPageInfoList_CollectionChanged;

            InitializeComponent();
        }

        private void InspectionPageInfoList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            dataContextChanged = true;
        }

        private void InspectionPageInfoList_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InspectionPageInfoList_ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Edit_ListViewItem();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InspectionPageInfoList_ListView.ItemsSource = inspectionPageInfoList;

        }

        private void Up_Button_Click(object sender, RoutedEventArgs e)
        {
            InspectionPageInfo item = InspectionPageInfoList_ListView.SelectedItem as InspectionPageInfo;

            if (item != null && InspectionPageInfoList_ListView.SelectedIndex > 0)
            {
                inspectionPageInfoList.Move(InspectionPageInfoList_ListView.SelectedIndex, InspectionPageInfoList_ListView.SelectedIndex - 1);
            }
        }

        private void Down_Button_Click(object sender, RoutedEventArgs e)
        {
            InspectionPageInfo item = InspectionPageInfoList_ListView.SelectedItem as InspectionPageInfo;

            if (item != null && InspectionPageInfoList_ListView.SelectedIndex < (inspectionPageInfoList.Count - 1))
            {
                inspectionPageInfoList.Move(InspectionPageInfoList_ListView.SelectedIndex, InspectionPageInfoList_ListView.SelectedIndex + 1);
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            InspectionPageInfo tmp = new InspectionPageInfo();
            InspectionDetailWindow subWindow = new InspectionDetailWindow(tmp, true);
            subWindow.Owner = this;
            subWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            subWindow.ShowDialog();

            if (tmp.PageName != string.Empty || tmp.Url != string.Empty)
            {
                inspectionPageInfoList.Add(tmp);
            }
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Edit_ListViewItem();
        }

        private void Del_Button_Click(object sender, RoutedEventArgs e)
        {
            InspectionPageInfo item = InspectionPageInfoList_ListView.SelectedItem as InspectionPageInfo;
            if (item == null)
            {
                MessageBox.Show("선택된 항목이 없습니다.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                inspectionPageInfoList.Remove(item);
            }
        }

        private void Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            if(dataContextChanged)
            {
                xmlController.SetInspectionList(inspectionPageInfoList);
                ListView lv = Owner.FindName("Inspection_ListView") as ListView;
                ObservableCollection<InspectionPageInfo> tmp = lv.Items.SourceCollection as ObservableCollection<InspectionPageInfo>;

                tmp.Clear();

                foreach (InspectionPageInfo item in inspectionPageInfoList)
                {
                    tmp.Add(item);
                }

                dataContextChanged = false;

                Close();
            }
            else
            {
                Close();
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (dataContextChanged)
            {
                e.Cancel = true;

                MessageBoxResult result = MessageBox.Show("수정된 항목이 있습니다.\n그래도 취소하시겠습니까?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    this.Hide();
                    base.OnClosing(e);
                }
            }
        }

        private void Edit_ListViewItem()
        {
            InspectionPageInfo item = InspectionPageInfoList_ListView.SelectedItem as InspectionPageInfo;
            if (item == null)
            {
                MessageBox.Show("선택된 항목이 없습니다.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                InspectionDetailWindow subWindow = new InspectionDetailWindow(item, true);
                subWindow.Owner = this;
                subWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                subWindow.ShowDialog();
            }
        }
    }
}
