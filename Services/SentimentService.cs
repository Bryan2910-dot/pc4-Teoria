using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML;
using pc4_Teoria.Models;

namespace pc4_Teoria.Services
{
    public class SentimentService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        private readonly PredictionEngine<SentimentModel, SentimentPrediction> _predEngine;

        public SentimentService(string dataPath)
        {
            _mlContext = new MLContext();
            var trainer = new SentimentTrainer(dataPath);
            var model = trainer.TrainModel(_mlContext, out var schema);
            _model = model;
            _predEngine = _mlContext.Model.CreatePredictionEngine<SentimentModel, SentimentPrediction>(_model);
        }

        public SentimentPrediction Predict(string text)
        {
            var input = new SentimentModel { Text = text };
            return _predEngine.Predict(input);
        }
    }
}