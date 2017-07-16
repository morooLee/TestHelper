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

namespace TestHelper.Windows.GNB
{
    /// <summary>
    /// GNBEditWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GNBEditWindow : Window
    {
        GNBPageInfo gnbPageInfo = new GNBPageInfo();
        GNBPageInfo tmp = new GNBPageInfo();

        public GNBEditWindow(GNBPageInfo item)
        {
            tmp.Clone(item);
            gnbPageInfo = item;
            InitializeComponent();
        }

        private void Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            bool emptyCategory = false;
            bool emptyName = false;
            bool notUri = false;

            if (Category_TextBlock.Text == "카테고리를 선택하세요.")
            {
                emptyCategory = true;
                Category_Label.Foreground = Brushes.Red;
            }
            else
            {
                Category_Label.Foreground = Brushes.Black;
            }

            if (PageName_TextBox.Text == string.Empty)
            {
                emptyName = true;
                PageName_Label.Foreground = Brushes.Red;
            }
            else
            {
                PageName_Label.Foreground = Brushes.Black;
            }

            try
            {
                Uri tmpUri = new Uri(URL_TextBox.Text);
                URL_Label.Foreground = Brushes.Black;
            }
            catch
            {
                notUri = true;
                URL_Label.Foreground = Brushes.Red;
            }

            if(emptyCategory || emptyName || notUri)
            {
                MessageBox.Show("빈칸이 있거나 URL이 잘못되었습니다.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                gnbPageInfo.Category = tmp.Category;
                gnbPageInfo.Name = tmp.Name;
                gnbPageInfo.Url = tmp.Url;
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (tmp.Category == Category.Common)
            {
                Category_TextBlock.Text = "공통 페이지";
            }
            else if (tmp.Category == Category.PCOnline)
            {
                Category_TextBlock.Text = "온라인 게임";
            }
            else if (tmp.Category == Category.Mobile)
            {
                Category_TextBlock.Text = "모바일 게임";
            }
            else
            {
                Category_TextBlock.Text = "카테고리를 선택하세요.";
            }

            Category_TextBlock.Width = Category_StackPanel.ActualWidth;

            PageName_TextBox.Text = tmp.Name;
            URL_TextBox.Text = tmp.Url;
        }

        private void Category_Menu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Common_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            tmp.Category = Category.Common;
            Category_TextBlock.Text = "공통 페이지";
        }

        private void PCOnline_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            tmp.Category = Category.PCOnline;
            Category_TextBlock.Text = "온라인 게임";
        }

        private void Mobile_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            tmp.Category = Category.Mobile;
            Category_TextBlock.Text = "모바일 게임";
        }
    }
}
