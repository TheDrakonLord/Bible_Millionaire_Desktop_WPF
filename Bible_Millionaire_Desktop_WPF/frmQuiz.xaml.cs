/* Title: frmQuiz.xaml.cs
 * Author: Neal Jamieson
 * Version: 1.0.0.0
 * 
 * Description:
 *     the main form that handles the majority of quiz functionality
 *     
 * Dependencies:
 *     system
 *     system.windows
 *     system.windows.input
 *     system.windows.media
 *     system.windows.media.imaging
 *     system.media
 *     system.componentModel
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
using System.Media;
using System.ComponentModel;

namespace Bible_Millionaire_Desktop_WPF
{
    /// <summary>
    /// Interaction logic for frmQuiz.xaml
    /// </summary>
    public partial class frmQuiz : Window
    {
        private const int _cintStartingX = 896;
        private const int _cintStartingY = 488;
        private Size _oldSize = new Size(_cintStartingX, _cintStartingY);

        // variable declarations
        /// <summary>
        /// declaration for the class that controls various core data aspects of the system
        /// </summary>
        public static millionaireQuiz gameShow = new millionaireQuiz();
        private int _intTimer = millionaireQuiz.cintTimerStartRound1;
        /// <summary>
        /// primary background audio player for the system
        /// </summary>
        public static SoundPlayer backgroundAudio = new SoundPlayer(Properties.Resources.explain_the_rules);

        System.Windows.Threading.DispatcherTimer tmrStart = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer tmrMain = new System.Windows.Threading.DispatcherTimer();

        BackgroundWorker bwQLoader = new BackgroundWorker();

        private readonly string[] _strRules =
        {
            "Welcome to: Who Wants to be a Millionaire: Bible Edition!",

            "This is a quiz game where you answer multiple " +
                "choice questions based on the bible.",

            "Every correct answer increases your score",

            "Incorrect answers will end your game " +
                "immediately and you will lose what you earned",

            "$1000 and $32,000 are safety nets. Answering " +
                "these questions correctly guarantees",

            "that you will keep that score at a minimum",

            "To help you out, you have 3 lifelines",

            "Look in the book gives you a bible verse " +
                "with a clue to the correct answer",

            "you'll have a limited amount of time " +
                "to look the verse up so be quick.",

            "phone a friend allows you to call " +
                "upon one person to answer the question",

            "Have a hint gives you a hint to the " +
                "correct answer",

            "ask the audience allows you to poll " +
                "the audience for what they think the correct answer is.",

            "50:50 removes two incorrect answers",

            "phone a friend and ask the audience are " +
                "only available when classic settings are on",

            "at any time you can walk away and " +
                "you will keep the score earned so far",

            "And that is all you need to know. " +
                "So lets play Who Want's to be a Millionaire!"
        };

        private int _intCurrentRule = 0;
        private int _intRulesDisplay = 0;
        private int _intLifelineDisplay = 0;
        private int _hotseatDuration = 0;
        private int _introDuration = 12;
        private int _finalAnswerDuration = 10;
        private int _resultAudioDuration = 5;

        // constructors
        /// <summary>
        /// primary constructor for the quiz form
        /// </summary>
        public frmQuiz()
        {
            InitializeComponent();
        }


        // defined methods
        /// <summary>
        /// determines the current point in the application lifecycle and loads the appropriate audio file into the player
        /// </summary>
        public static void selectAudio()
        {
            switch (gameShow.currentState)
            {
                case millionaireQuiz.quizStates.start:
                    switch (gameShow.startState)
                    {
                        case millionaireQuiz.startStates.rules:
                            backgroundAudio.SoundLocation = "Audio/explain_the_rules.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case millionaireQuiz.startStates.lifelines:
                            backgroundAudio.SoundLocation = "Audio/lifelines.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case millionaireQuiz.startStates.hotseat:
                            backgroundAudio.SoundLocation = "Audio/in_the_hot_seat.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        default:
                            break;
                    }
                    break;
                case millionaireQuiz.quizStates.intro:
                    switch (gameShow.currentQ)
                    {
                        case 0:
                            backgroundAudio.SoundLocation = "Audio/lets_play_1_5_9.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 4:
                            backgroundAudio.SoundLocation = "Audio/lets_play_1_5_9.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 5:
                            backgroundAudio.SoundLocation = "Audio/lets_play_6to10.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 6:
                            backgroundAudio.SoundLocation = "Audio/lets_play_7to11.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 7:
                            backgroundAudio.SoundLocation = "Audio/lets_play_32k.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 8:
                            backgroundAudio.SoundLocation = "Audio/lets_play_1_5_9.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 9:
                            backgroundAudio.SoundLocation = "Audio/lets_play_6to10.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 10:
                            backgroundAudio.SoundLocation = "Audio/lets_play_7to11.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 11:
                            backgroundAudio.SoundLocation = "Audio/lets_play_1mil.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        default:
                            break;
                    }
                    break;
                case millionaireQuiz.quizStates.mainPhase:
                    switch (gameShow.currentQ)
                    {
                        case 0:
                            backgroundAudio.SoundLocation = "Audio/questions_1to4.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 1:
                            backgroundAudio.SoundLocation = "Audio/questions_1to4.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 2:
                            backgroundAudio.SoundLocation = "Audio/questions_1to4.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 3:
                            backgroundAudio.SoundLocation = "Audio/questions_1to4.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 4:
                            backgroundAudio.SoundLocation = "Audio/question_4k.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 5:
                            backgroundAudio.SoundLocation = "Audio/question_8k.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 6:
                            backgroundAudio.SoundLocation = "Audio/question_16k.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 7:
                            backgroundAudio.SoundLocation = "Audio/question_32k.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 8:
                            backgroundAudio.SoundLocation = "Audio/question_125k.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 9:
                            backgroundAudio.SoundLocation = "Audio/question_250k.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 10:
                            backgroundAudio.SoundLocation = "Audio/question_500k.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 11:
                            backgroundAudio.SoundLocation = "Audio/question_1mil.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        default:
                            break;
                    }
                    break;
                case millionaireQuiz.quizStates.lifeline:
                    switch (gameShow.selectedLifeline)
                    {
                        case millionaireQuiz.lifelines.lookInTheBook:
                            backgroundAudio.SoundLocation = "Audio/phone_a_friend.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case millionaireQuiz.lifelines.haveAHint:
                            backgroundAudio.SoundLocation = "Audio/ask_the_audience.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case millionaireQuiz.lifelines.doubleYourChances:
                            backgroundAudio.SoundLocation = "Audio/lifeline5050.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case millionaireQuiz.lifelines.askTheAudience:
                            backgroundAudio.SoundLocation = "Audio/ask_the_audience.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case millionaireQuiz.lifelines.phoneAFriend:
                            backgroundAudio.SoundLocation = "Audio/phone_a_friend.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        default:
                            break;
                    }
                    break;
                case millionaireQuiz.quizStates.selected:
                    switch (gameShow.currentQ)
                    {
                        case 0:
                            backgroundAudio.SoundLocation = "Audio/final_answer_4to8.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 1:
                            backgroundAudio.SoundLocation = "Audio/final_answer_4to8.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 2:
                            backgroundAudio.SoundLocation = "Audio/final_answer_4to8.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 3:
                            backgroundAudio.SoundLocation = "Audio/final_answer_4to8.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 4:
                            backgroundAudio.SoundLocation = "Audio/final_answer_5to9.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 5:
                            backgroundAudio.SoundLocation = "Audio/final_answer_6to10.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 6:
                            backgroundAudio.SoundLocation = "Audio/final_answer_7to11.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 7:
                            backgroundAudio.SoundLocation = "Audio/final_answer_32k.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 8:
                            backgroundAudio.SoundLocation = "Audio/final_answer_5to9.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 9:
                            backgroundAudio.SoundLocation = "Audio/final_answer_6to10.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 10:
                            backgroundAudio.SoundLocation = "Audio/final_answer_7to11.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        case 11:
                            backgroundAudio.SoundLocation = "Audio/final_answer_1mil.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        default:
                            break;
                    }
                    break;
                case millionaireQuiz.quizStates.result:
                    switch (gameShow.result)
                    {
                        case millionaireQuiz.resultStates.win:
                            switch (gameShow.currentQ)
                            {
                                case 0:
                                    backgroundAudio.SoundLocation = "Audio/win_1to3.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 1:
                                    backgroundAudio.SoundLocation = "Audio/win_1to3.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 2:
                                    backgroundAudio.SoundLocation = "Audio/win_1to3.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 3:
                                    backgroundAudio.SoundLocation = "Audio/win_4to8.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 4:
                                    backgroundAudio.SoundLocation = "Audio/win_4k.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 5:
                                    backgroundAudio.SoundLocation = "Audio/win_8k.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 6:
                                    backgroundAudio.SoundLocation = "Audio/win_16k.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 7:
                                    backgroundAudio.SoundLocation = "Audio/win_4to8.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 8:
                                    backgroundAudio.SoundLocation = "Audio/win_125k.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 9:
                                    backgroundAudio.SoundLocation = "Audio/win_250k.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 10:
                                    backgroundAudio.SoundLocation = "Audio/win_500k.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 11:
                                    backgroundAudio.SoundLocation = "Audio/win_1mil.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case millionaireQuiz.resultStates.lose:
                            switch (gameShow.currentQ)
                            {
                                case 0:
                                    backgroundAudio.SoundLocation = "Audio/lose_1to4.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 1:
                                    backgroundAudio.SoundLocation = "Audio/lose_1to4.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 2:
                                    backgroundAudio.SoundLocation = "Audio/lose_1to4.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 3:
                                    backgroundAudio.SoundLocation = "Audio/lose_1to4.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 4:
                                    backgroundAudio.SoundLocation = "Audio/lose_5to9.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 5:
                                    backgroundAudio.SoundLocation = "Audio/lose_6to10.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 6:
                                    backgroundAudio.SoundLocation = "Audio/lose_7to11.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 7:
                                    backgroundAudio.SoundLocation = "Audio/lose_32k.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 8:
                                    backgroundAudio.SoundLocation = "Audio/lose_5to9.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 9:
                                    backgroundAudio.SoundLocation = "Audio/lose_6to10.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 10:
                                    backgroundAudio.SoundLocation = "Audio/lose_7to11.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                case 11:
                                    backgroundAudio.SoundLocation = "Audio/lose_1mil.wav";
                                    backgroundAudio.LoadAsync();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case millionaireQuiz.resultStates.outOfTime:
                            backgroundAudio.SoundLocation = "Audio/out_of_time.wav";
                            backgroundAudio.LoadAsync();
                            break;
                        default:
                            break;
                    }
                    break;
                case millionaireQuiz.quizStates.transition:

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// intializes the application lifecycle
        /// </summary>
        public void startPhase()
        {
            gameShow.currentState = millionaireQuiz.quizStates.start;
            if (Properties.Settings.Default.sbolSkipTutorial)
            {
                gameShow.startState = millionaireQuiz.startStates.lifelines;
            }
            else
            {
                gameShow.startState = millionaireQuiz.startStates.rules;
            }
            selectAudio();
            tmrStart.IsEnabled = true;
            tmrStart.Start();
            lblQuestion.Visibility = Visibility.Visible;
            backgroundAudio.Play();
        }

        /// <summary>
        /// loads the current question and prepares it for display
        /// </summary>
        public void setQuestion()
        {
            gameShow.advance();     
            lblQuestion.Visibility = Visibility.Visible;
            lblQuestion.Text = gameShow.question;
            lblAnswerA.Visibility = Visibility.Visible;
            lblAnswerA.Text = gameShow.AnswerA;
            lblAnswerB.Visibility = Visibility.Visible;
            lblAnswerB.Text = gameShow.AnswerB;
            lblAnswerC.Visibility = Visibility.Visible;
            lblAnswerC.Text = gameShow.AnswerC;
            lblAnswerD.Visibility = Visibility.Visible;
            lblAnswerD.Text = gameShow.AnswerD;
            if (Properties.Settings.Default.sbolTimerEnabled)
            {
                lblClock.Visibility = Visibility.Visible;
            }
            else
            {
                lblClock.Visibility = Visibility.Hidden;
            }
            
            _intTimer = gameShow.currentTimer();
            lblQValue.Visibility = Visibility.Visible;
            lblQValue.Content = $"{gameShow.qValues} QUESTION";
            bwQLoader.RunWorkerAsync();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tmrStart.Tick += new EventHandler(tmrStart_Tick);
            tmrStart.Interval = new TimeSpan(0, 0, 1);
            tmrMain.Tick += new EventHandler(tmrMain_Tick);
            tmrMain.Interval = new TimeSpan(0, 0, 1);

            bwQLoader.DoWork += bwQLoader_DoWork;

            lblQValue.Content = gameShow.qValues + " QUESTION";
            lblQuestion.Text = _strRules[0];
            lblQuestion.Visibility = Visibility.Hidden;
            lblAnswerA.Visibility = Visibility.Hidden;
            lblAnswerB.Visibility = Visibility.Hidden;
            lblAnswerC.Visibility = Visibility.Hidden;
            lblAnswerD.Visibility = Visibility.Hidden;
            pbxLifelineA.Visibility = Visibility.Hidden;
            pbxLifelineB.Visibility = Visibility.Hidden;
            pbxLifelineC.Visibility = Visibility.Hidden;
            lblClock.Visibility = Visibility.Hidden;
            pbxWalkAway.Visibility = Visibility.Hidden;
            lblQValue.Visibility = Visibility.Hidden;
            if (Properties.Settings.Default.sbolLifelineAClassic)
            {
                pbxLifelineA.Source = new BitmapImage(
    new Uri("pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/lifeline_askaudience.png"));
            }
            if (Properties.Settings.Default.sbolLifelineBClassic)
            {
                pbxLifelineB.Source = new BitmapImage(
    new Uri("pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/lifeline_phone.png"));
            }
            startPhase();

        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            switch (gameShow.currentState)
            {
                case millionaireQuiz.quizStates.intro:
                    if (_introDuration > 0 && gameShow.currentQ != 1 && gameShow.currentQ != 2 && gameShow.currentQ != 3)
                    {
                        _introDuration--;
                    }
                    else
                    {
                        _introDuration = 12;
                        gameShow.currentState = millionaireQuiz.quizStates.mainPhase;
                        backgroundAudio.Stop();
                        selectAudio();
                        backgroundAudio.PlayLooping();
                        setQuestion();
                    }
                    break;
                case millionaireQuiz.quizStates.mainPhase:
                    if (_intTimer > 0)
                    {
                        _intTimer--;
                        lblClock.Content = _intTimer.ToString("00");
                    }
                    else if (Properties.Settings.Default.sbolTimerEnabled)
                    {
                        gameShow.result = millionaireQuiz.resultStates.outOfTime;
                        gameShow.currentState = millionaireQuiz.quizStates.result;
                        backgroundAudio.Stop();
                        selectAudio();
                        backgroundAudio.Play();
                        _intTimer = gameShow.currentTimer();
                    }
                    break;
                case millionaireQuiz.quizStates.lifeline:
                    break;
                case millionaireQuiz.quizStates.selected:
                    if (_finalAnswerDuration > 0)
                    {
                        _finalAnswerDuration--;
                    }
                    else
                    {
                        gameShow.currentState = millionaireQuiz.quizStates.result;
                        backgroundAudio.Stop();
                        selectAudio();
                        backgroundAudio.Play();
                        _finalAnswerDuration = gameShow.finalAudioDuration;
                    }
                    break;
                case millionaireQuiz.quizStates.result:
                    switch (gameShow.selectedAnswer)
                    {
                        case millionaireQuiz.answerOptions.A:
                            switch (gameShow.correctAnswer)
                            {
                                case millionaireQuiz.answerOptions.A:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_A_correct.png")));
                                    break;
                                case millionaireQuiz.answerOptions.B:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_A_Bcorrect.png")));
                                    break;
                                case millionaireQuiz.answerOptions.C:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_A_Ccorrect.png")));
                                    break;
                                case millionaireQuiz.answerOptions.D:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_A_Dcorrect.png")));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case millionaireQuiz.answerOptions.B:
                            switch (gameShow.correctAnswer)
                            {
                                case millionaireQuiz.answerOptions.A:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_B_Acorrect.png")));
                                    break;
                                case millionaireQuiz.answerOptions.B:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_B_correct.png")));
                                    break;
                                case millionaireQuiz.answerOptions.C:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_B_Ccorrect.png")));
                                    break;
                                case millionaireQuiz.answerOptions.D:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_B_Dcorrect.png")));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case millionaireQuiz.answerOptions.C:
                            switch (gameShow.correctAnswer)
                            {
                                case millionaireQuiz.answerOptions.A:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_C_Acorrect.png")));
                                    break;
                                case millionaireQuiz.answerOptions.B:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_C_Bcorrect.png")));
                                    break;
                                case millionaireQuiz.answerOptions.C:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_C_correct.png")));
                                    break;
                                case millionaireQuiz.answerOptions.D:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_C_Dcorrect.png")));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case millionaireQuiz.answerOptions.D:
                            switch (gameShow.correctAnswer)
                            {
                                case millionaireQuiz.answerOptions.A:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_D_Acorrect.png")));
                                    break;
                                case millionaireQuiz.answerOptions.B:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_D_Bcorrect.png")));
                                    break;
                                case millionaireQuiz.answerOptions.C:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_D_Ccorrect.png")));
                                    break;
                                case millionaireQuiz.answerOptions.D:
                                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_D_correct.png")));
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    if (_resultAudioDuration > 0)
                    {
                        _resultAudioDuration--;
                    }
                    else
                    {
                        _resultAudioDuration = 10;
                        switch (gameShow.result)
                        {
                            case millionaireQuiz.resultStates.win:
                                if (gameShow.currentQ < millionaireQuiz.cintMaxQuestion)
                                {
                                    gameShow.currentState = millionaireQuiz.quizStates.transition;
                                }
                                else
                                {
                                    backgroundAudio.Stop();
                                    backgroundAudio.Dispose();
                                    tmrMain.Stop();
                                    tmrMain.IsEnabled = false;
                                    formProvider.quizForm.Hide();
                                    formProvider.endForm.Show();
                                }
                                break;
                            case millionaireQuiz.resultStates.lose:
                                backgroundAudio.Stop();
                                backgroundAudio.Dispose();
                                tmrMain.Stop();
                                tmrMain.IsEnabled = false;
                                formProvider.quizForm.Hide();
                                formProvider.endForm.Show();
                                break;
                            case millionaireQuiz.resultStates.outOfTime:
                                backgroundAudio.Stop();
                                backgroundAudio.Dispose();
                                tmrMain.Stop();
                                tmrMain.IsEnabled = false;
                                formProvider.quizForm.Hide();
                                formProvider.endForm.Show();
                                break;
                            default:
                                break;
                        }
                    }
                    
                    break;
                case millionaireQuiz.quizStates.transition:
                    gameShow.currentQ++;
                    gameShow.currentState = millionaireQuiz.quizStates.intro;
                    
                    lblQuestion.Visibility = Visibility.Hidden;
                    lblQValue.Visibility = Visibility.Hidden;
                    lblAnswerA.Visibility = Visibility.Hidden;
                    lblAnswerB.Visibility = Visibility.Hidden;
                    lblAnswerC.Visibility = Visibility.Hidden;
                    lblAnswerD.Visibility = Visibility.Hidden;
                    lblClock.Visibility = Visibility.Hidden;
                    backgroundAudio.Stop();
                    if (gameShow.currentQ >= 4)
                    {
                        selectAudio();
                        backgroundAudio.Play();
                    }
                    this.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_base.png")));
                    break;
                default:
                    break;
            }
        }

        private void tmrStart_Tick(object sender, EventArgs e)
        {
            switch (gameShow.startState)
            {
                case millionaireQuiz.startStates.rules:
                    if (_intRulesDisplay >= 4)
                    {
                        try
                        {
                            lblQuestion.Text = _strRules[_intCurrentRule];
                        }
                        catch (IndexOutOfRangeException)
                        {
                        }
                        _intCurrentRule++;
                        _intRulesDisplay = 0;
                    }
                    else
                    {
                        _intRulesDisplay++;
                    }
                    if (_intCurrentRule >= 18)
                    {
                        lblQuestion.Visibility = Visibility.Hidden;
                        gameShow.startState = millionaireQuiz.startStates.lifelines;
                        backgroundAudio.Stop();
                        selectAudio();
                        backgroundAudio.Play();
                    }
                    break;
                case millionaireQuiz.startStates.lifelines:
                    switch (_intLifelineDisplay)
                    {
                        case 0:
                            pbxLifelineA.Visibility = Visibility.Visible;
                            _intLifelineDisplay++;
                            break;
                        case 1:
                            pbxLifelineB.Visibility = Visibility.Visible;
                            _intLifelineDisplay++;
                            break;
                        case 2:
                            pbxLifelineC.Visibility = Visibility.Visible;
                            _intLifelineDisplay++;
                            break;
                        default:
                            pbxWalkAway.Visibility = Visibility.Visible;
                            gameShow.startState = millionaireQuiz.startStates.hotseat;
                            backgroundAudio.Stop();
                            selectAudio();
                            backgroundAudio.Play();
                            break;
                    }
                    break;
                case millionaireQuiz.startStates.hotseat:
                    if (_hotseatDuration < 10)
                    {
                        _hotseatDuration++;
                    }
                    else
                    {
                        gameShow.startState = millionaireQuiz.startStates.rules;
                        gameShow.currentState = millionaireQuiz.quizStates.intro;
                        backgroundAudio.Stop();
                        selectAudio();
                        backgroundAudio.Play();
                        tmrMain.IsEnabled = true;
                        tmrStart.Stop();
                        tmrMain.Start();
                        tmrStart.IsEnabled = false;
                    }
                    break;
                default:
                    break;
            }
        }

        private void lifelineA()
        {
            if (gameShow.LifelineAAvailable)
            {
                gameShow.currentState = millionaireQuiz.quizStates.lifeline;
                if (Properties.Settings.Default.sbolLifelineAClassic)
                {
                    pbxLifelineA.Source = new BitmapImage(
                        new Uri("pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/lifeline_ask_audience.png"));

                    gameShow.selectedLifeline = millionaireQuiz.lifelines.askTheAudience;
                    backgroundAudio.Stop();
                    selectAudio();
                    backgroundAudio.Play();
                }
                else
                {
                    pbxLifelineA.Source = new BitmapImage(
                        new Uri("pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/lifeline_look_in_the_book_used.png"));

                    gameShow.selectedLifeline = millionaireQuiz.lifelines.lookInTheBook;
                    backgroundAudio.Stop();
                    selectAudio();
                    backgroundAudio.Play();
                }
                formProvider.quizForm.Hide();
                formProvider.lifelineForm.Show();
            }
        }

        private void pbxLifelineA_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            lifelineA();
        }

        private void pbxLifelineA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D1)
            {
                lifelineA();
            }
        }

        private void lifelineB()
        {
            if (gameShow.LifelineBAvailable)
            {
                gameShow.currentState = millionaireQuiz.quizStates.lifeline;
                if (Properties.Settings.Default.sbolLifelineBClassic)
                {
                    pbxLifelineB.Source = new BitmapImage(
                        new Uri("pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/lifeline_phone_used.png"));
                    gameShow.selectedLifeline = millionaireQuiz.lifelines.phoneAFriend;
                    backgroundAudio.Stop();
                    selectAudio();
                    backgroundAudio.Play();
                }
                else
                {
                    pbxLifelineB.Source = new BitmapImage(
                        new Uri("pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/lifeline_have_a_hint_used.png"));

                    gameShow.selectedLifeline = millionaireQuiz.lifelines.haveAHint;
                    backgroundAudio.Stop();
                    selectAudio();
                    backgroundAudio.Play();
                }
                formProvider.quizForm.Hide();
                formProvider.lifelineForm.Show();
            }
        }

        private void pbxLifelineB_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            lifelineB();
        }

        private void pbxLifelineB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D2)
            {
                lifelineB();
            }
        }

        private void lifelineC()
        {
            if (gameShow.LifelineCAvailable)
            {
                gameShow.selectedLifeline = millionaireQuiz.lifelines.doubleYourChances;
                gameShow.currentState = millionaireQuiz.quizStates.lifeline;
                backgroundAudio.Stop();
                selectAudio();
                backgroundAudio.Play();
                switch (gameShow.lifelineDouble1)
                {
                    case millionaireQuiz.answerOptions.A:
                        lblAnswerA.Visibility = Visibility.Hidden;
                        break;
                    case millionaireQuiz.answerOptions.B:
                        lblAnswerB.Visibility = Visibility.Hidden;
                        break;
                    case millionaireQuiz.answerOptions.C:
                        lblAnswerC.Visibility = Visibility.Hidden;
                        break;
                    case millionaireQuiz.answerOptions.D:
                        lblAnswerD.Visibility = Visibility.Hidden;
                        break;
                    default:
                        break;
                }

                switch (gameShow.lifelineDouble2)
                {
                    case millionaireQuiz.answerOptions.A:
                        lblAnswerA.Visibility = Visibility.Hidden;
                        break;
                    case millionaireQuiz.answerOptions.B:
                        lblAnswerB.Visibility = Visibility.Hidden;
                        break;
                    case millionaireQuiz.answerOptions.C:
                        lblAnswerC.Visibility = Visibility.Hidden;
                        break;
                    case millionaireQuiz.answerOptions.D:
                        lblAnswerD.Visibility = Visibility.Hidden;
                        break;
                    default:
                        break;
                }
                pbxLifelineC.Source = new BitmapImage(
    new Uri("pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/lifeline_5050_used.png"));

                gameShow.currentState = millionaireQuiz.quizStates.mainPhase;
                backgroundAudio.Stop();
                selectAudio();
                backgroundAudio.Play();
            }
        }

        private void pbxLifelineC_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            lifelineC();
        }

        private void pbxLifelineC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D3)
            {
                lifelineC();
            }
        }

        private void walkAway()
        {
            gameShow.result = millionaireQuiz.resultStates.walkAway;
            backgroundAudio.Stop();
            backgroundAudio.Dispose();
            tmrMain.Stop();
            tmrMain.IsEnabled = false;
            formProvider.quizForm.Hide();
            formProvider.endForm.Show();
        }

        private void pbxWalkAway_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            walkAway();
        }

        private void pbxWalkAway_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D4)
            {
                walkAway();
            }
        }

        private void answerA()
        {
            if (gameShow.currentState == millionaireQuiz.quizStates.mainPhase)
            {
                gameShow.currentState = millionaireQuiz.quizStates.selected;
                gameShow.selectedAnswer = millionaireQuiz.answerOptions.A;
                gameShow.checkAnswer();
                this.Background = new ImageBrush(new BitmapImage(
                    new Uri("pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_A_base.png")));

                backgroundAudio.Stop();
                selectAudio();
                backgroundAudio.Play();
                _finalAnswerDuration = gameShow.finalAudioDuration;
            }
        }

        private void lblAnswerA_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            answerA();
        }

        private void lblAnswerA_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                answerA();
            }
        }

        private void answerB()
        {
            if (gameShow.currentState == millionaireQuiz.quizStates.mainPhase)
            {
                gameShow.currentState = millionaireQuiz.quizStates.selected;
                gameShow.selectedAnswer = millionaireQuiz.answerOptions.B;
                gameShow.checkAnswer();
                this.Background = new ImageBrush(new BitmapImage(
    new Uri("pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_B_base.png")));

                backgroundAudio.Stop();
                selectAudio();
                backgroundAudio.Play();
                _finalAnswerDuration = gameShow.finalAudioDuration;
            }
        }

        private void lblAnswerB_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            answerB();
        }

        private void lblAnswerB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.B)
            {
                answerB();
            }
        }

        private void answerC()
        {
            if (gameShow.currentState == millionaireQuiz.quizStates.mainPhase)
            {
                gameShow.currentState = millionaireQuiz.quizStates.selected;
                gameShow.selectedAnswer = millionaireQuiz.answerOptions.C;
                gameShow.checkAnswer();
                this.Background = new ImageBrush(new BitmapImage(
    new Uri("pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_C_base.png")));

                backgroundAudio.Stop();
                selectAudio();
                backgroundAudio.Play();
                _finalAnswerDuration = gameShow.finalAudioDuration;
            }
        }

        private void lblAnswerC_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            answerC();
        }

        private void lblAnswerC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C)
            {
                answerC();
            }
        }

        private void answerD()
        {
            if (gameShow.currentState == millionaireQuiz.quizStates.mainPhase)
            {
                gameShow.currentState = millionaireQuiz.quizStates.selected;
                gameShow.selectedAnswer = millionaireQuiz.answerOptions.D;
                gameShow.checkAnswer();
                this.Background = new ImageBrush(new BitmapImage(
    new Uri("pack://application:,,,/Bible_Millionaire_Desktop_WPF;component/Images/quiz_select_D_base.png")));

                backgroundAudio.Stop();
                selectAudio();
                backgroundAudio.Play();
                _finalAnswerDuration = gameShow.finalAudioDuration;
            }
        }

        private void lblAnswerD_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            answerD();
        }

        private void lblAnswerD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D)
            {
                answerD();
            }
        }

        void bwQLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            gameShow.loadQuestion(gameShow.currentQ + 1);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Return:
                    break;
                case Key.D1:
                    lifelineA();
                    break;
                case Key.D2:
                    lifelineB();
                    break;
                case Key.D3:
                    lifelineC();
                    break;
                case Key.D4:
                    walkAway();
                    break;
                case Key.A:
                    answerA();
                    break;
                case Key.B:
                    answerB();
                    break;
                case Key.C:
                    answerC();
                    break;
                case Key.D:
                    answerD();
                    break;
                case Key.W:
                    walkAway();
                    break;
                default:
                    break;
            }
        }
    }
}
