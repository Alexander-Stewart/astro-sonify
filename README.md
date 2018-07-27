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
STEPS FOR SETUP:

1. Open a new Unity Project, add the Project Files to the Assets folder in Unity.
2. Go to the Assets Store, and download the SteamVR Assets.
3. Download RealSpace3DAudio trial version from https://realspace3daudio.com, and import their assets folder.
4. Go to File -> Build Settings -> Player Settings -> Scripting Define Symbols and type in either MS1 or MS2 into the text box.
5. Go to the Scences folder, and select a the completed movement system that you selected in step 4 to test. Each completed scene has a different movement system inside. To switch Movement systems, repeat steps 4 and 5.


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
   
   06/14/18: Continued working on second movement system. Now camera can track between left and 
   right controllers, as well as switch between the two. stlll working on a way to have relative position now,
   as well as a way to tell when the person is near the origin of the super nova. Below are the added and changed
   scripts:
   Added these new scripts:
- NegateTracking
- FindCenter
Updated These Scripts:
- NearOrigin
Work Hours: 9-4

06/18/18: Tested out the RealSpace3DAudio plug-in, and compared it to the Steam Audio plug-in that has similar features. I found RealSpace3DAudio easier to use once set up. Added a word document outlining my thoughts and what I found about RealSpace3dAudio. Tried to add haptic feedback for when at actual center of super nova for MS2, but it would stop other haptic feedback from working, will fix tomorrow. 
Work Hours: 9-5

06/19/18: Finished Movement System 2, and now have moved on to the audio portion of this project. Still looking to clean code and improve movement systems as well moving forward. Each script that is used has a comment at the top letting you know what it is used for. Will add to README tomorrow to have descriptions of each movement system and how everything fits together code wise and component wise on the game objects.
Work Hours: 9-5:30

06/20/18: Continued to learn how to manipulate audio in Unity through built in Unity features and RealSpace3D audio. Turns out you can manipulate specific audio effects in the Audio Mixer, you just have to make sure you set it to be editable before trying to access it through scripting. TestSound scene now changes the audio for in multiple ways depending on distance, rotation and height above the listener. Also, need to find more about how Open Space Program handles audio for space and how they maniplate it, talk to David more? Lastly, fixed some aspects of MS2, can now rotate without the controller rotating awkwardly. All updated scripts, prefabs, and scenes have been added. 
Work Hours: 9-3

06/21/18: Continued improving movement systems. MS1 is completely done, and there are two settings that need to be tested to see which is better. Starting learning about how to use Paraview, and trying to get the density data from the vtk file.
work hours: 9-5

06/25/18: Created a function that generates a random spherical harmonic, and then writes it to a file. Also have a method that can read files, and takes each coordinate and density data pair and adds them to a hashtable. Working on incorporating the data into the demos, but did not finish and I will try to wrap them up tomorrow morning.
work hours: 9-5

06/26/18: Finished the sound system with MS1, but could use tweaking and scalability. Looked at volumetric rendering for unity, it is possible, but need to research more. I will have more information tomorrow morning in the meeting. 
work hours: 9-5

06/27/18: Looked up financials for each of the plug ins and put them into a text document. Also cleaned up code for readability.
work hours: 9-5

06/28/18: Created a movement system that you can fly around in. If you press one of the analog sticks, a ray will shoot out of the controller, and if it hits a desginated spot on the supernova, a screen will pop up with a description of what part of the super nova that is.
work hours: 9-5

07/09/18: Continued learning about shaders to manipulate the asset that allows for volumetric rendering. Created a new Movement System, where you move by doing a pulling motion. 
work hours: 9-5

07/16/18: Started on implementing the gradient and curl based sonification system. created methods to find the gradient at each point in the scalar field. Next will be to get the audio source to rotate around the user and face the gradient. 

07/23/18: Finished implementing the Audio Sources pointing to direction of greatest density, and updated cleaned up the code to run smoother. Started to add in the sound to old movement systems, and will start on a way to quickly change between movement systems tomorrow.

07/24/18: Continued to implement the Audio system into old systems. The first two systems I made are very messy and hard to integrate new things into, I will redo them tomorrow, as I feel I know how to accomplish what they do in a simpler fashion. I also looked more into blending the different meshes of the supernova together, it is difficult in Unity, but may be possible to create an animation of the transformation in Blender.

07/25/18: Having a hard time recreating the first two Movement Systems, will come back to them once I have a way to quick switch between movement systems and make the sounds more distinguishable. 

07/26/18: Created a way to switch between movement systems without having to exit build/play mode. Started on different drums playing when you enter different parts of the super nova.

07/27/18: Finished adding triggers to where as you move along surface of super nova, different drums play as you are near certain areas/features.
