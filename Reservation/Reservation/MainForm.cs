using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservation
{
    public class MainForm : Form
    {
        private readonly Color PrimaryColor = Color.FromArgb(33, 47, 61);
        private readonly Color SecondaryColor = Color.FromArgb(52, 73, 94);
        private readonly Color AccentColor = Color.FromArgb(41, 128, 185);
        private readonly Color SuccessColor = Color.FromArgb(46, 204, 113);
        private readonly Color DangerColor = Color.FromArgb(231, 76, 60);
        private readonly Color WarningColor = Color.FromArgb(241, 196, 15);
        public MainForm()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Gestion des Vols";
            this.Size = new Size(1200, 700);
            this.MinimumSize = new Size(900, 850);
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10);
            this.DoubleBuffered = true;
            MainPanel();
        }

        private void MainPanel()
        {
            Panel TitlePanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = SecondaryColor
            };


            Label titre = new Label
            {
                Text = "Reservation des billets d'avion",
                ForeColor = Color.White,
                Dock = DockStyle.Left,
                Size = new Size(450, 50),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                BackColor = Color.Transparent
            };

            TitlePanel.Controls.Add(titre);
            this.Controls.Add(TitlePanel);

            Panel corps = new Panel
            {
                Dock = DockStyle.Fill,
            };

            Panel SidePanel = new Panel
            {
                Width = 350,
                BackColor = SecondaryColor,
                Dock = DockStyle.Left,
                Padding = new Padding(0, 100, 0, 0),
            };


            Label formLabel = new Label
            {
                Text = "Menu de réservation",
                ForeColor = SecondaryColor,
                Dock = DockStyle.Top,
                Size = new Size(350, 50),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                BackColor = Color.WhiteSmoke
            };

            var buttonPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 280,
                Padding = new Padding(20, 100, 20, 0)
            };

            Button btnRes = CreateActionButton("Reservation", AccentColor, (s, e) => OpenRes());
            Button i1 = espace();
            Button btnPassager = CreateActionButton("Passager", AccentColor, (s, e) => OpenPass());
            Button i2 = espace();
            Button btnVol = CreateActionButton("Vol", AccentColor, (s, e) => OpenVol());
            Button i3 = espace();
            Button btnPaie = CreateActionButton("Paiement", AccentColor, (s, e) => OpenPaie());
            Button i4 = espace();
            Button btnBillet = CreateActionButton("Billet", AccentColor, (s, e) => OpenBillet());
            Button i5 = espace();
            Button btnQuit = CreateActionButton("Quitter", AccentColor, (s, e) => this.Close());

            buttonPanel.Controls.AddRange(new[] { btnQuit, i5, btnVol, i1, btnBillet, i2, btnPassager, i3, btnPaie, i4, btnRes });

            SidePanel.Controls.Add(formLabel);
            SidePanel.Controls.Add(buttonPanel);

            Panel Body = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(600, 150, 200, 100)
            };

            Panel imgPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Width = Body.Width - 200,
            };

            PictureBox picture = new PictureBox();
            picture.Image = Properties.Resources.avion;
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.Dock = DockStyle.Fill;

            imgPanel.Controls.Add(picture);

            Panel lPanel = new Panel
            {
                Dock = DockStyle.Top,
                BackColor = DangerColor,
            };

            Label imgLabel = new Label
            {
                Text = "Reservation des billets pour toutes les vols !",
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                Size = new Size(350, 50),
                MaximumSize = new Size(imgPanel.Width, 50),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                BackColor = SecondaryColor
            };

            lPanel.Controls.Add(imgLabel);

            Body.Controls.Add(imgLabel);
            Body.Controls.Add(imgPanel);
            corps.Controls.Add(SidePanel);
            corps.Controls.Add(Body);
            this.Controls.Add(corps);
        }

        private Button espace()
        {
            Button p = new Button();
            p.Size = new Size(100, 20);
            p.Dock = DockStyle.Top;
            p.BackColor = Color.Transparent;
            p.FlatStyle = FlatStyle.Flat;
            p.FlatAppearance.BorderSize = 0;
            p.Enabled = false;
            return p;
        }

        private Button CreateActionButton(string text, Color color, EventHandler handler)
        {
            var btn = new Button
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += handler;
            btn.Cursor = Cursors.Hand;
            return btn;
        }
        private void OpenVol()
        {
            VolForm vol = new VolForm();
            vol.Visible = Visible;
            this.Hide();
        }
        private void OpenPass()
        {
            PassagerForm passager = new PassagerForm();
            passager.Visible = Visible;
            this.Hide();
        }
        private void OpenRes()
        {
            ResForm res = new ResForm();
            res.Visible = Visible;
            this.Hide();
        }
        private void OpenBillet()
        {
            BilletForm billet = new BilletForm();
            billet.Visible = Visible;
            this.Hide();
        }
        private void OpenPaie()
        {
            PaieForm paie = new PaieForm();
            paie.Visible = Visible;
            this.Hide();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "MainForm";
            this.ResumeLayout(false);

        }
    }
}
