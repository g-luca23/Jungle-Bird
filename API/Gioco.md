# Documentazione Classe `Gioco`

## **Descrizione**

La classe `Gioco` rappresenta il nodo principale del livello di gioco e gestisce il flusso di gioco, il caricamento delle risorse, e la logica per la visualizzazione di oggetti come rami e uccelli. Include anche funzionalità per la gestione del punteggio, l'esperienza e l'interazione con il giocatore tramite input.

---

## **Proprietà**

| Nome               | Tipo          | Descrizione                                      |
| ------------------ | ------------- | ------------------------------------------------ |
| `pauseMenu`        | `PauseMenu`   | Menu di pausa visualizzabile durante il gioco.   |
| `winScreen`        | `WinScreen`   | Schermata visualizzata al termine di un livello. |
| `branchL`          | `PackedScene` | Scena del ramo sinistro caricata.                |
| `branchR`          | `PackedScene` | Scena del ramo destro caricata.                  |
| `experiencePoints` | `int`         | Punti esperienza accumulati.                     |
| `tmp`              | `BirdClick`   | Riferimento all'ultimo uccello istanziato.       |

---

## **Metodi**

### Nome della Funzione: `_Ready`

```csharp
public override void _Ready()
```

#### **Descrizione**

Metodo chiamato automaticamente all'avvio del livello. Carica le risorse necessarie (rami e uccelli) e imposta il livello iniziale.

#### **Logica**

- Carica i rami e gli uccelli da file `.tscn`.
- Imposta il livello corrente e avvia la musica di gioco.
- Inizializza il timer per il suggerimento del livello.

---

### Nome della Funzione: `AddExperience`

```csharp
public void AddExperience(int xp)
```

#### **Descrizione**

Aggiunge punti esperienza al giocatore durante la partita.

#### **Parametri**

| Nome | Tipo  | Descrizione                     |
| ---- | ----- | ------------------------------- |
| `xp` | `int` | Punti esperienza da aggiungere. |

---

### Nome della Funzione: `_onHintbuttonPressed`

```csharp
public void _onHintbuttonPressed()
```

#### **Descrizione**

Esegue la mossa migliore disponibile quando viene premuto il pulsante del suggerimento.

---

### Nome della Funzione: `GetExperiencePoints`

```csharp
public int GetExperiencePoints()
```

#### **Descrizione**

Restituisce il totale dei punti esperienza accumulati.

---

### Nome della Funzione: `_Input`

```csharp
public override void _Input(InputEvent @event)
```

#### **Descrizione**

Gestisce gli input del giocatore, permettendo di eseguire azioni specifiche premendo determinati tasti.

#### **Tasti Gestiti**

- **K**: Esegue la mossa migliore.
- **L**: Esegue una mossa alternativa.
- **P**: Salta il livello corrente.

---

### Nome della Funzione: `_Process`

```csharp
public override void _Process(double delta)
```

#### **Descrizione**

Aggiornamento continuo del gioco. Controlla se il livello è stato completato e visualizza la schermata di vittoria.

---

### Nome della Funzione: `_OnPausaPressed`

```csharp
public void _OnPausaPressed()
```

#### **Descrizione**

Visualizza il menu di pausa quando il gioco è in esecuzione.

---

### Nome della Funzione: `DisplayBranch`

```csharp
public void DisplayBranch(char direction, int y)
```

#### **Descrizione**

Aggiunge un ramo come nodo alla scena e ne imposta direzione e posizione

**Parametri**

| Nome        | Tipo   | Descrizione                                            |
| ----------- | ------ | ------------------------------------------------------ |
| `direction` | `char` | Direzione del ramo (`l` per sinistra, `r` per destra). |
| `y`         | `int`  | Posizione verticale del ramo.                          |

---

### Nome della Funzione: `DisplayBird`

```csharp
public void DisplayBird(BirdType bt)
```

#### **Descrizione**

Aggiunge come nodo un uccello di un tipo specifico all'interno del livello.

#### **Parametri**

| Nome | Tipo       | Descrizione                      |
| ---- | ---------- | -------------------------------- |
| `bt` | `BirdType` | Tipo di uccello da visualizzare. |

---

### Nome della Funzione: `setLastMalus`

```csharp
public void setLastMalus(string malus)
```

#### **Descrizione**

Imposta il malus per l'ultimo uccello aggiunto come nodo.

#### **Parametri**

| Nome    | Tipo     | Descrizione                            |
| ------- | -------- | -------------------------------------- |
| `malus` | `string` | Malus da applicare all'ultimo uccello. |

---

### Nome della Funzione: `GetGameHeight`

```csharp
public static int GetGameHeight()
```

#### **Descrizione**

Restituisce l'altezza attuale della finestra di gioco.

---

### Nome della Funzione: `_StartMusic`

```csharp
public void _StartMusic()
```

#### **Descrizione**

Avvia la musica di gioco all'inizio del livello.

