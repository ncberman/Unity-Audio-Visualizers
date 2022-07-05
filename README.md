# Unity-Audio-Visualizers
> Latest Release: https://github.com/ncberman/Unity-Audio-Visualizers/releases/tag/Alpha

This Program has 3 primary scenes
- Login page
- 3D Hex Island Visualizer
- Space Line Visualizer

## Login Page
![LoginPage](https://user-images.githubusercontent.com/72539181/177244777-675b1467-fd35-4b45-a032-cca489aad37f.PNG)

> This page is a non-functional page just to add a buffer between the user opening the application and the actual meat of the application. THis page has a blur shader applied to a application-wide panel to create an interesting looking background.





## 3D Hex Island Visualizer
    'ESC' - Changes your camera back to the default camera
    'Right Mouse Button' - Allows you to rotate the camera around the island
    'Left Mouse Button' - Clicking on a hex will zoom you into that hex
![HexVisual](https://user-images.githubusercontent.com/72539181/177244786-9da79c2c-6cfa-4b69-a4ad-d6b51c74c034.PNG)

> This page contains a procedurally generated formation of Hexes. These hexes are generated in a spiral from the center hex and are drawn entirely in code. After the entire island is generated the hexes are chopped into octaves and assigned a certain octave to listen and respond to. This Hex Island has 2 types of visualizer styles, the default is a raw style that increases the y-scale of the hex in accordance to the value of the octave. The other style is called a 'Beat' style and does not respond to spectrum / octave data until it reaches a certain threshold in which the hex scales to a set maximum height.

![HexSettings](https://user-images.githubusercontent.com/72539181/177244788-b094b756-dab7-4738-bd46-68243128bb0f.PNG)

> These are the procedural settings of the Hex Island, entering a new number of hexes will increase the size of the island, hieght, sets the maximum height the hex can scale to in the 'Beat' style visualizer, size changes the size of every hex, and offset changes the spacing of the hexes.

    
    
    
    
    
## Space Line Page
![DoubleCurved](https://user-images.githubusercontent.com/72539181/177244793-a91e1577-69bb-433a-bbec-ecaccfc41c00.PNG)

> This page is an entirely 2D page that uses dynamic particle speed with layers of particles to give the illusion of moving through a 3D space. The visualizer has 7 different styles, this style analyzes the bottom 7 octaves and draws a curve between the 7 points to create a curved line.


![Volumetric](https://user-images.githubusercontent.com/72539181/177244797-0deb8311-e90a-481a-bea4-bca8b4366a67.PNG)

> This is another style called 'Volumetric' which outlines the peaks of the different octave values.


![Filled](https://user-images.githubusercontent.com/72539181/177244800-ca818fa0-06b4-4af8-88c6-6aca25a99a35.PNG)

> This style is called 'Filled Volumetric' and instead of modifying the position of the line we add and modify width keys to the unity line renderer to visualize the fullness of the octaves.
