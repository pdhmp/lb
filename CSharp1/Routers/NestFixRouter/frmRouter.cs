using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace NestFixRouter
{
    public partial class frmRouter : Form
    {
        bool AutoStart = false;
        static string ConfigurationFolder = @"C:\NFixRouter\";
        string ConfigurationFile = ConfigurationFolder + "StartConfig.cfg";

        private const int __GridColumnStatus = 0;
        private const int __GridColumnName = 1;
        private const int __GridColumnSession = 2;
        private const int __GridColumnEnable = 3;
        private const int __GridColumnDisable = 4;
        private const int __GridColumnLast = 5;

        private FixRouter _oRouter;
        private Dictionary<string, DataGridViewRow> _dcSessionRow = new Dictionary<string, DataGridViewRow>();

        public delegate void StringDelegate(string stringParameter);

        public frmRouter()
        {
            InitializeComponent();
            _oRouter = new FixRouter(this);
        }

        public void LogMessage(string message)
        {
            StreamWriter sw = new StreamWriter(_oRouter._sLogPath + "\\" + DateTime.Today.ToString("yyyyMMdd") + "_RouterLog.txt", true);
            sw.WriteLine(DateTime.Now.ToString() + "\t" + message);

            sw.Close();
            sw.Dispose();

            //if (this.InvokeRequired)
            //    this.Invoke(new StringDelegate(_Log), message);
            //else
            //    _Log(message);
        }
        public void UpdateStatus(string session, bool connected)
        {
            if (connected)
            {
                if (this.InvokeRequired)
                    this.Invoke(new StringDelegate(_Logon), session);
                else
                    _Logon(session);
            }
            else
            {
                if (this.InvokeRequired)
                    this.Invoke(new StringDelegate(_Logout), session);
                else
                    _Logout(session);
            }
        }
        public void UpdateSession(string session, bool enabled)
        {
            if (enabled)
            {
                if (this.InvokeRequired)
                    this.Invoke(new StringDelegate(_Enable), session);
                else
                    _Enable(session);
            }
            else
            {
                if (this.InvokeRequired)
                    this.Invoke(new StringDelegate(_Disable), session);
                else
                    _Disable(session);
            }
        }
        public void BuildInboundGrid(List<string> connections)
        {
            _BuildGrid(dgvInbound, connections);
        }
        public void BuildOutboundGrid(List<string> connections)
        {
            _BuildGrid(dgvInbound, connections);
        }

        //private void _Log(string msg)
        //{
        //    txtRouter.Text += msg + Environment.NewLine;
        //    txtRouter.Select(txtRouter.TextLength, 0);
        //    txtRouter.ScrollToCaret();
        //}
        private void _Logon(string session)
        {
            DataGridViewCell cell = _dcSessionRow[session].Cells[__GridColumnStatus];
            cell.Value = "ON";
            cell.Style.ForeColor = Color.White;
            cell.Style.BackColor = Color.Green;
            cell.Style.SelectionForeColor = Color.White;
            cell.Style.SelectionBackColor = Color.Green;
        }
        private void _Logout(string session)
        {
            DataGridViewCell cell = _dcSessionRow[session].Cells[__GridColumnStatus];
            cell.Value = "OFF";
            cell.Style.ForeColor = Color.White;
            cell.Style.BackColor = Color.Red;
            cell.Style.SelectionForeColor = Color.White;
            cell.Style.SelectionBackColor = Color.Red;
        }
        private void _Enable(string session)
        {
            DataGridViewCell cell = _dcSessionRow[session].Cells[__GridColumnSession];
            cell.Value = "ON";
            cell.Style.ForeColor = Color.White;
            cell.Style.BackColor = Color.Green;
            cell.Style.SelectionForeColor = Color.White;
            cell.Style.SelectionBackColor = Color.Green;
        }
        private void _Disable(string session)
        {
            DataGridViewCell cell = _dcSessionRow[session].Cells[__GridColumnSession];
            cell.Value = "OFF";
            cell.Style.ForeColor = Color.White;
            cell.Style.BackColor = Color.Red;
            cell.Style.SelectionForeColor = Color.White;
            cell.Style.SelectionBackColor = Color.Red;
        }
        private void _BuildGrid(DataGridView grid, List<string> connections)
        {
            foreach (string curConnection in connections)
            {
                DataGridViewRow oConnectionRow;
                if (!_dcSessionRow.TryGetValue(curConnection, out oConnectionRow))
                {
                    int iNewRow = grid.Rows.Add("OFF", curConnection, "ON", "Start", "Stop", "Started at " + DateTime.Now.ToShortTimeString());
                    _dcSessionRow.Add(curConnection, grid.Rows[iNewRow]);
                }
                else
                {
                    oConnectionRow.Cells[__GridColumnLast].Value = "Reset at " + DateTime.Now.ToShortTimeString();
                }
            }
        }

        private void btnStartAcceptor_Click(object sender, EventArgs e)
        {
            btnStartAcceptor.Enabled = false;
            btnStopAcceptor.Enabled = true;

            List<string> connections = _oRouter.AcceptorStart();
            _BuildGrid(dgvInbound, connections);
        }
        private void btnStopAcceptor_Click(object sender, EventArgs e)
        {
            btnStartAcceptor.Enabled = true;
            btnStopAcceptor.Enabled = false;

            foreach (DataGridViewRow curRow in dgvInbound.Rows)
                if (_dcSessionRow.ContainsKey((string)curRow.Cells[__GridColumnName].Value))
                    lock (_dcSessionRow)
                        _dcSessionRow.Remove((string)curRow.Cells[__GridColumnName].Value);

            dgvInbound.Rows.Clear();
            _oRouter.AcceptorStop();
        }
        private void dgvInbound_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == __GridColumnEnable) 
                _oRouter.EnableInbound((string)dgvInbound[__GridColumnName, e.RowIndex].Value);
            else if (e.ColumnIndex == __GridColumnDisable) 
                _oRouter.DisableInbound((string)dgvInbound[__GridColumnName, e.RowIndex].Value);
        }
        private void btnEnableInboundSessions_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow curRow in dgvInbound.Rows)
                _oRouter.EnableInbound((string)curRow.Cells[__GridColumnName].Value);
        }
        private void btnDisableInboundSessions_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow curRow in dgvInbound.Rows)
                _oRouter.DisableInbound((string)curRow.Cells[__GridColumnName].Value);
        }

        private void btnStartInitiator_Click(object sender, EventArgs e)
        {
            btnStartInitiator.Enabled = false;
            btnStopInitiator.Enabled = true;

            List<string> connections = _oRouter.InitiatorStart();
            _BuildGrid(dgvOutbound, connections);
        }
        private void btnStopInitiator_Click(object sender, EventArgs e)
        {
            btnStartInitiator.Enabled = true;
            btnStopInitiator.Enabled = false;

            foreach (DataGridViewRow curRow in dgvOutbound.Rows)
                if (_dcSessionRow.ContainsKey((string)curRow.Cells[__GridColumnName].Value))
                    lock (_dcSessionRow)
                        _dcSessionRow.Remove((string)curRow.Cells[__GridColumnName].Value);

            dgvOutbound.Rows.Clear();            
            _oRouter.InitiatorStop();
        }
        private void dgvOutbound_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == __GridColumnEnable) 
                _oRouter.EnableOutbound((string)dgvOutbound[__GridColumnName, e.RowIndex].Value);
            else if (e.ColumnIndex == __GridColumnDisable)
                _oRouter.DisableOutbound((string)dgvOutbound[__GridColumnName, e.RowIndex].Value);
        }
        private void btnEnableOutboundSessions_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow curRow in dgvOutbound.Rows)
                  _oRouter.EnableOutbound((string)curRow.Cells[__GridColumnName].Value);
        }
        private void btnDisableOutboundSessions_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow curRow in dgvOutbound.Rows)
                _oRouter.DisableOutbound((string)curRow.Cells[__GridColumnName].Value);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeStartOption(checkBox1.Checked);
        }

        private void ChangeStartOption(bool aux)
        {
            if (aux)
            {
                StreamWriter sw = new StreamWriter(ConfigurationFile);
                sw.WriteLine("true");
                sw.Close();
            }
            else
            {
                StreamWriter sw = new StreamWriter(ConfigurationFile);
                sw.WriteLine("false");
                sw.Close();
            }
        }

        private void CheckStartOption()
        {
            if (!Directory.Exists(ConfigurationFolder))
                Directory.CreateDirectory(ConfigurationFolder);

            if (File.Exists(ConfigurationFile))
            {
                StreamReader sr = new StreamReader(ConfigurationFile);
                string tempLine = "";
                while ((tempLine = sr.ReadLine()) != null)
                {
                    if (tempLine == "true") AutoStart = true; else AutoStart = false;
                }
                sr.Close();
            }
            else
            {
                File.Create(ConfigurationFile).Close();
                StreamWriter sw = new StreamWriter(ConfigurationFile, true);
                sw.WriteLine("false");
                sw.Close();
                AutoStart = false;
            }
            if (AutoStart)
            {
                checkBox1.Checked = true;
                btnStartAcceptor_Click(this, new EventArgs());
                System.Threading.Thread.Sleep(1000);
                btnStartInitiator_Click(this, new EventArgs());
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void frmRouter_Load(object sender, EventArgs e)
        {
            CheckStartOption();
        }
    }
}
