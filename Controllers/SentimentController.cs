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
    
    public class SentimentController : Controller
    {
        private readonly SentimentService _sentimentService;

        public SentimentController()
        {
            var dataPath = "feature-ml-sentiment/Data/sentiment-data.tsv";
            _sentimentService = new SentimentService(dataPath);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Analyze(string text)
        {
            var result = _sentimentService.Predict(text);
            var model = new SentimentInputViewModel
            {
                Text = text,
                Prediction = result.Prediction ? "Positivo" : "Negativo",
                Score = result.Score
            };
            return View("Result", model);
        }
    }
}