
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mysqlx.Crud;
using System.Windows.Markup;
using System.Data.SqlClient;
using MongoDB.Driver.Core.Configuration;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace iNir
{
    public partial class Form4 : Form
    {
        // Строка подключения к базе данных MySQL
        string connectionString = "server=localhost;database=imirr;username=root;password=pussykiller21!;";
        private MySqlConnection connection;

        public Form4()
        {
            InitializeComponent();
            // Заполнение комбобоксов
            guna2ComboBox1.Items.Add("Самовывоз");
            guna2ComboBox1.Items.Add("Курьерская доставка");

            // Установка начального выбранного элемента
            guna2ComboBox1.SelectedIndex = 0; // "Самовывоз" по умолчанию
            guna2ComboBox1.SelectedIndexChanged += guna2ComboBox1_SelectedIndexChanged_1; // Use the correct event handler

            // Делаем поле ввода адреса видимым по умолчанию
            guna2TextBox2.Visible = true;
            // Делаем лейбл видимым по умолчанию
            label5.Visible = true;
        }

        internal void AddItemlistBox1(string itemToAdd, string v)
        {
            listBox1.Items.Add(itemToAdd + " (ID: " + v + ")");
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            // Получаем данные из текстовых полей
            string customerID = guna2TextBox4.Text;
            string deliveryAddress = guna2TextBox2.Text;
            string deliveryMethod = guna2ComboBox1.SelectedItem?.ToString(); // Получаем выбранный способ доставки

            // Проверяем введенные данные (особенно customerID)
            if (!int.TryParse(customerID, out int parsedCustomerID))  // Пытаемся преобразовать в целое число
            {
                MessageBox.Show("Неверный формат CustomerID. Пожалуйста, введите число.");  // "Неверный формат CustomerID. Пожалуйста, введите число."
                return; // Останавливаем выполнение, если неверный формат
            }

            if (string.IsNullOrEmpty(deliveryMethod))
            {
                MessageBox.Show("Пожалуйста, выберите способ доставки."); //"Пожалуйста, выберите способ доставки."
                return;
            }
            if (string.IsNullOrEmpty(customerID) || string.IsNullOrEmpty(deliveryAddress))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            try
            {
                // Получаем выбранное название товара из ListBox (предполагая, что у вас есть listBox1)
                if (listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите товар из списка.");
                    return;
                }

                string productName = RemoveControlCharacters(listBox1.SelectedItem.ToString()).Trim();
                Console.WriteLine("Form4: productName из listBox1 = [" + productName + "]");

                // Создаем соединение с базой данных
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Открываем соединение
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM product WHERE Name = @Name";
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Name", productName.Trim());

                        // Вывод сгенерированного SQL-запроса
                        string commandText = checkCommand.CommandText;
                        foreach (MySqlParameter parameter in checkCommand.Parameters)
                        {
                            commandText = commandText.Replace(parameter.ParameterName, "'" + parameter.Value.ToString() + "'");
                        }
                        Console.WriteLine("Form4: Сгенерированный SQL-запрос = " + commandText);

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());


                        // Если товар найден, добавляем заказ
                        string insertQuery = "INSERT INTO ordeer (CustomerID, DeliveryMethod, DeliveryAddress) VALUES (@CustomerID, @DeliveryMethod, @DeliveryAddress)"; // Исправлена опечатка и пробелы
                        using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@CustomerID", parsedCustomerID); // Используем преобразованное целое число
                            insertCommand.Parameters.AddWithValue("@DeliveryMethod", deliveryMethod);   // Добавляем способ доставки
                            insertCommand.Parameters.AddWithValue("@DeliveryAddress", deliveryAddress);  // Используем значение из TextBox2

                            insertCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Заказ успешно добавлен!");

                        // Очищаем текстовые поля
                        guna2TextBox4.Text = "";
                        guna2TextBox2.Text = "";
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка при добавлении заказа: " + ex.Message);
                // Log the error for debugging
                Console.Error.WriteLine("Database Error: " + ex.Message + "\n" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении заказа: " + ex.Message);
                // Log the error for debugging
                Console.Error.WriteLine("General Error: " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        private static string RemoveControlCharacters(string str)
        {
            return string.IsNullOrEmpty(str) ? str : new string(str.Where(c => !char.IsControl(c)).ToArray());
        }

        private void guna2ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Проверяем, выбран ли "Самовывоз"
            if (guna2ComboBox1.SelectedItem.ToString() == "Самовывоз")
            {
                // Если выбран "Самовывоз", скрываем поле ввода адреса
                guna2TextBox2.Visible = false;
                label5.Visible = false;
            }
            else
            {
                // Если выбрана "Курьерская доставка", показываем поле ввода адреса
                guna2TextBox2.Visible = true;
                label5.Visible = true;
            }
        }
    }
}

