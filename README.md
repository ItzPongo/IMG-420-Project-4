# IMG-420-Project-4

## Overview
This project implements a **2D top-down tile-based adventure game** using **Godot 4.4** and **C# (.NET 8)**.  
It features player movement, animated sprites, enemy pathfinding, collectible coins, particle effects, and a live score UI.  
The level is built using a TileMap and TileSet for grid-based world layout.

---

## Basic Requirements
1. **Tile-based world**
   - Level layout created with a TileMap node and a TileSet defining floor, walls, and obstacles.
   - Collisions added in the TileSet editor to prevent walking through walls.

2. **Player character**
   - Uses `CharacterBody2D` as the root with `AnimatedSprite2D` and `CollisionShape2D` children.
   - Movement handled in `_PhysicsProcess()` using velocity vectors and `MoveAndSlide()`.
   - Supports 4-directional movement and idle/walk animations.

3. **Sprite animation**
   - Implemented for both player and enemies using `AnimatedSprite2D`.
   - Includes directional animations (up, down, left, right).

4. **Enemies with pathfinding**
   - Each enemy uses `NavigationAgent2D` to follow the player.
   - Navigation layers are defined in the TileSet and painted onto walkable areas.
   - Enemies dynamically avoid walls and obstacles.

5. **Particle effects**
   - Implemented using `Particles2D` nodes and `ParticlesMaterial`.
   - Triggered when enemies die or collectibles are picked up.

6. **Interactions and simple physics**
   - Player collides with walls and interacts with coins.
   - Coins increase score through `ScoreManager` and then disappear (`QueueFree()`).

7. **UI and feedback**
   - Score displayed on-screen using a `Label` node updated by `ScoreManager.cs`.
   - Real-time feedback through animations, collisions, and particles.

---

## What was added / changed
- Added C# gameplay scripts for player, enemy, coins, and score management.
- Added particle effect for enemy death and coin pickup.
- Implemented navigation for enemy pathfinding.
- Integrated coin collection and score system.
- Added responsive animations for player and enemies.
- Organized all assets and scripts into clean directory structure.

---

## Files & Structure
```
├── Assets/
│ ├── Sprites/ # Player, Enemy, Coin, and Particle sprites
│ └── Tileset/ # Floor TileSet
│
├── Scenes/
│ ├── Main.tscn # Main scene containing world, UI, and game logic
│ ├── Player.tscn # Player setup with CharacterBody2D root
│ ├── Enemy.tscn # Enemy with NavigationAgent2D and animation
│ ├── Coin.tscn # Collectible coin scene
│ ├── DeathParticles.tscn# Particle effect scene
│
└── Scripts/
├── Player.cs
├── Enemy.cs
├── Coin.cs
├── CoinSpawner.cs
├── Pickup.cs
├── ScoreManager.cs
```

---

## Gameplay Summary
- **Movement Controls:**  
  - Arrow keys for Up, Down, Left, Right movement.  
- **Objective:**  
  - Collect coins to increase your score while avoiding enemies.  
- **Interactions:**  
  - Player collides with walls and solid objects.  
  - Coins disappear and update score when collected.  
  - Enemies chase the player using pathfinding.

---

## How Systems Work

### Player
- Moves using input vector:
  ```csharp
  var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
  Velocity = direction * Speed;
  MoveAndSlide();
- Animation switches between idle and directional walk cycles.

### Enemy 
- Navigation handled by NavigationAgent2D.
- Continuously updates its target to the player’s position.
- Triggers death particle on defeat.

### Coin & Scoring 
-Each coin increases score via: 
 ```csharp
 var scoreManager = GetTree().Root.GetNode<ScoreManager>("ScoreManager");
 scoreManager.AddScore(2);
```
-Coin despawns with QueueFree() after pickup.

### Particle Effects 
-Implemented for death and collection events. 
Controlled through Particles2D and ParticlesMaterial.

---

## Troubleshooting / common fixes
- **Missing assemblies / compile errors:** Open the `.csproj` and set `Sdk="Godot.NET.Sdk/<your-version>"` to match your Godot Mono binary.

---
