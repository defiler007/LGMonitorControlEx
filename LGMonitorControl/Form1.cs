using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace LGMonitorControl
{
    public partial class Form1 : Form
    {
        private MonitorManager _monitorManager;
        private List<MonitorData> _monitors = new List<MonitorData>();

        public Form1()
        {
            InitializeComponent();

            _monitorManager = new MonitorManager();
            _monitorManager.Initialize();

            _monitors = _monitorManager.Monitors
                .SelectMany(a => a.physicalMonitors.Select(b => new MonitorData(b, a.rect)))
                .OrderBy(r => r.PositionX)
                .ToList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSettings();
            LoadDataGridView();

            backgroundWorker1.RunWorkerAsync();
        }

        private void LoadSettings()
        {
            Settings.Instance.Load();

            if (_monitorManager.GetCurrentGameMode(_monitors, out LG.GameMode.Modes mode))
            {
                LG.GameMode.currentMode = mode;
            }

            if (Settings.Instance.StartMinimized) this.WindowState = FormWindowState.Minimized;
            checkBox_Minimized.Checked = Settings.Instance.StartMinimized;
            comboBox_Defaultmode.DataSource = Enum.GetValues(typeof(LG.GameMode.Modes));
            comboBox_Defaultmode.SelectedItem = Settings.Instance.DefaultMode;
            checkBox_Autostart.Checked = Settings.Instance.GetAutostartState();

            // 1. Apply saved window size
            if (Settings.Instance.WindowWidth > 0 && Settings.Instance.WindowHeight > 0)
            {
                this.Width = Settings.Instance.WindowWidth;
                this.Height = Settings.Instance.WindowHeight;
            }

            // 2. Apply saved window position
            if (Settings.Instance.WindowX != -10000 && Settings.Instance.WindowY != -10000)
            {
                System.Drawing.Point savedLocation = new System.Drawing.Point(Settings.Instance.WindowX, Settings.Instance.WindowY);

                // Verify the saved location is actually visible on an active screen 
                // (prevents the app from getting lost if you unplugged a monitor)
                bool isVisible = false;
                foreach (Screen screen in Screen.AllScreens)
                {
                    if (screen.WorkingArea.Contains(savedLocation))
                    {
                        isVisible = true;
                        break;
                    }
                }

                if (isVisible)
                {
                    this.StartPosition = FormStartPosition.Manual; // Tells Windows not to override our custom location
                    this.Location = savedLocation;
                }
            }

        }

        private void LoadDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DataSource = Settings.Instance.Applications;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;

            WindowName.DataPropertyName = "WindowName";
            WindowName.SortMode = DataGridViewColumnSortMode.Automatic;

            Mode.DataSource = Enum.GetValues(typeof(LG.GameMode.Modes));
            Mode.ValueType = typeof(LG.GameMode.Modes);
            Mode.DataPropertyName = "GameMode";
            Mode.SortMode = DataGridViewColumnSortMode.Automatic;

            // 1. Make the 'Mode' dropdown column exactly wide enough to fit its text contents
            Mode.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // 2. Make the 'WindowName' text column fill all remaining horizontal space
            WindowName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Add Drag and Drop Functionality Setup
            dataGridView1.AllowDrop = true;
            dataGridView1.DragEnter += DataGridView1_DragEnter;
            dataGridView1.DragDrop += DataGridView1_DragDrop;

            if (!string.IsNullOrEmpty(Settings.Instance.LastSortColumn) && Settings.Instance.LastSortOrder != SortOrder.None)
            {
                if (dataGridView1.Columns.Contains(Settings.Instance.LastSortColumn))
                {
                    DataGridViewColumn sortColumn = dataGridView1.Columns[Settings.Instance.LastSortColumn];
                    ListSortDirection direction = Settings.Instance.LastSortOrder == SortOrder.Descending
                        ? ListSortDirection.Descending
                        : ListSortDirection.Ascending;

                    dataGridView1.Sort(sortColumn, direction);
                }
            }
        }

        private void DataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                // Ensure at least one file dropped is an executable
                if (files.Any(f => Path.GetExtension(f).Equals(".exe", StringComparison.OrdinalIgnoreCase)))
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void DataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                bool addedNew = false;

                foreach (string file in files)
                {
                    if (Path.GetExtension(file).Equals(".exe", StringComparison.OrdinalIgnoreCase))
                    {
                        string exeName = Path.GetFileName(file);

                        // Fallback to default user preference modes
                        ApplicationData newApp = new ApplicationData(exeName, Settings.Instance.DefaultMode);
                        Settings.Instance.Applications.Add(newApp);
                        addedNew = true;
                    }
                }

                if (addedNew)
                {
                    Settings.Instance.Save();
                    dataGridView1.Refresh();
                }
            }
        }

        private void AddNewApplication()
        {
            ApplicationData newApp = new ApplicationData("<Enter Window Name or Drop Exe>", LG.GameMode.Modes.SRGB);
            Settings.Instance.Applications.Add(newApp);
        }

        private void RemoveApplication()
        {
            if (Settings.Instance.Applications.Count > 0)
            {
                try
                {
                    Settings.Instance.Applications.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                }
                catch
                {
                    Settings.Instance.Applications.RemoveAt(0);
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (true)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    var activeApp = WindowScanner.GetActiveWindowInfo();
                    bool foundWindow = false;

                    if ((activeApp.WindowTitle != null || activeApp.ExeName != null) && _monitors != null && _monitors.Count > 0)
                    {
                        foreach (var app in Settings.Instance.Applications)
                        {
                            // 1. Matches Title (Case Insensitive)
                            bool matchesTitle = !string.IsNullOrWhiteSpace(app.WindowName) &&
                                                activeApp.WindowTitle != null &&
                                                activeApp.WindowTitle.IndexOf(app.WindowName, StringComparison.OrdinalIgnoreCase) >= 0;

                            // 2. Matches Executable Exact Name (e.g. "chrome.exe")
                            bool matchesExe = !string.IsNullOrWhiteSpace(app.WindowName) &&
                                              activeApp.ExeName != null &&
                                              activeApp.ExeName.Equals(app.WindowName, StringComparison.OrdinalIgnoreCase);

                            if (matchesTitle || matchesExe)
                            {
                                foundWindow = true;
                                _monitorManager.ChangeGameMode(_monitors, app.GameMode);
                                break;
                            }
                        }

                        if (!foundWindow)
                        {
                            _monitorManager.ChangeGameMode(_monitors, Settings.Instance.DefaultMode);
                        }
                    }
                }
                System.Threading.Thread.Sleep(100);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
            this.Activate();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddNewApplication();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            RemoveApplication();
        }

        private void checkBox_Autostart_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Autostart.Checked)
            {
                Settings.Instance.RegisterInStartup(true);
            }
            else
            {
                Settings.Instance.RegisterInStartup(false);
            }
        }


        private void checkBox_Minimized_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.StartMinimized = checkBox_Minimized.Checked;
            Settings.Instance.Save();
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell is DataGridViewComboBoxCell)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dataGridView1.EndEdit();
            }

            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewComboBoxCell)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dataGridView1.BeginEdit(true);
                ((ComboBox)dataGridView1.EditingControl).DroppedDown = true;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell)dataGridView1.Rows[e.RowIndex].Cells[1];
            if (cb.Value != null)
            {
                dataGridView1.Invalidate();
                Settings.Instance.Applications[e.RowIndex].GameMode = (LG.GameMode.Modes)cb.Value;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save the window size and location only if it is in a normal state
            // (We don't want to save X/Y/Width/Height as 0 if the app is closed while minimized)
            if (this.WindowState == FormWindowState.Normal)
            {
                Settings.Instance.WindowWidth = this.Width;
                Settings.Instance.WindowHeight = this.Height;
                Settings.Instance.WindowX = this.Location.X;
                Settings.Instance.WindowY = this.Location.Y;
            }

            if (dataGridView1.SortedColumn != null)
            {
                Settings.Instance.LastSortColumn = dataGridView1.SortedColumn.Name;
                Settings.Instance.LastSortOrder = dataGridView1.SortOrder;
            }
            else
            {
                Settings.Instance.LastSortColumn = string.Empty;
                Settings.Instance.LastSortOrder = SortOrder.None;
            }

            Settings.Instance.Save();
        }


        private void comboBox_Defaultmode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Settings.Instance.DefaultMode = (LG.GameMode.Modes)comboBox_Defaultmode.SelectedItem;
            Settings.Instance.Save();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Settings.Instance.Save();
        }
    }
}
