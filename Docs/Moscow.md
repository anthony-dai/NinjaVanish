# MosCow

## Must Haves:

•	Designing our own player and enemies

•	Create our own skeletal animation for main character

•	Use particle system for smoke

•	Game is able to detect when something is in the shadow

•	Player is visible for detection by the enemy in light

•	Players cannot be detected by the enemy in the dark (shadows, dark rooms)

•	Player can still see itself clearly despite the dark surroundings

•	Audio mixer with master volume, SFX volume and music volume that the player is able to customize.

•	AI to enemies that determine the path they take for patrol

•	Enemy has a detection zone in front of him

•	When the player is visible and in the detection zone of the enemy, the enemy runs towards the player.

•	When the enemy is close enough to the player and in detection zone, the game is over. YOU LOST

•	Enemy is able to hear the sounds that the player makes if it is close enough to the player (suspicious)

•	When an enemy detects something suspicious, it will go that location where it happened

•	When enemy reaches this location where something suspicious happened, it will stay there for a short time and look around. (Investigating)

•	When an enemy is done investigating, it returns to its original position

•	Make premade rooms for each floor

•	An AI will choose rooms randomly for each floor

•	AI will try to choose more difficult rooms from the list, the more rooms the player clears

•	Smoke spreads and disperses over time

•	When the player in the smoke the player is harder to be detected

•	Menus: Start, loading, end screen (game over, end of the room reached),  options, pausing menus

•	Surveillance Cameras that have detection zones

•	Rotating surveillance cameras

•	FPS independent  implement Time.deltaTime everywhere where possible or use fixedupdate where possible

•	Player camera follows the player from a fixed distance with angled topdown view

•	Wall of current room that is infront the player camera becomes invisible

•	Track stats: steps taken, amount of times spotted, time, how far you’ve gotten

•	Save highscores based on the how far you’ve gotten and time stats

•	Set up an online central server that stores player stats



## Should Haves:

•	Create our own environment assets

•	Apply skeletal animation to other entities other than the main character

•	Use particle system for other effects (fire, water)

•	Circle around the player that indicates how far the sounds that the player makes travel.

•	Circle becomes bigger when performing “loud” actions (sprinting, dashing)

•	AI is able change certain elements of the rooms when choosing

•	Player is able to change the overall difficulty in the settings (easy, medium, hard)

•	If a vent is near a smoke, the smoke clears faster

•	Menus: inventory menu

•	Moving platforms

•	Different kind of traps

•	Track more stats

•	Create your own data analytics tool




## Could Haves:

•	Use throwable objects to destroy light sources

•	Implementing animated textures to game objects

•	When an enemy detects something suspicious he has the option to communicate this to other enemies.

•	Enemies determine their path based on each other

•	Local multiplayer (split screen)


## Won’t haves:

•	Online Multiplayer feature
