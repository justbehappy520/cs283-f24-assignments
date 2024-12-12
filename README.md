# cs283-f24-assignments
Assignment framework for CS283 Game Programming

# Final Project: Summer Camp
### Instructions
**How to Build:** 

**How to Play:**

### Player

**Description:** 

**Script:** 

### Environment
![Scene_Map](https://github.com/user-attachments/assets/3ec67e5d-fab5-4473-b76e-e199cd6039f6)

![Scene_Outside](https://github.com/user-attachments/assets/db4975ad-0299-4ae6-8a93-6f6311f700e2)

![Scene_Gate](https://github.com/user-attachments/assets/c62d4bda-a8a4-4828-aeee-7674d7de62d2)

![Scene_Fire](https://github.com/user-attachments/assets/a943b08e-89f8-4fe6-acff-26ac9cf6d469)

https://github.com/user-attachments/assets/5a599bc6-af85-4d24-be01-0632a129f34d

![Scene_Key](https://github.com/user-attachments/assets/fbc9c5ac-2c08-485f-92ca-285530933a39)

### Ghost NPC
![Ghost_Still](https://github.com/user-attachments/assets/4c359ff8-1bf9-41de-8150-0ff5f184e11b)

https://github.com/user-attachments/assets/e52c12bd-c885-4b96-a193-7319c071b510

**Description:** Across the maze wanders five ghosts, just a little gimmick set up by the Camp Director, or remnants of past campers trapped forever in the unending green...? All the same, the little ghosts are rather cute, and mostly harmless. They typically wander the maze aimlessly, as if they've lost all direction. When you draw near them, they turn to trail after you instead. Should you come into contact with them, you will find yourself teleported to one of five random locations in the maze. I hope you get your bearings straight haha.

**Script:** The ghost NPC's behavior is implemented in the GhostBehavior.cs script. This script is composed of a simple behavior tree with a wander behavior, a follow behavior, and a newly implemented teleport behavior. The behaviors are dictated by distance from player such that the ghost will teleport the player to a random set location if close enough, follow player is within range, and wander otherwise.

### Bear NPC

**Description:** Just a (mostly) friendly little guy wandering around. He's strangely translucent in the sense that you can see his individual working body parts, but that's what makes him so special (and not my utter confusion as to why the little guy exported like that). This poor bear has been trapped in this maze for so long, just longing for a friend, but his paws are just a tad too sharp, a single swing and you lose a heart (cry).

**Script:** 

### Key Collection
https://github.com/user-attachments/assets/932b7af6-4173-48fc-b1f5-27c79e0b3498

https://github.com/user-attachments/assets/31f62244-52a4-4a6e-886a-9ccc5ce16d94

https://github.com/user-attachments/assets/8f619019-da56-41b1-b94d-46c73c685f65

**Description:** The famed key to the wrought iron fence!! Your ultimate goal and your ticket to freedom is this little key that magically floats and spins above the ground. Located at the center-ish of the maze, you must brave the unknown paths, seek out the center, and collect the key!!

**Script:** The KeyHoverAnimation.cs and KeyCollectAnimation.cs are the scripts that implement the animation behavior of the key while idle or while being collected. The CollectMechanism.cs is the script that implements the deactivation of the key and the updating of the in-game UI when the key is collected by the player.

### Lives Tracker

**Description:** 

**Script:** 

### UI
https://github.com/user-attachments/assets/27d933ee-7f96-4b72-8825-fcd931c34ed0

**Description:** The UI of the main menu screen is implemented on another scene entirely, MainMenu.unity. Initially, I planned to set it up as another UI screen in the Project.unity scene. However, I figured it wouldn't hurt to leave myself open to the possibility of adding levels should I choose to come back to this project at a later date, and it would be easier to uniformize the main menu screen in this way.

**Script:** The MainMenu.cs script implements the behavior of the buttons on the start screen UI. The 'START' button starts the game

https://github.com/user-attachments/assets/3089cb88-7ec4-410f-a608-3ca6bba217c6

**Description:** 

**Script:** The PauseMenu.cs script implements the behavior of the buttons on the pause screen UI.

https://github.com/user-attachments/assets/40ace5e5-419a-4add-99da-f9187dcbe580

**Description:** 

**Script:** The YayEndMenu.cs script implements the behavior of the buttons on the good-ending game over screen UI.

**Description:** 

**Script:** The NayEndMenu.cs script implements the behavior of the buttons on the bad-ending game over screen UI.

### Notes

**Resources:** 

# A10: Behavior
### Minion
https://github.com/user-attachments/assets/915a2e12-fd63-4876-a33a-06d83360de60
### Unique
https://github.com/user-attachments/assets/d78a4da4-1c34-4e89-9c65-fb41021595a1


As I was making the recording for BehaviorUnique.cs, Unity kinda just crashed. I was also running into some issues with BehaviorUnique.cs not always working properly. I tested it earlier and the FleeBehavior was functioning fine, as was the WanderBehavior. I was struggling with QuoteBehavior though.

# A09: Wander
**NPC**: https://www.turbosquid.com/3d-models/gingerbread-cookies-3d-1836164 by GetDeadEntertainment
### Wander
https://github.com/user-attachments/assets/638c167f-fb71-4d9b-a485-4e0dfdd518b6

# A08: Collection Game
### Game Collection
https://github.com/user-attachments/assets/d5e4e07a-2f62-4b7c-b82d-280f08363aaf
### Spawner
https://github.com/user-attachments/assets/68b4c312-abc4-45e3-a99d-0de5cbd3baa9

**Collectible:** https://www.turbosquid.com/3d-models/food-free-2098576 by ithappy

Something is wrong with my Spawner. The collectables that it spawns, it takes me two tries to activate the animation and have the object disappear. I'm not entirely sure what's wrong or how to fix, but I'm probably missing something in the triggers.

Also I'm unsure what happened to my Game window. It's been like that since forever.

# A07: Motion and Collision
### Motion Controller
https://github.com/user-attachments/assets/b4b5a9f2-3552-4b7c-bda1-81c67fa386df
### Collision Physics
https://github.com/user-attachments/assets/15368c66-99b7-4616-91a1-b327890c2740

**New Character:** https://www.turbosquid.com/3d-models/low-poly-ankylosaurus-rig-1630692 by RJAnimations

# A06: Bezier Faster Stronger
### Spline with degree-1 polynomial
https://github.com/user-attachments/assets/318b1117-ea85-4d47-a5b8-2e3cfb21852d
### Spline with degree-3 polynomial
https://github.com/user-attachments/assets/c35e92ae-4275-4685-b250-49a0d2cdaf89
### Gaze Controller
https://github.com/user-attachments/assets/88444099-c09f-40d3-b8b7-3061e89736f5
### Two-Link Controller
https://github.com/user-attachments/assets/75fd9236-49ca-4cea-9d28-63508a0a7985

Something is wrong with 2.2.2, it definitely has something to do with how I'm pulling some random angle equations to calculate the rotation. I'm still not entirely sure how one exactly does this.

# A05: Spring into Action
### Rigid Camera
https://github.com/user-attachments/assets/9e7a9b4a-5156-443f-ac9a-85851e034b03
### Spring Camera
https://github.com/user-attachments/assets/d2d6a25c-fa4b-435d-b3c9-e8d824a55686

# A04: Flythrough Tour
### FlyCamera
https://github.com/user-attachments/assets/1cac8d6b-8dbc-4779-9dbd-8225b6f2b3dd
### Tour
https://github.com/user-attachments/assets/83565fd1-b2cc-4846-be8e-5d6eb5350275

There is a slight issue where the rotation in the tour script does not work when both FlyCamera.cs and Tour.cs are activated on the camera. However, once I uncheck FlyCamera.cs and only have Tour.cs running, the rotation works fine. I'm not sure where the conflict might be.

### Points of Interest
![donut_mountain](https://github.com/user-attachments/assets/6e971077-83b7-4bad-b513-305972d050b1)
![donut_x_o](https://github.com/user-attachments/assets/f0d8eef1-bc53-4614-90b9-d62168487e86)

### Character
https://github.com/user-attachments/assets/9d064b91-ad21-473b-96e0-d8a62463ce41

**Character:** https://www.turbosquid.com/3d-models/animated-lowpoly-dragon-2184488
By SoyTancha

# A03: Create a Virtual World
### Home Area
![Screenshot 2024-09-20 172802](https://github.com/user-attachments/assets/e93c8685-0d76-455b-a4f2-813b2ba1b747)
### Quest Area
![Screenshot 2024-09-20 172820](https://github.com/user-attachments/assets/95189352-f671-4e78-80ef-2cfd7d35f7fc)

They ended up turning out rather boring, but after sinking so much time into fiddling with donuts, I lost steam when I got to the Quest Area. They're supposed to be marshmallows... I'm unsure if the final poly count if acceptable.

**Skybox:** https://polyhaven.com/a/autumn_field_puresky
By Sergej Majboroda

# A01: Hello CS283!
### Particle System
https://github.com/user-attachments/assets/ed9e2116-5f2b-4f16-aa95-a4948e38b2fd

I am unsure how to make a gif. So all I can provide is a questionable screen recording. I hope that this is acceptable.
Thank you!
