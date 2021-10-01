/* Program.cs
 * Author: Tyler Richey (tbtaco)
 * Info: See UserInterface.cs
 */
using System;
using System.Windows.Forms;

namespace Pi
{
    /// <summary>
    /// Main Entry Point For The Program
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Main Entry Point For The Program
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new UserInterface());
            }
            catch(Exception e)
            {
                MessageBox.Show("Error:\n\n" + e);
            }
        }
    }
}
