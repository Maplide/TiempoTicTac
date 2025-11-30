# 🎮 Tiempo Tic-Tac  
### Un juego puzzle–plataformas basado en manipulación del tiempo  
**Desarrollado por:** Alfredo Quispe Ubaldo – *AQU Studio*  
**Motor:** 6000.2.5f1  
**Plataformas:** WebGL (itch.io, Unity Play)

---

## 📹 DEMO / JUGAR EN LÍNEA

🔗 **itch.io:** *https://maplide.itch.io/tiempo-tic-tac*  
🔗 **Unity Play:** *https://play.unity.com/en/games/fbc8a760-66c7-4099-adf8-7909f3fd7d68/tiempo-tic-tac*

---

## 📝 Descripción General

**Tiempo Tic-Tac** es un juego 2D puzzle–plataformas ambientado en un laboratorio científico.  
El jugador debe superar obstáculos utilizando una mecánica única: **grabar y reproducir los movimientos de objetos** como cajas y plataformas para activar mecanismos y abrirse camino hasta la salida.

El juego combina:

- Plataformas clásicas  
- Resolución de puzzles  
- Enemigos mecánicos  
- Grabación y reproducción temporal  
- Interactuables como puertas, presión, plataformas temporizadas  
- Sistema de pausa, créditos y HUD completo  

---

## 🎮 Mecánicas Principales

### ⏺️ Sistema de Record & Replay
Selecciona un objeto, graba su movimiento y luego reprodúcelo automáticamente.

| Acción | Tecla |
|-------|-------|
| Seleccionar objeto anterior | **Q** |
| Seleccionar siguiente objeto | **E** |
| Iniciar / detener grabación | **R** |
| Reproducir / detener reproducción | **T** |

El objeto seleccionado muestra un **halo verde**.

---

### 👤 Movimiento del Jugador

- **A / D** – mover  
- **Space** – saltar  
- Físicas estables, detección de suelo precisa, controles responsivos.

---

## ⚙️ Elementos del Nivel
- **Crono-Bloc (enemigo):** patrulla y se enfurece si lo tocas, aumentando su velocidad.  
- **Plataformas temporizadas:** aparecen/desaparecen.  
- **Placas de presión:** abren puertas mientras hay una caja encima.  
- **Puertas animadas:** se abren con puzzles o al final del nivel.  
- **Respawn de cajas:** si caen, vuelven al inicio y resetean grabación.  

---

## ⏸️ Sistema de Pausa
Incluye:  
- Continuar  
- Reintentar  
- Volver al menú principal  

---

## 💰 Sistema Freemium
Implementado usando **PlayerPrefs**:

- Botón “Comprar Premium”  
- Botón “Nivel Premium”  
- Persistencia del estado premium  
- UI dinámica según la compra  

Archivo principal: `PremiumStore.cs`

---

## 🎁 Daily Reward (Recompensa Diaria)
Sistema funcional que habilita una recompensa cada 24h (o 1 min en pruebas):

- Botón especial “Reclamar”  
- Guarda la fecha automáticamente  
- Deshabilita después de reclamar  
- Muestra un mensaje emergente (fade)

Archivo principal: `DailyRewardManager.cs`

---

## 🔊 Sistema de Audio Unificado

Incluye música y efectos:

- Salto  
- Puertas  
- Cronobloc  
- Plataformas  
- Grabación/Reproducción  

Archivo principal: `GameAudioManager.cs`

---

## 🧪 Testing Automatizado (Edit Mode + Play Mode)

### ✔️ Edit Mode Tests
- PlayerController tiene valores válidos  
- Daily Reward activa el botón si es primera vez  
- PremiumStore desbloquea correctamente  

### ✔️ Play Mode Tests
- RecordableObject graba y reproduce trayectorias reales  

Ubicación:  
`Assets/Tests/EditMode/`  
`Assets/Tests/PlayMode/`

---

## 📘 Créditos
- **Programación:** Alfredo Quispe  
- **Diseño de niveles:** Alfredo Quispe  
- **Arte generado con IA y editado manualmente:** AQU Studio  
- **Testing automatizado:** AQU Studio  
- **Publicación y documentación:** AQU Studio  

---

## 📄 Licencia

Uso académico / educativo.  
No se permite uso comercial sin autorización del autor.

---

## 👤 Autor

**Alfredo Quispe Ubaldo**  
GitHub: https://github.com/Maplide  
AQU Studio – 2025
