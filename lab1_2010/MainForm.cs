using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace lab1_2010
{
    public partial class MainForm : Form
    {
        private ImageConverter imageConverter;
        private TextureHandler textureHandler;

        private List<byte[,]> pixelArrays = new List<byte[,]>();
        private List<int[,]> countOfPrimitivesMatrixes = new List<int[,]>();

        public MainForm()
        {
            imageConverter = new ImageConverter();
            textureHandler = new TextureHandler();
            InitializeComponent();

            
            for (int i = 0; i < 4; i++)
            {
                dataGridView3.Rows.Add();
            }
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            string fileName;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                Bitmap bitmap = (Bitmap)Bitmap.FromFile(fileName);
                pictureBox1.Image = bitmap;
                if(pixelArrays.Count > 0)
                {
                    pixelArrays[0] = imageConverter.ConvertGrayBmpToPixelArray(bitmap);
                } else
                {
                    pixelArrays.Add(imageConverter.ConvertGrayBmpToPixelArray(bitmap));
                }
                imageConverter.WriteGrayBmpToDataGridView(bitmap, dataGridView1);
            }
        }

        private void saveGrayButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }

        private void countPrimitivesButton_Click(object sender, EventArgs e)
        {
            countOfPrimitivesMatrixes.Clear();
            for (int i = 0; i < pixelArrays.Count; i++)
            {
                countOfPrimitivesMatrixes.Add(textureHandler.countPrimitivesDiagonal1(pixelArrays[i]));
                dataGridView3.Rows[i].Cells[0].Value = textureHandler.calculateK(countOfPrimitivesMatrixes[i]);
                dataGridView3.Rows[i].Cells[1].Value = textureHandler.calculateKPP(countOfPrimitivesMatrixes[i]);
                dataGridView3.Rows[i].Cells[2].Value = textureHandler.calculateDPP(countOfPrimitivesMatrixes[i]);
                dataGridView3.Rows[i].Cells[3].Value = textureHandler.calculateEUS(countOfPrimitivesMatrixes[i]);
                dataGridView3.Rows[i].Cells[4].Value = textureHandler.calculateED(countOfPrimitivesMatrixes[i]);
                dataGridView3.Rows[i].Cells[5].Value = textureHandler.calculatePP(countOfPrimitivesMatrixes[i], pixelArrays[i]);
            }
            imageConverter.WriteIntArrayToDataGridView(countOfPrimitivesMatrixes[0], dataGridView2);
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].Visible = false;
            textureHandler.countPrimitivesDiagonal1(pixelArrays[0], dataGridView1, imageConverter);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string fileName;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                Bitmap bitmap = (Bitmap)Bitmap.FromFile(fileName);
                pictureBox2.Image = bitmap;
                if (pixelArrays.Count > 1)
                {
                    pixelArrays[1] = imageConverter.ConvertGrayBmpToPixelArray(bitmap);
                }
                else
                {
                    pixelArrays.Add(imageConverter.ConvertGrayBmpToPixelArray(bitmap));
                }
                Console.WriteLine(pixelArrays);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fileName;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                Bitmap bitmap = (Bitmap)Bitmap.FromFile(fileName);
                pictureBox3.Image = bitmap;
                if (pixelArrays.Count > 2)
                {
                    pixelArrays[2] = imageConverter.ConvertGrayBmpToPixelArray(bitmap);
                }
                else
                {
                    pixelArrays.Add(imageConverter.ConvertGrayBmpToPixelArray(bitmap));
                }
                Console.WriteLine(pixelArrays);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string fileName;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                Bitmap bitmap = (Bitmap)Bitmap.FromFile(fileName);
                pictureBox4.Image = bitmap;
                if (pixelArrays.Count > 3)
                {
                    pixelArrays[3] = imageConverter.ConvertGrayBmpToPixelArray(bitmap);
                }
                else
                {
                    pixelArrays.Add(imageConverter.ConvertGrayBmpToPixelArray(bitmap));
                }
                Console.WriteLine(pixelArrays);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pixelArrays.Count > 1)
                label1.Text = "R12 = " + textureHandler.calculateR(pixelArrays[0], pixelArrays[1]);
            if (pixelArrays.Count > 2)
                label2.Text = "R13 = " + textureHandler.calculateR(pixelArrays[0], pixelArrays[2]);
            if (pixelArrays.Count > 3)
                label3.Text = "R14 = " + textureHandler.calculateR(pixelArrays[0], pixelArrays[3]);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
