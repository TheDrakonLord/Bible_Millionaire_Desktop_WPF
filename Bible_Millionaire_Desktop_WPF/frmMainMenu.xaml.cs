/* Title: frmMainMenu.xaml.cs
 * Author: Neal Jamieson
 * Version: 1.0.0.0
 * 
 * Description:
 *     primary entry form for the application. provides the player access to the settings form and to start the game
 *     
 * Dependencies:
 *     system.windows
 *     system.windows.input
 *     system.media
 *     
 * References:
 *     Based upon the hit game show: Who wants to be a millionaire
 *         [1] M. Cohen et al., “Who Wants to Be a Millionaire,” Buena Vista Television, Celador Productions, Disney-ABC Domestic Television, Valleycrest Productions.‌
 *     Based upon the book of the similar name that has since been lost and no longer shows up in online searches or databases
 *         [1] Unkown, Who Wants to be a Millionaire: Bible Edition. Unkown: Unkown, p. Whole.‌
 */

using System.Windows;
using System.Windows.Input;
using System.Media;

namespace Bible_Millionaire_Desktop_WPF
{
    /// <summary>
    /// Interaction logic for frmMainMenu.xaml
    /// </summary>
    public partial class frmMainMenu : Window
    {
        private bool bolInSettings = false;
        SoundPlayer mainTheme = new SoundPlayer(Properties.Resources.main_theme_radio_edit);
        /// <summary>
        /// primary constructor for the main menu form
        /// </summary>
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mainTheme.Stop();
            mainTheme.Dispose();
            formProvider.mainMenuForm.Hide();
            formProvider.quizForm.ShowDialog();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            bolInSettings = true;
            formProvider.mainMenuForm.Hide();
            formProvider.settingsForm.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainTheme.PlayLooping();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible == true && bolInSettings)
            {
                bolInSettings = false;
                formProvider.settingsForm.Close();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                mainTheme.Stop();
                mainTheme.Dispose();
                formProvider.mainMenuForm.Hide();
                formProvider.quizForm.ShowDialog();
            }
            else if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
