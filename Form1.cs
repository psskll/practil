using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace iNir
{
    public partial class Form1 : Form
    {
        // Строка подключения к базе данных MySQL
        string connectionString = "server=localhost;database=imirr;username=root;password=pussykiller21!;";

        // Объект DataTable для хранения данных из таблицы
        private DataTable productDataTable = new DataTable();

        // Текущая строка, редактируемая в TextBox
        private int currentRow = -1;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox8_TextChanged(object sender, EventArgs e)
        {

        }
       //пустые методы
        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Получаем данные из текстовых полей
            string FirstName = guna2TextBox1.Text;
            string LastName = guna2TextBox2.Text;
            string Email = guna2TextBox6.Text;
            string Phone = guna2TextBox3.Text;
            string Adress = guna2TextBox4.Text;
            string login = guna2TextBox8.Text;
            string password = guna2TextBox10.Text;
            string kod = guna2TextBox11.Text;

            // Проверяем, заполнены ли поля
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Adress) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(kod))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            // Проверка длины номера телефона
            if (Phone.Length != 11 || !Phone.All(char.IsDigit))
            {
                MessageBox.Show("Номер телефона должен содержать 11 цифр.");
                return;
            }

            // Проверка длины пароля
            if (password.Length != 8 || !password.All(char.IsDigit))
            {
                MessageBox.Show("Пароль должен содержать 8 цифр.");
                return;
            }

            // Проверяем, существует ли пользователь с таким логином
            if (UserExists(login))
            {
                MessageBox.Show("Пользователь уже зарегистрирован.");
                return;
            }

            try
            {
                // Создаем соединение с базой данных
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Открываем соединение
                    connection.Open();

                    // Создаем команду INSERT для вставки данных
                    string query = "INSERT INTO customer (FirstName, LastName, Email, Phone, Address, login, password, kod) VALUES (@name, @surname, @email, @phone, @adress, @login, @password, @kod)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Добавляем параметры для команды
                        command.Parameters.AddWithValue("@name", FirstName);
                        command.Parameters.AddWithValue("@surname", LastName);
                        command.Parameters.AddWithValue("@email", Email);
                        command.Parameters.AddWithValue("@phone", Phone);
                        command.Parameters.AddWithValue("@adress", Adress);
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@kod", kod);

                        // Выполняем команду INSERT
                        command.ExecuteNonQuery();

                        MessageBox.Show("Регистрация прошла успешно!");

                        // Очищаем текстовые поля
                        guna2TextBox1.Text = "";
                        guna2TextBox2.Text = "";
                        guna2TextBox3.Text = "";
                        guna2TextBox4.Text = "";
                        guna2TextBox6.Text = "";
                        guna2TextBox8.Text = "";
                        guna2TextBox10.Text = "";
                        guna2TextBox11.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при регистрации: " + ex.Message);
            }
        }
       
        private bool UserExists(string login)
        {
            
                bool exists = false;
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT 1 FROM users WHERE login = @login";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@login", login);
                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                exists = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Данный пользователь не был зарегестрирован ранее ");
                }
                return exists;
           

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
           //нет поясняющих коментариев
            string login = guna2TextBox7.Text;
            string password = guna2TextBox5.Text;
            string kod = guna2TextBox9.Text;

            // Input validation: Check for empty fields and password length
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(kod) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (password.Length != 8)
            {
                MessageBox.Show("Пароль должен содержать ровно 8 символов.");
                return;
            }


            try
            {
                // Создаем соединение с базой данных
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Открываем соединение
                    connection.Open();

                    // Создаем команду SELECT для проверки пользователя
                    string query = "SELECT COUNT(*) FROM customer WHERE login = @login AND password = @password AND kod = @kod";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Добавляем параметры для команды
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@kod", kod);

                        // Выполняем команду SELECT
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                        {
                            // Пользователь найден, авторизация успешна
                            MessageBox.Show("Авторизация успешна!");

                            // Очистка полей ввода
                            guna2TextBox7.Text = "";
                            guna2TextBox5.Text = "";
                            guna2TextBox9.Text = "";

                            // Проверка кода доступа
                            if (kod == "менеджер") // Замените "определённый_код_доступа" на нужный код
                            {
                                // Переход на вкладку менеджера
                                Manager managerForm = new Manager(); // Открытие формы для менеджера
                                managerForm.Show();
                            }
                            else
                            {
                                // Если код доступа не соответствует, открываем стандартную форму
                                Form2 form2 = new Form2(); // Передача логина и пароля в конструктор Form2
                                form2.Show();
                            }

                            this.Hide(); // Скрываем текущую форму
                        }
                        else
                        {
                            // Пользователь не найден, авторизация неуспешна
                            MessageBox.Show("Неверный логин, код или пароль.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при авторизации: " + ex.Message);
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
