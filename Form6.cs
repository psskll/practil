using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using MySql.Data.MySqlClient;
using static iNir.Form3;

namespace iNir
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        private Color activeBackroundColor = Color.FromArgb(199, 159, 239);
        private Color activeForegroundColor = Color.FromArgb(106, 90, 205);

        private Color defaultBackgroundColor = Color.FromArgb(230, 230, 250);
        private Color defaultForekgroundColor = Color.FromArgb(200, 200, 200);

        //для перетаскивания формы
        bool dragging = false;
        Point dragCursorPoint;
        Point dragFormPoint;

        private void SetButtonColors(IconButton button, Color backcolor, Color forecolor)
        {
            button.BackColor = backcolor;
            button.ForeColor = forecolor;
            button.IconColor = forecolor;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            string poo = "SELECT * FROM ordeer"; // Исправленное название таблицы
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
                    dataGridView1.Columns[0].HeaderText = "ID Заказа";
                    dataGridView1.Columns[1].HeaderText = "ID Покупателя";
                    dataGridView1.Columns[2].HeaderText = "Способ доставки";
                    dataGridView1.Columns[3].HeaderText = "Адрес доставки";
                    connection.Close();
                }
            }
        }
    }
}
