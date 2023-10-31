using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace NewStrongOpen
{
    /// <summary>
    /// Main form class
    /// </summary>
    public partial class frmStrongOpen : Form
    {
        #region Atributes Region

        List<SORunner> SORunnerList = new List<SORunner>();

        #endregion

        #region Initalizing Methods Region

        /// <summary>
        /// Class constructor
        /// </summary>
        public frmStrongOpen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method initializes the form, loading all the necessary runners.
        /// It loads the runners into an array and starts the thread that performs the runners´ starting process
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event args</param>
        private void frmStrongOpen_Load(object sender, EventArgs e)
        {
            //Load historical runner into runners´ array
            CreateHistRunner(@"D:\Strategies\StrongOpen\TestFiles\20110321_Proxydiff.txt");

            //Start the thread to perform the starting sequence
            Thread runThread = new Thread(StartRunSequence);
            runThread.Start();
        }

        /// <summary>
        /// Creates a historical runner using the specified ProxyDiff file
        /// </summary>
        /// <param name="_FileName">Full path for the ProxyDiff file</param>
        private void CreateHistRunner(string _FileName)
        {
            //Create runner using IBOV as hedge
            //SORunner tempSORunner1 = new SORunner(true, _FileName, 1073, "IBOV", 5);
            //SORunnerList.Add(tempSORunner1);

            //Create runner using BOVA11 as hedge
            SORunner tempSORunner2 = new SORunner(true, _FileName, 5526, "BOVA11", 1000);            
            SORunnerList.Add(tempSORunner2);
        }


        /// <summary>
        /// Performs the runners´ starting sequence for each runner in the SORunnerList array
        /// </summary>
        private void StartRunSequence()
        {
            //Loop through the requested Strong Open runners
            foreach (SORunner tempSORunner in SORunnerList)
            {
                //Increase the counter of active runners
                GlobalVars.Instance.RunCounter++;
          
                //Block if the number of active runners exceed the maximum
                while (GlobalVars.Instance.RunCounter > 5)
                {
                    System.Threading.Thread.Sleep(1000);
                }                

                //Start the new runner
                tempSORunner.Start();
            }
        }

        #endregion
    }
}
