/* UserInterface.cs
 * Author: Tyler Richey (tbtaco)
 * Completion Date: 11/18/2016
 * Description:
 *     This program calculates pi using the Gregory–Leibniz series algorithm.
 *     The first window to appear asks for the:
 *         Precision: Number of decimals to use.
 *         Iterations: Number of terms to add in the Gregory-Leibniz series.
 *     The minimum number of iterations a thread can have is set to 50000.
 *     It will divide the work between the threads to give the best performance.
 *     The program will asynchronously calculate different sections of the series
 *     In different threads, while also keeping the Loading UI up to date. (28 Total Threads)
 *     Once the parts are done calculating, the Main UI adds them up and displays.
 *     I have the first 350 digits of pi saved as a variable to be used in % error.
 * Tested So Far: 1 Million Digits, 10 Million Iterations (No Errors To Report, Just A Long Wait)
 * Result Of Test: 1.6 * 10 ^ -13 % Error (3 Hours, Maxed Out CPU Resulting In Longer Waits.  25 Hour Original)
 */ 
using System;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pi
{
    /// <summary>
    /// The main interface of the program.
    /// This gets the number of decimals and iterations from the user.
    /// </summary>
    public partial class UserInterface : Form
    {
        /// <summary>
        /// Variable used for referencing this interface.
        /// </summary>
        public UserInterface _MainUI;
        /// <summary>
        /// Variable used for referencing the loading interface.
        /// </summary>
        public PiLoading _LoadingUI;
        /// <summary>
        /// Variable used for referencing the results interface.
        /// </summary>
        public PiResult _ResultUI;
        /// <summary>
        /// Minimum number of iterations a thread will have before a new thread is added.
        /// </summary>
        public const int _minimumIterationsPerThread = 50000;
        /// <summary>
        /// An array to hold the label objects in the loading interface.
        /// </summary>
        public Label[] _labels;
        /// <summary>
        /// An array to hold the progress bar objects in the loading interface.
        /// </summary>
        public ProgressBar[] _progressBars;
        /// <summary>
        /// An array to hold the threads created when separating the work load
        /// </summary>
        public Thread[] _threads;
        /// <summary>
        /// An array to hold the pieces of pi created from the multiple threads.
        /// These are added together after the threads generate the parts.
        /// </summary>
        public BigInteger[] _partialPi;
        /// <summary>
        /// The first 350 digits of pi, to only be used in calculating % error.
        /// (This is never referenced while calculating pi, only after for % error)
        /// </summary>
        public BigInteger _First_350_For_Error_Checking_ = BigInteger.Parse(
            "3141592653589793238462643383279502884197169399375" +
            "1058209749445923078164062862089986280348253421170" +
            "6798214808651328230664709384460955058223172535940" +
            "8128481117450284102701938521105559644622948954930" +
            "3819644288109756659334461284756482337867831652712" +
            "0190914564856692346034861045432664821339360726024" +
            "9141273724587006606315588174881520920962829254091");
        /// <summary>
        /// 1000 Zeros to shorten initial creation of the constant giving us our decimal precision later on.
        /// </summary>
        public String _1000_Zeros_ =
            "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
        /// <summary>
        /// Initialization.
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
            _MainUI = this;
            _LoadingUI = new PiLoading(_MainUI);
            _ResultUI = new PiResult(_MainUI);
            _labels = new Label[] {
                _LoadingUI.uxL1, _LoadingUI.uxL2, _LoadingUI.uxL3, _LoadingUI.uxL4, _LoadingUI.uxL5,
                _LoadingUI.uxL6, _LoadingUI.uxL7, _LoadingUI.uxL8, _LoadingUI.uxL9, _LoadingUI.uxL10,
                _LoadingUI.uxL11, _LoadingUI.uxL12, _LoadingUI.uxL13, _LoadingUI.uxL14, _LoadingUI.uxL15,
                _LoadingUI.uxL16, _LoadingUI.uxL17, _LoadingUI.uxL18, _LoadingUI.uxL19, _LoadingUI.uxL20,
                _LoadingUI.uxLTotal};
            _progressBars = new ProgressBar[] {
                _LoadingUI.uxB1, _LoadingUI.uxB2, _LoadingUI.uxB3, _LoadingUI.uxB4, _LoadingUI.uxB5,
                _LoadingUI.uxB6, _LoadingUI.uxB7, _LoadingUI.uxB8, _LoadingUI.uxB9, _LoadingUI.uxB10,
                _LoadingUI.uxB11, _LoadingUI.uxB12, _LoadingUI.uxB13, _LoadingUI.uxB14, _LoadingUI.uxB15,
                _LoadingUI.uxB16, _LoadingUI.uxB17, _LoadingUI.uxB18, _LoadingUI.uxB19, _LoadingUI.uxB20,
                _LoadingUI.uxBTotal};
            Thread.CurrentThread.Name = "tbtaco.Pi.Thread.UI";
        }
        /// <summary>
        /// Runs when the interface loads.
        /// Positions the window in the center of the screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void UserInterface_Load(object sender, EventArgs ev)
        {
            StartPosition = FormStartPosition.Manual;
            Location = new Point(Screen.GetBounds(default(Point)).Width / 2 - base.Width / 2,
                                 Screen.GetBounds(default(Point)).Height / 2 - base.Height / 2);
            BringToFront();
        }
        /// <summary>
        /// Hides this interface and sets up everything else for the rest of the code.
        /// This is the main method that ties everything together.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private async void button_Click(object sender, EventArgs ev)
        {
            Opacity = 0;
            ShowInTaskbar = false;
            if (uxIterations.Value <= 10 || uxPrecision.Value <= 10)
                MessageBox.Show("Warning: Lower Values For Precision And\n" +
                                "Iterations May Lead To Odd Behavior Of The\n" +
                                "Series Implementation.  Adding In Rounding\n" +
                                "And Averaging, It's Best To Stick With Higher\n" +
                                "Numbers To Get Better Results Overall...");
            _LoadingUI.Show();
            _LoadingUI.BringToFront();
            _LoadingUI.Update();
            BigInteger pi = BigInteger.Zero;
            StringBuilder tempVal = new StringBuilder("1");
            while (tempVal.Length <= (long)uxPrecision.Value)
            {
                if (((long)uxPrecision.Value - tempVal.Length) / 1000 > 1)
                    tempVal.Append(_1000_Zeros_);
                else
                    tempVal.Append('0');
            }
            BigInteger constant = BigInteger.Parse(tempVal.ToString());
            BigInteger piBound1 = BigInteger.Zero;
            long iterations = (long)uxIterations.Value;
            long precision = (long)uxPrecision.Value;
            long threads = iterations / _minimumIterationsPerThread;
            if (threads == 0)
                threads = 1;
            else if (threads > 20)
                threads = 20;
            long perThread = iterations / threads;
            long remainder = iterations % (threads * perThread);
            _threads = new Thread[threads];
            _partialPi = new BigInteger[threads];
            for (int i = 0; i < 20; i++)
                if (i >= threads)
                    _progressBars[i].Value = 1000;
            for (int i = 0; i < _threads.Length; i++)
            {
                long start = perThread * i;
                if (i + 1 == _threads.Length)
                    perThread += remainder + 1;
                _threads[i] = new Thread(() => CalculatePi(i, start, perThread - 1, constant, _MainUI));
                _threads[i].Name = "tbtaco.Pi.Thread." + i;
                _threads[i].IsBackground = true;
                _threads[i].Start();
                Thread.Sleep(100);//Waiting For Them To Initialize, Only Takes ~5ms
            }
            await WaitForAllToFinish(_MainUI);
            foreach (Thread thread in _threads)
                thread.Join();
            Thread.Sleep(100);//Waiting For Them To Finish, Only Takes ~5ms
            _LoadingUI.Text = "Complete! Processing...";
            for(int i=0;i<_partialPi.Length;i++)
                piBound1 = BigInteger.Add(piBound1, _partialPi[i]);
            BigInteger piBound2 = BigInteger.Add(piBound1, 
                BigInteger.Divide(
                    BigInteger.Multiply(constant, 
                        BigInteger.Parse((4 * Math.Pow(-1, iterations + 1)).ToString())),
                            BigInteger.Parse((2 * (iterations + 1) + 1).ToString())));
            BigInteger difference = BigInteger.Abs(BigInteger.Divide(BigInteger.Subtract(piBound1, piBound2), 2));
            //difference = BigInteger.Zero;//For Just The Raw Iteration
            if (uxIterations.Value % 2 == 0)
                pi = BigInteger.Subtract(piBound1, difference);
            else
                pi = BigInteger.Add(piBound1, difference);
            String piString = pi.ToString();
            int length = _First_350_For_Error_Checking_.ToString().Length;
            if (piString.Length > length)
                piString = piString.Substring(0, length);
            else
                while (piString.Length < length)
                    piString += "0";
            String piFinalString = pi.ToString();
            if (pi < 1)
                MessageBox.Show("Error: Pi Is Too Low To Display/Calculate Properly...");
            _ResultUI.uxTextBox.Text = piFinalString.Substring(0, 1) + "." + 
                piFinalString.Substring(1, piFinalString.Length - 1);
            _ResultUI.Text = "Pi Calculator Results (" + (
                Math.Abs(
                    Convert.ToDecimal(
                        BigInteger.Divide(
                            BigInteger.Multiply(
                                BigInteger.Subtract(_First_350_For_Error_Checking_, 
                                    BigInteger.Parse(piString)), 
                                        BigInteger.Parse("100000000000000000000")), 
                                            _First_350_For_Error_Checking_).ToString()))
                                                / 1000000000000000000).ToString("N15") + "% Error)";
            _ResultUI.Show();
            _LoadingUI.Close();
            _ResultUI.BringToFront();
            //Ends here, Showing the result interface and waiting for another event to be activated.
            //Only event (since this interface is hidden) is closing the result interface, which closes this as well.
            //As a side note, closing the loading interface will also close this interface, exiting the program too.
        }
        /// <summary>
        /// An asynchronous method to be run on each thread created to calculate a portion of the series.
        /// (Thinking back now, I probably don't need this to be asynchronous.  Will leave it
        /// for now though as I've used it in that way for testing along the way.
        /// </summary>
        /// <param name="index">ID Number I've given to each thread.  This doubles as what index to use in arrays.</param>
        /// <param name="startIndex">Start position in the series to calculate.</param>
        /// <param name="length">Number of terms to calculate in the series.</param>
        /// <param name="constant">The constant calculated to give us the precision we want.</param>
        /// <param name="UI">The main interface, used to invoke methods from there.</param>
        /// <returns>Task, for Asynchronous handling.</returns>
        private Task CalculatePi(int index, long startIndex, long length, BigInteger constant, UserInterface UI)
        {
            return Task.Run(() =>
            {
                Thread.Sleep((20 - index) * 500);//Using This To Sync Some Of The Bars, Not Have Just 1 Firing Etc...
                BigInteger partialPi = BigInteger.Zero;
                for (long i = startIndex; i <= startIndex + length; i++)
                {
                    partialPi = BigInteger.Add(partialPi, BigInteger.Divide(
                        BigInteger.Multiply(constant, BigInteger.Parse((4 * Math.Pow(-1, i)).ToString())),
                        BigInteger.Parse((2 * i + 1).ToString())));
                    int value = 1000;
                    if(length>1)
                        value = (int)(((i - startIndex) * 1000) / (length - 1));
                    if (i % 907 == 0)//Changed from 1000 so it's a little more random (prime)
                    {
                        UI.Invoke(new UpdateProgressBarDelegate(UpdateProgressBar), new object[] { index, value, UI });
                        UI.Invoke(new UpdateLabelDelegate(UpdateLabel), new object[] { index, UI });
                    }
                }
                UI.Invoke(new UpdateProgressBarDelegate(UpdateProgressBar), new object[] { index, 1000, UI });
                UI.Invoke(new UpdateLabelDelegate(UpdateLabel), new object[] { index, UI });
                UI.Invoke(new SetPartialPiDelegate(SetPartialPi), new object[] { index, partialPi, UI });
            });
        }
        /// <summary>
        /// Delegate for the SetPartialPi method.
        /// Sets the value for the partial pi at the specified index.
        /// </summary>
        /// <param name="index">ID Number I've given to each thread.  This doubles as what index to use in arrays.</param>
        /// <param name="pi">Value of pi to set the partial pi to.</param>
        /// <param name="UI">The main interface, used to invoke methods from there.</param>
        private delegate void SetPartialPiDelegate(int index, BigInteger pi, UserInterface UI);
        /// <summary>
        /// Sets the value for the partial pi at the specified index.
        /// </summary>
        /// <param name="index">ID Number I've given to each thread.  This doubles as what index to use in arrays.</param>
        /// <param name="pi">Value of pi to set the partial pi to.</param>
        /// <param name="UI">The main interface, used to invoke methods from there.</param>
        private void SetPartialPi(int index, BigInteger pi, UserInterface UI)
        {
            if (UI.InvokeRequired)
                UI.Invoke(new SetPartialPiDelegate(SetPartialPi), new object[] { index, pi, UI });
            UI._partialPi[index] = pi;
        }
        /// <summary>
        /// Delegate for the UpdateProgressBar method
        /// Sets the value to the progress bar at a certain index.
        /// </summary>
        /// <param name="index">ID Number I've given to each thread.  This doubles as what index to use in arrays.</param>
        /// <param name="value">The value to set a progress bar to.</param>
        /// <param name="UI">The main interface, used to invoke methods from there.</param>
        private delegate void UpdateProgressBarDelegate(int index, int value, UserInterface UI);
        /// <summary>
        /// Sets the value to the progress bar at a certain index.
        /// </summary>
        /// <param name="index">ID Number I've given to each thread.  This doubles as what index to use in arrays.</param>
        /// <param name="value">The value to set a progress bar to.</param>
        /// <param name="UI">The main interface, used to invoke methods from there.</param>
        private void UpdateProgressBar(int index, int value, UserInterface UI)
        {
            if (_progressBars[index].InvokeRequired)
                UI.Invoke(new UpdateProgressBarDelegate(UpdateProgressBar), new object[] { index, value, UI });
            _progressBars[index].Value = value;
        }
        /// <summary>
        /// Delegate for the UpdateLabel method.
        /// Updates a label at an index using the progress bar for the value.
        /// </summary>
        /// <param name="index">ID Number I've given to each thread.  This doubles as what index to use in arrays.</param>
        /// <param name="UI">The main interface, used to invoke methods from there.</param>
        private delegate void UpdateLabelDelegate(int index, UserInterface UI);
        /// <summary>
        /// Updates a label at an index using the progress bar for the value.
        /// </summary>
        /// <param name="index">ID Number I've given to each thread.  This doubles as what index to use in arrays.</param>
        /// <param name="UI">The main interface, used to invoke methods from there.</param>
        private void UpdateLabel(int index, UserInterface UI)
        {
            if (_labels[index].InvokeRequired)
                UI.Invoke(new UpdateLabelDelegate(UpdateLabel), new object[] { index, UI });
            double value = _progressBars[index].Value / 10.0;
            _labels[index].Text = value.ToString("N1") + "%";
        }
        /// <summary>
        /// Delegate for the UpdateTotal method.
        /// Updates the bottom bar, that shows the total progress of the other bars.
        /// </summary>
        /// <param name="UI">The main interface, used to invoke methods from there.</param>
        private delegate void UpdateTotalDelegate(UserInterface UI);
        /// <summary>
        /// Updates the bottom bar, that shows the total progress of the other bars.
        /// </summary>
        /// <param name="UI">The main interface, used to invoke methods from there.</param>
        private void UpdateTotal(UserInterface UI)
        {
            if (_progressBars[0].InvokeRequired)
                UI.Invoke(new UpdateTotalDelegate(UpdateTotal), new object[] { UI });
            int active = 0;
            double percent = 0;
            for(int i=0;i<20;i++)
            {
                String[] parts = _labels[i].Text.Split('%');
                if(parts.Length==2)
                {
                    active++;
                    percent += Convert.ToDouble(parts[0]);
                }
            }
            if(active!=0)
            {
                percent = percent / active;
                _progressBars[20].Value = (int)(percent * 10);
                _labels[20].Text = percent.ToString("N1") + "%";
                UI._LoadingUI.Update();
            }
        }
        /// <summary>
        /// Delegate for the CheckOnThreads method.
        /// Checks the status bars and returns true only if all are at 100%.
        /// </summary>
        /// <param name="UI">The main interface, used to invoke methods from there.</param>
        /// <returns>True/False showing if the program can move on yet.</returns>
        private delegate bool CheckOnThreadsDelegate(UserInterface UI);
        /// <summary>
        /// Checks the status bars and returns true only if all are at 100%.
        /// </summary>
        /// <param name="UI">The main interface, used to invoke methods from there.</param>
        /// <returns>True/False showing if the program can move on yet.</returns>
        private bool CheckOnThreads(UserInterface UI)
        {
            if (_progressBars[0].InvokeRequired)
                UI.Invoke(new CheckOnThreadsDelegate(CheckOnThreads), new object[] { UI });
            foreach (ProgressBar bar in _progressBars)
                if (bar.Value != 1000)
                    return false;
            return true;
        }
        /// <summary>
        /// Asynchronous method that runs on a new thread until all calculations are complete.
        /// </summary>
        /// <param name="UI">The main interface, used to invoke methods from there.</param>
        /// <returns>Task, for Asynchronous handling.</returns>
        private Task WaitForAllToFinish(UserInterface UI)
        {
            return Task.Run(() =>
            {
                bool b = false;
                while(!b)
                {
                    try
                    {
                        UI.Invoke(new UpdateTotalDelegate(UpdateTotal), new object[] { UI });
                    }
                    catch (Exception) { return; }
                    b = (bool) UI.Invoke(new CheckOnThreadsDelegate(CheckOnThreads), new object[] { UI });
                    Thread.Sleep(10);//Stops From Spamming It, Tieing Up The Main Interface
                }
                Thread.Sleep(7777);//Finish Filling Bars, Windows Animation Takes A While Sometimes...
            });
        }
    }
}
