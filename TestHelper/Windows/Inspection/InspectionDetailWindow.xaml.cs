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
        InspectionPageInfo inspectionPageInfo = new InspectionPageInfo();
        bool isEdited = false;

        public InspectionDetailWindow(InspectionPageInfo item, bool isEdited)
        {
            this.isEdited = isEdited;

            if (item != null)
            {
                inspectionPageInfo = item;
            }

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataUpdate(inspectionPageInfo);

            if (isEdited)
            {
                Button btn = new Button();
                btn.Name = "Cancel_Button";
                btn.Margin = new Thickness(10, 0, 0, 0);
                btn.Content = "취소";
                btn.Click += new RoutedEventHandler(Cancel_Button_Click);
                Buttons_StackPanel.Children.Add(btn);
            }
        }

        public void DataUpdate(InspectionPageInfo item)
        {
            if (item != null)
            {
                inspectionPageInfo = item;

                PageName_TextBox.Text = item.PageName;
                URL_TextBox.Text = item.Url;
                Date_TextBox.Text = item.InspectionDate;
            }
            
            if (isEdited)
            {
                PageName_TextBox.IsReadOnly = false;
                URL_TextBox.IsReadOnly = false;
                Date_TextBox.IsReadOnly = true;
                Date_TextBox.Text = "수정할 때는 사용하지 않는 항목입니다.";
                Date_TextBox.IsEnabled = false;
                HelpWords_StackPanel.Visibility = Visibility.Hidden;
                Buttons_StackPanel.Margin = new Thickness(0);
            }
        }

        private void Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            if (isEdited)
            {
                if (PageName_TextBox.Text == string.Empty || URL_TextBox.Text == string.Empty)
                {
                    MessageBox.Show("페이지명 또는 URL을 입력하세요.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    inspectionPageInfo.PageName = PageName_TextBox.Text;
                    inspectionPageInfo.Url = URL_TextBox.Text;
                    isEdited = false;

                    Close();
                }
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
            if (isEdited)
            {
                e.Cancel = true;

                if (PageName_TextBox.Text != string.Empty || URL_TextBox.Text != string.Empty)
                {
                    MessageBoxResult result = MessageBox.Show("취소하시겠습니까?", "Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.OK)
                    {
                        this.Hide();
                        base.OnClosing(e);
                    }
                }
                else
                {
                    this.Hide();
                    base.OnClosing(e);
                }
            }
        }
    }
}
