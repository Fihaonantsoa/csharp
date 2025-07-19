using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservation
{
    public class AccueilFrame : Form
    {
        private Button btn6, btn7, btn8, btn9;
        public HeaderContainer HeaderPanel;

        private readonly Color PrimaryColor = Color.FromArgb(33, 47, 61);
        private readonly Color SecondaryColor = Color.FromArgb(52, 73, 94);
        private readonly Color AccentColor = Color.FromArgb(41, 128, 185);
        private readonly Color SuccessColor = Color.FromArgb(46, 204, 113);
        private readonly Color DangerColor = Color.FromArgb(231, 76, 60);
        private readonly Color WarningColor = Color.FromArgb(241, 196, 15);
        public AccueilFrame()
        {
            this.Text = "Gestion de Reservation de billet d'avion";
            this.Size = new Size(1200, 700);
            this.MinimumSize = new Size(900, 850);
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10);
            this.DoubleBuffered = true;
            InitialiUI();
            MainPanel();
        }
        private void InitialiUI()
        {
            HeaderPanel = new HeaderContainer
            {
                Dock = DockStyle.Top,
                Height = 150
            };
            this.Controls.Add(HeaderPanel);
        }
        private void MainPanel()
        {
            Panel corps = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AccentColor,
                Padding = new Padding(0, 150, 0, 0)
            };

            Panel Body = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AccentColor,
                Padding = new Padding(600, 20, 0, 0)
            };

            Image img1 = Properties.Resources.fly;
            Image img2 = Properties.Resources.avion;
            Image img3 = Properties.Resources.payer;
            Panel content = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
            };
            Label llab = new Label
            {
                Text = "Reservation des billets pour une vol avec assurance vers votre Déstination",
                Font = new Font("Monotype Corsiva", 18, FontStyle.Regular),
                ForeColor = AccentColor,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            content.Controls.Add(llab);
            Panel foot = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
            };


            Panel Bpanel1 = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Width = 1100,
                Padding = new Padding(0, 30, 50, 30),
            };

            btn6 = CreateActionButton("Gestion des Billets", new Size(390, 0), AccentColor, (s, e) => OpenBillet());
            Button i1 = espace();
            btn7 = CreateActionButton("Vue des Billets", new Size(390, 0), AccentColor, (s, e) => VueBillet());
            Button i2 = espace();
            btn8 = CreateActionButton("Effectuer une reservation", new Size(390, 0), AccentColor, (s, e) => Action());
            Button i3 = espace();
            btn9 = CreateActionButton("Annuler une reservation", new Size(390, 0), AccentColor, (s, e) => Action());
            btn9.BackColor = Color.Red;

            Bpanel1.Controls.AddRange(new[] { btn6, i1, btn7, i2, btn8, i3, btn9 });
            foot.Controls.Add(Bpanel1);
            CreatePanel(Body, content, new Point(118, 20), new Size(1685, 70));
            CreateImage(Body, img1, new Point(120, 120), new Size(500, 500));
            CreateImage(Body, img2, new Point(660, 120), new Size(600, 500));
            CreateImage(Body, img3, new Point(1300, 120), new Size(500, 500));
            CreatePanel(Body, foot, new Point(118, 650), new Size(1685, 150));

            corps.Controls.Add(Body);
            this.Controls.Add(corps);
        }
        public Button espace()
        {
            Button p = new Button();
            p.Size = new Size(10, 0);
            p.Dock = DockStyle.Right;
            p.BackColor = Color.Transparent;
            p.FlatStyle = FlatStyle.Flat;
            p.FlatAppearance.BorderSize = 0;
            p.Enabled = false;
            return p;
        }
        public GradientButton CreateActionButton(string text, Size taille, Color color, EventHandler handler)
        {
            var btn = new GradientButton
            {
                Text = text,
                Dock = DockStyle.Right,
                Height = 40,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                MinimumSize = taille,
                Margin = new Padding(0, 0, 0, 20)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += handler;
            btn.Cursor = Cursors.Hand;
            return btn;
        }
        public void CreatePanel(Panel parent, Panel content, Point location, Size size)
        {
            int borderRadius = 20;
            int shadowOffset = 5;

            Panel shadowPanel = new Panel
            {
                Size = size,
                Location = new Point(location.X + shadowOffset, location.Y + shadowOffset),
                BackColor = Color.FromArgb(50, 0, 0, 0),
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, size.Width, size.Height), borderRadius))
            };

            Panel mainPanel = new Panel
            {
                Size = size,
                Location = location,
                BackColor = Color.White,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, size.Width, size.Height), borderRadius))
            };

            Panel PanelP = new Panel
            {
                Dock = DockStyle.Fill
            };
            PanelP.Controls.Add(content);
            mainPanel.Controls.Add(PanelP);
            parent.Controls.Add(mainPanel);
            parent.Controls.Add(shadowPanel);
        }
        public void CreateImage(Panel parent, Image image, Point location, Size size)
        {
            int borderRadius = 20;
            int shadowOffset = 5;

            Panel shadowPanel = new Panel
            {
                Size = size,
                Location = new Point(location.X + shadowOffset, location.Y + shadowOffset),
                BackColor = Color.FromArgb(50, 0, 0, 0),
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, size.Width, size.Height), borderRadius))
            };

            Panel mainPanel = new Panel
            {
                Size = size,
                Location = location,
                BackColor = Color.White,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, size.Width, size.Height), borderRadius))
            };

            PictureBox picture = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Dock = DockStyle.Fill
            };

            mainPanel.Controls.Add(picture);
            parent.Controls.Add(mainPanel);
            parent.Controls.Add(shadowPanel);
        }
        public GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter - 1, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter - 1, bounds.Bottom - diameter - 1, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter - 1, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
        private void OpenBillet()
        {
            BilletForm billet = new BilletForm();
            billet.Visible = Visible;
            this.Hide();
        }
        private void VueBillet()
        {
            VueBillet billet = new VueBillet();
            billet.Visible = Visible;
            this.Hide();
        }
        public void Action()
        {
            return;
        }
    }
}
