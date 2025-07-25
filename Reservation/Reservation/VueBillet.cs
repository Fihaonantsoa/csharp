using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Reservation
{
    public class VueBillet : Form
    {
        private Panel Premier, Second, Third, Fourth, Fifth;
        private ResService Reservice;
        private BilletService serviceBillet;
        private PassagerService PassagerService;
        private PaieService servicePaie;
        private Button btn1, btn2, btn3, btn4, btn5;
        public HeaderContainer HeaderPanel;
        private TextBox RechercheInput, idtxt, nom, passeport, nationalite, tel, reserve;
        private TextBox RechercheInputRes, idRes, dateRes, prixRes, modePaie;
        private TextBox idBillet, Passager, Vol, Reserver, classe, siege, prixBillet, etat;
        private TextBox RechercheInputPaie, idPaie, idResPaie, datePaie, montantPaie;
        private TextBox Passager2, champs1, champs2, champs3;
        private DataGridView dgPass, dgRes, dgPaie, dgPassRes;
        private ComboBox RechercheBillet, cbRes;

        private readonly Color PrimaryColor = Color.FromArgb(33, 47, 61);
        private readonly Color SecondaryColor = Color.FromArgb(52, 73, 94);
        private readonly Color AccentColor = Color.FromArgb(41, 128, 185);
        private readonly Color SuccessColor = Color.FromArgb(46, 204, 113);
        private readonly Color DangerColor = Color.FromArgb(231, 76, 60);
        private readonly Color WarningColor = Color.FromArgb(241, 196, 15);
        public VueBillet()
        {
            serviceBillet = new BilletService();
            Reservice = new ResService();
            servicePaie = new PaieService();
            PassagerService = new PassagerService();
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
            ChargerPassager();
            ChargerRes();
            ChargerResevation();
            ChargerBillet();
            ChargerPaie();
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

        public void MainPanel()
        {
            Panel mainPanel = new Panel
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

            Panel content = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.LightBlue,
                Padding = new Padding(30, 30, 30, 30)
            };

            Panel llabPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(430, 0, 0, 0)
            };

            Panel Contenu = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 1190, 690), 20))
            };


            // Commencement du Frame Premier

            Premier = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.DodgerBlue,

            };

            Panel Detaille = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
            };


            Image imgLogo2 = Properties.Resources.somebody;
            CreateImage(Detaille, imgLogo2, new Point(20, 20), new Size(60, 60));

            Panel Nom2 = new Panel
            {
                Location = new Point(90, 20),
                Size = new Size(390, 60),
                BackColor = Color.White,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 390, 60), 10))
            };

            Label monLab2 = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Text = "Detaille du voyageur Selectionné !",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Monotype Corsiva", 16, FontStyle.Bold),
                BackColor = Color.DodgerBlue
            };
            
            Nom2.Controls.Add(monLab2);
            Detaille.Controls.Add(Nom2);

            int y = 110;
            int controlHeight = 75;

            idtxt = CreateFormControl("ID", ref y, controlHeight, Detaille);
            nom = CreateFormControl("Nom", ref y, controlHeight, Detaille);
            passeport = CreateFormControl("Passeport", ref y, controlHeight, Detaille);
            nationalite = CreateFormControl("Nationalité", ref y, controlHeight, Detaille);
            tel = CreateFormControl("Téléphone", ref y, controlHeight, Detaille);
            reserve = CreateFormControl("Reservation", ref y, controlHeight, Detaille);


            CreatePanel(Premier, Detaille, new Point(20, 100), new Size(500, 565));

            Panel CorpsContent = new Panel
            {
                Location = new Point(560, 20),
                Size = new Size(600, 650),
                BackColor = Color.White,
            };

            dgPass = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            
            dgPass.SelectionChanged += (s, e) => dgVol_selected(s, e);
            StyleDataGrid();

            CorpsContent.Controls.Add(dgPass);
            Premier.Controls.Add(CorpsContent);

            Panel BarRecherche = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(500, 60),
                BackColor = SecondaryColor,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 500, 60), 10))
            };

            Label LabRecherche = new Label
            {
                Dock = DockStyle.Left,
                Width = 150,
                Font = new Font("Calibri", 12, FontStyle.Bold),
                Text = "Recherche",
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White,
                ForeColor = SecondaryColor,
            };

            Panel ipres = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 13, 20, 13),
                BackColor = Color.White,
            };

            RechercheInput = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 14, FontStyle.Regular),
            };

            RechercheInput.TextChanged += (s, e) => Rechercher();

            ipres.Controls.Add(RechercheInput);
            BarRecherche.Controls.Add(ipres);
            BarRecherche.Controls.Add(LabRecherche);
            Premier.Controls.Add(BarRecherche);
            
            // Commencement du Second Frame
            
            Second = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.DodgerBlue,

            };


            Panel Detaille2 = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
            };


            Image imgLogo3 = Properties.Resources.avion_800_1;
            CreateImage(Detaille2, imgLogo3, new Point(20, 20), new Size(60, 60));

            Panel Nom3 = new Panel
            {
                Location = new Point(90, 20),
                Size = new Size(390, 60),
                BackColor = Color.White,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 390, 60), 10))
            };

            Label monLab3 = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Text = "Detaille du reservation !",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Monotype Corsiva", 16, FontStyle.Bold),
                BackColor = Color.DodgerBlue
            };

            Nom3.Controls.Add(monLab3);
            Detaille2.Controls.Add(Nom3);

            y = 150;
            controlHeight = 75;

            idRes = CreateFormControl("ID", ref y, controlHeight, Detaille2);
            dateRes = CreateFormControl("Date", ref y, controlHeight, Detaille2);
            prixRes = CreateFormControl("Prix", ref y, controlHeight, Detaille2);
            modePaie = CreateFormControl("Paiement", ref y, controlHeight, Detaille2);


            CreatePanel(Second, Detaille2, new Point(20, 100), new Size(500, 565));

            Panel CorpsContent2 = new Panel
            {
                Location = new Point(560, 20),
                Size = new Size(600, 650),
                BackColor = Color.White,
            };

            dgRes = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };

            dgRes.SelectionChanged += (s, e) => dgRes_selected(s, e);
            StyleDataGridRes();

            CorpsContent2.Controls.Add(dgRes);
            Second.Controls.Add(CorpsContent2);

            Panel BarRecherche2 = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(500, 60),
                BackColor = SecondaryColor,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 500, 60), 10))
            };

            Label LabRecherche2 = new Label
            {
                Dock = DockStyle.Left,
                Width = 150,
                Font = new Font("Calibri", 12, FontStyle.Bold),
                Text = "Recherche",
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White,
                ForeColor = SecondaryColor,
            };

            Panel ipres2 = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 13, 20, 13),
                BackColor = Color.White,
            };

            RechercheInputRes = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 14, FontStyle.Regular),
            };

            RechercheInputRes.TextChanged += (s, e) => RechercherRes();

            ipres2.Controls.Add(RechercheInputRes);
            BarRecherche2.Controls.Add(ipres2);
            BarRecherche2.Controls.Add(LabRecherche2);
            Second.Controls.Add(BarRecherche2);

            // Commencement du Troisieme Frame

            Third = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.DodgerBlue,

            };

            Panel Detaille4 = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.LightBlue,
            };


            Image imgLogo4 = Properties.Resources.somebody;
            CreateImage(Detaille4, imgLogo4, new Point(20, 20), new Size(60, 60));

            Panel Nom4 = new Panel
            {
                Location = new Point(90, 20),
                Size = new Size(415, 60),
                BackColor = Color.White,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 415, 60), 10))
            };

            Label monLab4 = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Text = "Detaille du Billet Selectionné !",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Monotype Corsiva", 16, FontStyle.Bold),
                BackColor = Color.DodgerBlue
            };

            Nom4.Controls.Add(monLab4);
            Detaille4.Controls.Add(Nom4);

            Panel ere1 = new Panel
            {
                Location = new Point(10, 100),
                Size = new Size(500, 340),
                BackColor = Color.WhiteSmoke,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 500, 340), 10)),
            };
            y = 20;

            idBillet = CreateFormControl("N° Billet", ref y, controlHeight, ere1);
            Reserver = CreateFormControl("Reservation", ref y, controlHeight, ere1);
            Passager = CreateFormControl("Passager", ref y, controlHeight, ere1);
            etat = CreateFormControl("Etat", ref y, controlHeight, ere1);

            Detaille4.Controls.Add(ere1);


            Panel ere2 = new Panel
            {
                Location = new Point(600, 100),
                Size = new Size(500, 340),
                BackColor = Color.WhiteSmoke,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 500, 340), 10)),
            };
            y = 20;

            Vol = CreateFormControl("N° Vol", ref y, controlHeight, ere2);
            classe = CreateFormControl("Classe", ref y, controlHeight, ere2);
            siege = CreateFormControl("N° Siege", ref y, controlHeight, ere2);
            prixBillet = CreateFormControl("Prix", ref y, controlHeight, ere2);

            Detaille4.Controls.Add(ere2);

            Panel BarRecherche4 = new Panel
            {
                Location = new Point(600, 20),
                Size = new Size(500, 60),
                BackColor = SecondaryColor,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 500, 60), 10))
            };

            Label LabRecherche4 = new Label
            {
                Dock = DockStyle.Left,
                Width = 150,
                Font = new Font("Calibri", 12, FontStyle.Bold),
                Text = "Recherche",
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White,
                ForeColor = SecondaryColor,
            };

            Panel ipres4 = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 13, 20, 13),
                BackColor = Color.White,
            };

            RechercheBillet = new ComboBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 14, FontStyle.Regular),
            };

            RechercheBillet.SelectedValueChanged += (s, e) => RechercherBillet();

            ipres4.Controls.Add(RechercheBillet);
            BarRecherche4.Controls.Add(ipres4);
            BarRecherche4.Controls.Add(LabRecherche4);
            Detaille4.Controls.Add(BarRecherche4);

            Panel FootBillet = new Panel
            {
                Location = new Point(10, 450),
                Size = new Size(1120, 190),
                BackColor = Color.WhiteSmoke,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 1120, 190), 10)),
            };

            Panel ere3 = new Panel
            {
                Dock = DockStyle.Left,
                Width = 560,
                BackColor = Color.WhiteSmoke,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 560, 340), 10)),
            };
            y = 40;

            Passager2 = CreateFormControl("Voyageur", ref y, controlHeight, ere3);
            champs1 = CreateFormControl("Champs 1", ref y, controlHeight, ere3);

            FootBillet.Controls.Add(ere3);

            Panel ere4 = new Panel
            {
                Dock = DockStyle.Right,
                Width = 510,
                BackColor = Color.WhiteSmoke,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 510, 340), 10)),
            };

            y = 40;

            champs2 = CreateFormControl("Champs 2", ref y, controlHeight, ere4);
            champs3 = CreateFormControl("Champs 3", ref y, controlHeight, ere4);

            FootBillet.Controls.Add(ere4);

            Detaille4.Controls.Add(FootBillet);
            CreatePanel(Third, Detaille4, new Point(20, 20), new Size(1140, 650));

            // The Fourth Panel 
            Fourth = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.DodgerBlue,

            };

            Panel Detaille5 = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
            };


            Image imgLogo5 = Properties.Resources.avion_800_1;
            CreateImage(Detaille5, imgLogo5, new Point(20, 20), new Size(60, 60));

            Panel Nom5 = new Panel
            {
                Location = new Point(90, 20),
                Size = new Size(390, 60),
                BackColor = Color.White,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 390, 60), 10))
            };

            Label monLab5 = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Text = "Detaille de Paiement !",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Monotype Corsiva", 16, FontStyle.Bold),
                BackColor = Color.DodgerBlue
            };

            Nom5.Controls.Add(monLab5);
            Detaille5.Controls.Add(Nom5);

            y = 150;
            controlHeight = 75;

            idPaie = CreateFormControl("ID", ref y, controlHeight, Detaille5);
            idResPaie = CreateFormControl("Reservation", ref y, controlHeight, Detaille5);
            montantPaie = CreateFormControl("Montant", ref y, controlHeight, Detaille5);
            datePaie = CreateFormControl("Date", ref y, controlHeight, Detaille5);


            CreatePanel(Fourth, Detaille5, new Point(20, 100), new Size(500, 565));

            Panel CorpsContent5 = new Panel
            {
                Location = new Point(560, 20),
                Size = new Size(600, 650),
                BackColor = Color.White,
            };


            dgPaie = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };

            dgPaie.SelectionChanged += (s, e) => dgPaie_selected(s, e);
            StyleDataGridResPaie();

            CorpsContent5.Controls.Add(dgPaie);
            Fourth.Controls.Add(CorpsContent5);

            Panel BarRecherche5 = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(500, 60),
                BackColor = SecondaryColor,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 500, 60), 10))
            };

            Label LabRecherche5 = new Label
            {
                Dock = DockStyle.Left,
                Width = 150,
                Font = new Font("Calibri", 12, FontStyle.Bold),
                Text = "Recherche",
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White,
                ForeColor = SecondaryColor,
            };

            Panel ipres5 = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 13, 20, 13),
                BackColor = Color.White,
            };

            RechercheInputPaie = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 14, FontStyle.Regular),
            };

            RechercheInputPaie.TextChanged += (s, e) => RechercherPaie();

            ipres5.Controls.Add(RechercheInputPaie);
            BarRecherche5.Controls.Add(ipres5);
            BarRecherche5.Controls.Add(LabRecherche5);
            Fourth.Controls.Add(BarRecherche5);

            // The Fifth Panel 
            Fifth = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.DodgerBlue,

            };

            Panel Detaille6 = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
            };

            Panel Nom6 = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(450, 60),
                BackColor = Color.White,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 450, 60), 10))
            };

            Label monLab6 = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Text = "Detaille de Paiement !",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Monotype Corsiva", 16, FontStyle.Bold),
                BackColor = Color.DodgerBlue
            };

            Nom6.Controls.Add(monLab6);
            Detaille6.Controls.Add(Nom6);

            y = 100;
            controlHeight = 75;

            cbRes = CreateFormControlCombo("Reservation", ref y, controlHeight, Detaille6);

            Panel dgPanel = new Panel
            {
                Location = new Point(20, 100),
                Size = new Size(300, 300),
                BackColor = Color.Red,
            };

            dgPassRes = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            StyleDataGridImp();

            dgPanel.Controls.Add(dgPassRes);
            Detaille6.Controls.Add(dgPanel);
            CreatePanel(Fifth, Detaille6, new Point(20, 20), new Size(500, 650));

            Panel CorpsContent6 = new Panel
            {
                Location = new Point(560, 20),
                Size = new Size(600, 650),
                BackColor = Color.White,
            };



            Contenu.Controls.Add(Fifth);
            Contenu.Controls.Add(Fourth);
            Contenu.Controls.Add(Third); 
            Contenu.Controls.Add(Premier);
            Contenu.Controls.Add(Second);

            llabPanel.Controls.Add(Contenu);
            Panel form1 = new Panel
            {
                Dock = DockStyle.Left,
                Width = 400,
                BackColor = Color.White,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 400, 690), 20))
            };

            Label llform = new Label
            {
                Dock = DockStyle.Top,
                Height = 60,
                Text = "Menu de Vue des billets",
                Font = new Font("Monotype Corsiva", 14),
                TextAlign = ContentAlignment.MiddleCenter,
            };

            Panel Bpanel1 = new Panel
            {
                Dock = DockStyle.Top,
                BackColor = Color.Transparent,
                Height = 390,
                Padding = new Padding(40, 20, 40, 0),
            };

            btn1 = CreateActionButton("Exporter un Billet", new Size(0, 50), AccentColor, (s, e) => showFifth());
            Button i1 = espace();
            btn2 = CreateActionButton("Paiement", new Size(0, 50), AccentColor, (s, e) => showFourth());
            Button i2 = espace();
            btn3 = CreateActionButton("Billets", new Size(0, 50), AccentColor, (s, e) => showThird());
            Button i3 = espace();
            btn4 = CreateActionButton("Voyageur", new Size(0, 50), AccentColor, (s, e) => showPremier());
            Button i4 = espace();
            btn5 = CreateActionButton("Reservation", new Size(0, 50), AccentColor, (s, e) => showSecond());

            Bpanel1.Controls.AddRange(new[] { btn1, i1, btn3, i2, btn2, i3, btn4, i4, btn5 });

            form1.Controls.Add(Bpanel1);
            form1.Controls.Add(llform);

            Image img1 = Properties.Resources.billet;
            CreateImage(form1, img1, new Point(38, 460), new Size(320, 200));

            content.Controls.Add(form1);
            content.Controls.Add(llabPanel);
            CreatePanel(Body, content, new Point(118, 50), new Size(1685, 750));

            mainPanel.Controls.Add(Body);
            this.Controls.Add(mainPanel);
        }
        private void ChargerResevation()
        {
            //DataTable dt = PassagerService.ObtenirTousRes(cbRes.SelectedValue.ToString());
            //cbRes.Items.Clear();
            //cbRes.DataSource = dt;
            //cbRes.DisplayMember = "idreserve";
            //cbRes.ValueMember = "idreserve";
        }

        private void StyleDataGridImp()
        {
            dgPassRes.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 14, FontStyle.Bold);
            dgPassRes.ColumnHeadersHeight = 60;
            dgPassRes.Font = new Font("Calibri", 12, FontStyle.Regular);
            dgPassRes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgPassRes.RowTemplate.Height = 50;
        }
        private void FormatDataGridImp()
        {
            if (dgPassRes.Columns.Count > 0)
            {
                dgPassRes.Columns["id"].HeaderText = "ID";
                dgPassRes.Columns["nom"].HeaderText = "Nom";
            }
        }
        private void ChargerImp()
        {
            string txt = cbRes.SelectedValue.ToString();
            dgPassRes.DataSource = PassagerService.ObtenirTousRes(txt);
            foreach (DataGridViewColumn col in dgPass.Columns)
            {
                if (col.Name != "id" && col.Name != "nom")
                {
                    col.Visible = false;
                }
            }
            FormatDataGridImp();
        }

        // Pour le Frame Premier du voyageur
        private void StyleDataGrid()
        {
            dgPass.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 14, FontStyle.Bold);
            dgPass.ColumnHeadersHeight = 60;
            dgPass.Font = new Font("Calibri", 12, FontStyle.Regular);
            dgPass.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgPass.RowTemplate.Height = 50;
        }
        private void FormatDataGrid()
        {
            if (dgPass.Columns.Count > 0)
            {
                dgPass.Columns["nom"].HeaderText = "Nom";
                dgPass.Columns["nationalite"].HeaderText = "Nationalité";
            }
        }
        private void ChargerPassager()
        {
            dgPass.DataSource = PassagerService.ObtenirTous();
            foreach (DataGridViewColumn col in dgPass.Columns)
            {
                if (col.Name != "nom" && col.Name != "nationalite")
                {
                    col.Visible = false;
                }
            }
            FormatDataGrid();
        }
        private void dgVol_selected(object sender, EventArgs e)
        {
            RemplirFormPassager();
            idtxt.Enabled = false;
        }
        private void RemplirFormPassager()
        {
            if (dgPass.CurrentRow != null)
            {
                string id = dgPass.CurrentRow.Cells[0].Value?.ToString();
                string Nom = dgPass.CurrentRow.Cells[1].Value?.ToString();
                string Passeport = dgPass.CurrentRow.Cells[2].Value?.ToString();
                string Nationalite = dgPass.CurrentRow.Cells[3].Value?.ToString();
                string Tel = dgPass.CurrentRow.Cells[4].Value?.ToString();
                string Res = dgPass.CurrentRow.Cells[5].Value?.ToString();

                idtxt.Text = id;
                nom.Text = Nom;
                passeport.Text = Passeport;
                nationalite.Text = Nationalite;
                tel.Text = Tel;
                reserve.Text = Res;
            }
        }
        private void Rechercher()
        {
            string texte = RechercheInput.Text.ToLower();
            if (string.IsNullOrEmpty(texte))
            {
                ChargerPassager();
                return;
            }

            DataTable dt = PassagerService.ObtenirTous();
            DataView dv = dt.DefaultView;
            dv.RowFilter = $"id LIKE '%{texte}%' OR nom LIKE '%{texte}%' OR nationalite LIKE '%{texte}%'";
            dgPass.DataSource = dv;
        }
        
        // Frame Second du Reservation
        private void StyleDataGridRes()
        {
            dgRes.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 14, FontStyle.Bold);
            dgRes.ColumnHeadersHeight = 60;
            dgRes.Font = new Font("Calibri", 12, FontStyle.Regular);
            dgRes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgRes.RowTemplate.Height = 50;
        }
        private void FormatDataGridRes()
        {
            if (dgRes.Columns.Count > 0)
            {
                dgRes.Columns["idreserve"].HeaderText = "Passager";
                dgRes.Columns["datereserve"].HeaderText = "Date";
            }
        }
        private void dgRes_selected(object sender, EventArgs e)
        {
            RemplirFormRes();
        }

        private void RemplirFormRes()
        {
            if (dgRes.CurrentRow != null)
            {
                string id = dgRes.CurrentRow.Cells[0].Value?.ToString();
                string daterese = dgRes.CurrentRow.Cells[1].Value?.ToString();
                string Prix = dgRes.CurrentRow.Cells[2].Value?.ToString();
                string modepaie = dgRes.CurrentRow.Cells[3].Value?.ToString();

                idRes.Text = id;
                prixRes.Text = Prix;
                modePaie.Text = modepaie;
                if (DateTime.TryParse(daterese, out DateTime date1))
                {
                    dateRes.Text = date1.ToString();
                }
            }
        }
        private void ChargerRes()
        {
            dgRes.DataSource = Reservice.ObtenirTous();
            foreach (DataGridViewColumn col in dgRes.Columns)
            {
                if (col.Name != "idreserve" && col.Name != "datereserve")
                {
                    col.Visible = false;
                }
            }
            FormatDataGridRes();
        }

        private void RechercherRes()
        {
            string texte = RechercheInputRes.Text.ToLower();
            if (string.IsNullOrEmpty(texte))
            {
                ChargerRes();
                return;
            }

            DataTable dt = Reservice.ObtenirTous();
            DataView dv = dt.DefaultView;
            dv.RowFilter = $"idvoyageur LIKE '%{texte}%'";
            dgRes.DataSource = dv;
        }

        // Pour le Third Panel
        private void RechercherBillet()
        {
            string texte = RechercheBillet.SelectedValue.ToString();

            DataTable dt = serviceBillet.ObtenirDetaille();

            if (string.IsNullOrEmpty(texte))
            {
                RemplirFormBillet(dt);
                return;
            }

            try
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = $"CONVERT(idbillet, System.String) = '{texte.Replace("'", "''")}'";

                RemplirFormBillet(dv.ToTable());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la recherche : " + ex.Message);
            }
        }

        private void ChargerBillet()
        {
            DataTable dt = serviceBillet.ObtenirTous();
            RechercheBillet.Items.Clear();
            RechercheBillet.DataSource = dt;
            RechercheBillet.DisplayMember = "idbillet";
            RechercheBillet.ValueMember = "idbillet";
        }
        private void RemplirFormBillet(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                idBillet.Text = dt.Rows[0]["idbillet"].ToString();
                Passager.Text = dt.Rows[0]["idvoyageur"].ToString();
                Vol.Text = dt.Rows[0]["idvol"].ToString();
                Reserver.Text = dt.Rows[0]["idreservation"].ToString();
                classe.Text = dt.Rows[0]["classe"].ToString();
                siege.Text = dt.Rows[0]["siege"].ToString();
                prixBillet.Text = dt.Rows[0]["prix"].ToString();
                etat.Text = dt.Rows[0]["etat"].ToString();
                Passager2.Text = dt.Rows[0]["nom"].ToString();
            }
        }

        //Quatrieme Frame

        // Pour le Frame Premier du voyageur
        private void StyleDataGridResPaie()
        {
            dgPaie.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 14, FontStyle.Bold);
            dgPaie.ColumnHeadersHeight = 60;
            dgPaie.Font = new Font("Calibri", 12, FontStyle.Regular);
            dgPaie.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgPaie.RowTemplate.Height = 50;
        }
        private void FormatDataGridPaie()
        {
            if (dgPaie.Columns.Count > 0)
            {
                dgPaie.Columns["idpaie"].HeaderText = "Paiement";
                dgPaie.Columns["datepaie"].HeaderText = "Date";
            }
        }
        private void ChargerPaie()
        {
            dgPaie.DataSource = servicePaie.ObtenirTous();
            foreach (DataGridViewColumn col in dgPaie.Columns)
            {
                if (col.Name != "idpaie" && col.Name != "datepaie")
                {
                    col.Visible = false;
                }
            }
            FormatDataGridPaie();
        }
        private void dgPaie_selected(object sender, EventArgs e)
        {
            RemplirFormPaie();
        }
        private void RemplirFormPaie()
        {
            if (dgPaie.CurrentRow != null)
            {
                string ID1 = dgPaie.CurrentRow.Cells[0].Value?.ToString();
                string ID2 = dgPaie.CurrentRow.Cells[1].Value?.ToString();
                string Montant = dgPaie.CurrentRow.Cells[2].Value?.ToString();
                string Date = dgPaie.CurrentRow.Cells[3].Value?.ToString();

                idPaie.Text = ID1;
                idResPaie.Text = ID2;
                montantPaie.Text = Montant;
                datePaie.Text = Date;
            }
        }
        private void RechercherPaie()
        {
            string texte = RechercheInputPaie.Text.ToLower();
            if (string.IsNullOrEmpty(texte))
            {
                ChargerPaie();
                return;
            }

            DataTable dt = servicePaie.ObtenirTous();
            DataView dv = dt.DefaultView;
            dv.RowFilter = $"idpaie LIKE '%{texte}%'";
            dgPaie.DataSource = dv;
        }

        // Construction des outils
        private ComboBox CreateFormControlCombo(string label, ref int y, int height, Panel parent)
        {
            var panel = new Panel
            {
                Location = new Point(20, y),
                Size = new Size(500, height),

            };


            var lbl = new Label
            {
                Text = label,
                Location = new Point(0, 0),
                AutoSize = true,
                MinimumSize = new Size(150, 40),
                BackColor = Color.DodgerBlue,
                Padding = new Padding(20, 0, 0, 0),
                Font = new Font("Calibri", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 150, 40), 10)),
                ForeColor = Color.White
            };

            var txt = new ComboBox
            {
                Location = new Point(180, 3),
                Size = new Size(260, 40),
                Font = new Font("Calibri", 14, FontStyle.Regular),
            };

            panel.Controls.Add(lbl);
            panel.Controls.Add(txt);
            parent.Controls.Add(panel);

            y += height;
            return txt;
        }
        private TextBox CreateFormControl(string label, ref int y, int height, Panel parent)
        {
            var panel = new Panel
            {
                Location = new Point(20, y),
                Size = new Size(500, height),

            };


            var lbl = new Label
            {
                Text = label,
                Location = new Point(0, 0),
                AutoSize = true,
                MinimumSize = new Size(150, 40),
                BackColor = Color.DodgerBlue,
                Padding = new Padding(20, 0, 0, 0),
                Font = new Font("Calibri", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Region = new Region(GetRoundedPath(new Rectangle(0, 0, 150, 40), 10)),
                ForeColor = Color.White
            };

            var txt = new TextBox
            {
                Location = new Point(180, 3),
                Size = new Size(260, 40),
                Font = new Font("Calibri", 14, FontStyle.Regular),
                Enabled = false
            };

            panel.Controls.Add(lbl);
            panel.Controls.Add(txt);
            parent.Controls.Add(panel);

            y += height;
            return txt;
        }
        public Button espace()
        {
            Button p = new Button();
            p.Size = new Size(0, 20);
            p.Dock = DockStyle.Top;
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
                Dock = DockStyle.Top,
                Height = 50,
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

        // Action attribuée au bouton
        public void Action()
        {
            return;
        }
        public void showPremier()
        {
            Premier.Visible = Visible;
            Second.Hide();
            Third.Hide();
            Fourth.Hide();
            Fifth.Hide();
        }
        public void showSecond()
        {
            Second.Visible = Visible;
            Premier.Hide();
            Third.Hide();
            Fourth.Hide();
            Fifth.Hide();
        }
        public void showThird()
        {
            Third.Visible = Visible;
            Second.Hide();
            Fourth.Hide();
            Fifth.Hide();
            Premier.Hide();
        }
        public void showFifth()
        {
            Fifth.Visible = Visible;
            Second.Hide();
            Third.Hide();
            Fourth.Hide();
            Premier.Hide();
        }
        public void showFourth()
        {
            Fourth.Visible = Visible;
            Second.Hide();
            Third.Hide();
            Fifth.Hide();
            Premier.Hide();
        }
    }
}
