using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace lab1_2010
{
    public class TextureHandler
    {
        private int getLongestDimensionLength<T>(T[,] array)
        {
            if(array.GetLength(0) > array.GetLength(1))
            {
                return array.GetLength(0);
            }
            return array.GetLength(1);
        }

        public int[,] countPrimitivesHorizontal(byte[,] pixelArray)
        {
            int[,] countOfPrimitivesMatrix = new int[256, getLongestDimensionLength(pixelArray)];
            for(int i = 0; i < pixelArray.GetLength(0); i++)
            {
                int primitiveLength = 1;
                byte currentBrightness = pixelArray[i, 0];
                for (int j = 0; j < pixelArray.GetLength(1); j++)
                {
                    if(pixelArray[i, j] == currentBrightness)
                    {
                        primitiveLength++;
                    }
                    else
                    {
                        countOfPrimitivesMatrix[currentBrightness, primitiveLength]++;
                        primitiveLength = 1;
                        currentBrightness = pixelArray[i, j];
                    }
                }
            }
            return countOfPrimitivesMatrix;
        }

        public int[,] countPrimitivesVertical(byte[,] pixelArray)
        {
            int[,] countOfPrimitivesMatrix = new int[255, getLongestDimensionLength(pixelArray)];            
            for (int i = 0; i < pixelArray.GetLength(1); i++)
            {
                int primitiveLength = 1;
                byte currentBrightness = pixelArray[0, i];
                for (int j = 0; j < pixelArray.GetLength(0); j++)
                {
                    if (pixelArray[j, i] == currentBrightness)
                    {
                        primitiveLength++;
                    }
                    else
                    {
                        countOfPrimitivesMatrix[currentBrightness, primitiveLength]++;
                        primitiveLength = 1;
                        currentBrightness = pixelArray[j, i];
                    }
                }
            }
            return countOfPrimitivesMatrix;

        }

        public int calculateK(int[,] countOfPrimitivesMatrix)
        {
            int K = 0;
            for (int i = 0; i < countOfPrimitivesMatrix.GetLength(0); i++)
            {
                for (int j = 2; j < countOfPrimitivesMatrix.GetLength(1); j++)
                {
                    K += countOfPrimitivesMatrix[i, j];
                }
            }
            return K;
        }

        public double calculateKPP(int[,] countOfPrimitivesMatrix)
        {
            int K = calculateK(countOfPrimitivesMatrix);
            double KPP = 0;
            for (int i = 0; i < countOfPrimitivesMatrix.GetLength(0); i++)
            {
                for (int j = 2; j < countOfPrimitivesMatrix.GetLength(1); j++)
                {
                    KPP += countOfPrimitivesMatrix[i, j] / (j * j); // (I * I)
                }
            }
            return KPP / K;
        }

        public double calculateDPP(int[,] countOfPrimitivesMatrix)
        {
            double K = calculateK(countOfPrimitivesMatrix);
            double DPP = 0;
            for (int i = 0; i < countOfPrimitivesMatrix.GetLength(0); i++)
            {
                for (int j = 2; j < countOfPrimitivesMatrix.GetLength(1); j++)
                {
                    DPP += countOfPrimitivesMatrix[i, j] * (j * j); // (I * I)
                }
            }
            return DPP / K;
        }

        public double calculateEUS(int[,] countOfPrimitivesMatrix)
        {
            double K = calculateK(countOfPrimitivesMatrix);
            double EUS = 0;
            for (int i = 0; i < countOfPrimitivesMatrix.GetLength(0); i++)
            {
                double countOfPremetives = 0;
                for (int j = 2; j < countOfPrimitivesMatrix.GetLength(1); j++)
                {
                    countOfPremetives += countOfPrimitivesMatrix[i, j]; // (I * I)
                }
                EUS += countOfPremetives * countOfPremetives;
            }
            return EUS / K;
        }

        public double calculateED(int[,] countOfPrimitivesMatrix)
        {
            double K = calculateK(countOfPrimitivesMatrix);
            double ED = 0;
            for (int j = 2; j < countOfPrimitivesMatrix.GetLength(1); j++)
            {
                double jaHzCheEtoNoTakNado = 0;
                for (int i = 0; i < countOfPrimitivesMatrix.GetLength(0); i++)
                {
                    jaHzCheEtoNoTakNado += countOfPrimitivesMatrix[i, j] 
                        * countOfPrimitivesMatrix[i, j]; // (I * I)
                }
                ED += jaHzCheEtoNoTakNado * jaHzCheEtoNoTakNado;
            }
            return ED / K;
        }

        public decimal calculatePP(int[,] countOfPrimitivesMatrix, byte[,] pixelArray)
        {
            decimal K = calculateK(countOfPrimitivesMatrix);
            
            return K / ( pixelArray.GetLength(0) * pixelArray.GetLength(1) );
        }

        public List<int> getPrimitivesBrightnesses(int[,] countOfPrimitivesMatrix)
        {
            List<int> primitivesBrightnesses = new List<int>();
            for (int i = 0; i < countOfPrimitivesMatrix.GetLength(0); i++)
            {
                for (int j = 2; j < countOfPrimitivesMatrix.GetLength(1); j++)
                {
                    if (countOfPrimitivesMatrix[i, j] > 0 && !primitivesBrightnesses.Contains(i))
                    {
                        primitivesBrightnesses.Add(i);
                    }
                }
            }

            return primitivesBrightnesses;
        }

        public double calculateR(byte[,] pixelArray1, byte[,] pixelArray2)
        {
            var countOfPrimitivesMatrix1 = this.countPrimitivesDiagonal1(pixelArray1);
            var countOfPrimitivesMatrix2 = this.countPrimitivesDiagonal1(pixelArray2);
            // 1
            double KPP1 = this.calculateKPP(countOfPrimitivesMatrix1);
            double DPP1 = this.calculateDPP(countOfPrimitivesMatrix1);
            double EUS1 = this.calculateEUS(countOfPrimitivesMatrix1);
            // 2
            double KPP2 = this.calculateKPP(countOfPrimitivesMatrix2);
            double DPP2 = this.calculateDPP(countOfPrimitivesMatrix2);
            double EUS2 = this.calculateEUS(countOfPrimitivesMatrix2);

            return Math.Sqrt(Math.Pow(KPP1 - KPP2, 2) + Math.Pow(DPP1 - DPP2, 2)
                + Math.Pow(EUS1 - EUS2, 2));
        }

        public int[,] countPrimitivesDiagonal1(byte[,] pixelArray, DataGridView table = null,
            ImageConverter imageConverter = null)
        {
            if (table != null && imageConverter != null)
            {
                imageConverter.WriteIntArrayToDataGridView(pixelArray, table);
            }
            // maybe works only for square arrays
            int[,] countOfPrimitivesMatrix = new int[256, getLongestDimensionLength(pixelArray) + 1];
            for (int k = 0; k < pixelArray.GetLength(1); k++)
            {
                int primitiveLength = 0;
                byte currentBrightness = pixelArray[k, 0];
                for (int j = 0; j <= k; j++)
                {
                    int i = k - j;

                    if (table != null)
                    {
                        table.Rows[i].Cells[j].Style.BackColor = Color.Red;
                    }

                    if (pixelArray[i, j] == currentBrightness)
                    {
                        primitiveLength++;
                        if (j == k)
                        {
                            countOfPrimitivesMatrix[currentBrightness, primitiveLength]++;
                            if (table != null && primitiveLength < 2)
                            {
                                table.Rows[i].Cells[j].Style.BackColor = Color.White;
                            }
                        }
                    }
                    else
                    {
                        countOfPrimitivesMatrix[currentBrightness, primitiveLength]++;
                        if (table != null && primitiveLength < 2 && j != 0)
                        {
                            table.Rows[i + 1].Cells[j - 1].Style.BackColor = Color.White;
                        }
                        primitiveLength = 1;
                        currentBrightness = pixelArray[i, j];
                        if(j == k)
                        {
                            countOfPrimitivesMatrix[pixelArray[i, j], 1]++;
                            if (table != null)
                            {
                                table.Rows[i].Cells[j].Style.BackColor = Color.White;
                            }
                        }
                    }
                }
            }
            for (int k = pixelArray.GetLength(1) - 2; k >= 0; k--)
            {
                int primitiveLength = 0;
                byte currentBrightness = pixelArray[k, 0];
                for (int j = 0; j <= k; j++)
                {
                    int i = k - j;
                    if (table != null)
                    {
                        table.Rows[pixelArray.GetLength(1) - j - 1]
                            .Cells[pixelArray.GetLength(1) - i - 1].Style.BackColor = Color.Red;
                    }
                    if (pixelArray[pixelArray.GetLength(1) - j - 1, pixelArray.GetLength(1) - i - 1] == currentBrightness)
                    {
                        primitiveLength++;
                        if (j == k)
                        {
                            countOfPrimitivesMatrix[currentBrightness, primitiveLength]++;
                            if (table != null && primitiveLength < 2)
                            {
                                table.Rows[pixelArray.GetLength(1) - j - 1]
                                    .Cells[pixelArray.GetLength(1) - i - 1].Style.BackColor = Color.White;
                            }
                        }
                    }
                    else
                    {
                        countOfPrimitivesMatrix[currentBrightness, primitiveLength]++;

                        if (table != null && primitiveLength < 2 && j != 0)
                        {
                            table.Rows[pixelArray.GetLength(1) - (j - 1) - 1]
                                .Cells[pixelArray.GetLength(1) - (i + 1) - 1].Style.BackColor = Color.White;
                        }

                        primitiveLength = 1;
                        currentBrightness = pixelArray[pixelArray.GetLength(1) - j - 1, pixelArray.GetLength(1) - i - 1];

                        if (j == k)
                        {
                            countOfPrimitivesMatrix[currentBrightness, 1]++;
                            if (table != null)
                            {
                                table.Rows[pixelArray.GetLength(1) - j - 1]
                                    .Cells[pixelArray.GetLength(1) - i - 1].Style.BackColor = Color.White;
                            }
                        }
                    }
                }
            }

            return countOfPrimitivesMatrix;
        }

    }
}
