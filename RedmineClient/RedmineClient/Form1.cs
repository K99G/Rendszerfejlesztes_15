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
    public partial class Form1 : Form
    {
        static HttpClient client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:4354/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //email és felhasználónév elküldése, majd ellenőrzése
            this.Hide();
            Form2 Form2= new Form2(textBox1.Text);
            Form2.Show();
        }
    }
}
