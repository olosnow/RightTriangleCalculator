
using RightTriangleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RightTriangleApi
{
    public static class RightTriangleCalculator
    {
        //Store const on resource file or app setting
        const int GridSize = 60;
        const int IsoscelesSidesLenght = 10;
        
        public static RightTrianglePosition GetPosition(int v1x, int v1y, int v2x, int v2y, int v3x, int v3y)
        {
            List<int> coordinatesValues = new List<int> { v1x, v1y, v2x, v2y, v3x, v3y };

            //TODO: Add Data Annotation Validators to model classes  
            //TODO: Implement client validation handlers
            //TODO: Check triangle orientation
            if (coordinatesValues.Sum() == 0)
            {
                throw new ArgumentNullException("Triangle coordinates must not be null.");
            }

            if (coordinatesValues.Any(c=> !(c <= GridSize && c % IsoscelesSidesLenght == 0)))
            {
                throw new ArgumentException("Triangle coordinates values are not valid.");
            }

            RightTriangleVertices coordinates = new RightTriangleVertices(v1x, v1y, v2x, v2y, v3x, v3y);

            if (!IsIsoscelesRightTriangle(coordinates))
            {
                throw new ArgumentException("Triangle must be an isosceles right triangle.");
            }

            if (coordinates.V1 == coordinates.V2 || coordinates.V1 == coordinates.V3 || coordinates.V2 == coordinates.V3)
            {
                throw new ArgumentException("Some vertices have the same coordinates.");
            }

            RightTrianglePosition position = new RightTrianglePosition(
                CalculateRow(coordinates), 
                CalculateColumn(coordinates)
            );

            return position;
        }
        public static Dictionary<string, int[]> GetCoordinates(string row, int column)
        {
            if (row == null)
            {
                throw new ArgumentNullException("Row must not be null.");
            }

            int rowIndex = char.Parse(row.ToUpper()) - 'A' + 1;

            if (rowIndex < 0  || rowIndex > GridSize / IsoscelesSidesLenght)
            {
                throw new ArgumentException("Row value is Invalid.");
            }
            if (column <= 0 || column > GridSize / IsoscelesSidesLenght * 2)
            {
                throw new ArgumentException("Column value is Invalid.");
            }

            RightTrianglePosition position = new RightTrianglePosition(row, column);
            RightTriangleVertices vertices = CalculateVertices(position, rowIndex);

            return vertices.Coordinates;
        }
        private static string CalculateRow(RightTriangleVertices coordinates)
        {           
            int rowLetterIndex = coordinates.V1.y / IsoscelesSidesLenght - (coordinates.V1.y == coordinates.V2.y ? 0 : 1);
            string row = ((char)('A' + (rowLetterIndex))).ToString();

            return row;
        }
        private static int CalculateColumn(RightTriangleVertices coordinates)
        {
            int column = (coordinates.V1.x / IsoscelesSidesLenght * 2) + (coordinates.V1.y == coordinates.V2.y ? 0 : 1);
            return column;
        }        
        private static RightTriangleVertices CalculateVertices(RightTrianglePosition position, int rowIndex)
        {
            bool isColumnEven = position.Column % 2 == 0;

            int v3x = (position.Column + (isColumnEven ?  0 : 1))/2 * IsoscelesSidesLenght;
            int v3y = rowIndex * IsoscelesSidesLenght;
            int v2x = v3x - IsoscelesSidesLenght;
            int v2y = v3y - IsoscelesSidesLenght;
            int v1x = isColumnEven ? v3x: v2x;
            int v1y = isColumnEven ? v2y : v3y;            

            RightTriangleVertices vertices = new RightTriangleVertices (v1x, v1y, v2x, v2y, v3x, v3y);

            return vertices;
        }        
        private static bool IsIsoscelesRightTriangle(RightTriangleVertices coordinates)
        {
            if (coordinates.V1 == coordinates.V2 || coordinates.V1 == coordinates.V3 || coordinates.V2 == coordinates.V3) { 
            }    
            //Pythagorean Theorem = The square on the hypotenuse is equal to the sum of the squares on the other two sides

            var distanceA = CaulculateDistanceBetweenTwoPoints(coordinates.V1, coordinates.V2);
            var distanceB = CaulculateDistanceBetweenTwoPoints(coordinates.V1, coordinates.V3);
            var distanceC = CaulculateDistanceBetweenTwoPoints(coordinates.V2, coordinates.V3);

            if (distanceA != IsoscelesSidesLenght || distanceB != IsoscelesSidesLenght)
            {
                throw new ArgumentException("Isosceles sides should be " + IsoscelesSidesLenght + " long.");
            }

            bool isIsoscelesRightTriangle = Math.Sqrt(distanceA * distanceA + distanceB * distanceB) == distanceC;

            return isIsoscelesRightTriangle;
        }
        private static double CaulculateDistanceBetweenTwoPoints((int x, int y) pointA, (int x, int y) pointB)
        {
            var distance = Math.Sqrt(Math.Pow(pointA.x - pointB.x, 2) + Math.Pow(pointA.y - pointB.y, 2));
            return distance;
        }
    }
}
