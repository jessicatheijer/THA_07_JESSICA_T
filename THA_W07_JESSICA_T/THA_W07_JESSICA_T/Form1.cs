using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using THA_W07_JESSICA_T.Properties;

namespace THA_W07_JESSICA_T
{
    public partial class Form1 : Form
    {
        string[] listMovies = File.ReadAllText("MOVIE POSTERS.txt").Split(',');
        List<Button> listButtons = new List<Button>(); //BUY TICKET
        List<Button> listButtonsTime = new List<Button>(); //SHOW TIME
        List<Button> listButtonSeating = new List<Button>(); //SEATING
        List<string> ListTitles = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        Panel panelMain = new Panel();
        int x;
        int y;


        string hurufSeating = "ABCDEFGHJK";
        string showTimeChosen = " ";
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMainPage();

            //SHOW TIME BUTTONS MASUKIN LIST
            int x4 = 35;
            int y4 = 340;
            int counter = 1;
            for (int i = 1; i < 37; i++)
            {
                Button button_time = new Button();
                button_time.Size = new Size(53, 35);
                button_time.Location = new Point(x4, y4);
                button_time.Tag = counter.ToString() + i.ToString();
                button_time.Click += buttonTime_Click;
                string cek = button_time.Tag.ToString();
                if (cek.Substring(0, 1) == "1")
                {
                    button_time.Text = "11.00";
                }
                if (cek.Substring(0, 1) == "2")
                {
                    button_time.Text = "15.00";
                }
                if (cek.Substring(0, 1) == "3")
                {
                    button_time.Text = "19.00";
                }
                button_time.ForeColor = Color.White;
                button_time.BackColor = Color.Black;
                listButtonsTime.Add(button_time);
                x4 += 64;
                counter += 1;

                if (i % 3 == 0)
                {
                    x4 = 35;
                    counter = 1;
                }
            }
            //SEATING BUTTONS MASUKIN LIST

