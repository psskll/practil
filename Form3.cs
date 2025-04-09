using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace iNir
{
    public partial class Form3 : Form
    {
        private Form4 form4; // Экземпляр Form4

        public Form3()
        {
            InitializeComponent();
            form4 = new Form4();
            // Добавление обработчика события при выборе строки в DataGridView1
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        public class DatabaseConnection
        {
            private static string connectionString = "server=localhost;database=imirr;username=root;password=pussykiller21!;";

            public static MySqlConnection GetConnection()
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                return connection;
            }

            //Метод для получения строки подключения
            public static string GetConnectionString()
            {
                return connectionString;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
                // In Form3
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    try
                    {
                        // Add null checks and type conversions
                        int id;
                        string Name = GetCellValue(selectedRow, 1);
                        string Description = GetCellValue(selectedRow, 2);
                        string Price = GetCellValue(selectedRow, 3);
                        string CategoryID = GetCellValue(selectedRow, 4);
                        string ManufacturerID = GetCellValue(selectedRow, 5);
                        string WarrantyMonths = GetCellValue(selectedRow, 6);

                        if (!int.TryParse(GetCellValue(selectedRow, 0), out id) || string.IsNullOrEmpty(Name))
                        {
                            MessageBox.Show("Ошибка: Невозможно получить данные из выбранной строки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Уточните, какие данные вы хотите добавить в строку itemToAdd
                        string itemToAdd = $"{Name}";

                        // Добавляем отладочный вывод!
                        Console.WriteLine("Form3: itemToAdd = [" + itemToAdd + "], id = [" + id + "]");

                        // Получаем экземпляр формы 4 (если она уже открыта)
                        Form4 form4 = Application.OpenForms.OfType<Form4>().FirstOrDefault();

                        if (form4 == null)
                        {
                            // Если форма не открыта, создаем новую
                            form4 = new Form4();
                            form4.Show();
                        }

                        // Добавляем элемент в CheckedListBox (предполагается, что AddItemToCheckedListBox принимает id)
                        form4.AddItemlistBox1(itemToAdd, id.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Log the exception for debugging
                        Console.WriteLine(ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку из таблицы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            



        }

        private static string GetCellValue(DataGridViewRow row, int columnIndex)
        {
            if (row.Cells[columnIndex].Value == null || row.Cells[columnIndex].Value == DBNull.Value)
            {
                return string.Empty; // or return a default value
            }
            return row.Cells[columnIndex].Value.ToString().Trim(); //Added Trim() here
        }




        private void Form3_Load(object sender, EventArgs e)
        {
            string poo = "SELECT * FROM product"; // Исправленное название таблицы
            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(poo, connection))
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;

                    // Устанавливаем русские заголовки для столбцов
                    dataGridView1.Columns[0].HeaderText = "ID Продукта";
                    dataGridView1.Columns[1].HeaderText = "Название";
                    dataGridView1.Columns[2].HeaderText = "Описание";
                    dataGridView1.Columns[3].HeaderText = "Цена";
                    dataGridView1.Columns[4].HeaderText = "ID Категории";
                    dataGridView1.Columns[5].HeaderText = "ID Производителя";
                    dataGridView1.Columns[6].HeaderText = "Гарантийный срок";
                    connection.Close();
                }
            }
        }

      
        

      
    }
}
