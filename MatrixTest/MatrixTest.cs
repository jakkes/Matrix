using System;
using MatrixNET;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixTest
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void Equality()
        {
            Assert.IsTrue(Matrix.Parse("1,2,3,4", 2, 2) == Matrix.Parse("1,2,3,4", 2, 2));
            Assert.IsFalse(Matrix.Parse("1,2,3,4", 2, 2) == Matrix.Parse("4,3,2,1", 2, 2));
        }

        [TestMethod]
        public void MultiplyByNumber()
        {
            Assert.IsTrue(Matrix.Parse("1,2,3,4", 2, 2) * 5 == Matrix.Parse("5,10,15,20", 2, 2));
        }

        [TestMethod]
        public void MultiplyByMatrix()
        {
            Assert.IsTrue(Matrix.Parse("1,2,3,4", 2, 2) * Matrix.Parse("1,0,0,1", 2, 2) == Matrix.Parse("1,2,3,4", 2, 2));
            Assert.IsTrue(Matrix.Parse("1,0,2,-1,3,1", 2, 3) * Matrix.Parse("3,1,2,1,1,0", 3, 2) == Matrix.Parse("5,1,4,2", 2, 2));
        }

        [TestMethod]
        public void Add()
        {
            Assert.IsTrue(Matrix.Parse("1,2,3,4", 2, 2) + Matrix.Parse("4,3,2,1", 2, 2) == Matrix.Parse("5,5,5,5", 2, 2));
        }

        [TestMethod]
        public void Transform()
        {
            Assert.IsTrue(Matrix.Parse("1,2,3,4,5,6", 2, 3).Transform() == Matrix.Parse("1,4,2,5,3,6", 3, 2));
            Assert.IsTrue(Matrix.Parse("1,4,2,5,3,6", 3, 2).Transform() == Matrix.Parse("1,2,3,4,5,6", 2, 3));
        }

        [TestMethod]
        public void Det()
        {
            Assert.IsTrue(Matrix.Parse("1,0,0,1", 2, 2).Determinant() == 1);
            Assert.IsTrue(Matrix.Parse("1,2,3,4,5,6,7,8,9", 3, 3).Determinant() == 0);
        }
    }
}
