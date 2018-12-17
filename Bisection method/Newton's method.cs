using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bisection_method.Model;
using Newtons_method;

namespace Bisection_method
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private int _k = 0;
        private string _fx;


        private void Form1_Load(object sender, EventArgs e)
        {
            //настройки прогресс бара
            progressBar1.Minimum = 0;
        }

        //да, тут лучше переписать нормально, ео лень.
        private int v()
        {
            double tol;
            if (tolBox.Text == "")
            {
                MessageBox.Show(@"Вы не указали точность вычисления.");
                return 0;
            }
            tol = double.Parse(tolBox.Text);

            double a = 0, b = 0;
            if (!(double.TryParse(aBox.Text, out a) && double.TryParse(DeltaBox.Text, out b)))
                //     a = double.Parse(aBox.Text);
                //     b = double.Parse(bBox.Text);

                if (aBox.Text == "" || DeltaBox.Text == "")
                {
                    MessageBox.Show(@"Вы не указали диапазон поиска.");
                    return 0;
                }
            if (a >= b)
            {
                MessageBox.Show(
                    @"Вы неверно указали диапазон поиска, левая граница \n должна быть меньше правой (a < b).");
                return 0;
            }
            if (comboBoxf.Text == "")
            {
                MessageBox.Show(
                    @"Поле 'F' не может быть пустым, выберите элемент из списка \n или напишите вручную формулу.");
                return 0;
            }
            if (tolBox.Text == "")
            {
                MessageBox.Show(@"Укажите точность вычисления.");
                return 0;
            }
            if (k_maxBox.Text == "")
            {
                MessageBox.Show(@"Введите кол-во итераций.");
                return 0;
            }

            return 1;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                labelerr.Text = "";
                _fx = comboBoxf.Text;
                if (v() != 0)
                {
                    var model = new NewtonsModel
                    {
                        Func = _fx,
                        Tol = double.Parse(tolBox.Text),
                        IterationMax = int.Parse(k_maxBox.Text),
                        PointX = decimal.Parse(aBox.Text),
                        Delta = (decimal)double.Parse(DeltaBox.Text),
                        Epsilon = (decimal)double.Parse(tolBox.Text),
                        R = decimal.Parse(RBox.Text)
                    };

                    progressBar1.Visible = true;
                    progressBar1.Maximum = model.IterationMax +1;
                    progressBar1.Value = 0;

                    var stopWatch = new Stopwatch();
                    stopWatch.Start();

                    var newtonsMethod = new NewtonsMethod();

                    newtonsMethod.ProgressBarIncrement += ProgressBarIncrement;

                    var result = await Task.Run(() => newtonsMethod.Calculate(model));

                    stopWatch.Stop();
                    var ts = stopWatch.Elapsed;

                    if (!string.IsNullOrWhiteSpace(result.Error))
                    {
                        MessageBox.Show(result.Error);
                    }
                    else
                    {
                        Sec.Text = ts.TotalSeconds.ToString("0.0");
                        fx1outBox.Text = result.Fx.ToString(" 0e0");
                        x1uotFBox.Text = result.X.ToString(CultureInfo.InvariantCulture);
                        outTolBox.Text = result.Abc.ToString("0e0");
                        countinerBox.Text = result.Iteration.ToString();

                        if (result.Iteration == model.IterationMax && result.Abc > (decimal) model.Tol)
                            labelerr.Text = @"Решение с заданной точностью \n за K_Max(" + model.IterationMax +
                                            @")итераций не удалось найти.";
                    }

                    newtonsMethod.ProgressBarIncrement -= ProgressBarIncrement;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(@"Не удалось распознать F. " + ex.Message);
            }
        }

        private void xBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var number = e.KeyChar; // перевод в ASCII
            if (!char.IsDigit(number) && number != 8 && number != 45 && number != 43 && number != 44 && number != 101
            ) // если нет в условии, то не выводим символ на экран
                e.Handled = true;
        }

        private void deltaBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var number = e.KeyChar; // перевод в ASCII
            if (!char.IsDigit(number) && number != 8 && number != 45 && number != 43 && number != 44 && number != 101
            ) // если нет в условии, то не выводим символ на экран
                e.Handled = true;
        }

        private void tolBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var number = e.KeyChar; // перевод в ASCII
            if (!char.IsDigit(number) && number != 8 && number != 45 && number != 43 && number != 44 && number != 101
            ) // если нет в условии, то не выводим символ на экран
                e.Handled = true;
        }

        private void k_maxBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var number = e.KeyChar; // перевод в ASCII
            if (!char.IsDigit(number) && number != 8) // если нет в условии, то не выводим символ на экран
                e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //сброс полей вывода
            countinerBox.Text = "";
            x1uotFBox.Text = "";
            fx1outBox.Text = "";
            outTolBox.Text = "";
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            Sec.Text = "";
            labelerr.Text = "";
        }

        private void ProgressBarIncrement(int i)
        {
            Action action = () => { progressBar1.Value += i; };
            Invoke(action);
        }

        #region Not Used

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void countinerBox_TextChanged(object sender, EventArgs e)
        {
        }

        /* Ниже происходит обработка входного интерфейса      */
        private void fBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void bBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        #endregion

        private void labela_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}