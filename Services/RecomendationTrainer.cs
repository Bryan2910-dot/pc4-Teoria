using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using pc4_Teoria.Models;

namespace pc4_Teoria.Services
{
    public class RecommendationTrainer
    {
        private readonly string _dataPath;

        public RecommendationTrainer(string dataPath)
        {
            _dataPath = dataPath;
        }

        public ITransformer TrainModel(MLContext mlContext, out DataViewSchema inputSchema)
        {
            var data = mlContext.Data.LoadFromTextFile<RatingModel>(_dataPath, hasHeader: true, separatorChar: ',');

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = nameof(RatingModel.UserId),
                MatrixRowIndexColumnName = nameof(RatingModel.ProductId),
                LabelColumnName = "Label",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var pipeline = mlContext.Transforms.Conversion
                .MapValueToKey(nameof(RatingModel.UserId))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(nameof(RatingModel.ProductId)))
                .Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            var model = pipeline.Fit(data);
            inputSchema = data.Schema;
            return model;
        }
    }
}