            for (int s = 0; s < 36; s++)
            {
                int x5 = 50;
                int y5 = 12;
                int counterr = 0;
                List<int> randomIndices = GetRandomIndices(100, 50, s);
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        int text = j + 1;
                        Button buttonSeating = new Button();
                        buttonSeating.Size = new Size(27, 27);
                        buttonSeating.Location = new Point(x5, y5);
                        buttonSeating.BackColor = Color.LightGray;
                        buttonSeating.Text = hurufSeating[i] + text.ToString();
                        buttonSeating.Tag = "0";
                        buttonSeating.Font = new Font(buttonSeating.Font.FontFamily, 6);
                        buttonSeating.Click += buttonSeating_Click;
                        //panelSeating.Controls.Add(buttonSeating);
                        x5 += 28;
                        if (j == 1 || j == 7)
                        {
                            x5 += 25;
                        }
                        if (randomIndices.Contains(counterr))
                        {
                            buttonSeating.BackColor = Color.Salmon;
                            buttonSeating.Enabled = false;
                        }
                        counterr++;

                        listButtonSeating.Add(buttonSeating);
                    }
                    x5 = 50;
                    y5 += 28;
                }
            }

        }

        private List<int> GetRandomIndices(int maxIndex, int count, int seed)
        {
            List<int> indices = Enumerable.Range(0, maxIndex).ToList();
            Random random = new Random(seed); // Use a seed value based on page number

            // Shuffle the indices using Fisher-Yates algorithm
            for (int i = indices.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                int temp = indices[i];
                indices[i] = indices[j];
                indices[j] = temp;
            }

            return indices.Take(count).ToList();
        }
        public void LoadMainPage()
        {
            int x = 35;
            int y = 15;

            int x2 = 45;
            int y2 = 145;

            int x3 = 35;
            int y3 = 165;
            //PANEL MAIN
            panelMain.Size = new Size(715, 400);
            panelMain.Location = new Point(25, 50);
            panelMain.BackColor = Color.Maroon;
            this.Controls.Add(panelMain);


            ListTitles.Clear();
            //MOVIE POSTERS!!!
            for (int i = 0; i < 24; i++)
            {
                if (i % 2 == 0)
                {
                    AddPoster(i + 1, 90, 125, x, y);
                    x += 110;

                    if (i == 10)
                    {
                        y = 200;
                        x = 35;
                    }
                    //MOVIE TITLE!!!
                    Label movieTitle = new Label();
                    movieTitle.Text = listMovies[i];
                    movieTitle.Location = new Point(x2, y2);
                    movieTitle.ForeColor = Color.White;
                    movieTitle.AutoSize = true;
                    panelMain.Controls.Add(movieTitle);
                    ListTitles.Add(movieTitle.Text);
                    x2 += 110;
                    if (i == 10)
                    {
                        y2 = 330;
                        x2 = 50;
                    }
                    if (i == 14)
                    {
                        x2 -= 18;
                    }
                }
            }

            //BUY TICKET BUTTONS
            for (int i = 0; i < 12; i++)
            {
                Button button_buyTicket = new Button();
                button_buyTicket.Location = new Point(x3, y3);
                button_buyTicket.Size = new Size(90, 25);
                button_buyTicket.Text = "Buy Ticket";
                button_buyTicket.ForeColor = Color.White;
                button_buyTicket.BackColor = Color.Black;
                button_buyTicket.Tag = i.ToString();
                button_buyTicket.Click += buttonTicket_Click;
                listButtons.Add(button_buyTicket);
                panelMain.Controls.Add(button_buyTicket);
                x3 += 110;
                if (i == 5)
                {
                    y3 = 350;
                    x3 = 35;
                }
            }
        }

        public void AddPoster(int nomerposter, int size1, int size2, int loc1, int loc2)
        {
            PictureBox moviePoster = new PictureBox();
            moviePoster.Size = new Size(size1, size2);
            moviePoster.SizeMode = PictureBoxSizeMode.StretchImage;
            moviePoster.Location = new Point(loc1, loc2);
            Image image = (Image)Properties.Resources.ResourceManager.GetObject(listMovies[nomerposter]);
            moviePoster.Image = image;
            panelMain.Controls.Add(moviePoster);
        }

        Panel panelSeating = new Panel();

        Label nomorkursi = new Label();

        Label labelJam = new Label();

        Label labelShowTime = new Label();

        int tandaposter;
        int tandaposter2;
        public void buttonTicket_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            //BACK BUTTON
            Button buttonBack = new Button();
            buttonBack.Location = new Point(10, 10);
            buttonBack.Size = new Size(60, 30);
            buttonBack.ForeColor = Color.White;
            buttonBack.BackColor = Color.Black;
            buttonBack.Text = "Back";
            buttonBack.Click += buttonBack_Click;
            panelMain.Controls.Add(buttonBack);

            //PANEL SEATING
            panelSeating.Size = new Size(430, 327);
            panelSeating.Location = new Point(250, 33);
            panelSeating.BackColor = Color.Black;
            panelMain.Controls.Add(panelSeating);

            //MOVIE POSTER
            int total;
            Button btn = sender as Button;
            for (int i = 0; i < 12; i++)
            {
                if (btn.Tag.ToString() == i.ToString())
                {
                    tandaposter2 = i;
                    AddPoster(i + (i + 1), 180, 245, 35, 65);

                    //MOVIE SHOW TIME BUTTONS
                    total = i + (i*2);
                    panelMain.Controls.Add(listButtonsTime[total]);
                    panelMain.Controls.Add(listButtonsTime[total+1]);
                    panelMain.Controls.Add(listButtonsTime[total+2]);
                }
            }

            //SHOW TIME LABEL
            labelShowTime.Text = "Please select the show time below";
            labelShowTime.AutoSize = true;
            labelShowTime.Location = new Point(40, 320);
            labelShowTime.ForeColor = Color.White;
            panelMain.Controls.Add(labelShowTime);

            //LABEL NOMOR KURSI
            nomorkursi.Text = "Chosen Seat: ";
            nomorkursi.ForeColor = Color.White;
            nomorkursi.Font = new Font(nomorkursi.Font.FontFamily, 8, FontStyle.Bold);
            nomorkursi.Location = new Point(250, 369);
            nomorkursi.AutoSize = true;
            panelMain.Controls.Add(nomorkursi);

            //LABEL JAM YANG DIPILIH
            labelJam.Text = "Chosen Time: " + showTimeChosen;
            labelJam.AutoSize = true;
            labelJam.ForeColor = Color.White;
            labelJam.Font = new Font(labelJam.Font.FontFamily, 8, FontStyle.Bold);
            labelJam.Location = new Point(560, 13);
            panelMain.Controls.Add(labelJam);

            //BUTTON BUY TICKET 2
            Button buttonBuyTicketFIX = new Button();
            buttonBuyTicketFIX.Text = "Book Seat";
            buttonBuyTicketFIX.Location = new Point(607, 365);
            buttonBuyTicketFIX.Size = new Size(75, 25);
            buttonBuyTicketFIX.BackColor = Color.Black;
            buttonBuyTicketFIX.ForeColor = Color.White;
            buttonBuyTicketFIX.Click += buttonBuyTicketFIX_Click;
            panelMain.Controls.Add(buttonBuyTicketFIX);

            Button buttonReset = new Button();
            buttonReset.Text = "Reset Seats";
            buttonReset.Location = new Point(250, 8);
            buttonReset.Size = new Size(75, 22);
            buttonReset.BackColor = Color.Black;
            buttonReset.ForeColor = Color.White;
            buttonReset.Click += buttonReset_Click;
            panelMain.Controls.Add(buttonReset);
        }
        public void buttonReset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Reset all seats to available?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question); ;
            if (result == DialogResult.Yes)
            {
                for (int i = tandaposter; i < tandaposter + 100; i++)
                {
                    listButtonSeating[i].BackColor = Color.LightGray;
                    listButtonSeating[i].Enabled = true;
                }

                nomorkursi.Text = "Chosen Seat: ";
            }
        }

        string infoBeli;
        public void buttonBuyTicketFIX_Click(object sender, EventArgs e)
        {
            bool adaIjo = false;
            bool adakuning = false;
            foreach (Button btn in listButtonsTime)
            {
                if (btn.BackColor == Color.Green)
                {
                    adaIjo = true;
                }
            }

            foreach (Button btn2 in listButtonSeating)
            {
                if (btn2.BackColor == Color.Gold)
                {
                    adakuning = true;
                }
            }

            if (adakuning == true && adaIjo == true)
            {
                infoBeli = "Movie: " + ListTitles[tandaposter2] + "\n" + labelJam.Text + "\n" + nomorkursi.Text + "\n" ;
                DialogResult result = MessageBox.Show(infoBeli + "Buy Ticket for " + counter + " Seats?", "Ticket Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                   foreach(Button btnn in listButtonSeating)
                    {
                        if (btnn.BackColor == Color.Gold)
                        {
                            btnn.BackColor = Color.Salmon;
                            btnn.Enabled = false;
                        }
                    }
                    string infobeli2 = "Aww makasi suda beli tiket di cinemanya jessi! lopee <3 \nTicket Booked: \n" + infoBeli;
                    MessageBox.Show(infobeli2, "Ticket Purchased", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    nomorkursi.Text = "Chosen Seat: ";
                }
            }
            else
            {
                MessageBox.Show("Please choose show time and seat before buying", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void buttonBack_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelSeating.Controls.Clear();
            foreach (Button btnn in listButtonsTime)
            {
                btnn.BackColor = Color.Black;
            }

            foreach(Button btnn in listButtonSeating)
            {
                if (btnn.BackColor == Color.Gold)
                {
                    btnn.BackColor = Color.LightGray;
                }
            }
            showTimeChosen = " ";
            LoadMainPage();
        }
        public void buttonTime_Click(object sender, EventArgs e)
        {

            nomorkursi.Text = "Chosen Seat: ";

            foreach(Button btnn in listButtonsTime)
            {
                btnn.BackColor = Color.Black;
            }

            foreach (Button btnn in listButtonSeating)
            {
                if (btnn.BackColor == Color.Gold)
                {
                    btnn.BackColor = Color.LightGray;
                }
            }

            Button btn = sender as Button;
            showTimeChosen = btn.Text.ToString();
            btn.BackColor = Color.Green;

            string tagnya = btn.Tag.ToString();
            if (tagnya.Length == 2)
            {
                tandaposter = (Convert.ToInt32(tagnya.Substring(1, 1)) - 1) * 100;
            }
            else
            {
                tandaposter = (Convert.ToInt32(tagnya.Substring(1, 2)) - 1) * 100;
            }
            
            //LABEL JAM
            labelJam.Text = "Chosen Time: " + showTimeChosen;

            panelSeating.Controls.Clear();
            //SEATING LABEL
            int x6 = 111;
            int y6 = 20;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Label labelHuruf = new Label();
                    labelHuruf.Location = new Point(x6, y6);
                    labelHuruf.Text = hurufSeating[j].ToString();
                    labelHuruf.AutoSize = true;
                    labelHuruf.ForeColor = Color.White;
                    panelSeating.Controls.Add(labelHuruf);
                    y6 += 28;
                }
                x6 += 193;
                y6 = 20;
            }

            //BUTONNS SEATING
            for (int i = tandaposter; i < tandaposter+100; i++)
            {
                panelSeating.Controls.Add(listButtonSeating[i]);
            }
            //BUTTON LAYAR
            Button btnlayar = new Button();
            btnlayar.Size = new Size(340, 20);
            btnlayar.Location = new Point(45, 295);
            btnlayar.Text = "Screen";
            btnlayar.BackColor = Color.LightGray;
            btnlayar.Enabled = false;
            panelSeating.Controls.Add(btnlayar);
        }

        int counter;
        public void buttonSeating_Click(object sender, EventArgs e)
        {
            counter = 0;
            Button btn = sender as Button;
            nomorkursi.Text = "Chosen Seat: ";
            if (btn.Tag.ToString() == "0")
            {
                btn.BackColor = Color.Gold;
                btn.Tag = "1";
            }
            else if (btn.Tag.ToString() == "1")
            {
                btn.BackColor = Color.LightGray;
                btn.Tag = "0";
            }
            foreach(var btnn in listButtonSeating)
            {
                if (btnn.BackColor == Color.Gold)
                {
                    nomorkursi.Text += btnn.Text + " ";
                    counter++;
                }
            }
        }
    }
}
