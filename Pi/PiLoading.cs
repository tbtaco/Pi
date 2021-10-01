/* PiLoading.cs
 * Author: Tyler Richey (tbtaco)
 * Info: See UserInterface.cs
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pi
{
    /// <summary>
    /// The loading interface to use between the main interface and result interface.
    /// This simply displays the status of the threads used in the main interface.
    /// </summary>
    public partial class PiLoading : Form
    {
        /// <summary>
        /// A reference to the main interface.
        /// </summary>
        UserInterface UI;
        /// <summary>
        /// Text to look for when closing this interface.
        /// If this isn't the title, we know the interface was closed before it could complete the tasks. (By a Human)
        /// </summary>
        String ClosingText = "Complete! Processing...";
        /// <summary>
        /// Initialization.
        /// </summary>
        /// <param name="UI"></param>
        public PiLoading(UserInterface UI)
        {
            InitializeComponent();
            this.UI = UI;
        }
        /// <summary>
        /// Runs when the interface loads.
        /// Positions the window in the center of the screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void PiLoading_Load(object sender, EventArgs ev)
        {
            StartPosition = FormStartPosition.Manual;
            Location = new Point(Screen.GetBounds(default(Point)).Width / 2 - base.Width / 2,
                                 Screen.GetBounds(default(Point)).Height / 2 - base.Height / 2);
        }
        /// <summary>
        /// If this interface is closed by a Human, it also closes the main interface.
        /// Else it will just close this interface and continue running code from the main interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void PiLoading_FormClosing(object sender, FormClosingEventArgs ev)
        {
            if (!sender.ToString().Equals("Pi.PiLoading, Text: "+ClosingText))
                UI.Close();
        }
    }
}