using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        private double result = 0;
        private string operation = "";
        private bool isOperationPerformed = false;

        public Form1()
        {
            InitializeComponent();
            InitializeCalculator();
            this.Text = "Калькулятор"; // Название окна
            this.BackColor = Color.LightGray; // Цвет фона формы
        }

        private void InitializeCalculator()
        {
            // Настройка экрана
            TextBox textBoxDisplay = new TextBox
            {
                Name = "textBoxDisplay",
                Text = "0",
                ReadOnly = true,
                Width = 200,
                Height = 40,
                Location = new Point(10, 10),
                Font = new Font("Arial", 16),
                TextAlign = HorizontalAlignment.Right
            };
            this.Controls.Add(textBoxDisplay);

            // Настройка кнопок цифр и операций
            string[] buttonTexts = { "7", "8", "9", "/", "4", "5", "6", "*", "1", "2", "3", "-", "0", "C", "=", "+" };
            int x = 10, y = 60;

            for (int i = 0; i < buttonTexts.Length; i++)
            {
                Button button = new Button
                {
                    Text = buttonTexts[i],
                    Width = 50,
                    Height = 50,
                    Location = new Point(x, y),
                    Font = new Font("Arial", 14),
                    BackColor = Color.LightSteelBlue, // Цвет кнопок
                    FlatStyle = FlatStyle.Flat // Плоский стиль кнопок
                };
                button.Click += Button_Click;
                this.Controls.Add(button);

                x += 50;

                if ((i + 1) % 4 == 0)
                {
                    x = 10;
                    y += 50;
                }
            }

            // Подпись "Developed by Матвеева Дарья Алексеевна"
            Label labelSignature = new Label
            {
                Text = "Developed by Матвеева Дарья Алексеевна",
                AutoSize = true,
                Location = new Point(10, y + 60),
                Font = new Font("Arial", 10, FontStyle.Italic),
                ForeColor = Color.DarkSlateGray
            };
            this.Controls.Add(labelSignature);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TextBox textBoxDisplay = (TextBox)this.Controls["textBoxDisplay"];

            if (button.Text == "C")
            {
                textBoxDisplay.Text = "0";
                result = 0;
                operation = "";
                isOperationPerformed = false;
                return;
            }

            if (button.Text == "=")
            {
                switch (operation)
                {
                    case "+": textBoxDisplay.Text = (result + Double.Parse(textBoxDisplay.Text)).ToString(); break;
                    case "-": textBoxDisplay.Text = (result - Double.Parse(textBoxDisplay.Text)).ToString(); break;
                    case "*": textBoxDisplay.Text = (result * Double.Parse(textBoxDisplay.Text)).ToString(); break;
                    case "/":
                        if (textBoxDisplay.Text != "0")
                        {
                            textBoxDisplay.Text = (result / Double.Parse(textBoxDisplay.Text)).ToString();
                        }
                        else
                        {
                            MessageBox.Show("Деление на ноль невозможно!");
                        }
                        break;
                }
                result = Double.Parse(textBoxDisplay.Text);
                operation = "";
                isOperationPerformed = true;
            }
            else if (button.Text == "+" || button.Text == "-" || button.Text == "*" || button.Text == "/")
            {
                operation = button.Text;
                result = Double.Parse(textBoxDisplay.Text);
                isOperationPerformed = true;
            }
            else
            {
                if ((textBoxDisplay.Text == "0") || isOperationPerformed)
                    textBoxDisplay.Text = "";
                isOperationPerformed = false;
                textBoxDisplay.Text += button.Text;
            }
        }
    }
}
