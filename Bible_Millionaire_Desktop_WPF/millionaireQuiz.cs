/* Title: millionaireQuiz.cs
 * Author: Neal Jamieson
 * Version: 1.0.0.0
 * 
 * Description:
 *     data structure that stores and manages the core data for the system
 *     
 * Dependencies:
 *     system
 *     system.collections.generic
 *     system.linq
 *     system.xml.linq
 *     
 * References:
 *     Based upon the hit game show: Who wants to be a millionaire
 *         [1] M. Cohen et al., “Who Wants to Be a Millionaire,” Buena Vista Television, Celador Productions, Disney-ABC Domestic Television, Valleycrest Productions.‌
 *     Based upon the book of the similar name that has since been lost and no longer shows up in online searches or databases
 *         [1] Unkown, Who Wants to be a Millionaire: Bible Edition. Unkown: Unkown, p. Whole.‌
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Bible_Millionaire_Desktop_WPF
{
    /// <summary>
    /// primary data hanlder for the system
    /// </summary>
    public class millionaireQuiz
    {
        /// <summary>
        /// the minimum value for referencing quesion IDs
        /// </summary>
        public const int cintMinQuestion = 0;
        /// <summary>
        /// the maximum value for referencing question IDs
        /// </summary>
        public const int cintMaxQuestion = 11;
        /// <summary>
        /// the minimum value for referencing quiz IDs
        /// </summary>
        public const int cintMinQuiz = 0;
        /// <summary>
        /// the maximum value for referencing quiz IDs
        /// </summary>
        public const int cintMaxQuiz = 29;

        /// <summary>
        /// the duration of the start phase in round 1
        /// </summary>
        public const int cintTimerStartRound1 = 45;
        /// <summary>
        /// the duration of the start phase in round 2
        /// </summary>
        public const int cintTimerStartRound2 = 60;
        /// <summary>
        /// the duration of the start phase in round 3
        /// </summary>
        public const int cintTimerStartRound3 = 90;


        /// <summary>
        /// the duration of the ask the audience lifeline timer
        /// </summary>
        public const int cintTimerLifelineAudience = 31;
        /// <summary>
        /// the duration of the phone a friend lifeline timer
        /// </summary>
        public const int cintTimerLifelinePhoneAFriend = 40;
        /// <summary>
        /// the duration of the look in the book lifeline timer
        /// </summary>
        public const int cintTimerLifelineBook = 40;
        /// <summary>
        /// the duration of the have a hint lifeline timer
        /// </summary>
        public const int cintTimerLifelineHaveAHint = 31;
        /// <summary>
        /// the duration of the 5050 lifeline timer
        /// </summary>
        public const int cintTimerLifeline5050 = 3;

        /// <summary>
        /// enum containing the various types of lifeline
        /// </summary>
        public enum lifelines
        {
            /// <summary>
            /// the look in the book lifeline
            /// </summary>
            lookInTheBook,
            /// <summary>
            /// the have a hint lifeline
            /// </summary>
            haveAHint,
            /// <summary>
            /// the 5050 lifeline
            /// </summary>
            doubleYourChances,
            /// <summary>
            /// the ask the audience lifeline
            /// </summary>
            askTheAudience,
            /// <summary>
            /// the phone a friend lifeine
            /// </summary>
            phoneAFriend
        }

        /// <summary>
        /// enum containing the various states the quiz can be in
        /// </summary>
        public enum quizStates
        {
            /// <summary>
            /// when the quiz first begins (before the first question)
            /// </summary>
            start,
            /// <summary>
            /// the period just before a question is displayed
            /// </summary>
            intro,
            /// <summary>
            /// the period when a question is displayed and the timer is counting down
            /// </summary>
            mainPhase,
            /// <summary>
            /// the period when a lifeline has been requested by the player
            /// </summary>
            lifeline,
            /// <summary>
            /// the period when the user has selected their answer choice
            /// </summary>
            selected,
            /// <summary>
            /// the period when the correct answer is displayed
            /// </summary>
            result,
            /// <summary>
            /// the period after the result is displayed but before the next question is loaded
            /// </summary>
            transition
        }

        /// <summary>
        /// enum representing the various sub-states in the start phase
        /// </summary>
        public enum startStates
        {
            /// <summary>
            /// the period when the tutorial is running
            /// </summary>
            rules,
            /// <summary>
            /// the period when the lifelines pop up on screen
            /// </summary>
            lifelines,
            /// <summary>
            /// the period when the hotseat audio cue plays
            /// </summary>
            hotseat
        }

        /// <summary>
        /// enum representing the various sub-states of the result phase
        /// </summary>
        public enum resultStates
        {
            /// <summary>
            /// sub-state for when the user has selected a correct answer
            /// </summary>
            win,
            /// <summary>
            /// sub-state for when the user has selected an incorrect answer
            /// </summary>
            lose,
            /// <summary>
            /// sub-state for when the user has run out of time
            /// </summary>
            outOfTime,
            /// <summary>
            /// sub-state for when the user has chosen the walk-away option
            /// </summary>
            walkAway
        }

        /// <summary>
        /// enum representing the various answer choices
        /// </summary>
        public enum answerOptions
        {
            /// <summary>
            /// answer choice a
            /// </summary>
            A,
            /// <summary>
            /// answer choice b
            /// </summary>
            B,
            /// <summary>
            /// answer choice c
            /// </summary>
            C,
            /// <summary>
            /// answer choice d
            /// </summary>
            D
        }

        private answerOptions _correctAnswer;
        private answerOptions _double1;
        private answerOptions _double2;
        private answerOptions _selectedAnswer;

        private answerOptions _correctAnswerNext;
        private answerOptions _double1Next;
        private answerOptions _double2Next;

        private readonly string[] _qValues = { "$100", "$300", "$400", "$1,000", "$4,000", "$8,000", "$16,000", "$32,000", "$125,000", "$250,000", "$500,000", "$1 Million" };
                
        /// <summary>
        /// string representing the score of the current question (e.g. $100)
        /// </summary>
        public string qValues
        {
            get
            {
                return _qValues[_intCurrentQ];
            }
        }

        /// <summary>
        /// returns a string representing the players final score
        /// </summary>
        public string winnings
        {
            get
            {
                switch (_result)
                {
                    case resultStates.win:
                        return _qValues[11];
                    case resultStates.lose:
                        if (_intCurrentQ < 4)
                        {
                            return "$0";
                        }
                        else if (_intCurrentQ >= 4 && _intCurrentQ < 8)
                        {
                            return _qValues[3];
                        }
                        else
                        {
                            return _qValues[7];
                        }
                    case resultStates.outOfTime:
                        if (_intCurrentQ < 4)
                        {
                            return "$0";
                        }
                        else if (_intCurrentQ >= 4 && _intCurrentQ < 8)
                        {
                            return _qValues[3];
                        }
                        else
                        {
                            return _qValues[7];
                        }
                    case resultStates.walkAway:
                        return _qValues[_intCurrentQ - 1];
                    default:
                        return "$0";
                }
            }
        }


        private readonly int[] _finalAudioDuration = { 5, 5, 5, 19, 21, 20, 20, 29, 21, 20, 20, 29 };

        /// <summary>
        /// integer representing the duration of the final answer sound effect
        /// </summary>
        public int finalAudioDuration
        {
            get
            {
                return _finalAudioDuration[_intCurrentQ];
            }
        }

        private int[] _qSelections = new int[cintMaxQuestion + 1];

        private bool _bolLifelineAAvailable = true;
        private bool _bolLifelineBAvailable = true;
        private bool _bolLifelineCAvailable = true;

        /// <summary>
        /// represents if the first lifeline option is avalable (has not been used yet)
        /// </summary>
        public bool LifelineAAvailable
        {
            get
            {
                return _bolLifelineAAvailable;
            }
        }

        /// <summary>
        /// represents if the Second lifeline option is avalable (has not been used yet)
        /// </summary>
        public bool LifelineBAvailable
        {
            get
            {
                return _bolLifelineBAvailable;
            }
        }

        /// <summary>
        /// represents if the third lifeline option is avalable (has not been used yet)
        /// </summary>
        public bool LifelineCAvailable
        {
            get
            {
                return _bolLifelineCAvailable;
            }
        }

        private int _intCurrentQ = cintMinQuestion;

        /// <summary>
        /// returns an integer representing the current question number (0-11)
        /// </summary>
        public int currentQ
        {
            get
            {
                return _intCurrentQ;
            }

            set
            {
                _intCurrentQ = value;
            }
        }

        private quizStates _CurrentState = quizStates.intro;

        /// <summary>
        /// gets or sets the current state that the quiz is in
        /// </summary>
        public quizStates currentState
        {
            get
            {
                return _CurrentState;
            }

            set
            {
                _CurrentState = value;
            }
        }

        private startStates _startState = startStates.rules;

        /// <summary>
        /// gets or sets the curent start sub-state that the quiz is in
        /// </summary>
        public startStates startState
        {
            get
            {
                return _startState;
            }

            set
            {
                _startState = value;
            }
        }

        private resultStates _result = resultStates.win;

        /// <summary>
        /// gets or sets the result of the current question
        /// </summary>
        public resultStates result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
            }
        }

        private lifelines _selectedLifeline = lifelines.askTheAudience;

        /// <summary>
        /// gets or sets the lifeline the player selected 
        /// </summary>
        public lifelines selectedLifeline
        {
            get
            {
                return _selectedLifeline;
            }
            set
            {
                _selectedLifeline = value;
            }
        }

        private string _strQuestion;
        private string _strAnswerA;
        private string _strAnswerB;
        private string _strAnswerC;
        private string _strAnswerD;
        private string _strLifelineBook;
        private string _strLifelineHint;
        private string _strLifelineDoubleA;
        private string _strLifelineDoubleB;
        private string _strCorrectAnswer;


        private string _strQuestionNext;
        private string _strAnswerANext;
        private string _strAnswerBNext;
        private string _strAnswerCNext;
        private string _strAnswerDNext;
        private string _strLifelineBookNext;
        private string _strLifelineHintNext;
        private string _strLifelineDoubleANext;
        private string _strLifelineDoubleBNext;
        private string _strCorrectAnswerNext;

        /// <summary>
        /// returns a string representing the question prompt
        /// </summary>
        public string question
        {
            get
            {
                return _strQuestion;
            }
        }

        /// <summary>
        /// returns a string representing answer option A
        /// </summary>
        public string AnswerA
        {
            get
            {
                return _strAnswerA;
            }
        }

        /// <summary>
        /// returns a string representing answer option B
        /// </summary>
        public string AnswerB
        {
            get
            {
                return _strAnswerB;
            }
        }

        /// <summary>
        /// returns a string representing answer option C
        /// </summary>
        public string AnswerC
        {
            get
            {
                return _strAnswerC;
            }
        }

        /// <summary>
        /// returns a string representing answer option D
        /// </summary>
        public string AnswerD
        {
            get
            {
                return _strAnswerD;
            }
        }

        /// <summary>
        /// returns a string representing the prompt for the look in the book lifeline
        /// </summary>
        public string LifelineBook
        {
            get
            {
                return _strLifelineBook;
            }
        }

        /// <summary>
        /// returns a string representing the prompt for the have a hint lifeline
        /// </summary>
        public string LifelineHint
        {
            get
            {
                return _strLifelineHint;
            }
        }

        /// <summary>
        /// returns the first of the two incorrect answer choices elminated by the 5050 lifeline
        /// </summary>
        public answerOptions lifelineDouble1
        {
            get
            {
                return _double1;
            }
        }

        /// <summary>
        /// returns the second of the two incorrect answer choices elminated by the 5050 lifeline
        /// </summary>
        public answerOptions lifelineDouble2
        {
            get
            {
                return _double2;
            }
        }

        /// <summary>
        /// returns the correct answer
        /// </summary>
        public answerOptions correctAnswer
        {
            get
            {
                return _correctAnswer;
            }
        }

        /// <summary>
        /// gets or sets the answer selected by the player
        /// </summary>
        public answerOptions selectedAnswer
        {
            get
            {
                return _selectedAnswer;
            }
            set
            {
                _selectedAnswer = value;
            }
        }

        private readonly XElement _questionDB;

        /// <summary>
        /// primary constructor for the primary data handler
        /// </summary>
        public millionaireQuiz()
        {
            _questionDB = XElement.Load("Data/quiz_data.xml");
            selectQuestions();
            loadQuestion(0);
        }

        /// <summary>
        /// randomly selects the questions to be used during the current playthrough and stores them
        /// </summary>
        public void selectQuestions()
        {
            Random rand = new Random();
            for (int i = 0; i < _qSelections.Length; i++)
            {

                _qSelections[i] = rand.Next() % 30;
            }
        }

        /// <summary>
        /// retrieves the specified question from the database and stores it as the next question to be displayed
        /// </summary>
        /// <param name="q">the question number to be loaded</param>
        public void loadQuestion(int q)
        {
            IEnumerable<XElement> questionRetrieve =
                from el in _questionDB.Elements("quiz")
                where (string)el.Attribute("id") == (_qSelections[q] + 1).ToString()
                select el;

            questionRetrieve = from el in questionRetrieve.Elements("question")
                            where (string)el.Attribute("id") == (q + 1).ToString()
                            select el;


            _strQuestionNext = (string)
                    (from el in questionRetrieve.Descendants("text")
                     select el).First();


            _strAnswerANext = (string)
                (from el in questionRetrieve.Elements("answer")
                 where (string)el.Attribute("id") == "A"
                 select el).First();

            _strAnswerBNext = (string)
                (from el in questionRetrieve.Elements("answer")
                 where (string)el.Attribute("id") == "B"
                 select el).First();

            _strAnswerCNext = (string)
                (from el in questionRetrieve.Elements("answer")
                 where (string)el.Attribute("id") == "C"
                 select el).First();

            _strAnswerDNext = (string)
                (from el in questionRetrieve.Elements("answer")
                 where (string)el.Attribute("id") == "D"
                 select el).First();

            _strLifelineBookNext = (string)
                (from el in questionRetrieve.Elements("lifeline")
                 where (string)el.Attribute("type") == "LookInTheBook"
                 select el).First();

            _strLifelineHintNext = (string)
                (from el in questionRetrieve.Elements("lifeline")
                 where (string)el.Attribute("type") == "HaveAHint"
                 select el).First();

            IEnumerable<XElement> detailRetrieve =
                from el in questionRetrieve.Elements("lifeline")
                where (string)el.Attribute("type") == "DoubleYourChances"
                select el;

            _strLifelineDoubleANext = (string) 
                (from el in detailRetrieve.Elements("incorrectAnswer")
                where (string)el.Attribute("id") == "1"
                select el).First();

            switch (_strLifelineDoubleANext)
            {
                case "A":
                    _double1Next = answerOptions.A;
                    break;
                case "B":
                    _double1Next = answerOptions.B;
                    break;
                case "C":
                    _double1Next = answerOptions.C;
                    break;
                case "D":
                    _double1Next = answerOptions.D;
                    break;
                default:
                    break;
            }

            _strLifelineDoubleBNext = (string)
                (from el in detailRetrieve.Elements("incorrectAnswer")
                 where (string)el.Attribute("id") == "2"
                 select el).First();

            switch (_strLifelineDoubleBNext)
            {
                case "A":
                    _double2Next = answerOptions.A;
                    break;
                case "B":
                    _double2Next = answerOptions.B;
                    break;
                case "C":
                    _double2Next = answerOptions.C;
                    break;
                case "D":
                    _double2Next = answerOptions.D;
                    break;
                default:
                    break;
            }

            _strCorrectAnswerNext = (string)
                (from el in questionRetrieve.Elements("correctAnswer")
                 select el).First();

            switch (_strCorrectAnswerNext)
            {
                case "A":
                    _correctAnswerNext = answerOptions.A;
                    break;
                case "B":
                    _correctAnswerNext = answerOptions.B;
                    break;
                case "C":
                    _correctAnswerNext = answerOptions.C;
                    break;
                case "D":
                    _correctAnswerNext = answerOptions.D;
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// returns and integer representing the duration for the main timer
        /// </summary>
        /// <returns>integer duration of the timer</returns>
        public int currentTimer()
        {
            if (_intCurrentQ < 4)
            {
                return cintTimerStartRound1;
            }
            else if (_intCurrentQ >=4 && _intCurrentQ < 8)
            {
                return cintTimerStartRound2;
            }
            else
            {
                return cintTimerStartRound3;
            }
        }

        /// <summary>
        /// returns an integer representing the timer duration for the currently selected lifeline
        /// </summary>
        /// <returns>integer duration of the lifeline timer</returns>
        public int currentLifelineTimer()
        {
            switch (_selectedLifeline)
            {
                case lifelines.lookInTheBook:
                    return cintTimerLifelineBook;
                case lifelines.haveAHint:
                    return cintTimerLifelineHaveAHint;
                case lifelines.doubleYourChances:
                    return cintTimerLifeline5050;
                case lifelines.askTheAudience:
                    return cintTimerLifelineAudience;
                case lifelines.phoneAFriend:
                    return cintTimerLifelinePhoneAFriend;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// determines if the selected answer was correct and sets the result state to the appropriate value
        /// </summary>
        public void checkAnswer()
        {
            if (_selectedAnswer == _correctAnswer)
            {
                _result = resultStates.win;
            }
            else
            {
                _result = resultStates.lose;
            }
        }

        /// <summary>
        /// moves the question stored as next to the current slot
        /// </summary>
        public void advance()
        {
            _strQuestion = _strQuestionNext;
            _strAnswerA = _strAnswerANext;
            _strAnswerB = _strAnswerBNext;
            _strAnswerC = _strAnswerCNext;
            _strAnswerD = _strAnswerDNext;
            _strLifelineBook = _strLifelineBookNext;
            _strLifelineHint = _strLifelineHintNext;
            _strLifelineDoubleA = _strLifelineDoubleANext;
            _strLifelineDoubleB = _strLifelineDoubleBNext;
            _strCorrectAnswer = _strCorrectAnswerNext;
            _correctAnswer = _correctAnswerNext;
            _double1 = _double1Next;
            _double2 = _double2Next;
        }
    }
}
