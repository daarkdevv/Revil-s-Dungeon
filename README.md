# Revil's Dungeon

A Unity-based 2D roguelike prototype featuring procedural dungeon generation, real-time combat, and item progression.

> **Note:** This was my **first game development project ever** and served as a learning experience. The code has since been significantly refactored with improved naming conventions, architecture, and best practices. This project does not reflect my current coding ability.

## Project Overview

Revil's Dungeon is an action roguelike prototype built with Unity 2020.3.21f1 that features:

- **Procedural Dungeon Generation** - Dynamically generated rooms with intelligent object spawning, room-specific loot distribution, and environmental hazards
- **Dynamic Combat System** - Multi-hit combo attacks with directional variations (up, down, forward), stamina-based abilities, damage falloff, and critical hit mechanics
- **Player Dash Mechanic** - High-risk dash ability with cooldown, invincibility frames, stamina cost, and visual dash ghost trail effect
- **Advanced Enemy AI** - Multi-layered AI with player detection radius, line-of-sight raycast verification, random patrol behavior, and enemy-to-enemy collision avoidance
- **Damage & Knockback System** - Enemies deal variable damage (50-100% of base), knockback on hit, and visual feedback with particle effects
- **Inventory & Loot System** - Item stacking with visual counter displays, key collection for chest unlocking, potion healing, and procedurally spawned treasure
- **Character Progression** - XP-based leveling with 7 progression tiers, attribute points for upgrades (health, speed, damage), and stat-based gameplay modifiers
- **Comprehensive Stats System** - Health, speed, damage, luck, intelligence, charisma, resistance, agility, accuracy, evasion, and defense tracking
- **Responsive Camera System** - Cinemachine-integrated virtual camera with drag-to-pan controls, smooth tracking, and zoom capabilities
- **Mini-Map System** - Real-time mini-map UI showing room layout, player position, enemy locations, and interactive exploration tracking.
- **Advanced Pathfinding** - A* Pathfinding Project integration with dynamic graph updates when chests open, seamless route recalculation
- **UI Animation Framework** - LeanTween-based animations for inventory screens, multi-page inventory system with smooth transitions, and visual feedback handlers
- **Polish & Feedback** - Camera shake on dash, slider shake on damage hits, invincibility animation loops, blood particle effects, enemy health bars, and status displays

## Project Structure

```
Assets/
├── Scripts/                 # C# game scripts
│   ├── Ai/                 # AI and enemy behavior
│   ├── Camera/             # Camera control systems
│   ├── DungeonGeneration/  # Procedural generation
│   ├── PlayerScript/       # Player mechanics
│   ├── Other/              # Utility scripts
│   └── *.cs                # Individual gameplay systems
├── ScriptableObjects/      # Data definitions (inventory, upgrades, etc.)
└── [other folders]         # Art assets and dependencies
```

## Key Features & Systems

### Player Systems
- **PlayerMovement.cs** - Core movement with dash mechanic
  - Stamina-based dash with directional force and cooldown management
  - Dash ghost trail visual effects spawned at intervals
  - Stamina cost deductions and invincibility frame integration
  - Camera shake trigger on dash execution
  
- **PlayerAttack.cs** - Multi-directional combo attack system
  - 3-hit combo chains for standard, up, and down attacks
  - Attack cooldown and directional input handling (Z, X, C keys)
  - Knockback force application to enemies
  - Coordinated with invincibility frames to prevent mid-combo hits

- **PlayerTakeDamage.cs** - Damage handling and death mechanics
  - Invincibility frame system after taking damage
  - Health slider updates with shake feedback
  - Blood particle instantiation on hit
  - Animation triggers for hurt/death states
  - Death state that locks player input and disables collision

- **ItemCounter.cs** - Central game state manager (Singleton)
  - Tracks coins, keys, and health/stamina values
  - Manages XP progression with 7 level tiers
  - Stores all character stats (attack, defense, speed, evasion, etc.)
  - Supports real-time stat display updates

### Enemy AI Systems
- **Ai.cs** - Enemy detection and pathfinding
  - Circular detection radius for player awareness
  - Line-of-sight raycasting through environment obstacles
  - State-based pathfinding enable/disable (stops chasing if blocked for 10 seconds)
  - Random walk behavior when player not detected
  - Animator-driven flip for directional facing

- **EnemyAttack.cs** - Combat execution
  - Range-based attack trigger when reaching path destination
  - Variable damage (50-100% of base value) for combat variety
  - Attack animation with damage immunity check for dashing players
  - Attack cooldown system (0.7s between attacks)

- **AvoidEnemy.cs** - Enemy collision avoidance
  - Prevents clustering with other enemies
  - Distance-based separation movement
  - Smooth avoidance without stopping pathfinding

