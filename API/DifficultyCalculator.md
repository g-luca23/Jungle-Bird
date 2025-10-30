### Nome della Classe: `DifficultyCalculator`

```csharp
public static class DifficultyCalculator
```

#### **Descrizione**
Classe statica per calcolare i parametri di gioco in base al livello di difficoltà.

---

### Nome della Funzione: `CalcMaxSpots`

```csharp
public static int CalcMaxSpots(int difficulty)
```

#### **Descrizione**
Calcola il numero massimo di posizioni disponibili (`spots`) in base al livello di difficoltà.

#### **Parametri**
| Nome        | Tipo  | Descrizione                                  |
|-------------|-------|----------------------------------------------|
| `difficulty`| `int` | Livello di difficoltà corrente.              |

#### **Valore di Ritorno**
Un numero intero che rappresenta il massimo numero di spot disponibili in un ramo.

#### **Dettagli Aggiuntivi**
**Logica**:
- Restituisce `4` se la difficoltà è inferiore a 2.
- Restituisce `5` se la difficoltà è compresa tra 2 e 4.
- Restituisce `6` se la difficoltà è compresa tra 5 e 7.
- Restituisce `7` per difficoltà superiori a 7.

---

### Nome della Funzione: `CalcNBirdType`

```csharp
public static int CalcNBirdType(int difficulty)
```

#### **Descrizione**
Calcola il numero di tipi di uccelli in base al livello di difficoltà.

#### **Parametri**
| Nome        | Tipo  | Descrizione                                  |
|-------------|-------|----------------------------------------------|
| `difficulty`| `int` | Livello di difficoltà corrente.              |

#### **Valore di Ritorno**
Un numero intero che rappresenta il numero di tipi di uccelli.

#### **Dettagli Aggiuntivi**
**Logica**:
- Restituisce `difficulty + 4` per difficoltà fino a 4.
- Restituisce `difficulty + 3` per difficoltà da 5 a 7.
- Restituisce `difficulty + 2` per difficoltà superiori a 7.