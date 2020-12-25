/* Title: frmLifelin.xaml.cs
 * Author: Neal Jamieson
 * Version: 1.0.0.0
 * 
 * Description:
 *     this form handles the display and execution of the various lifelines used in the game
 *     
 * Dependencies:
 *     system
 *     system.windows
 *     system.windows.input
 *     system.windows.media
 *     system.windows.media.imaging
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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Bible_Millionaire_Desktop_WPF
{
    /// <summary>
    /// Interaction logic for frmLifeline.xaml
    /// </summary>
    public partial class frmLifeline : Window
    {
        private int _timer;
        private int _audienceInstructionCount = 0;
        private int _instructionSwitchCount = 3;

        System.Windows.Threading.DispatcherTimer tmrLifeline = new System.Windows.Threading.DispatcherTimer();

        private readonly string[] _audiencePrompt =
        {
            "Hey audience, it looks like we need your help. Think about the question and follow these instructions:",
            "Raise your hand if you think the answer is A",
            "Put your hand down",
            "Raise your hand if you think the answer is B",
            "Put your hand down",
            "Raise your hand if you think the answer is C",
            "Put your hand down",
            "Raise your hand if you think the answer is D",
            "Put your hand down"
        };

        /// <summary>
        /// primary constructor for the lifeline form
        /// </summary>
        public frmLifeline()
        {
            InitializeComponent();
        }

        private void lblLifeline_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (frmQuiz.gameShow.selectedLifeline)
            {
                case millionaireQuiz.lifelines.lookInTheBook:
                    lblLifeline.Text = frmQuiz.gameShow.LifelineBook;
                    tmrLifeline.IsEnabled = true;
                    frmQuiz.backgroundAudio.Play();
                    tmrLifeline.Start();
                    break;
                case millionaireQuiz.lifelines.haveAHint:
                    break;
                case millionaireQuiz.lifelines.doubleYourChances:
                    break;
                case millionaireQuiz.lifelines.askTheAudience:
                    break;
                case millionaireQuiz.lifelines.phoneAFriend:
                    lblLifeline.Text = frmQuiz.gameShow.question;
                    tmrLifeline.IsEnabled = true;
                    tmrLifeline.Start();
                    break;
                default:
                    break;
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                _timer = frmQuiz.gameShow.currentLifelineTimer();
                switch (frmQuiz.gameShow.selectedLifeline)
                {
                    case millionaireQuiz.lifelines.lookInTheBook:
                        frmQuiz.backgroundAudio.Stop();
                        lblClock.Content = _timer.ToString("00");
                        this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/lifeline_book_screen.png")));
                        lblLifeline.Text = "click here when you have your bible ready";
                        break;
                    case millionaireQuiz.lifelines.haveAHint:
                        lblClock.Content = _timer.ToString("00");
                        this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/lifeline_hint_screen.png")));
                        lblLifeline.Text = frmQuiz.gameShow.LifelineHint;
                        tmrLifeline.IsEnabled = true;
                        tmrLifeline.Start();
                        break;
                    case millionaireQuiz.lifelines.doubleYourChances:
                        break;
                    case millionaireQuiz.lifelines.askTheAudience:
                        lblClock.Content = _timer.ToString("00");
                        this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/lifeline_audience_screen.png")));
                        lblLifeline.Text = _audiencePrompt[0];
                        tmrLifeline.IsEnabled = true;
                        tmrLifeline.Start();
                        break;
                    case millionaireQuiz.lifelines.phoneAFriend:
                        lblClock.Content = _timer.ToString("00");
                        this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/lifeline_phone_screen.png")));
                        lblLifeline.Text = "Select one person to tell you what they believe the correct answer is. Click here once you have chosen.";
                        break;
                    default:
                        break;
                }

            }
        }

        private void tmrLifeline_Tick(object sender, EventArgs e)
        {
            switch (frmQuiz.gameShow.selectedLifeline)
            {
                case millionaireQuiz.lifelines.lookInTheBook:
                    if (_timer > 0)
                    {
                        _timer--;
                        lblClock.Content = _timer.ToString("00");
                    }
                    else
                    {
                        tmrLifeline.IsEnabled = false;
                        tmrLifeline.Stop();
                        frmQuiz.gameShow.currentState = millionaireQuiz.quizStates.mainPhase;
                        frmQuiz.backgroundAudio.Stop();
                        frmQuiz.selectAudio();
                        frmQuiz.backgroundAudio.PlayLooping();
                        formProvider.lifelineForm.Hide();
                        formProvider.quizForm.Show();
                    }
                    break;
                case millionaireQuiz.lifelines.haveAHint:
                    if (_timer > 0)
                    {
                        _timer--;
                        lblClock.Content = _timer.ToString("00");
                    }
                    else
                    {
                        tmrLifeline.IsEnabled = false;
                        tmrLifeline.Stop();
                        frmQuiz.gameShow.currentState = millionaireQuiz.quizStates.mainPhase;
                        frmQuiz.backgroundAudio.Stop();
                        frmQuiz.selectAudio();
                        frmQuiz.backgroundAudio.PlayLooping();
                        formProvider.lifelineForm.Hide();
                        formProvider.quizForm.Show();
                    }
                    break;
                case millionaireQuiz.lifelines.doubleYourChances:
                    break;
                case millionaireQuiz.lifelines.askTheAudience:
                    if (_audienceInstructionCount < 9)
                    {
                        if (_instructionSwitchCount > 0)
                        {
                            _instructionSwitchCount--;
                        }
                        else
                        {
                            _instructionSwitchCount = 3;
                            _audienceInstructionCount++;
                            try
                            {
                                lblLifeline.Text = _audiencePrompt[_audienceInstructionCount];
                            }
                            catch (IndexOutOfRangeException)
                            {

                            }

                        }
                        if (_timer >= 0)
                        {
                            _timer--;
                            lblClock.Content = _timer.ToString("00");
                        }

                    }
                    else
                    {
                        _audienceInstructionCount = 0;
                        tmrLifeline.IsEnabled = false;
                        tmrLifeline.Stop();
                        frmQuiz.gameShow.currentState = millionaireQuiz.quizStates.mainPhase;
                        frmQuiz.backgroundAudio.Stop();
                        frmQuiz.selectAudio();
                        frmQuiz.backgroundAudio.PlayLooping();
                        formProvider.lifelineForm.Hide();
                        formProvider.quizForm.Show();
                    }
                    break;
                case millionaireQuiz.lifelines.phoneAFriend:
                    if (_timer > 0)
                    {
                        _timer--;
                        lblClock.Content = _timer.ToString("00");
                    }
                    else
                    {
                        tmrLifeline.IsEnabled = false;
                        tmrLifeline.Stop();
                        frmQuiz.gameShow.currentState = millionaireQuiz.quizStates.mainPhase;
                        frmQuiz.backgroundAudio.Stop();
                        frmQuiz.selectAudio();
                        frmQuiz.backgroundAudio.PlayLooping();
                        formProvider.lifelineForm.Hide();
                        formProvider.quizForm.Show();
                    }
                    break;
                default:
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tmrLifeline.Tick += new EventHandler(tmrLifeline_Tick);
            tmrLifeline.Interval = new TimeSpan(0, 0, 1);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                switch (frmQuiz.gameShow.selectedLifeline)
                {
                    case millionaireQuiz.lifelines.lookInTheBook:
                        lblLifeline.Text = frmQuiz.gameShow.LifelineBook;
                        tmrLifeline.IsEnabled = true;
                        frmQuiz.backgroundAudio.Play();
                        tmrLifeline.Start();
                        break;
                    case millionaireQuiz.lifelines.haveAHint:
                        break;
                    case millionaireQuiz.lifelines.doubleYourChances:
                        break;
                    case millionaireQuiz.lifelines.askTheAudience:
                        break;
                    case millionaireQuiz.lifelines.phoneAFriend:
                        lblLifeline.Text = frmQuiz.gameShow.question;
                        tmrLifeline.IsEnabled = true;
                        tmrLifeline.Start();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