### Dungeon Generation & Loot
- **RandomItemSpanwer.cs** - Sophisticated procedural spawning system
  - Room-size-based loot limits (Small/Medium/Large)
  - Per-room enemy spawner limits with configurable maximums
  - Multi-directional raycasting to check environmental validity
  - Step-based random walk placement algorithm
  - Intelligent spawning that avoids doors, walls, and obstacles
  - Separate pools for different item types and enemies

- **OpenChest.cs** - Interactive chest system
  - Key requirement verification before opening
  - Loot pool spawning with procedural drop locations
  - Animator integration for chest opening animation
  - Dynamic pathfinding graph updates when opened

- **KeyCollect.cs** - Key pickup system
  - Animated key collection feedback
  - Item counter increment with floating text popup
  - Color-coded notifications (silver for keys)

### UI & Inventory
- **SlotSys.cs** - Inventory slot management
  - Stack counter tracking and display
  - Max stack validation
  - Real-time text UI updates

- **UIAnimatorHandler.cs** - Complex animation framework
  - LeanTween-based smooth UI transitions
  - Multi-window inventory system with page navigation
  - Animated screen overlays (black screen transitions)
  - Quick slot bar management
  - Configurable ease functions (InOutQuad, etc.)

- **UpgradeButton.cs** - Stat upgrade interface
  - Attribute point validation
  - Visual state changes based on available points
  - Conditional button highlighting

- **PotionHealButton.cs** - Consumable item usage
  - Stack deduction on use
  - Automatic removal when depleted

### Camera & Controls
- **DragCamera.cs** - Pan and drag controls
  - Mouse-based camera dragging for map exploration
  - Cinemachine virtual camera integration
  - Movement threshold to prevent accidental pans
  - Maintains camera Z-depth for proper layering
  - Dynamic mode switching for mini-map vs main view

- **SetMiniMapState.cs** - Mini-map camera mode manager
  - Toggles between main map view and mini-map UI
  - Integrates with DragCamera for seamless transitions
  - Enables/disables camera control based on UI state

- **DistanceView.cs** - Zoom controls
  - Orthographic size adjustment for zoom
  - Distance-based view adaptation

- **IncreaseOrtographOrDecrease.cs** - Dynamic zoom system
  - Responds to player distance changes
  - Smooth zoom transitions

### Visual & Audio Feedback & UI
- **EnemyHealthBar.cs** - Enemy health display above enemies
- **healthslider.cs** - Player health UI with visual feedback
- **StaminaSlider.cs** - Real-time stamina display
- **XPSlider.cs** - Experience progression visualization
- **SliderShaking.cs** - Damage shake feedback effect
- **ShattersScript.cs** - Destructible object effects
- **ClosingInventory.cs** - Inventory window state management
- **SetMiniMapState.cs** - Mini-map UI state toggling
- **UIAnimatorHandler.cs** - Animated UI panel transitions with LeanTween

### Environment Interaction
- **DoorOpen.cs** - Door activation mechanics
- **DoorExit.cs** - Level exit transitions
- **DamageToDistractableObject.cs** - Environmental hazard interactions
- **BushTouch.cs** - Vegetation collision effects
- **RandomVelocityToObject.cs** - Physics-based object knockback
- **collisonDestroy.cs** - Collision-triggered destruction
- **ShowOnTop.cs** - Z-depth sorting for visual clarity

## Technical Highlights

### Architecture Patterns
- **Singleton Pattern** - `ItemCounter` serves as centralized game state manager
- **Component-Based Design** - Modular script responsibilities (movement, attack, damage separated)
- **Event-Driven Systems** - Collision callbacks, trigger-based animations
- **Pool-Based Spawning** - Efficient enemy and loot instantiation in procedural rooms

### Performance Optimizations
- Raycasting for efficient collision detection and line-of-sight checks
- Physics2D overlap circles for proximity-based detection (detection radius)
- Coroutine-based timing for dash ghost trails and animations
- Conditional pathfinding updates only when chests open (dynamic obstacles)

### Integration Technologies
- **Cinemachine** - Smooth camera follow and virtual camera management
- **Animator System** - State machine-driven character and enemy animations
- **Physics2D** - Rigidbody2D-based movement and knockback
- **TextMeshPro** - Dynamic text UI for inventory, damage numbers, and status

## Development Guidelines

### Git Workflow

This repository tracks **scripts only**. All asset files are ignored:

- ✅ **Tracked**: C# scripts (.cs files)
- ✅ **Tracked**: ScriptableObjects (.asset files)
- ✅ **Tracked**: Configuration files
- ❌ **Ignored**: Art assets (textures, sprites, audio)
- ❌ **Ignored**: Generated files (Library/, obj/, Temp/)
- ❌ **Ignored**: Editor cache

## License

**All Rights Reserved**

© 2025. This project and all of its contents are the proprietary and confidential work of the author. Unauthorized copying, reproduction, distribution, or use of any part of this project without explicit written permission is strictly prohibited.

This code is provided for portfolio and demonstration purposes only. All intellectual property rights are retained by the author.

