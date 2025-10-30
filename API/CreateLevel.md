# Documentazione Classe `CreateLevel`

## **Descrizione**

Classe con metodi statici per la gestione e generazione dei livelli di gioco, inclusa la configurazione degli elementi del livello, il calcolo dell'XP e la gestione del timer.

---

## **Proprietà**

| Nome               | Tipo     | Descrizione                                 |
| ------------------ | -------- | ------------------------------------------- |
| `RandomGenerator`  | `Random` | Generatore di numeri casuali per la classe. |
| `LevelTimer`       | `Timer`  | Timer associato al livello.                 |
| `ElapsedTime`      | `float`  | Tempo trascorso dall'inizio del livello.    |
| `numberOfMoves`    | `int`    | Numero di mosse effettuate nel livello.     |
| `currentGame`      | `Gioco`  | Istanza della partita corrente.             |
| `Difficulty`       | `int`    | Coefficiente di difficoltà del livello.     |
| `NumberOfBranches` | `int`    | Numero di rami generati nel livello.        |
| `NumberOfBirds`    | `int`    | Numero di uccelli generati nel livello.     |
| `LevelXP`          | `int`    | XP guadagnati nel livello.                  |

---

## **Metodi**

### Nome della Funzione: `Load`

```csharp
public static void Load(Gioco partita, int D, int Nrami, List<Godot.Collections.Dictionary<string, Variant>> birddatalist)
```

#### **Descrizione**

Rigenera la mappa del livello sulla base dei dati letti da file e passati come parametri.

#### **Parametri**

| Nome           | Tipo    | Descrizione                                    |
| -------------- | ------- | ---------------------------------------------- |
| `partita`      | `Gioco` | Istanza della partita corrente.                |
| `D`            | `int`   | Difficoltà.                                    |
| `Nrami`        | `int`   | Numero di rami.                                |
| `birddatalist` | `List`  | Lista con tutte le informazioni sugli uccelli. |

#### **Logica**

- Imposta il timer e connette i segnali.
- Imposta la difficoltà e il numero di rami.
- Chiama `CreateBranches` per generare i rami.
- Cicla attraverso `birddatalist` per visualizzare gli uccelli e gestire eventuali malus specifici.

---

### Nome della Funzione: `setupLevel`

```csharp
public static void setupLevel(Gioco partita)
```

#### **Descrizione**

Metodo principale per la generazione del livello. Imposta timer, difficoltà e chiama metodi per aggiungere rami, uccelli e malus.

#### **Parametri**

| Nome      | Tipo    | Descrizione                     |
| --------- | ------- | ------------------------------- |
| `partita` | `Gioco` | Istanza della partita corrente. |

#### **Logica**

- Configura il timer e lo avvia.
- Determina la difficoltà in base alla presenza di malus.
- Calcola il numero di rami e uccelli.
- Chiama `CreateBranches` per creare i rami.
- Chiama `CreateBirds` per generare gli uccelli.
- Aggiunge i malus con `CreateMalus.GenerateMalus`.

---

### Nome della Funzione: `resetLevel`

```csharp
public static void resetLevel()
```

#### **Descrizione**

Ferma il timer e reimposta i parametri del livello allo stato iniziale.

---

### Nome della Funzione: `StartLevelTimer`

```csharp
public static void StartLevelTimer()
```

#### **Descrizione**

Avvia il timer del livello e reimposta il tempo trascorso.

---

### Nome della Funzione: `StopLevelTimer`

```csharp
public static void StopLevelTimer()
```

#### **Descrizione**

Ferma il timer del livello.

---

### Nome della Funzione: `GetElapsedTime`

```csharp
public static float GetElapsedTime()
```

#### **Descrizione**

Ritorna il tempo totale trascorso dall'inizio del livello.

---

### Nome della Funzione: `CreateBranches`

```csharp
private static void CreateBranches(int branchSpacing)
```

#### **Descrizione**

Genera i rami e li aggiunge alla mappa del livello.

#### **Parametri**

| Nome            | Tipo  | Descrizione                      |
| --------------- | ----- | -------------------------------- |
| `branchSpacing` | `int` | Spaziatura verticale tra i rami. |

#### **Logica**

- Calcola la posizione verticale di ogni ramo.
- Determina il lato (sinistra/destra) per ogni ramo.
- Chiama `currentGame.DisplayBranch` per visualizzare i rami.

---

### Nome della Funzione: `CreateBirds`

```csharp
private static void CreateBirds()
```

#### **Descrizione**

Popola i rami generati con uccelli in base ai tipi disponibili.

#### **Logica**

- Mescola gli indici degli uccelli.
- Determina il tipo di uccello per ogni ramo.
- Chiama `currentGame.DisplayBird` per visualizzare gli uccelli.

---

### Nome della Funzione: `Shuffle`

```csharp
public static void Shuffle<T>(Random rng, T[] array)
```

#### **Descrizione**

Mescola casualmente un array di elementi.

#### **Parametri**

| Nome    | Tipo     | Descrizione                   |
| ------- | -------- | ----------------------------- |
| `rng`   | `Random` | Generatore di numeri casuali. |
| `array` | `T[]`    | Array da mescolare.           |

---

### Nome della Funzione: `CalculateXP`

```csharp
public static int CalculateXP(int timeTaken, int moves, int difficulty)
```

#### **Descrizione**

Calcola l'XP guadagnata al termine del livello.

#### **Parametri**

| Nome         | Tipo  | Descrizione                                |
| ------------ | ----- | ------------------------------------------ |
| `timeTaken`  | `int` | Tempo impiegato per completare il livello. |
| `moves`      | `int` | Numero di mosse effettuate.                |
| `difficulty` | `int` | Difficoltà del livello.                    |

#### **Logica**

- Calcola il bonus per il tempo e la penalità per le mosse.
- Somma i bonus legati ai rami e ai tipi di uccelli.
- Applica un moltiplicatore in base alla difficoltà.
- Restituisce l'XP totale calcolata.

---

### Nome della Funzione: `GetXpGained`

```csharp
public static int GetXpGained()
```

#### **Descrizione**

Restituisce l'XP totale guadagnata nel livello corrente.

---

### Nome della Funzione: `endLevel`

```csharp
public static void endLevel()
```

#### **Descrizione**

Conclude il livello, ferma il timer e calcola l'XP guadagnata.

#### **Logica**

- Imposta il livello come completato.
- Ferma il timer del livello.
- Calcola l'XP guadagnata usando `CalculateXP`.
- Aggiunge l'XP al gioco.