/* Title: frmEnd.xaml.cs
 * Author: Neal Jamieson
 * Version: 1.0.0.0
 * 
 * Descriptions: 
 *     This class forms the code-behind for the window that displays the final results achieved by the player.
 *     
 * Dependencies:
 *     System.Windows
 *     System.Media
 *     
 * References:
 *     Based upon the hit game show: Who wants to be a millionaire
 *         [1] M. Cohen et al., “Who Wants to Be a Millionaire,” Buena Vista Television, Celador Productions, Disney-ABC Domestic Television, Valleycrest Productions.‌
 *     Based upon the book of the similar name that has since been lost and no longer shows up in online searches or databases
 *         [1] Unkown, Who Wants to be a Millionaire: Bible Edition. Unkown: Unkown, p. Whole.‌
 */

using System.Windows;
using System.Media;

namespace Bible_Millionaire_Desktop_WPF
{
    /// <summary>
    /// Interaction logic for frmEnd.xaml
    /// </summary>
    public partial class frmEnd : Window
    {
        private SoundPlayer finalAudio = new SoundPlayer(Properties.Resources.closing_theme);
        
        /// <summary>
        /// primary constructor for the ending screen form
        /// </summary>
        public frmEnd()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (frmQuiz.gameShow.result == millionaireQuiz.resultStates.win)
            {
                lblResult.Content = "$1 MILLION";
            }
            else
            {
                lblResult.Content = frmQuiz.gameShow.winnings;
            }
            finalAudio.Play();
        }
    }
}
