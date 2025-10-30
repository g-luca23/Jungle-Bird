# Documentazione del Codice LogicInterface.cs

## Introduzione

Questa documentazione descrive il codice C# presente nel file `logicInteface.cs`, che implementa la logica di un gioco di smistamento di uccelli con diversi tipi di malus. La documentazione segue le linee guida definite nel README fornito.

---

## Strutture Dati e Enumerazioni

### Nome della Classe: `utils`

Questa classe statica contiene costanti e una mappa per la gestione dei tipi di uccelli.

#### **Descrizione**
Fornisce utilità e definizioni globali per il gioco.

#### **Membri**

*   `BirdToCharMap`: `Dictionary<string, ushort>` - Mappa i nomi degli uccelli a valori `ushort` utilizzati per la rappresentazione binaria.
*   `bombMaxMoves`: `const int` - Numero massimo di mosse prima che la bomba esploda.
*   `MaxBirdsPerBranch`: `static int` - Numero massimo di uccelli per ramo.

---

### Nome dell'Enumerazione: `MalusState`

```csharp
public enum MalusState
{
    None = 0,
    Key = 1 << 0,
    Cage = 1 << 1,
    Bomb = 1 << 2,
    Clock = 1 << 3,
    Sleep = 1 << 4
}
```

#### **Descrizione**
Definisce i possibili stati di malus che un uccello può avere. Utilizza una rappresentazione bitwise per consentire la combinazione di malus.

#### **Valori**

*   `None`: Nessun malus.
*   `Key`: Malus "Chiave".
*   `Cage`: Malus "Gabbia".
*   `Bomb`: Malus "Bomba".
*   `Clock`: Malus "Orologio".
*   `Sleep`: Malus "Sonno".

---

### Nome della Classe: `BirdSortState`

```csharp
public class BirdSortState { ... }
```

#### **Descrizione**
Rappresenta lo stato del gioco, inclusi i rami, gli uccelli presenti e i malus attivi.

#### **Membri**

*   `Branches`: `List<Stack<(ushort, MalusState)>>` - Lista di pile che rappresentano i rami. Ogni elemento della pila è una tupla contenente il tipo di uccello (`ushort`) e il suo stato di malus (`MalusState`).
*   `nBranchEmpty`: `int` - Numero di rami vuoti.
*   `totMoves`: `int` - Numero totale di mosse effettuate.
*   `sleepMalusBranches`: `int[]` - Array di due interi che memorizzano gli indici dei rami con il malus "Sonno".
*   `cageMalusBranch`: `int` - Indice del ramo con il malus "Gabbia".
*   `hasBombMalus`: `bool` - Indica se è presente il malus "Bomba".
*   `bomberType`: `ushort` - Tipo di uccello bomba.
*   `keyBirdType`: `ushort` - Tipo di uccello chiave.
*   `clockBirdType`: `ushort` - Tipo di uccello orologio.

#### **Costruttore**

### Nome del Costruttore: `BirdSortState`

```csharp
public BirdSortState(int branchCount, int MaxBirdsPerBranch);
```

#### **Descrizione**
Costruisce un nuovo stato del gioco con il numero specificato di rami e la capacità massima di uccelli per ramo.

#### **Parametri**

| Nome             | Tipo | Descrizione                                  |
|------------------|------|----------------------------------------------|
| `branchCount`    | `int`| Numero di rami nel gioco.                     |
| `MaxBirdsPerBranch` | `int` | Numero massimo di uccelli per ramo.        |

#### **Metodi**

### Nome della Funzione: `Clone`

```csharp
public BirdSortState Clone();
```

#### **Descrizione**
Crea una copia profonda dello stato del gioco.

#### **Valore di Ritorno**
Un nuovo oggetto `BirdSortState` che è una copia dello stato corrente.

### Nome della Funzione: `isExploding`

```csharp
public bool isExploding();
```

#### **Descrizione**
Verifica se la bomba è esplosa (se presente e se il numero di mosse ha superato il limite).

#### **Valore di Ritorno**
`true` se la bomba è esplosa, `false` altrimenti.

### Nome della Funzione: `FreeCageBirds`

```csharp
public void FreeCageBirds();
```

#### **Descrizione**
Libera gli uccelli dal malus "Gabbia" nel ramo specificato.

### Nome della Funzione: `freeBranch`

```csharp
public void freeBranch(Stack<(ushort, MalusState)> destination);
```

#### **Descrizione**
Libera un ramo se contiene solo uccelli dello stesso tipo e se è pieno. Gestisce anche l'attivazione dei malus chiave, orologio e bomba.

#### **Parametri**

| Nome        | Tipo                          | Descrizione                                                                 |
|-------------|-------------------------------|-----------------------------------------------------------------------------|
| `destination` | `Stack<(ushort, MalusState)>` | La pila che rappresenta il ramo da liberare.                               |

### Nome della Funzione: `FreeSleepingBirds`

```csharp
public void FreeSleepingBirds();
```

#### **Descrizione**
Libera gli uccelli dal malus "Sonno" nei rami specificati.

### Nome della Funzione: `MoveBird`

```csharp
public bool MoveBird(int from, int to);
```

