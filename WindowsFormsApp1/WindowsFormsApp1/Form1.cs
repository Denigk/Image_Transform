using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Bitmap image;
            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);
                    //вместо pictureBox1 укажите pictureBox, в который нужно загрузить изображение 

                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox2.Image = image;
                    pictureBox2.Invalidate();
                }
                catch
                {
                    DialogResult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // вставить изображение
            Bitmap image;
            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);
                    //вместо pictureBox1 укажите pictureBox, в который нужно загрузить изображение 

                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = image;
                    pictureBox1.Invalidate();
                }
                catch
                {
                    DialogResult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Если мы через диалог выбрали картинку, то грузим ее в битмап.
            Bitmap b1 = new Bitmap(pictureBox1.Image);
            Bitmap b2 = new Bitmap(pictureBox2.Image);

            //Объявляем переменные для значений высоты и ширины матрицы (картинки)...
            //...и тут же задаем значения этих переменных взяв их из высоты и ширины картинки в пикселях
            int height = b1.Height; //Это высота картинки, и наша матрица по вертикали будет состоять из точно такого же числа элементов.
            int width = b1.Width; //Это ширина картинки, т.е. число элементов матрицы по горизонтали
            //int height_2 = b2.Height;
            //int width_2 = b2.Width;

            b2 = new Bitmap(b2, new Size(width, height));

            Bitmap b3 = new Bitmap(width, height);

            //Тут мы объявляем саму матрицу в виде двумерного массива,
            int[,] colorMatrix_1 = new int[width, height];
            int[,] colorMatrix_2 = new int[width, height];
            long[,] colorMatrix_3 = new long[width, height];

            //Цикл будет выполняться от 0 и до тех пор, пока y меньше height (высоты матрицы и картинки)
            //На каждой итерации увеличиваем значение y на единицу.
            for (int y = 0; y < height; y++)
            {
                //А теперь сканируем горизонтальные строки матрицы:
                for (int x = 0; x < width; x++)
                {
                    //В матрицу добавляем цвет точки с координатами x,y из картинки b1.            
                    colorMatrix_1[x, y] = b1.GetPixel(x, y).G;
                    colorMatrix_2[x, y] = b2.GetPixel(x, y).G;

                    //colorMatrix_3[x, y] = (colorMatrix_1[x, y] - colorMatrix_2[x, y]) + 20;

                    colorMatrix_3[x, y] = (long)(Math.Sqrt(colorMatrix_1[x, y] * colorMatrix_2[x, y]));

                    int d = (int)((colorMatrix_3[x, y] < 255 ? colorMatrix_3[x, y] : 255) & (colorMatrix_3[x, y] > 0 ? colorMatrix_3[x, y] : 0));
                    b3.SetPixel(x, y, Color.FromArgb(d, d, d));
                }
            }

            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Image = b3;
            b3.Save("C:\\WindowsFormsApp1\\Conv_img.jpg");
        }
    }
}
