using Newtonsoft.Json;
using RedmineClient.DTOs;
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

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            string managerName = labelManager.Text;
            //saját feladatok betöltése
            //var response = await client.GetStringAsync(managerName); 
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(managerName);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as string
                        string responseData = await response.Content.ReadAsStringAsync();


                        TaskDTO[] data = Newtonsoft.Json.JsonConvert.DeserializeObject<TaskDTO[]>(responseData);

                        // Bind the data to the DataGridView
                        dataGridViewTracks.DataSource = data;
                    }
                    else
                    {
                        MessageBox.Show("Failed to retrieve data from the server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Error connecting to server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            //projektek listázása
            await LoadData();
        }
        private async Task LoadData()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as string
                        string responseData = await response.Content.ReadAsStringAsync();

                        // Parse the response data (assuming it's in JSON format)
                        // You may need to adjust this part based on your actual data format
                        // For example, if your data is in XML format, you would deserialize it differently
                        // Here, I'm assuming the response data is a JSON array of objects
                        // and you have a class representing the structure of your data
                        ProjectDTO[] data = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectDTO[]>(responseData);

                        // Bind the data to the DataGridView
                        dataGridViewProjects.DataSource = data;
                    }
                    else
                    {
                        MessageBox.Show("Failed to retrieve data from the server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Error connecting to server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async Task button2_ClickAsync(object sender, EventArgs e)
        {
            string projectName = textBoxProjectName.Text;
            string name = textBoxName.Text;
            string description = richTextBoxDescription.Text;
            string developer = comboBoxDeveloper.SelectedText;
            string manager = labelManager.Text;
            //feladat felvétele
            if (string.IsNullOrWhiteSpace(projectName) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(description) ||
                string.IsNullOrWhiteSpace(developer) ||
                string.IsNullOrWhiteSpace(manager))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TaskDTO task = new TaskDTO
            {
                Name = name,
                Description = description,
                Project = projectName,
                Developer = developer,
                Manager = manager,
                DateTime = DateTime.Now
                // You may want to set other properties such as ID and DateTime if applicable
            };

            await SendTaskData(task);

        }
        private async Task SendTaskData(TaskDTO task)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Serialize TaskDTO object to JSON
                    string json = JsonConvert.SerializeObject(task);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Send PUT request to the server
                    HttpResponseMessage response = await client.PutAsync(client.BaseAddress, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Task data sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to send task data to the server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"Error connecting to server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
