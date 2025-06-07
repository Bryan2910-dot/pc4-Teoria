using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML;
using pc4_Teoria.Models;

namespace pc4_Teoria.Services
{
    public class RecommendationService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        private readonly PredictionEngine<RatingModel, RatingPrediction> _predEngine;
        private readonly List<string> _productIds;

        public RecommendationService(string dataPath)
        {
            _mlContext = new MLContext();
            var trainer = new RecommendationTrainer(dataPath);
            _model = trainer.TrainModel(_mlContext, out var schema);
            _predEngine = _mlContext.Model.CreatePredictionEngine<RatingModel, RatingPrediction>(_model);

            // Extraer lista de productos del archivo CSV
            var data = _mlContext.Data.LoadFromTextFile<RatingModel>(dataPath, hasHeader: true, separatorChar: ',');
            _productIds = _mlContext.Data.CreateEnumerable<RatingModel>(data, reuseRowObject: false)
                                         .Select(x => x.ProductId)
                                         .Distinct()
                                         .ToList();
        }

        public List<(string ProductId, float Score)> Recommend(string userId, int top = 5)
        {
            var predictions = _productIds.Select(pid => new
            {
                ProductId = pid,
                Score = _predEngine.Predict(new RatingModel { UserId = userId, ProductId = pid }).Score
            });

            return predictions.OrderByDescending(p => p.Score)
                              .Take(top)
                              .Select(p => (p.ProductId, p.Score))
                              .ToList();
        }
    }
}