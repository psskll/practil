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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            // Добавление обработчика события при выборе строки в DataGridView1
            // Добавление обработчика события при выборе строки в DataGridView1
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void guna2Button1_Click(object sender, EventArgs e)//Изменить
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для добавления в базу данных.", "Ошибка");
                return;
            }

            try
            {
                // Get values, handling nulls and using TryParse
                if (!int.TryParse(dataGridView1.SelectedRows[0].Cells[0].Value?.ToString(), out int ProductID) ||
                    !decimal.TryParse(dataGridView1.SelectedRows[0].Cells[3].Value?.ToString(), out decimal Price) ||
                    !int.TryParse(dataGridView1.SelectedRows[0].Cells[4].Value?.ToString(), out int CategoryID) ||
                    !int.TryParse(dataGridView1.SelectedRows[0].Cells[5].Value?.ToString(), out int ManufacturerID) ||
                    !int.TryParse(dataGridView1.SelectedRows[0].Cells[6].Value?.ToString(), out int WarrantyMonths))
                {
                    MessageBox.Show("Ошибка при конвертации данных. Проверьте правильность данных в выбранной строке.", "Ошибка");
                    return;
                }


                string Name = dataGridView1.SelectedRows[0].Cells[1].Value?.ToString() ?? "";
                string Discription = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString() ?? "";



                string query = "INSERT INTO product (ProductID, Name, Discription, Price, CategoryID, ManufacturerID, WarrantyMonths) " +
                               "VALUES (@ProductID, @Name, @Discription, @Price, @CategoryID, @ManufacturerID, @WarrantyMonths)";




                using (MySqlConnection connection = DatabaseConnection.GetConnection())
                using (MySqlCommand command = new MySqlCommand(query, connection))


                {
                    // Add parameters
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Discription", Discription);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@CategoryID", CategoryID);
                    command.Parameters.AddWithValue("@ManufacturerID", ManufacturerID);
                    command.Parameters.AddWithValue("@WarrantyMonths", WarrantyMonths);


                    try


                    {


                        connection.Open();

                        command.ExecuteNonQuery();


                        MessageBox.Show("Данные успешно добавлены в базу данных!", "Успешно");





                    }




                    catch (MySqlException ex)


                    {


                        // Handle specific MySqlExceptions (e.g., duplicate key, foreign key constraint)

                        if (ex.Number == 1062) // Duplicate entry error code


                        {


                            MessageBox.Show("Ошибка: Товар с таким ID уже существует.", "Ошибка");

                        }


                        else if (ex.Number == 1452) // Foreign key constraint error code


                        {
                            MessageBox.Show("Ошибка: Неверный CategoryID или ManufacturerID.", "Ошибка");



                        }


                        else



                        {



                            MessageBox.Show($"Ошибка MySQL: {ex.Message}", "Ошибка");




                        }

                    }

                    catch (Exception ex)
                    {

                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");



                    }


                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка");
            }
        }


        public class DatabaseConnection
        {
            public static MySqlConnection GetConnection()
            {
                string connectionString = "server=localhost;database=imirr;username=root;password=pussykiller21!;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                return connection;
            }

        }

        private void Form5_Load(object sender, EventArgs e)
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
                    dataGridView1.Columns[0].HeaderText = "ID";
                    dataGridView1.Columns[1].HeaderText = "Название";
                    dataGridView1.Columns[2].HeaderText = "Описание";
                    dataGridView1.Columns[3].HeaderText = "Цена";
                    dataGridView1.Columns[4].HeaderText = "ID Категории";
                    dataGridView1.Columns[5].HeaderText = "ID Производителя";
                    dataGridView1.Columns[6].HeaderText = "Гарантия";
                    connection.Close();
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)//УДАЛИТЬ
        {

            // Проверяем, выбрана ли строка в DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка");
                return;
            }

            // Получаем ID выбранной строки из DataGridView (предполагается, что ID находится в первом столбце)
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            // Формируем SQL-запрос для удаления строки
            string p = "DELETE FROM product WHERE ProductID = @id"; // Используйте имя таблицы "q" 

            // Создаем объект MySqlCommand
            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(p, connection))
                {
                    // Добавляем параметр для защиты от SQL-инъекций
                    command.Parameters.AddWithValue("@id", id); // Передайте ID

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Строка успешно удалена из базы данных!", "Успешно");
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e, MySqlConnection mySqlConnection)//ОБНОВИТЬ
        {



            //Remove LoadBooksData() - No longer needed


        }

        private void UpdateDataGridView1()
        {
            try
            {
                using (MySqlConnection connection = DatabaseConnection.GetConnection())
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM product", connection))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    connection.Open();
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;

                    // Set column headers efficiently (only once)
                    if (dataGridView1.Columns.Count > 6) // Check for all expected columns
                    {
                        dataGridView1.Columns[0].HeaderText = "ProductID";
                        dataGridView1.Columns[1].HeaderText = "Name";
                        dataGridView1.Columns[2].HeaderText = "Discription";
                        dataGridView1.Columns[3].HeaderText = "Price";
                        dataGridView1.Columns[4].HeaderText = "CategoryID";
                        dataGridView1.Columns[5].HeaderText = "ManufacturerID";
                        dataGridView1.Columns[6].HeaderText = "WarrantyMonths";
                    }


                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка MySQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Общая ошибка: {ex.Message}");
            }
        }

        private static void Form_Load(object sender, EventArgs e)

        {

            UpdateDataGridView();

        }

        private static void guna2Button3_Click1(object sender, EventArgs e)
        {

            UpdateDataGridView();



        }

        private static void UpdateDataGridView()
        {
            throw new NotImplementedException();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
