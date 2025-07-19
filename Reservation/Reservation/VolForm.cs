using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Reservation
{
    public class VolForm : Form
    {
        private VolService service;
        private DateTimePicker datedep, datearr;
        private TextBox idtxt, statut, recherche;
        private Button btnAjouter, btnModifier, btnSupprimer, btnActualiser;
        private DataGridView dgVol;

        private readonly Color PrimaryColor = Color.FromArgb(33, 47, 61);
        private readonly Color SecondaryColor = Color.FromArgb(52, 73, 94);
        private readonly Color AccentColor = Color.FromArgb(41, 128, 185);
        private readonly Color SuccessColor = Color.FromArgb(46, 204, 113);
        private readonly Color DangerColor = Color.FromArgb(231, 76, 60);
        private readonly Color WarningColor = Color.FromArgb(241, 196, 15);
        public VolForm()
        {
            InitializeUI();
            service = new VolService();
            ChargerVol();
            ViderChamps();
        }

        private void InitializeUI()
        {
            this.Text = "Gestion des Vols";
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
                Text = "Gestion des Vols",
                ForeColor = Color.White,
                Dock = DockStyle.Left,
                Size = new Size(350, 50),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20,0,0,0),
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
                Text = "Formulaire de vol",
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
            datedep = CreateFormControlDate("Date de départ", ref y, controlHeight, sideCenter);
            datearr = CreateFormControlDate("Date d'arrivée", ref y, controlHeight, sideCenter);
            statut = CreateFormControl("Statut", ref y, controlHeight, sideCenter);

            var buttonPanel = new Panel {
                Dock = DockStyle.Bottom,
                Height = 280,
                Padding = new Padding(20,0,20,0)
            };

            btnAjouter = CreateActionButton("Ajouter", AccentColor, (s, e) => Ajouter());
            Button i1 = espace();
            btnModifier = CreateActionButton("Modifier", AccentColor, (s, e) => Modifier());
            Button i2 = espace();
            btnSupprimer = CreateActionButton("Supprimer", AccentColor, (s, e) => Supprimer());
            Button i3 = espace();
            btnActualiser = CreateActionButton("Actualiser", AccentColor, (s, e) => Actualiser());

            buttonPanel.Controls.AddRange(new[] {  btnActualiser, i1, btnSupprimer, i2, btnModifier, i3, btnAjouter });
            sideCenter.Controls.Add(buttonPanel);
            Gauche.Controls.Add(sideCenter);

            Panel Donnees = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30,0,0,0)
            };

            Panel Rpanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(0,10,0,0)
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
            dgVol = new DataGridView
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
            dgVol.SelectionChanged += (s, e) => dgVol_selected(s, e);
            StyleDataGrid();

            Dgpanel.Controls.Add(dgVol);

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
            dgVol.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgVol.ColumnHeadersHeight = 50;
            dgVol.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgVol.RowTemplate.Height = 40;
        }
        private void FormatDataGrid()
        {
            if (dgVol.Columns.Count > 0)
            {
                dgVol.Columns["idvol"].HeaderText = "ID";
                dgVol.Columns["datedep"].HeaderText = "Départ";
                dgVol.Columns["datearr"].HeaderText = "Arrivée";
                dgVol.Columns["statut"].HeaderText = "Statut";
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
            if (dgVol.CurrentRow != null)
            {
                string id = dgVol.CurrentRow.Cells[0].Value?.ToString();
                string depart = dgVol.CurrentRow.Cells[1].Value?.ToString();
                string arrive = dgVol.CurrentRow.Cells[2].Value?.ToString();
                string state = dgVol.CurrentRow.Cells[3].Value?.ToString();

                idtxt.Text = id;
                statut.Text = state;
                if (DateTime.TryParse(depart, out DateTime date1) && DateTime.TryParse(arrive, out DateTime date2))
                {
                    datedep.Value = date1;
                    datearr.Value = date2;
                }
            }
        }

        private Boolean VerifierChamps()
        {
            string id = "";
            string date = "";
            string state = "";
            if (string.IsNullOrEmpty(idtxt.Text.Trim()))
            {
                id = "L'identifiant est requis!";
            }
            if(datedep.Value > datearr.Value)
            {
                date = "La date d'arriver doit être aprés la date du départ !";
            }
            if (string.IsNullOrEmpty(statut.Text.Trim()))
            {
                state = "Le statut est requis!";
            }
            
            string res = id + "\n" + date + "\n" + state;

            if (string.IsNullOrEmpty(res.Trim()))
            {
                return true;
            }
            MessageBox.Show(res.Trim(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        private void Actualiser()
        {
            ChargerVol();
            ViderChamps();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // VolForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "VolForm";
            this.ResumeLayout(false);

        }

        private void Ajouter()
        {
            if (VerifierChamps())
            {
                try
                {
                    DateTime depart = datedep.Value;
                    DateTime arrive = datearr.Value;

                    var vol = new Vol(idtxt.Text, depart, arrive, statut.Text);

                    service.Ajouter(vol);
                    ChargerVol();
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
                    DateTime depart = datedep.Value;
                    DateTime arrive = datearr.Value;

                    var vol = new Vol(idtxt.Text, depart, arrive, statut.Text);

                    service.Modifier(vol);
                    ChargerVol();
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
                    if (MessageBox.Show("Confirmer la suppression ?", "Confirmation",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        service.Supprimer(idtxt.Text);
                        ChargerVol();
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
        private void ChargerVol()
        {
            dgVol.DataSource = service.ObtenirTous();
            FormatDataGrid();
        }

        private void Rechercher()
        {
            string texte = recherche.Text.ToLower();
            if (string.IsNullOrEmpty(texte))
            {
                ChargerVol();
                return;
            }

            DataTable dt = service.ObtenirTous();
            DataView dv = dt.DefaultView;
            dv.RowFilter = $"idvol LIKE '%{texte}%' OR statut LIKE '%{texte}%'";
            dgVol.DataSource = dv;
        }

        private void ViderChamps()
        {
            idtxt.Text = "";
            statut.Text = "";
            datedep.Value = DateTime.Now;
            datearr.Value = DateTime.Now;
            idtxt.Enabled = true;
        }
    }
}
