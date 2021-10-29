
using System;

namespace RightTriangleApi
{    
    public static class GridRightTriangleCalculator
    {
        const int GridSize = 60;
        const int IsoscelesSidesLenght = 10;        

        public static string GetTriangleGridPosition(GridRightTriangle triangle)
        {
            string row = GetTriangleRow(triangle);
            int column = GetTriangleColumn(triangle);

            return row + column.ToString();
        }
        private static string GetTriangleRow(GridRightTriangle triangle)
        {
            int triangleMidPointY = triangle.MidPointY();
            int rowLetterIndex = ((GridSize - triangleMidPointY) / 10) + 1;
            string row = ((Char)(65 + (rowLetterIndex - 1))).ToString();

            return row;
        }
        private static int GetTriangleColumn(GridRightTriangle triangle)
        {
            int columnNumber = triangle.V1.x / IsoscelesSidesLenght * 2 + (triangle.V1.y > triangle.MidPointY() ? 0 : 1);
            return columnNumber;
        }

        public static GridRightTriangle GetTriangle(char row, int column) {

            bool isColumnEven = column % 2 == 0;
            int columnCoordinate = (isColumnEven? column: column + 1) / 2 * IsoscelesSidesLenght;
            int rowCoordinate = GetRowCoordinate(row);

            GridRightTriangle triangle = new GridRightTriangle (
                (columnCoordinate - (isColumnEven ? 0 : IsoscelesSidesLenght), rowCoordinate - (isColumnEven ? 0 : IsoscelesSidesLenght)),
                (columnCoordinate - IsoscelesSidesLenght, rowCoordinate),
                (columnCoordinate, rowCoordinate - IsoscelesSidesLenght)
            );

            return triangle;
        }

        private static int GetRowCoordinate(char row)
        {
            int rowIndex = char.ToUpper(row) - 'A' + 1;
            int rowCoordinate = GridSize - (rowIndex - 1) * IsoscelesSidesLenght;

            return rowCoordinate;
        }
    }
}
