using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace es_Dnevnik
{
    public partial class Form1 : Form
    {
        int broj_sloga = 0;
        DataTable tabela;
        private void Load_Data()
        {
            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM osoba", veza);
            veza.Open();
            tabela = new DataTable();
            adapter.Fill(tabela);
            veza.Close();
        }
        private void Txt_Load()
        {
            if (tabela.Rows.Count == 0)
                {
                    textBoxID.Text = "";
                    textBoxIme.Text = "";
                    textBoxPrezime.Text = "";
                    textBoxAdresa.Text = "";
                    textBoxJMBG.Text = "";
                    textBoxEmail.Text = "";
                    textBoxLozinka.Text = "";
                    textBoxUloga.Text = "";
                    buttonDelete.Enabled = false;
                }
            else
            {
                textBoxID.Text = tabela.Rows[broj_sloga]["id"].ToString();
                textBoxIme.Text = tabela.Rows[broj_sloga]["ime"].ToString();
                textBoxPrezime.Text = tabela.Rows[broj_sloga]["prezime"].ToString();
                textBoxAdresa.Text = tabela.Rows[broj_sloga]["adresa"].ToString();
                textBoxJMBG.Text = tabela.Rows[broj_sloga]["jmbg"].ToString();
                textBoxEmail.Text = tabela.Rows[broj_sloga]["email"].ToString();
                textBoxLozinka.Text = tabela.Rows[broj_sloga]["pass"].ToString();
                textBoxUloga.Text = tabela.Rows[broj_sloga]["uloga"].ToString();
                buttonDelete.Enabled = true;
            }

            if (broj_sloga == tabela.Rows.Count - 1)
            {
                buttonForward.Enabled = false;
                buttonLast.Enabled = false;
            }
            else
            {
                buttonForward.Enabled = true;
                buttonLast.Enabled = true;
            }
            if (broj_sloga == 0)
            {
                buttonBack.Enabled = false;
                buttonFirst.Enabled = false;
            }
            else
            {
                buttonBack.Enabled = true;
                buttonFirst.Enabled = true;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabela = new DataTable();
            Load_Data();
            Txt_Load();
        }

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            broj_sloga = 0;
            Txt_Load();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            broj_sloga--;
            Txt_Load();
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            broj_sloga = tabela.Rows.Count - 1;
            Txt_Load();
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            broj_sloga++;
            Txt_Load();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            StringBuilder naredba = new StringBuilder("INSERT INTO osoba (ime, prezime, adresa, jmbg, email, pass, uloga) VALUES ('");
            naredba.Append(textBoxIme.Text + "', '");
            naredba.Append(textBoxPrezime.Text + "', '");
            naredba.Append(textBoxAdresa.Text + "', '");
            naredba.Append(textBoxJMBG.Text + "', '");
            naredba.Append(textBoxEmail.Text + "', '");
            naredba.Append(textBoxLozinka.Text + "', '");
            naredba.Append(textBoxUloga.Text + "')");
            SqlConnection veza = Konekcija.Connect();
            SqlCommand komanda = new SqlCommand(naredba.ToString(), veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Load_Data();
            Txt_Load();
            broj_sloga = tabela.Rows.Count - 1;
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            StringBuilder naredba = new StringBuilder("UPDATE osoba SET ");
            naredba.Append("ime = '" + textBoxIme.Text + "', ");
            naredba.Append("prezime = '" + textBoxPrezime.Text + "', ");
            naredba.Append("adresa = '" + textBoxAdresa.Text + "', ");
            naredba.Append("jmbg = '" + textBoxJMBG.Text + "',  ");
            naredba.Append("email = '" + textBoxEmail.Text + "', ");
            naredba.Append("pass = '" + textBoxLozinka.Text + "', ");
            naredba.Append("uloga = '" + textBoxUloga.Text + "'");
            naredba.Append("WHERE id = " + textBoxID.Text);
            SqlConnection veza = Konekcija.Connect();
            SqlCommand komanda = new SqlCommand(naredba.ToString(), veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Load_Data();
            Txt_Load();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            StringBuilder naredba = new StringBuilder("DELETE FROM osoba WHERE id = " + textBoxID.Text);
            SqlConnection veza = Konekcija.Connect();
            SqlCommand komanda = new SqlCommand(naredba.ToString(), veza);
            bool brisano = false;
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
                brisano = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (brisano)
            {
                Load_Data();
                if (broj_sloga > 0) broj_sloga--;
                Txt_Load();
            }
        }
    }
}
