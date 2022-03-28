using Microsoft.AspNetCore.Mvc;
using SpiderCoordinates.Models;
using System.Diagnostics;

namespace SpiderCoordinates.Controllers
{
    /// <summary>
    /// Controller to calculate the spider coordinates
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Default Controller and Method-for Spider coordinates
        /// </summary>
        /// <param name="spiderCoordinatesViewModel"></param>
        /// <returns></returns>
        public IActionResult Index(SpiderCoordinatesViewModel spiderCoordinatesViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    spiderCoordinatesViewModel.finalCoordinates = GetSpiderCoordinatesValue(spiderCoordinatesViewModel);
                    ViewBag.finalCoordinates = spiderCoordinatesViewModel.finalCoordinates;
                    ViewBag.PathtobeFollowed = spiderCoordinatesViewModel.Direction;
                    ViewBag.MatrixSize =spiderCoordinatesViewModel.MatrixCoordinates;
                }
                
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }
            return View();
        }

       

    /// <summary>
    /// Posting the values from UI to calculate the final coordinates
    /// </summary>
    /// <param name="spiderCoordinatesViewModel"></param>
    /// <returns></returns>
     [HttpPost]
        public IActionResult GetSpiderCoordinates(SpiderCoordinatesViewModel spiderCoordinatesViewModel)
        {
            
            if (ModelState.IsValid)
            {
               
                spiderCoordinatesViewModel.finalCoordinates = GetSpiderCoordinatesValue(spiderCoordinatesViewModel);
            }
            return View();
        }
        /// <summary>
        /// Get the final coordinates from spiderview model
        /// </summary>
        /// <param name="spiderCoordinatesViewModel"></param>
        /// <returns></returns>
        public string GetSpiderCoordinatesValue(SpiderCoordinatesViewModel spiderCoordinatesViewModel)
        {
            try
            {
                var x = Convert.ToInt32(spiderCoordinatesViewModel.MatrixCoordinates.Split(",")[0]);
                var y = Convert.ToInt32(spiderCoordinatesViewModel.MatrixCoordinates.Split(",")[1]);

                var spider_x = Convert.ToInt32(spiderCoordinatesViewModel.StartingPoint.Split(",")[0]);
                var spider_y = Convert.ToInt32(spiderCoordinatesViewModel.StartingPoint.Split(",")[1]);
                var spider_face = spiderCoordinatesViewModel.StartingPoint.Split(",")[2];
                string spider_path = spiderCoordinatesViewModel.Direction;

                //** Setting Face of spider with degrees 0-Up;90-right,180-down 270-Left
                var degree = 0;
                if (spider_face == "left")
                {
                    degree = 270;
                }
                else if (spider_face == "right")
                {
                    degree = 90;
                }
                else if (spider_face == "up")
                {
                    degree = 0;
                }
                else if (spider_face == "down")
                {
                    degree = 180;
                }
                char[] chars = spider_path.ToCharArray();


                //** Looping through each path to be followed if F-then onse step ahead, if L/R then change degree
                for (int ctr = 0; ctr < chars.Length; ctr++)
                {
                    string move = chars[ctr].ToString();
                    if (move == "L")
                    {
                        degree = degree + 270;
                        if (degree >= 360)
                        {
                            degree = degree % 360;
                        }
                    }
                    else if (move == "R")
                    {
                        degree = degree + 90;
                        if (degree >= 360)
                        {
                            degree = degree % 360;
                        }
                    }
                    else if (move == "F")
                    {
                        if (degree == 0)
                        {
                            spider_y = spider_y + 1;
                        }
                        else if (degree == 90)
                        {
                            spider_x = spider_x + 1;
                        }
                        else if (degree == 270)
                        {
                            spider_x = spider_x - 1;
                        }
                        else if (degree == 180)
                        {
                            spider_y = spider_y - 1;
                        }
                    }
                }
                if (degree == 0)
                {
                    spider_face = "up";
                }
                else if (degree == 90)
                {
                    spider_face = "right";
                }
                else if (degree == 270)
                {
                    spider_face = "left";
                }
                else if (degree == 180)
                {
                    spider_face = "down";
                }
                if (spider_y > y || spider_y < 0 || Convert.ToInt32(spider_x) > x || spider_x < 0)
                {
                    return spiderCoordinatesViewModel.finalCoordinates = "Path Out of Matrix";
                }
                else
                {
                    return spiderCoordinatesViewModel.finalCoordinates = (spider_x.ToString() + "," + spider_y.ToString() + "," + spider_face);
                }
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }
            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}