#### **Descrizione**
Sposta uno o più uccelli dello stesso tipo da un ramo all'altro.

#### **Parametri**

| Nome   | Tipo | Descrizione                      |
|--------|------|----------------------------------|
| `from` | `int`| Indice del ramo di origine.      |
| `to`   | `int`| Indice del ramo di destinazione. |

#### **Valore di Ritorno**
`true` se la mossa è valida ed è stata eseguita, `false` altrimenti.


### Nome della Funzione: `CollectBirdsToMove`

```csharp
private static bool CollectBirdsToMove(Stack<(ushort, MalusState)> source, ushort birdToMove, Stack<(ushort, MalusState)> birdsToMove);
```

#### **Descrizione**
Sposta gli uccelli di un certo tipo da uno stack sorgente a uno stack di destinazione

#### **Parametri**

| Nome              | Tipo                          | Descrizione                        |
|-------------------|-------------------------------|------------------------------------|
| `source`          | `Stack<(ushort, MalusState)>` | Ramo di origine                    |
| `birdToMove`      | `ushort`                      | Tipo dell'uccello da spostare      |
| `birdsToMove`     | `Stack<(ushort, MalusState)>` | Stack con gli uccelli da spostare  |

#### **Valore di Ritorno**
`true` se si sposta almeno un uccello nello Stack `birdsToMove`, `false` altrimenti.



### Nome della Funzione: `isMoveValid`

```csharp
private bool IsMoveValid(Stack<(ushort, MalusState)> destination, int birdsToMoveCount)
```

#### **Descrizione**
Verifica se un'operazione di spostamento di un numero specifico di uccelli in una destinazione è valida, rispettando il limite massimo per ramo.

#### **Parametri**

| Nome              | Tipo                          | Descrizione                   |
|-------------------|-------------------------------|-------------------------------|
| `destination`     | `Stack<(ushort, MalusState)>` | 	Ramo di destinazione.       |
| `birdsToMoveCount`| `int`                         | Numero di uccelli da spostare.|

#### **Valore di Ritorno**
`true` se l'operazione di spostamento è valida, `false` altrimenti.



### Nome della Funzione: `UpdateEmptyBranches`

```csharp
private void UpdateEmptyBranches(Stack<(ushort, MalusState)> source, Stack<(ushort, MalusState)> destination)
```

#### **Descrizione**
Aggiorna il conteggio dei rami vuoti dopo un'operazione di spostamento, in base allo stato attuale dei rami sorgente e destinazione.

#### **Parametri**

| Nome         | Tipo                           | Descrizione           |
|--------------|--------------------------------|-----------------------|
| `source`     | `Stack<(ushort, MalusState)>`  | Ramo di origine.      |
| `destination`| `Stack<(ushort, MalusState)>`  | Ramo di destinazione. |



### Nome della Funzione: `PerformMove`

```csharp
private static void PerformMove(Stack<(ushort, MalusState)> birdsToMove, Stack<(ushort, MalusState)> destination);
```

#### **Descrizione**
Esegue lo spostamento degli uccelli dallo stack di uccelli da spostare alla destinazione.

#### **Parametri**

| Nome         | Tipo                           | Descrizione                                     |
|--------------|--------------------------------|-------------------------------------------------|
| `birdsToMove`| `Stack<(ushort, MalusState)>`  | Stack contenente gli uccelli da spostare.       |
| `destination`| `Stack<(ushort, MalusState)>`  | Stack di destinazione per gli uccelli spostati. |



### Nome della Funzione: `IsSolved`

```csharp
public bool IsSolved();
```

#### **Descrizione**
Verifica se il gioco è stato risolto (tutti i rami sono vuoti).

#### **Valore di Ritorno**
`true` se il gioco è risolto, `false` altrimenti.

### Nome della Funzione: `GetBirdName`

```csharp
private string GetBirdName(ushort bird);
```

#### **Descrizione**
Restituisce il nome dell'uccello dato il suo valore numerico.

#### **Parametri**

| Nome   | Tipo    | Descrizione                      |
|--------|---------|----------------------------------|
| `bird` | `ushort` | Il valore numerico dell'uccello. |

#### **Valore di Ritorno**
Il nome dell'uccello come stringa.

### Nome della Funzione: `CountConsecutiveBirds`

```csharp
private int CountConsecutiveBirds(Stack<(ushort, MalusState)> branch, ushort birdType);
```

#### **Descrizione**
Conta il numero di uccelli consecutivi dello stesso tipo in cima a un ramo.

#### **Parametri**

| Nome     | Tipo                          | Descrizione                                                                 |
|----------|-------------------------------|-----------------------------------------------------------------------------|
| `branch` | `Stack<(ushort, MalusState)>` | Il ramo da controllare.                                                      |
| `birdType` | `ushort` | Tipo di uccello da contare. |

#### **Valore di Ritorno**
Il numero di uccelli consecutivi.

### Nome della Funzione: `GetFormattedBoard`

```csharp
public string GetFormattedBoard();
```

#### **Descrizione**
Restituisce una rappresentazione formattata dello stato di gioco.

#### **Valore di Ritorno**
Una stringa che rappresenta lo stato di gioco.


