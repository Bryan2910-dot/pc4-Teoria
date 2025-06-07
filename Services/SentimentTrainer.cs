using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML;
using pc4_Teoria.Models;

namespace pc4_Teoria.Services
{
    public class SentimentTrainer
    {
        private readonly string _dataPath;

        public SentimentTrainer(string dataPath)
        {
            _dataPath = dataPath;
        }

        public ITransformer TrainModel(MLContext mlContext, out DataViewSchema inputSchema)
        {
            var data = mlContext.Data.LoadFromTextFile<SentimentModel>(_dataPath, hasHeader: true);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentModel.Text))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

            var model = pipeline.Fit(data);

            inputSchema = data.Schema;
            return model;
        }
    }
}