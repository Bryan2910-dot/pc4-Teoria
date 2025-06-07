using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pc4_Teoria.Models.ViewModels;
using pc4_Teoria.Services;

namespace pc4_Teoria.Controllers
{
    public class RecommendationController : Controller
    {
        private readonly RecommendationService _recommendationService;

        public RecommendationController()
        {
            var dataPath = "feature-ml-recommendation/Data/ratings-data.csv";
            _recommendationService = new RecommendationService(dataPath);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Recommend(string userId)
        {
            var recs = _recommendationService.Recommend(userId);
            var model = new RecommendationInputViewModel
            {
                UserId = userId,
                Recommendations = recs
            };
            return View("Result", model);
        }
    }
}