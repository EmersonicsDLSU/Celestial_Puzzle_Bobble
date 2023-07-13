# Celestial_Puzzle_Bobble
 MegaCatScreening  

Allotted Working Hours:  
Day 0(Thurs): 5 hours : analyzing, creating basic scripts 
Day 1(Friday): 5 hours : shooting and moving mechanic  
Day 2(Saturday): 5 hours : level design, creating prefabs  
Day 3(Sunday): 8 hours : fixing core features, level templates  
Day 4(Monday): 5 hours : added combination checker  
Day 5(Tuesday): 2 hours : fixing cannon functionalites  
Day 6(Wednesday): 5 hours : fixing combination checker  
Day 7(Wednesday): 12 hours :  UI, win/lose condition, moving ceiling, sfx  

Total of 47 hours.

Basic Level Template:  
Row: 10 & Col: 11  
Even Row: 11 columns | Odd Row: 10 columns

Things that I included:   
1. Object Pool System  
2. AudioManager  
3. Model View Control System  
4. Scriptable Objects (for tileMapLevels)  
5. UI (source: assets given from the screening)  
6. Animations  

Things to Improve:  
1. Verifying collapsable balls function is not efficient. It is checking all of the existing balls in the grid. Should only check the balls affected by the recently collapsed combination (a little inefficient, but precise / less bugs).  
2. Already have scriptable objects for the level templates; also easier for the designer to design the starting level colors(through text file).  However, there's no implementation for changing levels in the build, only in the editor where we can change the level we will play.  
3. Only 80% sure about the integrity of the combinationChecker and moving the balls when the ceiling is going down.  
4. Doesn't know how to convert sprite sheet containing letters to asset font. I had a harder time inputting the text UI since I'm grabbing each image one by one.

![image](https://github.com/EmersonicsDLSU/Celestial_Puzzle_Bobble/assets/80930588/05f9471c-6472-4f5a-96a6-8fa8a064b188)
![image](https://github.com/EmersonicsDLSU/Celestial_Puzzle_Bobble/assets/80930588/5674a28c-b721-42b5-ae19-8571cb12dd79)

 
