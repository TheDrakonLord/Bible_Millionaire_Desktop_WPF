/* Title: formProvider.cs
 * Author: Neal Jamieson
 * Version: 1.0.0.0
 * 
 * Description:
 *     This class allows for the system to maintain only a single instance of each form. 
 *     Using a single instance of each form reduces resource sonsumption and allows consistency throught the system.
 *     
 * Dependencies:
 *     This class has no dependencies 
 *     
 * References:
 *     Based upon the hit game show: Who wants to be a millionaire
 *         [1] M. Cohen et al., “Who Wants to Be a Millionaire,” Buena Vista Television, Celador Productions, Disney-ABC Domestic Television, Valleycrest Productions.‌
 *     Based upon the book of the similar name that has since been lost and no longer shows up in online searches or databases
 *         [1] Unkown, Who Wants to be a Millionaire: Bible Edition. Unkown: Unkown, p. Whole.‌
 */

namespace Bible_Millionaire_Desktop_WPF
{
    class formProvider
    {
        private static frmPresent _frmPresent;
        /// <summary>
        /// Controls the Introductory form
        /// </summary>
        public static frmPresent presentForm
        {
            get
            {
                if (_frmPresent == null)
                {
                    _frmPresent = new frmPresent();
                }
                return _frmPresent;
            }
        }

        private static frmMainMenu _frmMainMenu;
        /// <summary>
        /// Controls the main menu form
        /// </summary>
        public static frmMainMenu mainMenuForm
        {
            get
            {
                if (_frmMainMenu == null)
                {
                    _frmMainMenu = new frmMainMenu();
                }
                return _frmMainMenu;
            }
        }

        private static frmSettings _frmSettings;
        /// <summary>
        /// controls the settings form
        /// </summary>
        public static frmSettings settingsForm
        {
            get
            {
                if (_frmSettings == null)
                {
                    _frmSettings = new frmSettings();
                }
                return _frmSettings;
            }
        }

        private static frmQuiz _frmQuiz;
        /// <summary>
        /// controls the quiz form
        /// </summary>
        public static frmQuiz quizForm
        {
            get
            {
                if (_frmQuiz == null)
                {
                    _frmQuiz = new frmQuiz();
                }
                return _frmQuiz;
            }
        }

        private static frmLifeline _frmLifeline;
        /// <summary>
        /// controls the lifeline form
        /// </summary>
        public static frmLifeline lifelineForm
        {
            get
            {
                if (_frmLifeline == null)
                {
                    _frmLifeline = new frmLifeline();
                }
                return _frmLifeline;
            }
        }

        private static frmLadder _frmLadder;
        /// <summary>
        /// controls the ladder form. Not currently in use
        /// </summary>
        public static frmLadder ladderForm
        {
            get
            {
                if (_frmLadder == null)
                {
                    _frmLadder = new frmLadder();
                }
                return _frmLadder;
            }
        }

        private static frmFastest _frmFastest;
        /// <summary>
        /// controls the fastest finger form. not currently in use.
        /// </summary>
        public static frmFastest fastestForm
        {
            get
            {
                if (_frmFastest == null)
                {
                    _frmFastest = new frmFastest();
                }
                return _frmFastest;
            }
        }

        private static frmEnd _frmEnd;
        /// <summary>
        /// controls the ending screen form
        /// </summary>
        public static frmEnd endForm
        {
            get
            {
                if (_frmEnd == null)
                {
                    _frmEnd = new frmEnd();
                }
                return _frmEnd;
            }
        }
    }
}
