using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace pc4_Teoria.Models
{
    public class RatingModel
    {
        [LoadColumn(0)]
        public string UserId { get; set; }

        [LoadColumn(1)]
        public string ProductId { get; set; }

        [LoadColumn(2)]
        public float Label { get; set; }
    }

    public class RatingPrediction
    {
        public float Score { get; set; }
    }
}