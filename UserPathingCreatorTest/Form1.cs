using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace UserPathingCreatorTest
{
    public partial class Form1 : Form
    {
        private int borderSize = 2;
        private List<PathPoint> pathPoints = new List<PathPoint>();
        private List<PathPoint> pinnedPoints = new List<PathPoint>();
        private ListBox pinnedListBox = new ListBox();
        private Label coordLabel = new Label();
        private Panel pinnedPanel = new Panel();
        private Button toggleListButton = new Button();
        private Button toggleZButton = new Button();
        private TextBox zInputBox = new TextBox();
        private bool zAxisEnabled = false;
<<<<<<< HEAD
        private bool waitingForZClick = false;
        private Button setZButton = new Button();

        private Stack<List<PathPoint>> undoStack = new Stack<List<PathPoint>>();
        private Stack<List<PathPoint>> redoStack = new Stack<List<PathPoint>>();
=======


        private Stack<List<PathPoint>> undoStack = new Stack<List<PathPoint>>();
        private Stack<List<PathPoint>> redoStack = new Stack<List<PathPoint>>();

>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979

        // Zoom-related variables
        private float zoomLevel = 1.0f;
        private const float zoomStep = 0.1f;
        private float zoomMin = 1.0f;
        private const float zoomMax = 3.0f;
        public Form1()
        {
            InitializeComponent();

<<<<<<< HEAD
=======

>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
            undoButton = new Button();
            redoButton = new Button();
            new_file_button.Click += new_file_button_Click;
            save_button.Click += save_file_button_Click;
            load_file_button.Click += load_file_button_Click;
<<<<<<< HEAD
            toggleZButton.Text = "Enable Z Axis";
            toggleZButton.Width = 120;
            toggleZButton.Height = 30;
            toggleZButton.Location = new Point(10, 10);
            toggleZButton.Click += button1_Click_3;
            this.Controls.Add(toggleZButton);
=======
            // Button to toggle the Z-axis
            toggleZButton.Text = "Enable Z-Axis";
            toggleZButton.Width = 120;
            toggleZButton.Height = 30;
            toggleZButton.Location = new Point(10, redoButton.Bottom + 10); // Adjust location as needed
            toggleZButton.Click += (s, e) =>
            {
                zAxisEnabled = !zAxisEnabled;
                toggleZButton.Text = zAxisEnabled ? "Disable Z-Axis" : "Enable Z-Axis";
                zInputBox.Visible = zAxisEnabled;
            };
            this.Controls.Add(toggleZButton);

            // Z-axis input box
>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
            zInputBox.Width = 60;
            zInputBox.Height = 25;
            zInputBox.Location = new Point(10, toggleZButton.Bottom + 5);
            zInputBox.Visible = false;
            zInputBox.Text = "0";
            this.Controls.Add(zInputBox);
<<<<<<< HEAD
            setZButton.Text = "Set Z Value";
            setZButton.Width = 120;
            setZButton.Height = 30;
            setZButton.Location = new Point(10, zInputBox.Bottom + 10);
            setZButton.Visible = false;
=======
>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979

            setZButton.Click += (s, e) =>
            {
                waitingForZClick = true;
                MessageBox.Show("Click a point to set its Z value.", "Z Axis Input");
            };

<<<<<<< HEAD
            this.Controls.Add(setZButton);
=======

            // Enables double buffering
>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(127, 207, 120);

<<<<<<< HEAD
            // Label coordinates
=======
            // Coordinates label
>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
            coordLabel.AutoSize = true;
            coordLabel.Font = new Font("Segoe UI", 9);
            coordLabel.ForeColor = Color.Black;
            coordLabel.BackColor = Color.White;
            coordLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            coordLabel.TextAlign = ContentAlignment.TopRight;
            coordLabel.Location = new Point(this.Width - 150, 5);
            this.Controls.Add(coordLabel);

            this.Resize += (s, e) =>
            {
                coordLabel.Location = new Point(this.Width - coordLabel.Width - 20, 5);
                pathingCanvas.Invalidate();
            };

            // Setup pinned panel to be always visible
            pinnedPanel.Width = 200;
            pinnedPanel.Dock = DockStyle.Right;
            pinnedPanel.BackColor = Color.White;
            pinnedPanel.Visible = true;

<<<<<<< HEAD
            // Toggle button for pinned points
=======
            // Toggle button
>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
            toggleListButton.Text = "Points";
            toggleListButton.Height = 60;
            toggleListButton.Dock = DockStyle.Top;
            toggleListButton.FlatStyle = FlatStyle.Flat;
            toggleListButton.BackColor = Color.LightGray;
            toggleListButton.Click += (s, e) =>
            {
                pinnedListBox.Visible = !pinnedListBox.Visible;
            };

            // Pinned points
            pinnedListBox.Dock = DockStyle.Fill;
            pinnedListBox.Font = new Font("Segoe UI", 6f);
            pinnedListBox.Visible = true;

            // Lets the user edit the Z value manually
            ContextMenuStrip listBoxContextMenu = new ContextMenuStrip();
            ToolStripMenuItem editZMenuItem = new ToolStripMenuItem("Edit Z Value");
            editZMenuItem.Click += EditZMenuItem_Click;
            listBoxContextMenu.Items.Add(editZMenuItem);
            pinnedListBox.ContextMenuStrip = listBoxContextMenu;

            // Add to panel
            pinnedPanel.Controls.Add(pinnedListBox);
            pinnedPanel.Controls.Add(toggleListButton);
            this.Controls.Add(pinnedPanel);

            // Canvas setup
            pathingCanvas.Dock = DockStyle.Fill;
            pathingCanvas.BackColor = Color.White;
            pathingCanvas.Paint += PathingCanvas_Paint;
            pathingCanvas.MouseMove += PathingCanvas_MouseMove;
            pathingCanvas.MouseClick += PathingCanvas_MouseClick;
            pathingCanvas.MouseWheel += PathingCanvas_MouseWheel;
            pathingCanvas.Focus(); // ensures it gets scroll events

            // Force double buffering on canvas
            pathingCanvas.GetType().GetProperty("DoubleBuffered",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            ?.SetValue(pathingCanvas, true, null);
            this.Shown += (s, e) => pathingCanvas.Invalidate();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void save_file_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = "Save Path Points"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                List<string> lines = new List<string>();

                foreach (var item in pinnedListBox.Items)
                {
                    string[] coords = item.ToString().Split(',');
                    if (coords.Length == 2 &&
                        int.TryParse(coords[0], out int x) &&
                        int.TryParse(coords[1], out int y))
                    {
                        lines.Add($"{x} {y}");
                    }
                }

                File.WriteAllLines(sfd.FileName, lines);
                MessageBox.Show("File saved successfully!");
            }
        }
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
<<<<<<< HEAD
=======

>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
        private void Form1_Load(object sender, EventArgs e)
        {
            zoomMin = Math.Max((float)pathingCanvas.Width / 7924f, (float)pathingCanvas.Height / 1524f);
        }
        private void button1_Click(object sender, EventArgs e) { }
        private void button1_Click_1(object sender, EventArgs e) { }
<<<<<<< HEAD
=======
        private void button4_Click(object sender, EventArgs e) { }
>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
        private void button1_Click_2(object sender, EventArgs e) { }
        private void button1_Click_3(object sender, EventArgs e)
        {
            zAxisEnabled = !zAxisEnabled;
            button1.Text = zAxisEnabled ? "Disable Z axis" : "Enable Z axis";
            zInputBox.Visible = zAxisEnabled;
            setZButton.Visible = zAxisEnabled;
            UpdatePinnedPointsAndUI();

            if (zAxisEnabled)
            {
                MessageBox.Show("Left click a point to highlight and edit its Z value.", "Z Axis Enabled");
            }
        }
        private void button4_Click(object sender, EventArgs e) { }
        private void new_file_button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Create new file?\nThis will clear all current points.",
                "Confirm New File",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                pathPoints.Clear();
                pinnedPoints.Clear();
                pinnedListBox.Items.Clear();
                pathingCanvas.Invalidate();

                MessageBox.Show("New file started. All points cleared.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
<<<<<<< HEAD
=======

>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (pathPoints.Count > 0)
            {
                redoStack.Push(new List<PathPoint>(pathPoints));
                pathPoints = undoStack.Count > 0 ? undoStack.Pop() : new List<PathPoint>();
                UpdatePinnedPointsAndUI();
            }
        }
<<<<<<< HEAD
=======

>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
        private void RedoButton_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(new List<PathPoint>(pathPoints));
                pathPoints = redoStack.Pop();
                UpdatePinnedPointsAndUI();
            }
        }
