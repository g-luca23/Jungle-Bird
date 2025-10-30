## Documentazione di Optimization.cs (seguendo le linee guida)

**Classe:** `Optimization`

Questa classe statica fornisce metodi per la messa a punto dei parametri dell'algoritmo di risoluzione del puzzle di smistamento uccelli.

### Metodo: `FineTuneParameters`

```csharp
public static void FineTuneParameters(Func<BirdSortState, List<(int, int)>> solverFunc, BirdSortState initialState)
```

**Descrizione:**

Questo metodo esegue una ricerca a griglia per trovare la combinazione ottimale di parametri per l'euristica utilizzata dall'algoritmo di risoluzione (`solverFunc`). Il metodo valuta diverse configurazioni di pesi e moltiplicatori, misurando il numero di mosse necessarie per risolvere lo stato iniziale del gioco (`initialState`) con ogni combinazione.

**Parametri:**

| Nome           | Tipo                                    | Descrizione                                                          |
|----------------|-----------------------------------------|----------------------------------------------------------------------|
| `solverFunc`   | `Func<BirdSortState, List<(int, int)>>` | Funzione che implementa l'algoritmo del puzzle                       |
| `initialState` | `BirdSortState`                         | Stato iniziale del gioco utilizzato per la valutazione dei parametri |

**Valore di Ritorno:**

* **Nessun valore di ritorno esplicito.** Il metodo modifica internamente i valori statici dei parametri `incorrectBirdsWeight`, `uniformReward`, e `heuristicMultiplier` con la combinazione trovata come migliore.

**Dettagli dell'Implementazione:**

1. **Parametri da Ottimizzare:**
    * `incorrectBirdsWeight`: Peso della penalità per la presenza di uccelli errati in un ramo.
    * `uniformReward`: Bonus applicato per la formazione di rami uniformi (tutti gli uccelli dello stesso tipo).
    * `heuristicMultiplier`: Moltiplicatore utilizzato per bilanciare l'influenza dell'euristica rispetto al costo effettivo delle mosse.

2. **Ricerca a Griglia:**
    * Vengono definiti intervalli di valori da testare per ciascun parametro.
    * Il metodo itera su tutte le combinazioni possibili di valori per i parametri.

3. **Valutazione di una Combinazione:**
    * Per ogni combinazione di parametri, vengono impostati i valori di `incorrectBirdsWeight`, `uniformReward`, e `heuristicMultiplier`.
    * Si esegue una copia dello stato iniziale del gioco (`testState`).
    * Si chiama la funzione `solverFunc` per trovare una soluzione partendo da `testState`.
    * Si misura il tempo impiegato per trovare la soluzione.
    * Se il tempo di calcolo supera un limite predefinito (1 secondo), la combinazione viene scartata.
    * Se la funzione `solverFunc` non trova una soluzione, la combinazione viene scartata.
    * Se la soluzione è valida, si calcola il punteggio come il numero di mosse necessarie.

4. **Aggiornamento dei Migliori Parametri:**
    * Vengono conservati i valori di `incorrectBirdsWeight`, `uniformReward`, e `heuristicMultiplier` che hanno portato al punteggio migliore (minor numero di mosse).

5. **Stampa dei Risultati:**
    * Vengono stampati a debug i parametri migliori trovati e il punteggio corrispondente.

6. **Ripristino dei Valori Predefiniti:**
    * Al termine della ricerca, i valori di `incorrectBirdsWeight`, `uniformReward`, e `heuristicMultiplier` vengono ripristinati ai valori predefiniti.

**Note:**

* Questo metodo è pensato per l'ottimizzazione offline dei parametri e non dovrebbe essere utilizzato durante il normale gameplay.
* I valori predefiniti dei parametri (`incorrectBirdsWeight = 5`, `uniformReward = 1`, `heuristicMultiplier = 3`) sono un punto di partenza e potrebbero richiedere un ulteriore tuning in base alle specifiche del gioco.



### Metodo: `EvaluateParameters`

```csharp
private static int EvaluateParameters(
    Func<BirdSortState, List<(int, int)>> solverFunc,
    BirdSortState state,
    int incorrectWeight,
    int uniformReward,
    int heuristicMultiplier);
```

**Descrizione:**

Valuta un insieme di parametri applicandoli a una funzione di risoluzione e calcolando un punteggio basato su una logica specifica.

**Parametri:**

| Nome                  | Tipo                                      | Descrizione                                               |
|-----------------------|-------------------------------------------|-----------------------------------------------------------|
| `solverFunc`          | `Func<BirdSortState, List<(int, int)>>`   | Funzione che implementa l'algoritmo del puzzle            |
| `state`               | `BirdSortState`                           | Stato attuale del sistema di ordinamento degli uccelli.   |
| `incorrectWeight`     | `int`                                     | Peso assegnato agli errori nell'ordinamento.              |
| `uniformReward`       | `int`                                     | Ricompensa uniforme per il progresso corretto.            |
| `heuristicMultiplier` | `int`                                     | Moltiplicatore per il calcolo euristico.                  |

**Valore di Ritorno:**
Un valore `int` rappresentante il punteggio calcolato sulla base dei parametri forniti.



### Metodo: `ApplyBestParameters`

```csharp
private static void ApplyBestParameters(int incorrectWeight, int uniformReward, int heuristicMultiplier);
```

**Descrizione:**

Applica e registra i parametri ottimali trovati per il sistema di ordinamento degli uccelli.

**Parametri:**

| Nome                  | Tipo                                      | Descrizione                                               |
|-----------------------|-------------------------------------------|-----------------------------------------------------------|
| `incorrectWeight`     | `int`                                     | Peso assegnato agli errori nell'ordinamento.              |
| `uniformReward`       | `int`                                     | Ricompensa uniforme per il progresso corretto.            |
| `heuristicMultiplier` | `int`                                     | Moltiplicatore per il calcolo euristico.                  |