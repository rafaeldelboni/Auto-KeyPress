using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Auto_KeyPress
{
    public partial class KPForm : Form
    {
        #region Const & Variables
        const UInt32 WM_KEYDOWN = 0x0100;
        public int VK = 0;
        public int counter = 0;
        #endregion

        #region Component Actions

        #region KPForm
        public KPForm()
        {
            InitializeComponent();
        }

        private void KPForm_Load(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            StatusUpdate(counter.ToString());

            cmbKey.SelectedIndex = 4;

            LoadProcessNames();
        }
        #endregion

        #region btnStart
        private void btnStart_Click(object sender, EventArgs e)
        {
            StatusUpdate(counter.ToString());
            if (cmbProcess.Text == "")
            {
                StatusUpdate("Set the process!");
            }
            else if (txtTime.Text == "" || Convert.ToInt32(txtTime.Text) == 0)
            {
                StatusUpdate("Set the refresh time!");
            }
            else
            {
                SetKeyValue(cmbKey.Text);
                LockCommands(true);
                timerRefresh.Interval = Convert.ToInt32(txtTime.Text) * 1000;
                timerRefresh.Enabled = true;
                timerRefresh.Start();
            }
        }
        #endregion

        #region btnStop
        private void btnStop_Click(object sender, EventArgs e)
        {
            timerRefresh.Stop();
            timerRefresh.Enabled = false;
            LockCommands(false);
            counter = 0;
            StatusUpdate(counter.ToString());
        }
        #endregion

        #region btnRefresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProcessNames();
        }
        #endregion

        #region timerRefresh
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName(cmbProcess.Text);
            foreach (Process proc in processes)
                UserInterface.PostMessage(proc.MainWindowHandle, WM_KEYDOWN, VK, 0);

            counter = counter + 1;
            StatusUpdate(counter.ToString());
        }
        #endregion

        #region txtTime
        private void txtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) ||
                  char.IsSymbol(e.KeyChar) ||
                  char.IsWhiteSpace(e.KeyChar) ||
                  char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }
        #endregion

        #endregion

        #region Functions

        #region Status Update
        private void StatusUpdate(string Status)
        {
            lblStatus.Text = Status;
        }
        #endregion

        #region Load Process Names
        private void LoadProcessNames()
        {
            cmbProcess.Items.Clear();

            Process[] processes = Process.GetProcesses();
            int i = 1;
            foreach (Process proc in processes)
            {
                cmbProcess.Items.Add(proc.ProcessName);
                if (proc.ProcessName == "Ssms")
                {
                    cmbProcess.Items.Insert(0, proc.ProcessName);
                }
                else
                {
                    cmbProcess.Items.Insert(i, proc.ProcessName);
                }
                i++;
            }
        }
        #endregion

        #region Lock Commands
        private void LockCommands(bool locker)
        {
            if (locker)
            {
                cmbProcess.Enabled = false;
                cmbKey.Enabled = false;
                txtTime.Enabled = false;
                btnStart.Enabled = false;
                btnRefresh.Enabled = false;
                // Stop Button
                btnStop.Enabled = true;
            }
            else
            {
                cmbProcess.Enabled = true;
                cmbKey.Enabled = true;
                txtTime.Enabled = true;
                btnStart.Enabled = true;
                btnRefresh.Enabled = true;
                // Start Button
                btnStop.Enabled = false;
            }
        }
        #endregion

        #region Set Key Value
        private void SetKeyValue(string key)
        {
            // Based on cmbKey select, switch to Hex key value
            switch (key)
            {
                case "F1":
                    VK = 0x70;
                break;
                case "F2":
                    VK = 0x71;
                break;
                case "F3":
                    VK = 0x72;
                break;
                case "F4":
                    VK = 0x73;
                break;
                case "F5":
                    VK = 0x74;
                break;
                case "F6":
                    VK = 0x75;
                break;
                case "F7":
                    VK = 0x76;
                break;
                case "F8":
                    VK = 0x77;
                break;
                case "F9":
                    VK = 0x78;
                break;
                case "F10":
                    VK = 0x79;
                break;
                case "F11":
                    VK = 0x7A;
                break;
                case "F12":
                    VK = 0x7B;
                break;
            }
        }
        #endregion

        #endregion
    }
}
