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

        private const string BaseUrl = "http://localhost:4354/api/";
        public Form1()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:4354/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            //email és felhasználónév elküldése, majd ellenőrzése
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            HttpClient client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            HttpResponseMessage response;
            try
            {
                response = await client.PostAsync(BaseUrl, content);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                // Successful login
                // Redirect to another form
                
                Form2 Form2 = new Form2(textBox1.Text);
                Form2.Show();
                this.Hide();
            }
            else
            {
                // Failed login
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
