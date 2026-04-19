# Endless Runner in Unity 2D
### Object Pooling Laboratory

A 2D endless runner game inspired by the Chrome Dinosaur Game, built in Unity as a demonstration of the **Object Pooling** design pattern.

---

## Gameplay

The player automatically runs to the right. Your only input is jumping to avoid obstacles and collect coins.

- **Space / Arrow Up** to Jump
- Avoid obstacles to stay alive
- Collect coins to increase your score
- The game tracks your survival time and coin count
- On death, a Game Over screen shows your final score with options to restart or quit

---

## Object Pooling

Object Pooling is a design pattern that pre-instantiates a set of reusable GameObjects at the start of the game instead of creating and destroying them at runtime. This avoids the performance cost of frequent `Instantiate()` and `Destroy()` calls, which can cause garbage collection spikes.

In this project, pools are used for:

| Pool | Size | Purpose |
|---|---|---|
| Small Obstacle | 5 | Short ground obstacles |
| Large Obstacle | 5 | Tall ground obstacles |
| Flying Obstacle | 5 | Mid-air obstacles |
| Coins | 10 | Collectible coins |

When an obstacle or coin moves off screen, it is returned to the pool (deactivated) instead of destroyed. The spawner then reactivates it when a new object is needed.

---

## Project Structure

```
Assets/
├── Prefabs/
│   ├── SmallObstacle.prefab
│   ├── LargeObstacle.prefab
│   ├── FlyingObstacle.prefab
│   └── Coin.prefab
└── Scripts/
    ├── ObjectPool.cs          Core pooling logic
    ├── PlayerController.cs    Movement, jump, collision
    ├── CameraFollow.cs        Camera tracks player on X axis
    ├── GroundScroller.cs      Infinite ground illusion
    ├── Mover.cs               Moves pooled objects, returns them when off screen
    ├── ObstacleSpawner.cs     Spawns obstacles using the pool
    ├── CoinSpawner.cs         Spawns coins using the pool
    └── GameManager.cs         Score, time, Game Over, restart
```

---

## Scripts Overview

### `ObjectPool.cs`
Generic pool that pre-instantiates a list of GameObjects at `Awake()`. Exposes `GetObject()` to retrieve an available object and `ReturnObject()` to deactivate it back into the pool. Automatically expands if the pool runs out.

### `ObstacleSpawner.cs` / `CoinSpawner.cs`
Timed spawners that call `GetObject()` from their respective pools and position the object ahead of the player. `CoinSpawner` uses `Physics2D.OverlapCircle` to avoid spawning coins on top of obstacles.

### `Move.cs`
Attached to every pooled prefab. Moves the object to the left each frame. When the object is more than 20 units behind the player, it calls `SetActive(false)` to return itself to the pool.

### `PlayerController.cs`
Handles automatic rightward movement, jump input, ground detection via `OnCollisionEnter2D`, obstacle collision (triggers Game Over), and coin collection (triggers score and sound).

### `GameManager.cs`
Singleton that tracks elapsed time and coin count, updates the HUD each frame, and activates the Game Over panel with final stats on death.

---

## Audio

| Sound | Trigger |
|---|---|
| Jump SFX | Player presses Space or ↑ |
| Coin SFX | Player collides with a coin |
| Background Music | Loops from game start, stops on Game Over |

---

## Scene Setup

| GameObject | Components |
|---|---|
| `Player` | Sprite Renderer, Rigidbody2D, Box Collider 2D, PlayerController, AudioSource |
| `Ground` | Sprite Renderer, Box Collider 2D, GroundScroller |
| `Main Camera` | Camera, CameraFollow |
| `ObstacleSpawner` | ObstacleSpawner + 3 child ObjectPools |
| `CoinSpawner` | CoinSpawner + 1 child ObjectPool |
| `GameManager` | GameManager |
| `MusicManager` | AudioSource (loop) |

---

## How to Run

1. Open the project in **Unity 2022+**
2. Open the main scene under `Assets/Scenes/`
3. Press **Play** in the Unity Editor
4. Press **Space** or **Arrow Up** to jump

---

## Notes

- The scene is intentionally built at **GrayBox** level (colored primitives, no final art)
- `Application.Quit()` only works in a built executable, not in the Unity Editor
- Pool sizes can be adjusted in the Inspector on each `ObjectPool` component