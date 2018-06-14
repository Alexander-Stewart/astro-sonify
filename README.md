# astro-sonify
Work for Smithsonian Observatory Sonification project

# goal
To create a kind of sonification of 3D astronomy data.

# steps

The two important components are control over sound sources and an
absolute positioning system.  We want to be able to vary sound by
amplitude or pitch according to our location in space, and we also
want to be able to control the position in space absolutely.  Both
components are necessary.


1. Read OBJ file.

2. Absolute positioning.  (Suggest doing it by relative position of
   hand controllers, happy to hear other ideas.)
   
3. Sound control: pitch, timbre, volume, whatever, as a function of
   position. 



---------------------------------------------------------------------
   ALEX DAILY LOG:
   06/11/18: Started movement system and converted to SteamVR. 
   Looked at VRTK and need to discuss if we should use, has benifits 
   and drawbacks. Relative movement on one controller works, but need 
   to implement movement absolutely.
   Work Hours: 10-6

   06/12/18: Can now scale model up or down using the A and B buttons on 
   the right touch controller. Started script for haptic feedback, and still
   working on getting the absolute movement to work.
   Work Hours: 9-5 

   06/13/18: Have a prototype movement system where one controller is the origin and 
   the other is your relative position to the origin. When the relative 
   position controller is near the origin, it vibrates. if within 
   vibrating radius for 3 seconds, the origin and realtive postition 
   controllers switch hands. A and B on the right controller still
   scales the model.
   Work Hours: 9-5
   
   06/14/18: Set up new github branches and added important Unity files for backup. 
   Added Preprocessor Directives but need to improve for easier changing between 
   which testable movement system is to be used. Started on second movement system.
   Work Hours: 9-5
