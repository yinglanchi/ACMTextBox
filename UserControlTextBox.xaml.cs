using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ACM.Presentation.Views.Examples{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
public partial class UserControlTextBox : UserControl
    {
        public UserControlTextBox()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            ACM.Presentation.Controls.Text.ACMTextBox tbox = sender as ACM.Presentation.Controls.Text.ACMTextBox;
            tbox.IsTypingStart = true;
            if (e.Key == Key.Return)
            {
                tbox.IsTypingCompleted = true;
            }
        }
    }
}
