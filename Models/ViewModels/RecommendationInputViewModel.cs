using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pc4_Teoria.Models.ViewModels
{
    public class RecommendationInputViewModel
    {
        public string UserId { get; set; }
        public List<(string ProductId, float Score)> Recommendations { get; set; }
    }
}