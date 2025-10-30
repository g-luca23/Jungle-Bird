using System.Collections.Generic;
using System.Linq;

namespace LogicInterface{
  public static class AStarSolver
  {
    public static List<(int from, int to)> SolvePuzzleAStar(BirdSortState initialState)
    {
      var openSet = new PriorityQueue<(BirdSortState state, List<(int, int)> moves), int>();
      var visitedStates = new HashSet<string>();
      
      InitializeOpenSet(initialState, openSet, visitedStates);

      while (openSet.Count > 0)
      {
        var (currentState, moves) = openSet.Dequeue();

        // Se abbiamo raggiunto la soluzione
        if (currentState.IsSolved())
        {
          return moves;
        }

        ExploreNeighbors(currentState, moves, openSet, visitedStates);
      }
      
      return new List<(int from, int to)>(); // Nessuna soluzione trovata
    }

    public static void InitializeOpenSet(BirdSortState initialState, PriorityQueue<(BirdSortState state, List<(int, int)> moves), int> openSet, HashSet<string> visitedStates)
    {
        string initialKey = GetStateKey(initialState);
        visitedStates.Add(initialKey);

        // Aggiungi lo stato iniziale con costo iniziale 0
        openSet.Enqueue((initialState, new List<(int, int)>()), 0);
    }

    public static int CalculateCost(List<(int, int)> nextMoves, BirdSortState nextState)
    {
        int g = nextMoves.Count; // Numero di mosse fatte finora
        int h = Heuristic(nextState); // Stima del costo restante
        return g + h; // Costo totale stimato
    }

    private static void ExploreNeighbors(BirdSortState currentState, List<(int, int)> moves, PriorityQueue<(BirdSortState state, List<(int, int)> moves), int> openSet, HashSet<string> visitedStates)
    {
        foreach (int from in Enumerable.Range(0, currentState.Branches.Count))
        {
            foreach (int to in Enumerable.Range(0, currentState.Branches.Count).Where(to => to != from))
            {
                var nextState = currentState.Clone();

                if (nextState.MoveBird(from, to))
                {
                    string stateKey = GetStateKey(nextState);

                    if (visitedStates.Contains(stateKey) || nextState.isExploding())
                    {
                        continue;
                    }

                    visitedStates.Add(stateKey);
                    var nextMoves = new List<(int, int)>(moves) { (from, to) };

                    int f = CalculateCost(nextMoves, nextState);
                    openSet.Enqueue((nextState, nextMoves), f);
                }
            }
        }
    }

		private static string GetStateKey(BirdSortState state)
		{
			return string.Join("|", state.Branches.Select(b => string.Join("", b)));
		}

		public static int Heuristic(BirdSortState state)
		{
				int heuristic = 0;
				foreach (var branch in state.Branches)
				{
						if (branch.Count == 0) {	 
							continue;
						}

						ushort topBird = branch.Peek().Item1;

						bool isUniform = true;

						foreach (var bird in branch.Where(x => x.Item1 != topBird))
						{
                isUniform = false;
                heuristic += Optimization.IncorrectBirdsWeight; 
                topBird = bird.Item1;	
						}


						if (isUniform)
						{
								// Calcola il bonus basato sulla lunghezza del ramo uniforme
								heuristic -= Optimization.UniformReward;
						}

						heuristic += Optimization.HeuristicMultiplier;

				}

				return heuristic;
		}

	}

}