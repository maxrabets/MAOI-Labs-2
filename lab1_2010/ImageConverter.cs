using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace lab1_2010
{
    public class ImageConverter
    {
        public void WriteBmpToDataGridView(Bitmap bitmap, DataGridView table)
        {
            table.Columns.Clear();

            for (int i = 0; i < bitmap.Width; i++)
            {
                table.Columns.Add(i.ToString(), i.ToString());
                table.Columns[i].Width = 90;
            }

            table.Rows.Clear();
            table.Rows.Add(bitmap.Height);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    table.Rows[j].HeaderCell.Value = j.ToString();
                    Color pixel = bitmap.GetPixel(i, j);
                    table.Rows[j].Cells[i].Value = $"{pixel.R}, {pixel.G}, {pixel.B}";
                }
            }
        }

        public void WriteGrayBmpToDataGridView(Bitmap bitmap, DataGridView table)
        {
            table.Columns.Clear();

            for (int i = 0; i < bitmap.Width; i++)
            {
                table.Columns.Add(i.ToString(), i.ToString());
                table.Columns[i].Width = 30;
            }

            table.Rows.Clear();
            table.Rows.Add(bitmap.Height);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    table.Rows[j].HeaderCell.Value = j.ToString();
                    Color pixel = bitmap.GetPixel(i, j);
                    table.Rows[j].Cells[i].Value = pixel.R;
                    
                    table.Rows[j].Cells[i].Style.BackColor = pixel;
                }
            }
        }

        public void WriteIntArrayToDataGridView(int[,] array, DataGridView table, List<int> highlitedValues = null)
        {
            table.Columns.Clear();

            for (int i = 0; i < array.GetLength(1); i++)
            {
                table.Columns.Add(i.ToString(), i.ToString());
                table.Columns[i].Width = 30;
            }

            table.Rows.Clear();
            table.Rows.Add(array.GetLength(0));

            for (int i = 0; i < array.GetLength(1); i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    table.Rows[j].HeaderCell.Value = j.ToString();
                    table.Rows[j].Cells[i].Value = array[j, i];
                    if (highlitedValues != null && highlitedValues.Contains(array[j, i]))
                    {
                        table.Rows[j].Cells[i].Style.BackColor = Color.Red;
                    }
                }
            }
        }

        public void WriteIntArrayToDataGridView(byte[,] array, DataGridView table, List<int> highlitedValues = null)
        {
            table.Columns.Clear();

            for (int i = 0; i < array.GetLength(1); i++)
            {
                table.Columns.Add(i.ToString(), i.ToString());
                table.Columns[i].Width = 30;
            }

            table.Rows.Clear();
            table.Rows.Add(array.GetLength(0));

            for (int i = 0; i < array.GetLength(1); i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    table.Rows[j].HeaderCell.Value = j.ToString();
                    table.Rows[j].Cells[i].Value = array[j, i];
                    if (highlitedValues != null && highlitedValues.Contains(array[j, i]))
                    {
                        table.Rows[j].Cells[i].Style.BackColor = Color.Red;
                    }
                }
            }
        }

        public Color[,] ConvertBmpToPixelArray(Bitmap colorBmp)
        {
            Color[,] pixelArray = new Color[colorBmp.Width, colorBmp.Height];
            for (int i = 0; i < colorBmp.Width; i++)
            {
                for (int j = 0; j < colorBmp.Height; j++)
                {
                    pixelArray[i, j] = colorBmp.GetPixel(i, j);
                }
            }

            return pixelArray;
        }

        public byte[,] ConvertGrayBmpToPixelArray(Bitmap colorBmp)
        {
            byte[,] pixelArray = new byte[colorBmp.Width, colorBmp.Height];
            for (int i = 0; i < colorBmp.Width; i++)
            {
                for (int j = 0; j < colorBmp.Height; j++)
                {
                    pixelArray[i, j] = colorBmp.GetPixel(i, j).B;
                }
            }

            return pixelArray;
        }

        public Bitmap CreateNonIndexedImage(Image src)
        {
            Bitmap newBmp = new Bitmap(src.Width, src.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics gfx = Graphics.FromImage(newBmp))
            {
                gfx.DrawImage(src, 0, 0);
            }
            return newBmp;
        }
    }
}
