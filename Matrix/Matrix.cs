using System.Linq;

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

        public string ToString()
        {
            string s = "";
            for (int i = 0; i < Rows; i++)
            {
                for (int n = 0; n < Cols; n++)
                {
                    s += _data[i][n] + ",";
                }
                s += System.Environment.NewLine;
            }
            return s.Remove(s.Length - 1 - System.Environment.NewLine.Length);
        }

        public Matrix Transform()
        {
            var r = new Matrix(Cols, Rows);
            for (int i = 0; i < Cols; i++)
                for (int n = 0; n < Rows; n++)
                    r._data[i][n] = _data[n][i];
            return r;
        }

        public double Determinant()
        {
            if (Cols == 2)
                return _data[0][0] * _data[1][1] - _data[0][1] * _data[1][0];
            double v = 0;
            for (int i = 0; i < Cols; i++)
            {
                var m = new Matrix(Rows - 1, Cols - 1);
                for (int n = 1; n < Rows; n++)
                {
                    bool skipped = false;
                    for (int a = 0; a < Cols; a++)
                    {
                        if (a == i)
                        {
                            skipped = true;
                            continue;
                        }
                        m._data[n - 1][skipped ? a - 1 : a] = _data[n][a];
                    }
                }
                v += _data[0][i] * System.Math.Pow(-1, i) * m.Determinant();
            }
            return v;
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

        public static Matrix operator -(Matrix one, Matrix two)
        {
            var r = new Matrix(one.Rows, one.Cols);
            for (int i = 0; i < one.Rows; i++)
            {
                for (int n = 0; n < one.Cols; n++)
                {
                    r._data[i][n] = one.Data[i][n] - two.Data[i][n];
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
            else if ((object)one == null || (object)two == null)
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

        public static bool operator !=(Matrix one, Matrix two)
        {
            return !(one == two);
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
                    d[i][n] = double.Parse(t[i * cols + n]);
                }
            }
            return new Matrix(d);
        }

        public static Matrix Identity(int size)
        {
            string s = "";
            for (int i = 0; i < size; i++)
            {
                for (int n = 0; n < size; n++)
                {
                    if (n == i)
                        s += "1,";
                    else s += "0,";
                }
            }
            return Matrix.Parse(s.Remove(s.Length - 1), size, size);
        }

        public static Matrix Transform(Matrix mat)
        {
            return mat.Transform();
        }

        public static double Det(Matrix mat)
        {
            return mat.Determinant();
        }
    }
}