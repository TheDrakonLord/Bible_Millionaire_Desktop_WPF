/* Title: frmPresent.xaml.cs
 * Author: Neal Jamieson
 * Version: 1.0.0.0
 * 
 * Description:
 *     This form is only used for presentation to the client. It will be disabled afterwards. Meant to provide the client a way to ~open~ their gift
 *     
 * Dependencies:
 *     system
 *     system.windows
 *     system.windows.input
 *     
 * References:
 *     Based upon the hit game show: Who wants to be a millionaire
 *         [1] M. Cohen et al., “Who Wants to Be a Millionaire,” Buena Vista Television, Celador Productions, Disney-ABC Domestic Television, Valleycrest Productions.‌
 *     Based upon the book of the similar name that has since been lost and no longer shows up in online searches or databases
 *         [1] Unkown, Who Wants to be a Millionaire: Bible Edition. Unkown: Unkown, p. Whole.‌
 */

using System;
using System.Windows;
using System.Windows.Input;

namespace Bible_Millionaire_Desktop_WPF
{
    /// <summary>
    /// Interaction logic for frmPresent.xaml
    /// </summary>
    public partial class frmPresent : Window
    {
        private bool _bolDevFlag = false;
        /// <summary>
        /// primary constructor for the present form
        /// </summary>
        public frmPresent()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            if (_bolDevFlag)
            {
                this.Hide();
                formProvider.mainMenuForm.ShowDialog();
            }
            else
            {
                if (DateTime.Now.CompareTo(DateTime.Parse("12/25/2020")) >= 0)
                {
                    this.Hide();
                    formProvider.mainMenuForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("It is too early to open this gift", "No Peeking!");
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_bolDevFlag)
                {
                    this.Hide();
                    formProvider.mainMenuForm.ShowDialog();
                }
                else
                {
                    if (DateTime.Now.CompareTo(DateTime.Parse("12/25/2020")) >= 0)
                    {
                        this.Hide();
                        formProvider.mainMenuForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("It is too early to open this gift", "No Peeking!");
                    }
                }
            }
            else if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
