# Documentazione Classe `GameInfo`

## **Descrizione**

Classe con metodi statici per gestire le informazioni riguardo la mappa di un livello.

---

## **Proprietà**

| Nome               | Tipo     | Descrizione                                 |
| ------------------ | -------- | ------------------------------------------- |
| `totalBranch`  | `int` | Numero totale di rami presente nel livello. |
| `currentMaxSpots`           | `int`    | Numero massimo di spot per ramo.                |
| `numBirdTypes`    | `int`   | Numero di tipi di uccello presenti nel livello.    |
| `finished`    | `bool`   | Flag che indica se un livello è stato completato o meno.    |

---

## **Metodi**

### Nome della Funzione: `reset`

```csharp
public static void reset()
```

#### **Descrizione**

Reimposta ai valori di default tutte le variabili legate al livello.

---

### Nome della Funzione: `setLevelInfo`

```csharp
public static void setLevelInfo(int currentmaxspots, int numbirdtypes)
```

#### **Descrizione**

Salva nelle variabili di istanza el informazioni passate attraverso parametri.

#### **Parametri**

| Nome      | Tipo    | Descrizione                     |
| --------- | ------- | ------------------------------- |
| `currentmaxspots` | `int` | Spot di un ramo. |
| `numbirdtypes` | `int` | Numero di tipi di uccello. |