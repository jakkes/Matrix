using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixNET
{
    public class Matrix
    {

        public int Rows { get { return _data.Length; } }
        public int Cols { get { return _data[0].Length; } }
        public double[][] Data { get { return _data; } }

        private double[][] _data;

        public Matrix(int rows, int cols)
        {
            _data = new double[rows][];
            for (int i = 0; i < rows; i++)
                _data[i] = new double[cols];
        }

        public Matrix(double[][] data)
        {
            _data = data;
        }

        public static Matrix operator +(Matrix one, Matrix two)
        {
            var r = new Matrix(one.Rows, one.Cols);
            for (int i = 0; i < one.Rows; i++)
            {
                for (int n = 0; n < one.Cols; n++)
                {
                    r._data[i][n] = one.Data[i][n] + two.Data[i][n];
                }
            }
            return r;
        }

        public static Matrix operator *(Matrix one, Matrix two)
        {
            var r = new Matrix(one.Rows, two.Cols);
            for (int i = 0; i < r.Rows; i++)
            {
                for (int n = 0; n < r.Cols; n++)
                {
                    double value = 0;
                    for (int a = 0; a < one.Data[i].Length; a++)
                    {
                        value += one.Data[i][a] * two.Data[a][n];
                    }
                    r._data[i][n] = value;
                }
            }
            return r;
        }

        public static Matrix operator *(Matrix one, double two)
        {
            var r = new Matrix(one.Rows, one.Cols);
            for (int i = 0; i < one.Rows; i++)
            {
                for (int n = 0; n < one.Cols; n++)
                {
                    r._data[i][n] = two * one._data[i][n];
                }
            }
            return r;
        }

        public static bool operator ==(Matrix one, Matrix two)
        {
            if (System.Object.ReferenceEquals(one, two))
                return true;
            else if ((object)one == null || (object)null == null)
                return false;
            if (one.Rows != two.Rows || one.Cols != two.Cols)
                return false;
            for (int i = 0; i < one.Rows; i++)
            {
                for (int n = 0; n < one.Cols; n++)
                {
                    if (one.Data[i][n] != two.Data[i][n])
                        return false;
                }
            }
            return true;
        }

        public static Matrix Parse(string values, int rows, int cols)
        {
            var t = values.Split(',');
            var d = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                d[i] = new double[cols];
                for (int n = 0; n < cols; n++)
                {
                    d[i][n] = double.Parse(t[(i + 1) * n]);
                }
            }
            return new Matrix(d);
        }
    }
}
