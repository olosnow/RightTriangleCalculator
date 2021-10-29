using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RightTriangleApi.Models;
using System.Collections.Generic;

namespace RightTriangleApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class RightTriangleController : ControllerBase
    {       

        private readonly ILogger<RightTriangleController> _logger;

        public RightTriangleController(ILogger<RightTriangleController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public string Get() => nameof(Ok);

        [HttpGet]
        public Dictionary<string, int[]> GetCoordinates(string row, int column)
        {
            Dictionary<string, int[]> coordinates = RightTriangleCalculator.GetCoordinates(row, column);   
            return coordinates;
        }

        [HttpGet]
        public RightTrianglePosition GetPosition(int v1x, int v1y, int v2x, int v2y, int v3x, int v3y)
        {
            RightTrianglePosition position = RightTriangleCalculator.GetPosition(v1x, v1y, v2x, v2y, v3x, v3y);
            return position;
        }
    }
}