<<<<<<< HEAD
=======

>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
        private void UpdatePinnedPointsAndUI()
        {
            pinnedPoints = new List<PathPoint>(pathPoints);
            pinnedListBox.Items.Clear();
            foreach (var point in pinnedPoints)
            {
<<<<<<< HEAD
                if (zAxisEnabled)
                    pinnedListBox.Items.Add($"{point.X},{point.Y},{point.Z}");
                else
                    pinnedListBox.Items.Add($"{point.X},{point.Y}");
            }
            pathingCanvas.Invalidate();
        }
=======
                pinnedListBox.Items.Add($"{point.X},{point.Y}");
            }
            pathingCanvas.Invalidate();
        }

>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
        private void load_file_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = "Load Path Points"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pathPoints.Clear();
                pinnedPoints.Clear();
                pinnedListBox.Items.Clear();

                string[] lines = File.ReadAllLines(ofd.FileName);
                foreach (string line in lines)
                {
                    string[] parts = line.Trim().Split(' ');
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0], out int x) &&
                        int.TryParse(parts[1], out int y))
                    {
                        PathPoint point = new PathPoint { X = x, Y = y, Action = "Added" };
                        pathPoints.Add(point);
                        pinnedPoints.Add(point);
                        pinnedListBox.Items.Add($"{x},{y}");
                    }
                }

                pathingCanvas.Invalidate();
                MessageBox.Show("File loaded successfully!");
            }
        }
