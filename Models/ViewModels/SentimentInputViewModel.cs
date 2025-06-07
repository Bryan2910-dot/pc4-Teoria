using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pc4_Teoria.Models.ViewModels
{
    public class SentimentInputViewModel
    {
        public string Text { get; set; }
        public string Prediction { get; set; }
        public float Score { get; set; }
    }
}