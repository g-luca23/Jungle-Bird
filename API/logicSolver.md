
## Documentazione del Codice di logicSolver.cs

Questa documentazione descrive la classe `AStarSolver` contenuta nel namespace `LogicInterface`. La classe implementa l'algoritmo A* per risolvere il puzzle di smistamento uccelli con malus. La documentazione segue le linee guida fornite.

---

## Classe: `AStarSolver`

Questa classe statica fornisce un metodo per risolvere il puzzle di smistamento uccelli utilizzando l'algoritmo A*.

### Nome della Funzione: `SolvePuzzleAStar`

```csharp
public static List<(int from, int to)> SolvePuzzleAStar(BirdSortState initialState);
```

#### **Descrizione**
Questo metodo implementa l'algoritmo di ricerca A* per trovare una sequenza di mosse che risolve il puzzle di smistamento degli uccelli. Prende uno stato iniziale del gioco e restituisce una lista di mosse, rappresentate come tuple di indici di rami (da, a).

#### **Parametri**

| Nome           | Tipo            | Descrizione                                                                        |
|----------------|-----------------|------------------------------------------------------------------------------------|
| `initialState` | `BirdSortState` | L'oggetto `BirdSortState` che rappresenta la configurazione iniziale del puzzle.   |

#### **Valore di Ritorno**
Una `List<(int from, int to)>` contenente le mosse necessarie per risolvere il puzzle. Ogni tupla `(from, to)` indica uno spostamento di uccelli dal ramo `from` al ramo `to`. Restituisce `null` se non viene trovata alcuna soluzione.

---

### Nome della Funzione: `InitializeOpenSet`

```csharp
private static void InitializeOpenSet(
    BirdSortState initialState, 
    PriorityQueue<(BirdSortState state, List<(int, int)> moves), int> openSet, 
    HashSet<string> visitedStates);
```

#### **Descrizione**
Inizializza l'insieme degli stati da esplorare per l'algoritmo A*, aggiungendo lo stato iniziale alla coda delle priorità e segnandolo come visitato.

#### **Parametri**

| Nome              | Tipo             | Descrizione                                                                                        |
|-------------------|------------------|----------------------------------------------------------------------------------------------------|
| `initialState`    | `BirdSortState`  | L'oggetto `BirdSortState` che rappresenta la configurazione iniziale del puzzle.                   |
| `openSet`         | `PriorityQueue<(BirdSortState state, List<(int, int)> moves), int>` | Coda delle priorità per gli stati da esplorare. |
| `visitedStates`   | `HashSet<string>`| Insieme degli stati già visitati.                                                                  |


---

### Nome della Funzione: `CalculateCost`

```csharp
private static int CalculateCost(List<(int, int)> nextMoves, BirdSortState nextState);
```

#### **Descrizione**
Calcola il costo stimato di uno stato specifico per l'algoritmo A*, combinando il costo effettivo delle mosse effettuate finora e una stima del costo rimanente.

#### **Parametri**

| Nome          | Tipo              | Descrizione                                       |
|---------------|-------------------|---------------------------------------------------|
| `nextMoves`   | `List<(int, int)>`| Sequenza di mosse effettuate fino a questo stato. |
| `nextState`   | `BirdSortState`   | Stato successivo per il quale calcolare il costo. |

#### **Valore di Ritorno**
Un valore `int` rappresentante il costo totale stimato per raggiungere lo stato obiettivo da quello attuale.

---

### Nome della Funzione: `ExploreNeighbors`

```csharp
private static void ExploreNeighbors(
    BirdSortState currentState, 
    List<(int, int)> moves, 
    PriorityQueue<(BirdSortState state, List<(int, int)> moves), int> openSet, 
    HashSet<string> visitedStates);
```

#### **Descrizione**
Esplora tutti gli stati vicini al corrente, valutando le mosse possibili e aggiungendo gli stati validi alla coda delle priorità.

#### **Parametri**

| Nome              | Tipo                  | Descrizione                                                                                   |
|-------------------|-----------------------|-----------------------------------------------------------------------------------------------|
| `currentState`    | `BirdSortState>`      | Stato attuale del puzzle.                                                                     |
| `moves`           | `List<(int, int)>`    | Sequenza di mosse effettuate fino allo stato attuale.                                         |
| `openSet`         | `PriorityQueue<(BirdSortState state, List<(int, int)> moves), int>` | Coda delle priorità per gli stati da esplorare. |
| `visitedStates`   | `HashSet<string>`     | Insieme degli stati già                                                                       |




### Nome della Funzione: `GetStateKey`

```csharp
private static string GetStateKey(BirdSortState state);
```

#### **Descrizione**
Questo metodo genera una chiave stringa univoca per uno stato del gioco. Questa chiave viene utilizzata per tenere traccia degli stati visitati e prevenire cicli infiniti durante la ricerca A*.

#### **Parametri**

| Nome    | Tipo              | Descrizione                                                                              |
|---------|-------------------|------------------------------------------------------------------------------------------|
| `state` | `BirdSortState` | L'oggetto `BirdSortState` per il quale generare la chiave.                                |

#### **Valore di Ritorno**
Una stringa che rappresenta una chiave univoca per lo stato del gioco.

---

### Nome della Funzione: `Heuristic`

```csharp
private static int Heuristic(BirdSortState state);
```

#### **Descrizione**
Questo metodo calcola un valore euristico per uno stato del gioco. L'euristica stima il costo rimanente per raggiungere uno stato risolto. Un'euristica ammissibile non sovrastima mai il costo reale.

#### **Parametri**

| Nome    | Tipo              | Descrizione                                                                  |
|---------|-------------------|------------------------------------------------------------------------------|
| `state` | `BirdSortState` | L'oggetto `BirdSortState` per il quale calcolare l'euristica.                 |

#### **Valore di Ritorno**
Un intero che rappresenta il valore euristico dello stato. Un valore più basso indica una stima di costo inferiore per la soluzione.

---

## Dettagli Implementativi A*

L'algoritmo A* implementato in `SolvePuzzleAStar` utilizza le seguenti strutture dati:

*   `openSet`: Una `PriorityQueue` che memorizza gli stati da esplorare, ordinati in base al loro costo totale stimato (f-score).
*   `visitedStates`: Un `HashSet` che tiene traccia degli stati già visitati, utilizzando le chiavi generate da `GetStateKey`.

L'algoritmo calcola il costo totale stimato `f` come la somma del costo effettivo del percorso fino allo stato corrente (`g`, il numero di mosse effettuate) e una stima del costo rimanente fino alla soluzione (`h`, calcolata dalla funzione `Heuristic`).

La funzione `Heuristic` considera i seguenti fattori:

*   **Rami Uniformi:** Premia i rami che contengono solo uccelli dello stesso tipo, incentivando la formazione di rami completi.
*   **Uccelli Errati:** Penalizza la presenza di uccelli di tipo diverso nello stesso ramo, scoraggiando configurazioni non ottimali.
*   **Multiplicatore Euristico:** Applica un moltiplicatore (`Optimization.heuristicMultiplier`) al valore euristico per bilanciare l'influenza dell'euristica rispetto al costo effettivo delle mosse.

