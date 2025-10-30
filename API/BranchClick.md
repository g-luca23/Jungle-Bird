### Nome della Funzione: `setStartPoint`

```csharp
public void setStartPoint();
```

#### **Descrizione**
La funzione setStartPoint imposta il punto di partenza e il segno del ramo (signBranch) per un oggetto in base alla sua posizione globale lungo l'asse X. Se l'oggetto si trova a sinistra dell'origine (GlobalPosition.X < 0), viene assegnato un punto di partenza specifico e un segno positivo; altrimenti, viene assegnato un punto di partenza diverso e un segno negativo. Il suo uso è molto importante in ready per capire se il ramo si trova a destra o a sinstra dello schermo per poi dopo utilizzare queste informazioni per calcolare le possibili posizioni degli uccelli sul barnch.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| ``  | ``  | |

#### **Valore di Ritorno**
La funzione non restituisce alcun valore (void). 

---

### Nome della Funzione: `setSlotPos`

```csharp
public void setSlotPos();
```

#### **Descrizione**
La funzione setSlotPos calcola e assegna le posizioni per una serie di slot lungo un ramo, memorizzandole in un array di vettori (branchSlotsPos). Le posizioni sono determinate in base al punto di partenza del ramo (startingPointX), alla direzione del ramo (signBranch), e alle informazioni sulla struttura del ramo (BranchPosInfo).

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| ``  | ``  | |

#### **Valore di Ritorno**
La funzione non restituisce alcun valore (void). 

#### **Esempio di Utilizzo**
Viene utilizzata per misurare dove posizionare gli uccelli sul ramo.

---

### Nome della Funzione: `_OnInputEvent`

```csharp
void _OnInputEvent(Node viewport, InputEvent @event, int shapeIdx);
```

#### **Descrizione**
La funzione _OnInputEvent è una funzione legata con un signal ad Area2D(hitbox branch) di Godot, gestisce gli eventi di input generati dall'interazione del mouse con un branch. Quando viene rilevato un clicK sinistro del mouse su un ramo, la funzione verifica se ci sono uccelli selezionati per il trasferimento. Se i criteri sono soddisfatti, trasferisce uno stormo di uccelli da un ramo all'altro.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| `viewport`  | `Node` | Il nodo viewport che riceve l'evento. |
| `@event`  | `InputEvent` | L'evento di input generato, come un clic del mouse. |
| `shapeIdx`  | `int` | L'indice della forma associata all'oggetto che ha generato l'evento. |

#### **Valore di Ritorno**
La funzione non restituisce alcun valore (void).

---

### Nome della Funzione: `_OnMouseEntered`

```csharp
void _OnMouseEntered()
```

#### **Descrizione**
La funzione è una funzione legata con un signal ad Area2D(hitbox branch) di Godot. Quando viene rilevato il mouse sopra al ramo cambia il suo sprite in selected per dare un feedback visivo al giocatore e rendere comprensibile quello che viene cliccato.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| ``  | ``  | |

#### **Valore di Ritorno**
La funzione non restituisce alcun  valore(void).

---

### Nome della Funzione: `_OnMouseExited`

```csharp
void _OnMouseExited()
```

#### **Descrizione**
La funzione è una funzione legata con un signal ad Area2D(hitbox branch) di Godot. Quando il mouse non si trova sopra al ramo cambia il suo sprite in default.

#### **Parametri**
| Nome | Tipo   | Descrizione                |
|------|--------|----------------------------|
| ``  | ``  | |

#### **Valore di Ritorno**
La funzione non restituisce alcun  valore(void).
