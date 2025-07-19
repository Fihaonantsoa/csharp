using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Reservation
{
    public class PassagerForm : Form
    {
        private PassagerService service;
        private TextBox idtxt, nom, recherche, passeport, nationalite, tel;
        private Button btnAjouter, btnModifier, btnSupprimer, btnActualiser;
        private DataGridView dgPass;

        private readonly Color PrimaryColor = Color.FromArgb(33, 47, 61);
        private readonly Color SecondaryColor = Color.FromArgb(52, 73, 94);
        private readonly Color AccentColor = Color.FromArgb(41, 128, 185);
        private readonly Color SuccessColor = Color.FromArgb(46, 204, 113);
        private readonly Color DangerColor = Color.FromArgb(231, 76, 60);
        private readonly Color WarningColor = Color.FromArgb(241, 196, 15);
        public PassagerForm()
        {
            InitializeUI();
            service = new PassagerService();
            ChargerPassager();
            ViderChamps();
        }

        private void InitializeUI()
        {
            this.Text = "Gestion des Passagers";
            this.Size = new Size(1200, 700);
            this.MinimumSize = new Size(900, 850);
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);
            this.DoubleBuffered = true;
            MainPanel();

        }

        private void MainPanel()
        {
            Panel mainpanel = new Panel();
            mainpanel.Dock = DockStyle.Fill;

            //Le Panel du Haut

            Panel TitlePanel = new Panel();
            TitlePanel.Size = new Size(0, 100);
            TitlePanel.BackColor = SecondaryColor;
            TitlePanel.Dock = DockStyle.Top;

            Label titre = new Label
            {
                Text = "Gestion des Passagers",
                ForeColor = Color.White,
                Dock = DockStyle.Left,
                Size = new Size(350, 50),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                BackColor = Color.Transparent
            };

            Panel PbtnRetour = new Panel
            {
                Dock = DockStyle.Right,
                Width = 200,
                Padding = new Padding(0, 20, 20, 0)
            };

            Button retour = new Button
            {
                Text = "Retour à l'accueil",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = AccentColor,
                Dock = DockStyle.Fill,
                MaximumSize = new Size(0, 40),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                ForeColor = Color.White,
            };

            retour.Click += (s, e) => BackHome();
            retour.FlatAppearance.BorderSize = 0;

            PbtnRetour.Controls.Add(retour);
            TitlePanel.Controls.Add(PbtnRetour);
            TitlePanel.Controls.Add(titre);
            //Le Corps 

            Panel Body = new Panel();
            Body.Dock = DockStyle.Fill;
            Body.BackColor = Color.Transparent;


            //Gauche du corps
            Panel Gauche = new Panel();
            Gauche.Size = new Size(350, 0);
            Gauche.Dock = DockStyle.Left;
            Gauche.BackColor = SecondaryColor;


            Label formLabel = new Label
            {
                Text = "Formulaire du Passager",
                ForeColor = SecondaryColor,
                Dock = DockStyle.Top,
                Size = new Size(350, 50),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                BackColor = Color.WhiteSmoke
            };
            Gauche.Controls.Add(formLabel);

            Panel sideCenter = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SecondaryColor
            };

            int y = 80;
            int controlHeight = 75;

            idtxt = CreateFormControl("ID", ref y, controlHeight, sideCenter);
            nom = CreateFormControl("Nom", ref y, controlHeight, sideCenter);
            passeport = CreateFormControl("Passeport", ref y, controlHeight, sideCenter);
            nationalite = CreateFormControl("Nationalité", ref y, controlHeight, sideCenter);
            tel = CreateFormControl("Téléphone", ref y, controlHeight, sideCenter);

            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 280,
                Padding = new Padding(20, 0, 20, 0)
            };

            btnAjouter = CreateActionButton("Ajouter", AccentColor, (s, e) => Ajouter());
            Button i1 = espace();
            btnModifier = CreateActionButton("Modifier", AccentColor, (s, e) => Modifier());
            Button i2 = espace();
            btnSupprimer = CreateActionButton("Supprimer", AccentColor, (s, e) => Supprimer());
            Button i3 = espace();
            btnActualiser = CreateActionButton("Actualiser", AccentColor, (s, e) => Actualiser());

            buttonPanel.Controls.AddRange(new[] { btnActualiser, i1, btnSupprimer, i2, btnModifier, i3, btnAjouter });
            sideCenter.Controls.Add(buttonPanel);
            Gauche.Controls.Add(sideCenter);

            Panel Donnees = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 0, 0, 0)
            };

            Panel Rpanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(0, 10, 0, 0)
            };

            Panel respanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 800
            };

            Label reslabel = new Label
            {
                Text = "Recherche : ",
                ForeColor = Color.Black,
                Dock = DockStyle.Left,
                Size = new Size(100, 50),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            recherche = new TextBox
            {
                Width = 280,
                Height = 40,
                Dock = DockStyle.Left
            };
            recherche.TextChanged += (s, e) => Rechercher();

            respanel.Controls.Add(recherche);
            respanel.Controls.Add(reslabel);
            Rpanel.Controls.Add(respanel);

            Panel Dgpanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 50, 0, 0)
            };
            dgPass = new DataGridView
            {

                Width = Donnees.Width,
                Dock = DockStyle.Fill,
                BackgroundColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            dgPass.SelectionChanged += (s, e) => dgVol_selected(s, e);
            StyleDataGrid();

            Dgpanel.Controls.Add(dgPass);

            Donnees.Controls.Add(Rpanel);
            Donnees.Controls.Add(Dgpanel);

            Body.Controls.Add(Donnees);
            Body.Controls.Add(Gauche);
            mainpanel.Controls.Add(Body);
            mainpanel.Controls.Add(TitlePanel);
            this.Controls.Add(mainpanel);
        }

        private void StyleDataGrid()
        {
            dgPass.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgPass.ColumnHeadersHeight = 50;
            dgPass.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgPass.RowTemplate.Height = 40;
        }
        private void FormatDataGrid()
        {
            if (dgPass.Columns.Count > 0)
            {
                dgPass.Columns["id"].HeaderText = "ID";
                dgPass.Columns["nom"].HeaderText = "Nom";
                dgPass.Columns["passeport"].HeaderText = "Passeport";
                dgPass.Columns["nationalite"].HeaderText = "Nationalité";
                dgPass.Columns["telephone"].HeaderText = "Téléphone";
            }
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

        private void BackHome()
        {
            AccueilFrame main = new AccueilFrame();
            main.Visible = Visible;
            this.Hide();
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
        private TextBox CreateFormControl(string label, ref int y, int height, Panel parent)
        {
            var panel = new Panel
            {
                Location = new Point(20, y),
                Size = new Size(310, height)

            };

            var lbl = new Label
            {
                Text = label,
                Location = new Point(0, 0),
                ForeColor = Color.White,
                AutoSize = true
            };

            var txt = new TextBox
            {
                Location = new Point(0, 25),
                Width = 350
            };

            panel.Controls.Add(lbl);
            panel.Controls.Add(txt);
            parent.Controls.Add(panel);

            y += height;
            return txt;
        }
        private DateTimePicker CreateFormControlDate(string label, ref int y, int height, Panel parent)
        {
            var panel = new Panel
            {
                Location = new Point(20, y),
                Size = new Size(310, height)

            };

            var lbl = new Label
            {
                Text = label,
                Location = new Point(0, 0),
                ForeColor = Color.White,
                AutoSize = true
            };

            var txt = new DateTimePicker
            {
                Location = new Point(0, 25),
                Width = 350,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd HH:mm",
            };

            panel.Controls.Add(lbl);
            panel.Controls.Add(txt);
            parent.Controls.Add(panel);

            y += height;
            return txt;
        }
        private void dgVol_selected(object sender, EventArgs e)
        {
            RemplirForm();
            idtxt.Enabled = false;
        }

        private void RemplirForm()
        {
            if (dgPass.CurrentRow != null)
            {
                string id = dgPass.CurrentRow.Cells[0].Value?.ToString();
                string Nom = dgPass.CurrentRow.Cells[1].Value?.ToString();
                string Passeport = dgPass.CurrentRow.Cells[2].Value?.ToString();
                string Nationalite = dgPass.CurrentRow.Cells[3].Value?.ToString();
                string Tel = dgPass.CurrentRow.Cells[4].Value?.ToString();

                idtxt.Text = id;
                nom.Text = Nom;
                passeport.Text = Passeport;
                nationalite.Text = Nationalite;
                tel.Text = Tel;
            }
        }

        private Boolean VerifierChamps()
        {
            string res = "";
            if (string.IsNullOrEmpty(idtxt.Text.Trim()) || string.IsNullOrEmpty(nom.Text.Trim()) 
                || string.IsNullOrEmpty(passeport.Text.Trim()) || string.IsNullOrEmpty(nationalite.Text.Trim())
                || string.IsNullOrEmpty(tel.Text.Trim()))
            {
                res = "Veuillez remplir toutes les Champs correctement !";
            }

            
            if (string.IsNullOrEmpty(res.Trim()))
            {
                return true;
            }
            
            MessageBox.Show(res.Trim(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        private void Actualiser()
        {
            ChargerPassager();
            ViderChamps();
        }
        private void Ajouter()
        {
            if (VerifierChamps())
            {
                try
                {
                    var pass = new Passager(idtxt.Text, nom.Text, passeport.Text, nationalite.Text, tel.Text);

                    service.Ajouter(pass);
                    ChargerPassager();
                    ViderChamps();
                    MessageBox.Show("Ajout des données reussie !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Exeption", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void Modifier()
        {
            if (string.IsNullOrWhiteSpace(idtxt.Text)) return;
            if (VerifierChamps())
            {
                try
                {
                    var pass = new Passager(idtxt.Text, nom.Text, passeport.Text, nationalite.Text, tel.Text);

                    service.Modifier(pass);
                    ChargerPassager();
                    ViderChamps();
                    MessageBox.Show("Modification des données reussie !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Exeption", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void Supprimer()
        {
            if (string.IsNullOrWhiteSpace(idtxt.Text)) return;
            else
            {
                try
                {
                    if (MessageBox.Show("Voulez vous confirmer la suppression ?", "Confirmation",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        service.Supprimer(idtxt.Text);
                        ChargerPassager();
                        ViderChamps();
                        MessageBox.Show("Supression des données reussie !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Exeption", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ChargerPassager()
        {
            dgPass.DataSource = service.ObtenirTous();
            FormatDataGrid();
        }

        private void Rechercher()
        {
            string texte = recherche.Text.ToLower();
            if (string.IsNullOrEmpty(texte))
            {
                ChargerPassager();
                return;
            }

            DataTable dt = service.ObtenirTous();
            DataView dv = dt.DefaultView;
            dv.RowFilter = $"id LIKE '%{texte}%' OR nom LIKE '%{texte}%' OR passeport LIKE '%{texte}%' OR nationalite LIKE '%{texte}%' OR telephone LIKE '%{texte}%'";
            dgPass.DataSource = dv;
        }

        private void ViderChamps()
        {
            idtxt.Text = "";
            nom.Text = "";
            passeport.Text = "";
            nationalite.Text = "";
            tel.Text = "";
            idtxt.Enabled = true;
        }
    }
}
