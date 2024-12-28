# TopDownShooter
A 2D top-down shooter featuring multiple enemy types, pickups, levels, and a complete game loop, built with efficient and scalable systems like MVC architecture, object pooling, and ScriptableObjects.

---
## PLAY ON: https://yashvardhan1.itch.io/topdownshooter

---

## Features

- Core Gameplay
    - Three Levels: Progress sequentially; levels are loaded additively for streamlined management.
    - Enemies:
        - Dog: Fast and agile.
        - Goblin: Tanky and strong.
        - Ranged Shooter: Fires projectiles at the player.
        - Turret: Stationary and shoots continuously.
    - Pickups:
        - Shield: Temporary invincibility.
        - Health Boost: Restores health.
        - Ammo Pickup: Refills ammo.
    
- Interactive Features
    - Complete Game Loop:
        - Lobby, pause, restart, game lost/win UI, and level transitions are all integrated seamlessly.
        - Progress through levels with clear objectives; the next level unlocks only after completing the current one.
      
- Audio-Visual Feedback
    - Sound Effects: Integrated sound effects for gameplay actions like collecting pickups, shooting, player damage, and UI interactions, enhancing immersion.
---
## Patterns Used

1. Object Pooling

    - Efficiently manages reusable objects:
        - Enemy Pool: Handles all enemy types (Dog, Goblin, Ranged Shooter, Turret).
        - Projectile Pool: Manages projectiles for ranged enemies, turrets, and the player.
        - Pickup Pool: Governs random and pre-placed pickups across levels.
      
2. Service Locator Pattern

    - A centralized Game Service manages all core services:

      - Player Service
      - Pickup Service
      - Level Service
      - UI Service
      - Sound Service
      - Common Pools for example: Pickup,Enemy,Projectile Pools
        
3. MVC (Model-View-Controller)

    - Strict adherence ensures modularity and scalability:

      - Model: Manages game data (enemy stats, pickups, and player state).
      - View: Handles UI elements, player visuals, and animations.
      - Controller: Implements game logic and connects Model and View.
        
4. Observer Pattern

   - Decouples game actions like starting, restarting, and transitioning between levels.
   - Example: On restarting, updates enemy spawns, resets player stats, and clears the level.

5.  ScriptableObjects

   - Used to store:
     - Enemy Attributes: Health, speed, and behaviors.
     - Pickup Configurations: Shield duration, health value, and ammo count.
     - Enables seamless scaling and reusability.



---
## SCREENSHOTS


<img src="https://github.com/user-attachments/assets/74a0c97c-328b-48b7-b00f-8bda138192b9" alt="Screenshot 1" width="400" height="600" style="margin: 20px;">
&nbsp;&nbsp;&nbsp;&nbsp;
<img src="https://github.com/user-attachments/assets/428af76a-5ad8-42f9-a5c3-225f274e0b0a" alt="Screenshot 2" width="400" height="600">
<br><br>
<img src="https://github.com/user-attachments/assets/b9b6f14c-d7c6-4011-86a7-12c685c0da86" alt="Screenshot 3" width="400" height="600" style="margin: 20px;">
&nbsp;&nbsp;&nbsp;&nbsp;
<img src="https://github.com/user-attachments/assets/bfef680b-28cf-4709-8cda-0bf3408b1b58" alt="Screenshot 4" width="400" height="600">

