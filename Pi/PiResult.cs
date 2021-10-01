/* PiResult.cs
 * Author: Tyler Richey (tbtaco)
 * Info: See UserInterface.cs
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pi
{
    /// <summary>
    /// This is the last interface that is displayed.
    /// It simply shows the value of pi we calculated all this time,
    /// as well as what our percent error is for the final value.
    /// </summary>
    public partial class PiResult : Form
    {
        /// <summary>
        /// A reference to the main interface.
        /// </summary>
        UserInterface UI;
        /// <summary>
        /// Initialization.
        /// </summary>
        /// <param name="UI"></param>
        public PiResult(UserInterface UI)
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
        private void PiResult_Load(object sender, EventArgs ev)
        {
            StartPosition = FormStartPosition.Manual;
            Location = new Point(Screen.GetBounds(default(Point)).Width / 2 - base.Width / 2,
                                 Screen.GetBounds(default(Point)).Height / 2 - base.Height / 2);
        }
        /// <summary>
        /// Closes the main interface too when this interface is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void PiResult_FormClosing(object sender, FormClosingEventArgs ev)
        {
            UI.Close();
        }
    }
}