<<<<<<< HEAD
=======

>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e) { }
        private void PathingCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Control canvas = sender as Control;
            if (canvas == null) return;

            g.Clear(Color.White);

            Pen axisPen = new Pen(Color.Black, 2);
            Pen gridPen = new Pen(Color.LightGray, 1);
            int spacing = (int)(20 * zoomLevel); // Adjust spacing based on zoom

            int centerX = canvas.Width / 2;
            int centerY = canvas.Height / 2;
<<<<<<< HEAD
            int logicalGridWidth = 7924;
            int logicalGridHeight = 1524;
            int halfWidth = (int)(logicalGridWidth / 2 * zoomLevel);
            int halfHeight = (int)(logicalGridHeight / 2 * zoomLevel);

            // Defines the 2D grid to be 7924 x 1524 units
            int maxGridWidth = (int)(7924 * zoomLevel);
            int maxGridHeight = (int)(1524 * zoomLevel);

            for (int x = 0; x <= canvas.Width; x += spacing)
                g.DrawLine(gridPen, x, 0, x, canvas.Height);

            for (int y = 0; y <= canvas.Height; y += spacing)
                g.DrawLine(gridPen, 0, y, canvas.Width, y);

            int dotSize = (int)(10 * zoomLevel); // Adjust dot size based on zoom
=======
>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979

            int logicalGridWidth = 7924;
            int logicalGridHeight = 1524;

            int halfWidth = (int)(logicalGridWidth / 2 * zoomLevel);
            int halfHeight = (int)(logicalGridHeight / 2 * zoomLevel);

            // Draw fixed grid up to 7924 x 1524 logical units
            int maxGridWidth = (int)(7924 * zoomLevel);
            int maxGridHeight = (int)(1524 * zoomLevel);

            for (int x = 0; x <= maxGridWidth; x += spacing)
                g.DrawLine(gridPen, x, 0, x, maxGridHeight);

            for (int y = 0; y <= maxGridHeight; y += spacing)
                g.DrawLine(gridPen, 0, y, maxGridWidth, y);


            int dotSize = (int)(10 * zoomLevel); // Adjust dot size based on zoom

            foreach (var point in pathPoints)
            {
                int x = (int)(point.X * zoomLevel);
                int y = (int)(point.Y * zoomLevel);

                if (x < 0 || x >= canvas.Width || y < 0 || y >= canvas.Height)
                    continue;

                using (Font emojiFont = new Font("Segoe UI Emoji", 12 * zoomLevel, GraphicsUnit.Pixel))
                {
                    g.DrawString("🌿", emojiFont, Brushes.Black, x - dotSize / 2, y - dotSize / 2);
                }

            }
        }
        private void PathingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            int centerX = pathingCanvas.Width / 2;
            int centerY = pathingCanvas.Height / 2;
            bool hovering = false;

            foreach (var point in pathPoints)
            {
                int px = (int)(point.X * zoomLevel);
                int py = (int)(point.Y * zoomLevel);

                Rectangle hitbox = new Rectangle(px - 5, py - 5, 10, 10);

                if (hitbox.Contains(e.Location))
                {
                    coordLabel.Text = "(" + point.X.ToString("F5") + ", " + point.Y.ToString("F5") + ")";
                    coordLabel.Location = new Point(this.Width - coordLabel.Width - 20, 5);
                    hovering = true;
                    break;
                }
            }

            if (!hovering)
                coordLabel.Text = string.Empty;
        }
        private void PathingCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            int spacing = (int)(20 * zoomLevel);
            int logicalX = ((int)(e.X / zoomLevel) / 20) * 20;
            int logicalY = ((int)(e.Y / zoomLevel) / 20) * 20;

