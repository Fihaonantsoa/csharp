using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservation
{
    public class HeaderContainer : Panel
    {
        public GradientButton btn1, btn2, btn3, btn4, btn5;
        public Panel Body;

        private readonly Color PrimaryColor = Color.FromArgb(33, 47, 61);
        private readonly Color SecondaryColor = Color.FromArgb(52, 73, 94);
        private readonly Color AccentColor = Color.FromArgb(41, 128, 185);
        private readonly Color SuccessColor = Color.FromArgb(46, 204, 113);
        private readonly Color DangerColor = Color.FromArgb(231, 76, 60);
        private readonly Color WarningColor = Color.FromArgb(241, 196, 15);
        public HeaderContainer()
        {
            InitializeUI();
        }
        public void InitializeUI()
        {
            TitlePanel();
        }

        public void TitlePanel()
        {
            Label Tlab1 = new Label
            {
                Dock = DockStyle.Top,
                Height = 30,
                Text = "Reservation avec assurance !",
                Font = new Font("Monotype Corsiva", 14, FontStyle.Regular),
                Padding = new Padding(30, 0, 0, 0),
                ForeColor = Color.White,
                BackColor = AccentColor,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Panel main = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.Transparent,
                Padding = new Padding(100, 0, 70, 0)
            };

            Panel Lpanel1 = new Panel()
            {
                Dock = DockStyle.Left,
                Width = 500,
                BackColor = Color.Transparent,
                Padding = new Padding(20, 30, 20, 30)
            };

            Label labTile = new Label
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Text = "Reservation des billets de vol",
                ForeColor = AccentColor,
                Font = new Font("Monotype Corsiva", 24),
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 450, 50), 10))
            };

            Panel Bpanel1 = new Panel
            {
                Dock = DockStyle.Right,
                BackColor = Color.Transparent,
                Width = 1100,
                Padding = new Padding(0, 30, 50, 30),
            };

            btn1 = CreateActionButton("Accueil", new Size(200, 0), AccentColor, (s, e) => Action());
            Button i1 = espace();
            btn2 = CreateActionButton("Passager", new Size(200, 0), AccentColor, (s, e) => OpenPass());
            Button i2 = espace();
            btn3 = CreateActionButton("Paiement", new Size(200, 0), AccentColor, (s, e) => OpenPaie());
            Button i3 = espace();
            btn4 = CreateActionButton("Vol", new Size(200, 0), AccentColor, (s, e) => OpenVol());
            Button i4 = espace();
            btn5 = CreateActionButton("Reservation", new Size(200, 0), AccentColor, (s, e) => OpenRes());

            Bpanel1.Controls.AddRange(new[] { btn1, i1, btn2, i2, btn3, i3, btn4, i4, btn5 });

            main.Controls.Add(Bpanel1);
            Lpanel1.Controls.Add(labTile);
            main.Controls.Add(Lpanel1);
            this.Controls.Add(main);
            this.Controls.Add(Tlab1);
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
        public void Action()
        {
            AccueilFrame mainFrame = new AccueilFrame();
            mainFrame.Visible = Visible;
            this.Parent.Hide();
        }
        private void OpenVol()
        {
            VolForm vol = new VolForm();
            vol.Visible = Visible;
            this.Parent.Hide();
        }
        private void OpenPass()
        {
            PassagerForm passager = new PassagerForm();
            passager.Visible = Visible;
            this.Parent.Hide();
        }
        private void OpenRes()
        {
            ResForm res = new ResForm();
            res.Visible = Visible;
            this.Parent.Hide();
        }
        private void OpenBillet()
        {
            BilletForm billet = new BilletForm();
            billet.Visible = Visible;
            this.Parent.Hide();
        }
        private void OpenPaie()
        {
            PaieForm paie = new PaieForm();
            paie.Visible = Visible;
            this.Parent.Hide();
        }
    }
}
