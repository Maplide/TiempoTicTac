# 🎮 Tiempo Tic-Tac  
### Un juego puzzle–plataformas basado en manipulación del tiempo  
**Desarrollado por:** Alfredo Quispe Ubaldo – *AQU Studio*  
**Motor:** 6000.2.5f1  
**Plataformas:** WebGL (itch.io, Unity Play)

---

## 📹 DEMO / JUGAR EN LÍNEA

🔗 **itch.io:** https://maplide.itch.io/tiempo-tic-tac  
🔗 **Unity Play:** https://play.unity.com/en/games/fbc8a760-66c7-4099-adf8-7909f3fd7d68/tiempo-tic-tac

---

## 📝 Descripción General

**Tiempo Tic-Tac** es un juego 2D puzzle–plataformas ambientado en un laboratorio científico.  
El jugador debe superar obstáculos utilizando una mecánica única: **grabar y reproducir movimientos de objetos** para resolver puzzles y avanzar por el nivel.

Incluye:

- Plataformas  
- Puzzles  
- Enemigos mecánicos  
- Grabación/Reproducción temporal  
- HUD completo, menú, créditos y pausa  
- Movimiento fluido y físicas estables  

---

## 🎮 Mecánicas Principales

### ⏺️ Record & Replay
Selecciona un objeto, graba su recorrido y reprodúcelo para usarlo a tu favor.

| Acción | Tecla |
|-------|-------|
| Objeto anterior | **Q** |
| Siguiente objeto | **E** |
| Grabar / detener | **R** |
| Reproducir / detener | **T** |

El objeto activo muestra **halo verde de selección**.

---

### 👤 Movimiento del Jugador

- **A / D** — mover  
- **Space** — saltar  
- Controles precisos y responsivos.

---

## ⚙️ Elementos del Nivel

- **Enemigo Crono-Bloc:** patrulla con animación y furia temporal.  
- **Plataformas temporizadas**  
- **Placas de presión y puertas animadas**  
- **Respawn de cajas**  
- **Detector de victoria**  

---

## ⏸️ Sistema de Pausa  
Incluye:

- Continuar  
- Reintentar  
- Menú principal  

---

## 💰 Sistema Freemium

Implementación con PlayerPrefs:

- Botón “Comprar Premium”  
- Botón “Nivel Premium”  
- Persistencia del estado después de cerrar el juego  
- UI dinámica  

Código principal: `PremiumStore.cs`

---

## 🎁 Daily Reward (Recompensa Diaria)

Sistema funcional con:

- Botón “Reclamar”  
- Persistencia de fecha  
- Disponibilidad cada 24 horas (1 minuto en pruebas)  
- Mensaje con fade  

Código: `DailyRewardManager.cs`

---

## 🔊 Sistema de Audio

Incluye efectos de:

- Salto  
- Puertas  
- Grabación  
- Reproducción  
- Enemigos  
- Música de fondo  

Código: `GameAudioManager.cs`

---

## 🧪 Testing Automatizado

### ✔️ Edit Mode
- PlayerController tiene valores válidos  
- DailyReward activa botón correctamente  
- PremiumStore desbloquea contenido  

### ✔️ Play Mode
- RecordableObject reproduce trayectorias grabadas  

Rutas:  
`Assets/Tests/EditMode/`  
`Assets/Tests/PlayMode/`

---

## 📘 Créditos

- **Programación:** Alfredo Quispe  
- **Diseño de niveles:** Alfredo Quispe  
- **Arte IA + edición manual:** AQU Studio  
- **Testing automatizado:** AQU Studio  
- **Publicación:** AQU Studio  

---

# 🧑‍💻 Roles del Estudio (Fase Gold – Publicación)

Durante la etapa final del proyecto los roles fueron:

### 🟦 Ingeniero de Release  
**Alfredo Quispe Ubaldo**  
- Configuración de Player Settings (icono, empresa, versión 1.0).  
- Build WebGL y corrección de errores URP.  
- Ajuste para navegadores (viewport, resolución, UI).

### 🟧 Diseñador de Monetización  
**Alfredo Quispe Ubaldo**  
- Implementación del sistema Freemium.  
- Persistencia permanente del estado premium.  
- Flujo UX del menú Premium.  

### 🟩 Product Manager  
**Alfredo Quispe Ubaldo**  
- Descripción corta y larga optimizada para tienda.  
- Screenshots, portada y presentación visual.  
- Publicación final en itch.io y Unity Play.  

---

# 📝 Reflexión Final (Post-Mortem del Semestre)

### ⭐ ¿Qué salió diferente a lo planeado?
Pensé que el sistema Record/Replay sería lo más complejo,  
pero integrar **UI, Premium, recompensas, audio, tests y publicación WebGL** tomó más tiempo y ajustes.  
También aprendí que optimizar para navegador requiere mucho detalle que no está en clase.

---

### ⭐ ¿Qué característica eliminaste para cumplir la fecha?

Para entregar un producto estable eliminé:

- Creación de musica propia
- Creación de SFX propia 
- Animaciones más avanzadas para cajas/puertas  
- Sistema extra de físicas avanzadas  

Esto permitió entregar un juego estable y pulido.

---

### ⭐ ¿Qué aprendiste del proceso completo?

Aprendí que hacer un juego no es solo programar:  
es **pulir, documentar, testear, versionar y publicar**.  
Comprendí el valor del ciclo completo:

**Idea → Nivel → Mecánicas → Pulido → Build → Publicación → Documentación.**

Y sobre todo:

> *“Shipping is a feature.”*  
Publicar un juego cambia completamente tu manera de ver el desarrollo.

---

## 📄 Licencia

Uso académico / educativo.  
No se permite uso comercial sin permiso del autor.

---

## 👤 Autor

**Alfredo Quispe Ubaldo**  
GitHub: https://github.com/Maplide  
AQU Studio – 2025
