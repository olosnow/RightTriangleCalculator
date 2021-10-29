namespace RightTriangleApi.Models
{
    public class RightTrianglePosition
    {        
        public string Row { get; set; }
        public int Column { get; set; }        
        public RightTrianglePosition(string row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
