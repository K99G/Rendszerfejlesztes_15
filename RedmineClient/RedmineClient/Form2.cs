using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedmineClient
{
    public partial class Form2 : Form
    {
        static HttpClient client = new HttpClient();
        public Form2(string name)
        {
            InitializeComponent();
            labelManager.Text = name;
            client.BaseAddress = new Uri("http://localhost:4354/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string managerName=labelManager.Text;
            //saját feladatok betöltése
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //projektek listázása
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string projectName=textBoxProjectName.Text;
            string name=textBoxName.Text;
            string description=richTextBoxDescription.Text;
            string developer=comboBoxDeveloper.SelectedText;
            string manager = labelManager.Text;
            //feladat felvétele
        }
    }
}
