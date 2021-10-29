using System;
using Xunit;

namespace RightTriangleApi.Test
{
    public class RightTriangleTest
    {
        [Fact]
        public void GetPosition_ReturnA1()
        {
            var result = RightTriangleCalculator.GetPosition(0, 10, 0, 0, 10, 10);
            Assert.True(result.Row == "A" && result.Column == 1);
        }

        [Fact]
        public void GetPosition_ReturnA2()
        {
            var result = RightTriangleCalculator.GetPosition(10, 0, 0, 0, 10, 10);
            Assert.True(result.Row == "A" && result.Column == 2);
        }

        [Fact]
        public void GetPosition_ReturnC5()
        {
            var result = RightTriangleCalculator.GetPosition(20, 30, 20, 20, 30, 30);
            Assert.True(result.Row == "C" && result.Column == 5);
        }

        [Fact]
        public void GetPosition_ReturnC6()
        {
            var result = RightTriangleCalculator.GetPosition(30, 20, 20, 20, 30, 30);
            Assert.True(result.Row == "C" && result.Column == 6);
        }

        [Fact]
        public void GetPosition_ReturnF12()
        {
            var result = RightTriangleCalculator.GetPosition(60, 50, 50, 50, 60, 60);
            Assert.True(result.Row == "F" && result.Column == 12);
        }

        [Theory]
        [InlineData(0, 50, 20, 50, 10, 40)]
        [InlineData(20, 60, 10, 50, 0, 60)]
        [InlineData(40, 0, 40, 20, 30, 30)]
        [InlineData(20, 40, 30, 30, 30, 60)]
        [InlineData(60, 10, 60, 20, 50, 0)]
        public void GetPosition_InvalidCoordinates_ReturnArgumentException(int v1x, int v1y, int v2x, int v2y, int v3x, int v3y)
        {
            Assert.Throws<ArgumentException>(() => RightTriangleCalculator.GetPosition(v1x, v1y, v2x, v2y, v3x, v3y));
        }

        [Theory]
        [InlineData("G", 1)]
        [InlineData("A", 0)]
        [InlineData("A", 13)]
        [InlineData("G", 0)]
        [InlineData("G", 13)]
        public void GetCoordinates_InvalidPosition_ReturnArgumentException(string row, int column)
        {
            Assert.Throws<ArgumentException>(() => RightTriangleCalculator.GetCoordinates(row, column));
        }
    }
}
