using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CritterSimulator.CritterStorage.Premade;

namespace CritterSimulator.Game
{
    // Class CritterFrame provides the user interface for a simple simulation program.
    public class CritterFrame : Form
    {
        private CritterModel myModel;
        private CritterPanel myPicture;
        private Timer myTimer;
        private Button[] counts;
        private Button countButton;
        private bool started;
        private static bool created;

        public CritterFrame(int width, int height)
        {
            // This prevents someone from trying to create their own copy of
            // the GUI components
            if (created)
                throw new InvalidOperationException("Only one world allowed");
            created = true;

            // Create frame and model
            Text = "EMBO critter simulation";
            FormBorderStyle = FormBorderStyle.Sizable;
            myModel = new CritterModel(width, height);

            // Set up critter picture panel
            myPicture = new CritterPanel(myModel);
            myPicture.Dock = DockStyle.Fill;
            Controls.Add(myPicture);

            AddTimer();
            ConstructSouth();

            // Initially it has not started
            started = false;
        }

        // Construct the controls and label for the southern panel
        private void ConstructSouth()
        {
            // Add timer controls to the south
            FlowLayoutPanel p = new FlowLayoutPanel();
            p.Dock = DockStyle.Bottom;
            p.AutoSize = true;
            p.WrapContents = false;

            TrackBar slider = new TrackBar();
            slider.Minimum = 0;
            slider.Maximum = 100;
            slider.Value = 20;
            slider.Width = 150;
            slider.Scroll += (sender, e) =>
            {
                double ratio = 1000.0 / (1 + Math.Pow(slider.Value, 0.3));
                myTimer.Interval = Math.Max(1, (int)(ratio - 180));
            };

            Label slowLabel = new Label();
            slowLabel.Text = "slow";
            slowLabel.AutoSize = true;

            Label fastLabel = new Label();
            fastLabel.Text = "fast";
            fastLabel.AutoSize = true;

            p.Controls.Add(slowLabel);
            p.Controls.Add(slider);
            p.Controls.Add(fastLabel);

            Button b1 = new Button();
            b1.Text = "start";
            b1.Click += (sender, e) => { myTimer.Start(); };
            p.Controls.Add(b1);

            Button b2 = new Button();
            b2.Text = "stop";
            b2.Click += (sender, e) => { myTimer.Stop(); };
            p.Controls.Add(b2);

            Button b3 = new Button();
            b3.Text = "step";
            b3.Click += (sender, e) => { DoOneStep(); };
            p.Controls.Add(b3);

            // Add debug button
            Button b4 = new Button();
            b4.Text = "debug";
            b4.Click += (sender, e) =>
            {
                myModel.ToggleDebug();
                myPicture.Invalidate();
            };
            p.Controls.Add(b4);

            // Add 100 button
            Button b5 = new Button();
            b5.Text = "next 100";
            b5.Click += (sender, e) => { Multistep(100); };
            p.Controls.Add(b5);

            Controls.Add(p);
        }

        // Starts the simulation... assumes all critters have already been added
        public void Start()
        {
            // Don't let anyone start a second time and remember if we have started
            if (started)
            {
                return;
            }
            // If they didn't add any critters, then nothing to do
            var countsEnumerable = myModel.GetCounts();
            bool hasCounts = false;
            foreach (var _ in countsEnumerable)
            {
                hasCounts = true;
                break;
            }
            if (!hasCounts)
            {
                Console.WriteLine("nothing to simulate--no critters");
                return;
            }
            started = true;
            AddClassCounts();
            myModel.UpdateColorString();
            AutoSize = true;
            Show();
        }

        // Add right-hand column showing how many of each critter are alive
        private void AddClassCounts()
        {
            var entries = new List<KeyValuePair<string, int>>(myModel.GetCounts());
            TableLayoutPanel p = new TableLayoutPanel();
            p.ColumnCount = 1;
            p.RowCount = entries.Count + 1;
            p.Dock = DockStyle.Right;
            p.AutoSize = true;

            counts = new Button[entries.Count];
            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = new Button();
                counts[i].AutoSize = true;
                p.Controls.Add(counts[i]);
            }

            // Add simulation count
            countButton = new Button();
            countButton.ForeColor = Color.Blue;
            countButton.AutoSize = true;
            p.Controls.Add(countButton);

            Controls.Add(p);
            SetCounts();
        }

        private void SetCounts()
        {
            int i = 0;
            int max = 0;
            int maxI = 0;
            foreach (var entry in myModel.GetCounts())
            {
                string s = string.Format("{0} ={1,4}", entry.Key, entry.Value);
                counts[i].Text = s;
                counts[i].ForeColor = Color.Black;
                if (entry.Value > max)
                {
                    max = entry.Value;
                    maxI = i;
                }
                i++;
            }
            counts[maxI].ForeColor = Color.Red;
            string stepText = string.Format("step ={0,5}", myModel.SimulationCount);
            countButton.Text = stepText;
        }

        // Add a certain number of critters of a particular class to the simulation
        public void Add(int number, Type critterType)
        {
            // Don't let anyone add critters after simulation starts
            if (started)
            {
                return;
            }
            // Temporarily turning on started flag prevents critter constructors from calling add
            started = true;
            myModel.Add(number, critterType);
            started = false;
        }

        // Creates a timer that calls the model's update method and repaints the display
        private void AddTimer()
        {
            myTimer = new Timer();
            myTimer.Interval = 100;
            myTimer.Tick += (sender, e) => { DoOneStep(); };
        }

        // One step of the simulation
        private void DoOneStep()
        {
            myModel.Update();
            SetCounts();
            myPicture.Invalidate();
        }

        // Advance the simulation until step % n is 0
        private void Multistep(int n)
        {
            myTimer.Stop();
            do
            {
                myModel.Update();
            } while (myModel.SimulationCount % n != 0);
            SetCounts();
            myPicture.Invalidate();
        }
    }
}
