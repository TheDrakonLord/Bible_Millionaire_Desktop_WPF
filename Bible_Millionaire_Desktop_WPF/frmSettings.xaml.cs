/* Title: frmSettings.xaml.cs
 * Author: Neal Jamieson
 * Version: 1.0.0.0
 * 
 * Description:
 *     allows the player to change various settings within the game
 *     
 * Dependencies:
 *     system.windows
 *     system.windows.input
 *     
 * References:
 *     Based upon the hit game show: Who wants to be a millionaire
 *         [1] M. Cohen et al., “Who Wants to Be a Millionaire,” Buena Vista Television, Celador Productions, Disney-ABC Domestic Television, Valleycrest Productions.‌
 *     Based upon the book of the similar name that has since been lost and no longer shows up in online searches or databases
 *         [1] Unkown, Who Wants to be a Millionaire: Bible Edition. Unkown: Unkown, p. Whole.‌
 */

using System.Windows;
using System.Windows.Input;

namespace Bible_Millionaire_Desktop_WPF
{
    /// <summary>
    /// Interaction logic for frmSettings.xaml
    /// </summary>
    public partial class frmSettings : Window
    {
        /// <summary>
        /// primary constructor for the settings form
        /// </summary>
        public frmSettings()
        {
            InitializeComponent();
        }



        private void radBook_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.sbolLifelineAClassic = false;
        }

        private void radAudience_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.sbolLifelineAClassic = true;
        }

        private void radHint_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.sbolLifelineBClassic = false;
        }

        private void radPhone_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.sbolLifelineBClassic = true;
        }

        private void radTimerOn_Checked(object sender, RoutedEventArgs e)
        {
            
            Properties.Settings.Default.sbolTimerEnabled = true;
        }

        private void radTimerOff_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.sbolTimerEnabled = false;
        }

        private void radTutorialOn_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.sbolSkipTutorial = false;
        }

        private void radTutorialOff_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.sbolSkipTutorial = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            radAudience.IsChecked = Properties.Settings.Default.sbolLifelineAClassic;
            radPhone.IsChecked = Properties.Settings.Default.sbolLifelineBClassic;
            radTimerOn.IsChecked = Properties.Settings.Default.sbolTimerEnabled;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            formProvider.settingsForm.Hide();
            formProvider.mainMenuForm.Show();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Properties.Settings.Default.Save();
            }
            else if (e.Key == Key.Escape)
            {
                formProvider.settingsForm.Hide();
                formProvider.mainMenuForm.Show();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            formProvider.settingsForm.Hide();
            formProvider.mainMenuForm.Show();
        }
    }
}
