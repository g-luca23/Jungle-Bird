# Documentazione Classe `CreateMalus`

## **Descrizione**

Classe con metodi statici per la gestione e generazione dei malus degli uccelli (cage, sleep, bomb), data una configurazione di rami/uccelli già creata.

---

## **Proprietà**

| Nome               | Tipo     | Descrizione                                 |
| ------------------ | -------- | ------------------------------------------- |
| `RandomGenerator`  | `Random` | Generatore di numeri casuali per la classe. |
| `NMalus`           | `int`    | Numero di malus da generare.                |
| `ExcludedTypes`    | `List`   | Lista dei tipi di uccelli su cui non si possono generare bonus/malus.    |
| `ExcludedBranches` | `List`   | Lista dei branch su cui non possono essere posizionati uccelli con bonus/malus.     |

---

## **Metodi**

### Nome della Funzione: `setupMalusData`

```csharp
public static void SetupMalusData()
```

#### **Descrizione**

Calcola il numero di malus da aggiungere al livello in base al livello.

#### **Logica**

- Ogni 8 livelli c'è un malus
- Ogni 15 livelli ci sono due malus

---

### Nome della Funzione: `GenerateMalus`

```csharp
public static void GenerateMalus(Gioco partita)
```

#### **Descrizione**

Data una configurazione di rami/uccelli, genera casualmente dei malus sempre diversi tra di loro, scegliendo tra bomb, cage e sleep.

#### **Parametri**

| Nome      | Tipo    | Descrizione                     |
| --------- | ------- | ------------------------------- |
| `partita` | `Gioco` | Istanza della partita corrente. |

#### **Logica**

- Sceglie casualmente il malus da applicare.
- Trova uno o piu rami su cui generare i bonus/malus, scartando quelli contenuti in `ExcludedBranches`.
- Genera casualmente uno spot su cui inserire bonus/malus, controllando `ExcludedTypes`
- Aggiunge a `ExcludedBranches` e `ExcludedTypes` le cose appena generate.
- Applica i malus.

#### **Limitazioni**
- Gli uccelli con malus possono stare solo nella metà più interna del ramo.
- Gli uccelli con clock e key possono stare solo nella metà più esterna del ramo.
- Se non ci sono posti disponibili nelle metà più esterne, gli uccelli con bonus vengono posizionati senza limitazioni, eventualmente in un ramo di backup.

---

### Nome della Funzione: `FillBranches`

```csharp
private static void FillBranches(List<BranchClick> malusB, ref BranchClick bonusB)
```

#### **Descrizione**

Trova dei rami non vuoti, non duplicati e non bannati su cui posizionare bonus e malus.

#### **Parametri**

| Nome      | Tipo    | Descrizione                     |
| --------- | ------- | ------------------------------- |
| `malusB` | `List` | Lista di rami su cui posizionare uno o più malus. |
| `bonusB` | `BranchClick` | Ramo su cui posizionare il bonus. |

---

### Nome della Funzione: `AllocateMalus`

```csharp
private static void AllocateMalus(BranchClick branch, List<BirdClick> targetMalus)
```

#### **Descrizione**

Assegna un malus a uno degli uccelli su un ramo specifico e aggiorna i tipi esclusi per la generazione di bonus.

#### **Parametri**

| Nome          | Tipo          | Descrizione    |
| ------------- | ------------- | -------------- |
| `branch`      | `BranchClick` | Ramo in cui verrà allocato il malus.                          |
| `targetMalus` | `List<BirdClick>` | Lista di uccelli da aggiornare con il malus assegnato.          |

#### **Logica**

- Determina uno slot casuale per il malus all'interno del limite.
- Aggiunge l'uccello selezionato alla lista di malus.
- Aggiunge i tipi di uccello esclusi alla lista `ExcludedTypes` per prevenire conflitti nella generazione di bonus.
- I tipi esclusi sono il tipo dell'uccello del bonus e tutti quelli più interni di esso.
- Aggiunge il ramo alla lista `ExcludedBranches`.

---

### Nome della Funzione: `FindBackup`

```csharp
private static BranchClick FindBackup()
```

#### **Descrizione**

Trova un ramo di backup che non sia stato escluso e che contenga almeno un uccello.

#### **Logica**

- Seleziona casualmente un ramo dalla lista di rami disponibili in `LevelStruct.branches`.
- Controlla che il ramo contenga uccelli e che non sia nella lista `ExcludedBranches`.
- Restituisce il ramo valido come backup.

---

### Nome della Funzione: `GetBirdList`

```csharp
private static List<BirdClick> GetBirdList(BranchClick b)
```

#### **Descrizione**

Restituisce una lista di tutti gli uccelli presenti su un determinato ramo.

#### **Parametri**

| Nome | Tipo          | Descrizione                  |
| ---- | ------------- | ---------------------------- |
| `b`  | `BranchClick` | Ramo di cui ottenere gli uccelli. |

---

### Nome della Funzione: `FindBonusBird`

```csharp
private static BirdClick FindBonusBird(List<BirdClick> l, int suggested)
```

#### **Descrizione**

Seleziona un uccello dalla lista per assegnare un bonus, evitando tipi esclusi.

#### **Parametri**

| Nome       | Tipo               | Descrizione                                       |
| ---------- | ------------------ | ------------------------------------------------- |
| `l`        | `List<BirdClick>`  | Lista degli uccelli disponibili.                 |
| `suggested`| `int`              | Indice suggerito per l'assegnazione del bonus.   |

#### **Logica**

- Controlla se l'uccello suggerito non appartiene ai tipi esclusi.
- Se il tipo è escluso, cerca il primo uccello nella lista che non sia escluso.