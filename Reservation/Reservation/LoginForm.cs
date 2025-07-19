using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reservation
{
    public class LoginForm : Form
    {
        private Label lblTitle;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private CheckBox chkShowPassword;
        private Button btnLogin;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Authentification";
            this.Size = new Size(450, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 245, 250);


            Panel container = new Panel
            {
                Size = new Size(360, 370),
                Location = new Point((this.ClientSize.Width - 360) / 2, 40),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(container);

            lblTitle = new Label
            {
                Text = "Authentification",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 80
            };
            container.Controls.Add(lblTitle);

            Label lblUser = CreateLabel("Nom d'utilisateur", 90);
            txtUsername = CreateTextBox(115);

            Label lblPass = CreateLabel("Mot de passe", 160);
            txtPassword = CreateTextBox(185);
            txtPassword.UseSystemPasswordChar = true;

            chkShowPassword = new CheckBox
            {
                Text = "Afficher le mot de passe",
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(40, 225)
            };
            chkShowPassword.CheckedChanged += (s, e) =>
            {
                txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
            };

            btnLogin = new Button
            {
                Text = "Se connecter",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(280, 45),
                Location = new Point(40, 270)
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click; 
            btnLogin.Cursor = Cursors.Hand;


            container.Controls.Add(lblUser);
            container.Controls.Add(txtUsername);
            container.Controls.Add(lblPass);
            container.Controls.Add(txtPassword);
            container.Controls.Add(chkShowPassword);
            container.Controls.Add(btnLogin);
        }

        private Label CreateLabel(string text, int y)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(40, y),
                Size = new Size(300, 25)
            };
        }

        private TextBox CreateTextBox(int y)
        {
            return new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(280, 30),
                Location = new Point(40, y)
            };
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (username == "admin" && password == "admin")
            {
                AccueilFrame accueil = new AccueilFrame();
                accueil.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
