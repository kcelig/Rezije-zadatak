using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Zadatak_11___Režije
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Pohrana()
        {
            string vrijed = "";
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                vrijed = vrijed + (string)dgv.Rows[i].Cells[0].Value + "\n" + (string)dgv.Rows[i].Cells[1].Value + "\n";
            }

            File.WriteAllText(Application.StartupPath + "\\rezije.txt", vrijed);
        }

        public void Citanje()
        {
            if (File.Exists(Application.StartupPath + "\\rezije.txt"))
            {
                string vrijed = File.ReadAllText(Application.StartupPath + "\\rezije.txt");
                char[] znak = new char[1];
                znak[0] = '\n';
                string[] v = vrijed.Split(znak, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < v.Length; i += 2)
                {
                    dgv.Rows.Add(v[i], v[i + 1]);
                }
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            Form2 forma = new Form2();
            if (forma.ShowDialog() == DialogResult.OK)
            {
                dgv.Rows.Add(forma.txtRezija.Text.Trim(), forma.txtIznos.Text.Trim());
                Pohrana();
                izracun();
            }
        }

        private void btnUredi_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                return;
            }
            Form2 forma = new Form2();
            forma.txtRezija.Text = (string)dgv.SelectedRows[0].Cells[0].Value;
            forma.txtIznos.Text = (string)dgv.SelectedRows[0].Cells[1].Value;

            if (forma.ShowDialog() == DialogResult.OK)
            {
                dgv.Rows.Remove(dgv.SelectedRows[0]);
                dgv.Rows.Add(forma.txtRezija.Text.Trim(), forma.txtIznos.Text.Trim());
                Pohrana();
                izracun();
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                return;
            }

            dgv.Rows.Remove(dgv.SelectedRows[0]);
            Pohrana();
            izracun();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Citanje();
            izracun();
        }

        private void izracun() {

            int sum = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells[1].Value != null)
                {
                    sum += Convert.ToInt32(dgv.Rows[i].Cells[1].Value.ToString());
                    txtUkupno.Text = Convert.ToString(sum);
                }
            }
        }
    }
}
