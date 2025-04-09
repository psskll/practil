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

namespace iNir
{
    public partial class Manager : Form
    {
        // Определяем цвета по умолчанию и активные цвета
        private Color defaultBackgroundColor = Color.White; // Выберите цвет
        private Color defaultForekgroundColor = Color.Black; // Выберите цвет
        private Color activeBackroundColor = Color.Blue;   // Выберите цвет
        private Color activeForegroundColor = Color.White;  // Выберите цвет
        private IconButton currentlyActiveButton = null; // Текущая активная кнопка

        public Manager()
        {
            InitializeComponent();
        }
        // Устанавливает цвета кнопки
        private void SetButtonColors(IconButton button, Color backcolor, Color forecolor)
        {
            button.BackColor = backcolor;
            button.ForeColor = forecolor;
            button.IconColor = forecolor;
        }
        // Открывает форму в панели
        private void OpenFormInPanel(Form form)
        {
            // Устанавливаем форму как безрамочную (если требуется)
            form.FormBorderStyle = FormBorderStyle.None;
            form.TopLevel = false; // Убираем верхний уровень
            form.Dock = DockStyle.Fill; // Заполняем панель

            // Очищаем panel3 перед добавлением новой формы
            panel3.Controls.Clear();

            // Добавляем форму в panel3
            panel3.Controls.Add(form);

            // Показываем форму
            form.Show();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                this.WindowState |= FormWindowState.Maximized;
            }
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
             Application.Exit();
        }
        // Активирует выбранную кнопку и деактивирует предыдущую
        private void ActivateButton(IconButton button)
        {
            // Деактивируем предыдущую активную кнопку
            if (currentlyActiveButton != null)
            {
                SetButtonColors(currentlyActiveButton, defaultBackgroundColor, defaultForekgroundColor);
            }

            // Активируем текущую кнопку
            currentlyActiveButton = button;
            SetButtonColors(button, activeBackroundColor, activeForegroundColor);

            // Скрываем левые панели
            leftPanel1.Visible = false;
            leftPanel2.Visible = false;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton((IconButton)sender);
            OpenFormInPanel(new Form5()); // Открываем Form3
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActivateButton((IconButton)sender);
            OpenFormInPanel(new Form6()); // Открываем Form4
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Кондиционер Samsung AR09TXHQASINUA / XUA создает и поддерживает комфортный микроклимат в помещении.Он обеспечивает высокую эффективность работы в режимах охлаждения, обогрева, вентиляции и осушения воздуха.");
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вентилятор Profi Care BV176132 — это напольная модель, предназначенная для эффективного охлаждения воздуха в помещениях. Оснащен тремя скоростными режимами, что позволяет регулировать интенсивность воздушного потока под разные потребности.");
        }
    

        private void guna2Button3_Click(object sender, EventArgs e)
        {
        MessageBox.Show("Портативная акустика JBL Boombox 3 Black (BOOMBOX3-BK) Будь-то поездка на автомобиле, пляжная вечеринка или пикник на природе — Boombox 3 обеспечивает мощный саунд в любой ситуации JBL");

    }

    private void guna2Button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Смартфон Apple iPhone 16 в исполнении черного цвета и объемом памяти 128 ГБ представляет собой передовой гаджет, сочетающий в себе мощные технические характеристики и стильный дизайн. ");

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
