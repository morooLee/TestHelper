using System;
using System.Collections.Generic;
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
using TestHelper.Models;

namespace TestHelper.Windows.Inspection
{
    /// <summary>
    /// InspectionDetailWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InspectionDetailWindow : Window
    {
        InspectionPageInfo inspectionPageInfo;

        public InspectionDetailWindow(InspectionPageInfo item)
        {
            if (item != null)
            {
                inspectionPageInfo = item;
            }
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataUpdate(inspectionPageInfo);
        }

        public void DataUpdate(InspectionPageInfo item)
        {
            inspectionPageInfo = item;

            PageName_TextBox.Text = item.PageName;
            URL_TextBox.Text = item.Url;
            Date_TextBox.Text = item.InspectionDate;
        }

        private void Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