<<<<<<< HEAD
            if (waitingForZClick && e.Button == MouseButtons.Left)
            {
                foreach (var point in pathPoints)
                {
                    int px = (int)(point.X * zoomLevel);
                    int py = (int)(point.Y * zoomLevel);
                    Rectangle hitbox = new Rectangle(px - 5, py - 5, 10, 10);

                    if (hitbox.Contains(e.Location))
                    {
                        string input = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter Z value (0 or higher):", "Set Z Value", point.Z.ToString());

                        if (int.TryParse(input, out int zValue) && zValue >= 0)
                        {
                            point.Z = zValue;
                            UpdatePinnedPointsAndUI();
                        }
                        else
                        {
                            MessageBox.Show("Invalid input. Must be a whole number 0 or higher.");
                        }
                        waitingForZClick = false;
                        return;
                    }
                }

                MessageBox.Show("No point clicked. Try again.");
                waitingForZClick = false;
                return;
            }
=======
            int spacing = (int)(20 * zoomLevel);
            int logicalX = ((int)((e.X - centerX) / zoomLevel) / 20) * 20;
            int logicalY = ((int)((centerY - e.Y) / zoomLevel) / 20) * 20;

>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979

            if (e.Button == MouseButtons.Right)
            {
                // Right-click: check for a nearby dot and remove it
                for (int i = 0; i < pathPoints.Count; i++)
                {
                    int px = (int)(pathPoints[i].X * zoomLevel);
                    int py = (int)(pathPoints[i].Y * zoomLevel);

                    undoStack.Push(new List<PathPoint>(pathPoints));
                    redoStack.Clear();
                    Rectangle hitbox = new Rectangle(px - 5, py - 5, 10, 10);
                    if (hitbox.Contains(e.Location))
                    {
                        var pointToRemove = pathPoints[i];
                        pathPoints.RemoveAt(i);

                        pinnedPoints.Remove(pointToRemove);
                        pinnedListBox.Items.Remove($"{pointToRemove.X},{pointToRemove.Y}");

                        pathingCanvas.Invalidate();
                        return;
                    }
                }
            }

            else if (e.Button == MouseButtons.Left)
            {
                if (pathPoints.Any(p => p.X == logicalX && p.Y == logicalY))
                {
                    MessageBox.Show("Point already exists at this location.", "Duplicate Point", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PathPoint newPoint = new PathPoint { X = logicalX, Y = logicalY, Action = "Added" };
                undoStack.Push(new List<PathPoint>(pathPoints));
                redoStack.Clear();
                pathPoints.Add(newPoint);
                pinnedPoints.Add(newPoint);
                pinnedListBox.Items.Add(zAxisEnabled ? $"{newPoint.X},{newPoint.Y},{newPoint.Z}" : $"{newPoint.X},{newPoint.Y}");
                pathingCanvas.Invalidate();
            }
        }
        private void PathingCanvas_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                zoomLevel = Math.Min(zoomMax, zoomLevel + zoomStep);
            else if (e.Delta < 0)
                zoomLevel = Math.Max(zoomMin, zoomLevel - zoomStep);

            pathingCanvas.Invalidate();
        }
<<<<<<< HEAD
        private void EditZMenuItem_Click(object sender, EventArgs e)
        {
            if (!zAxisEnabled || pinnedListBox.SelectedIndex == -1)
                return;
=======

        private void button1_Click_3(object sender, EventArgs e)
        {

        }
    }
>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979

            var selected = pinnedListBox.SelectedItem.ToString();
            var parts = selected.Split(',');

            if (parts.Length < 2 || !int.TryParse(parts[0], out int x) || !int.TryParse(parts[1], out int y))
                return;

            var point = pathPoints.FirstOrDefault(p => p.X == x && p.Y == y);
            if (point == null) return;

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                $"Set Z value for ({x},{y}):", "Edit Z", point.Z.ToString());

            if (int.TryParse(input, out int zValue) && zValue >= 0)
            {
                point.Z = zValue;
                UpdatePinnedPointsAndUI();
            }
            else
            {
                MessageBox.Show("Invalid Z value. Must be a whole number ≥ 0.");
            }
        }

    }
    public class PathPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
<<<<<<< HEAD
=======

>>>>>>> 9adea82d576d8207838175de6c52f4d82a2c8979
        public int Z { get; set; }
        public string Action { get; set; }
    }
}