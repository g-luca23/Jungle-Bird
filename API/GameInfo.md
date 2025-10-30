# Documentazione Classe `GameInfo`

## **Descrizione**

Classe con metodi statici per gestire il salvataggio, l'aggiornamento e la lettura del livello del giocatore.

---

## **Proprietà**

| Nome               | Tipo     | Descrizione                                 |
| ------------------ | -------- | ------------------------------------------- |
| `levelcreated`  | `uint` | Flag che dice se il livello corrente è stato generato o meno. |
| `currentLevel`           | `uint`    | Livello corrente del giocatore.                |
| `filePath`    | `string`   | Percorso in cui salvare e leggere le informazioni    |

---

## **Metodi**

### Nome della Funzione: `readCurrentLevel`

```csharp
public static void readCurrentLevel()
```

#### **Descrizione**

Legge da file le informazioni attraverso `FileAccess`.

---

### Nome della Funzione: `writeCurrentLevel`

```csharp
public static void writeCurrentLevel()
```

#### **Descrizione**

Scrive su file le informazioni attraverso `FileAccess`.