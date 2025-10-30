using System.Collections.Generic;
using System.Linq;
using Godot;

namespace LogicInterface
{
    public static class DevUtils
    {
        public static void DisplayState(BirdSortState state)
        {
            GD.Print("--- Bird Sort State ---");

            // Stampa i rami
            for (int i = 0; i < state.Branches.Count; i++)
            {
                var branch = state.Branches[i];
                if (branch.Count == 0)
                {
                    GD.Print($"Branch {i + 1}: [Empty]");
                    continue;
                }

                string branchContents = string.Join(" ", branch.Reverse().Select(FormatBird));
                GD.Print($"Branch {i + 1}: [{branchContents}]");
            }

            // Stampa le informazioni sui malus globali
            GD.Print("\n--- Global Malus Info ---");
            GD.Print($"Sleep Malus Branches: {FormatArray(state.SleepMalusBranches)}");
            GD.Print($"Cage Malus Branch: {(state.CageMalusBranch != -1 ? state.CageMalusBranch.ToString() : "None")}");
            GD.Print($"Has Bomb Malus: {state.HasBombMalus}");
        }

        public static string FormatSolution(List<(int from, int to)> solution, BirdSortState initialState)
        {
            if (solution == null || solution.Count == 0)
            {
                return "No solution found";
            }

            var formattedSolution = new List<string>();
            var currentState = initialState.Clone();

            foreach (var move in solution)
            {
                currentState.MoveBird(move.from, move.to);
                formattedSolution.Add($"Move from Branch {move.from + 1} to Branch {move.to + 1}");
                formattedSolution.Add(currentState.GetFormattedBoard());
            }

            formattedSolution.Add($"Total moves: {solution.Count}");
            return string.Join("\n\n", formattedSolution);
        }

        private static string FormatBird((ushort, MalusState) bird)
        {
            string birdName = GetBirdName(bird.Item1);
            string malus = GetMalusDescription(bird.Item2);
            return $"{birdName}{(string.IsNullOrEmpty(malus) ? "" : $" ({malus})")}";
        }

        private static string GetBirdName(ushort bird)
        {
            foreach (var pair in Utils.BirdToCharMap)
            {
                if (pair.Value == bird)
                {
                    return pair.Key;
                }
            }
            return "Unknown";
        }

        private static string GetMalusDescription(MalusState malus)
        {
            var malusList = new List<string>();
            if (malus.HasFlag(MalusState.Bomb)) malusList.Add("Bomb");
            if (malus.HasFlag(MalusState.Cage)) malusList.Add("Cage");
            if (malus.HasFlag(MalusState.Key)) malusList.Add("Key");
            if (malus.HasFlag(MalusState.Clock)) malusList.Add("Clock");
            if (malus.HasFlag(MalusState.Sleep)) malusList.Add("Sleep");
            return string.Join(", ", malusList);
        }

        private static string FormatArray(int[] array)
        {
            return string.Join(", ", array.Select(value => value != 0 && value >= 0 ? value.ToString() : "None"));
        }

    }
}
