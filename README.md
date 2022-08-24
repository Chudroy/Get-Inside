# Get-Inside
An indie videogame I made, after taking the 
[GameDev.tv Complete C# Unity Game Developer 2D Online Course](https://www.gamedev.tv/p/unity-2d-game-dev-course-2021/?coupon_code=SUMMER)

[Link to play the game.](https://play.unity.com/mg/other/get-inside-finished)

## Features

### Objective

The objective of the game is to follow the paths in each area, which eventually lead to a path through the forest to the next area. Each area is a village,
and the final area is your home.

### Player

- The player has three weapons: an axe, a "flare-gun" and a "shot-gun".

- The player also has access to a torch with the right mouse button, which will scare away organic enemies (i.e, not robots). Yetis and ghosts will stand still 
at a distance from the player, while aliens will run outright run away for a set duration.

- The player can sprint for a short time with the shift key. This sprint is fast-recharge, fast-depletion, so that the player can't just run away from all
enemies, but can maneuver more strategically around in combat. Even though it's a "sprint" mechanic, it's more akin to a "roll" mechanic.



### Enemies
- The enemies are spawned on the edge of the map, and are assigned a random point on the opposite side to go towards. They use astar pathfinding to work their way 
through the buildings. If the player enters the line of site of an enemy, they will change from a "docile" state to an "attack state". If the player eludes
an enemy for long enough, they will return to the docile state, leaving the player alone. In practice, though, the enemy or player death is usually the outcome.

#### Types of enemies

There are various types of enemies with different behaviours.

- The brown yeti-type enemies are the simplest. If they see you, they beeline towards you to attack.

- The alien-type enemies will shoot orbs of glowing light at you from a distance, which will track you for a limited time.

- The ghost-type enemies are heavy hitting and can bypass walls, but are slow moving and easy to dodge. They will also eventually leave you alone if you
avoid them for long enough.

- The sentry type enemies scan the area in 360º. If they detect the player, they will most like one-shot kill the player.

- The robot-type enemies will scan the area if you are within range of their line of site. If you are hit by their scanning laser, they'll shoot.

### Levels

The levels are made of sprite tilemaps, with some of the tiles marked as "obstacles" for the astar pathfinding. The edges of the map are forests, which block 
the player, but let enemies pass through them. This is achieved by putting the player, the enemies, and the special ghost-type enemies on different Unity layers,
thus allowing different forms of interaction with the environment.





