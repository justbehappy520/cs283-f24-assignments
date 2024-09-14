# Your README for A2 HERE
Instructions on gameplay:
Click on the screen to begin the game.
Use the left and right arrow keys, or the A and D keys to move the red box left and right.
Control the box left and right to collect the falling blue obstacles.
There are no penalties, play for as long as you are bored enough to do so.

Instructions on building:
All game objects (player, obstacles) are made using the Rectangle class, simple shapes. As is all of the
background, including the ground, the togglable name box, and the score board.
The red player character is controllable via arrow keys and A / D keys.
The blue obstacle boxes constantly update so they move down at a constant rate. Currently only one blue
obstacle spawns at a time. I have yet to figure out how to control having more of them spawn at a staggered
rate.
I'm not entirely sure what else to add here. I added in a collision checker after consulting with Professor
Aline. So now the program checks to see if any of the obstacle boxes' corners enters the red player box. If
so, the blue obstacle box will disappear and the score increments by one.

Some Issues:
The game states using enum are rather faulty. Though there is a "click to start" screen, I have a sneaking
suspicion the program is still running in the background.
I never quite understood how to get the game to terminate through having one of the obstacles touch the 
bottom of the screen. So while there is code for a GameStop screen, it never shows up, and the game runs
infinitely.
