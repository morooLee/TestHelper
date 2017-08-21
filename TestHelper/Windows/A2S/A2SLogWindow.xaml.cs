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

namespace TestHelper.Windows.A2S
{
    /// <summary>
    /// A2SLogWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class A2SLogWindow : Window
    {
        A2SLogList list;
        public A2SLogWindow(A2SLogList a2sLogList)
        {
            list = a2sLogList;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            A2SLogList_ListView.ItemsSource = list;
        }
    }
}
