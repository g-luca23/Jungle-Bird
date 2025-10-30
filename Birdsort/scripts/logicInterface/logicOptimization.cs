using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LogicInterface
{
    public static class Optimization
    {
        private static readonly int _incorrectBirdsWeight = 5;
        private static readonly int _uniformReward = 1;
        private static readonly int _heuristicMultiplier = 3;

        public static int IncorrectBirdsWeight => _incorrectBirdsWeight;
        public static int UniformReward => _uniformReward;
        public static int HeuristicMultiplier => _heuristicMultiplier;

        public static (int IncorrectBirdsWeight, int UniformReward, int HeuristicMultiplier) FineTuneParameters(
            Func<BirdSortState, List<(int, int)>> solverFunc,
            BirdSortState initialState)
        {
            int bestIncorrectBirdsWeight = 0, bestUniformReward = 0, bestHeuristicMultiplier = 0;
            int bestScore = int.MaxValue;

            // Default ranges for tuning
            var weightRange = GenerateRange(1, 10);
            var rewardRange = GenerateRange(1, 5);
            var multiplierRange = GenerateRange(1, 5);

            foreach (var incorrectWeight in weightRange)
            {
                foreach (var uniformReward in rewardRange)
                {
                    foreach (var heuristicMultiplier in multiplierRange)
                    {
                        int currentScore = EvaluateParameters(solverFunc, initialState, incorrectWeight, uniformReward, heuristicMultiplier);

                        if (currentScore < bestScore)
                        {
                            bestScore = currentScore;
                            bestIncorrectBirdsWeight = incorrectWeight;
                            bestUniformReward = uniformReward;
                            bestHeuristicMultiplier = heuristicMultiplier;
                        }
                    }
                }
            }

            return (bestIncorrectBirdsWeight, bestUniformReward, bestHeuristicMultiplier);
        }


        public static IEnumerable<int> GenerateRange(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                yield return i;
            }
        }

        public static int EvaluateParameters(
            Func<BirdSortState, List<(int, int)>> solverFunc,
            BirdSortState state,
            int incorrectWeight,
            int uniformReward,
            int heuristicMultiplier)
        {
            int score = 0;

            // Applicare i parametri alla logica e calcolare lo score
            solverFunc(state).ForEach(pair =>
            {
                score += incorrectWeight * pair.Item1 - uniformReward * pair.Item2 + heuristicMultiplier;
            });

            return score;
        }
    }
}
