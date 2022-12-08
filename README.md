# SketchboxVRSim

I enjoyed my first experience in VR development, I had to integrate a mock location to simulate the VR set controls and keep the VR experience rather than using a regular keyboard and mouse experience.

The guide and documentations were somewhat tricky because of the mock controls. Still, I managed to make some of the functionalities with a couple of hours of knowledge I got from reading through normcore documentation.

Normcore is a good networking solution, it has a unique approach where it is not heavily based on RPC (like most of the networking solutions) but rather focuses more on initiating realtime prefabs and keeping them and the realtime models synced through a Model-Sync-Interface pattern that I exploit in at best in the gathering functionality.

I kept visuals basic and focused on the logic but here is what I would do if I had time:
- customize the avatars and display their names above their heads.
- Add a brush select tool (Adjustable type, size, and color...)
- make the teleportation trail's color unique to the client and add VFX to make it more appealing.
- Add sounds and vibrations...

There are also some technical aspects that I would optimize, like the integration of a pooling system for (trails and brush strokes...)


--------------------------


Functionalities:
• Annotation ability.
• Teleport and trail functionality
• Gather Functionality


--------------------------


• Keyboard and mouse General Mock Controls:
- Activate the Head by holding the Right Mouse Button.
- Activate a hand by Holding Left Shift for Left Hand and Space Bar for Right Hand.
- Toggle On and Off the activation of a hand by clicking T for the Left Hand and Y for the Right Hand.
- Moving the mouse will move the active head and/or hands in the facing plane.
- Holding CRTL will rotate the actives instead of moving them.

• Annotation ability Controls:
- Activate the Right hand and click the Left Mouse Button to activate the brush tool, and Move the right hand to start drawing in 3D space.

• Teleport Controls:
- Activate either hand and aim the casting ray to a teleportation area; once the Reticle is visible, you can click G (Grip) to teleport.

*Gather Functionality:
- The first one to join the room is considered the Gatherer.
- If the instructor interacts with the box in the middle (Activate either hand and aim the casting ray, once the Reticle is visible you can click G (Grip) to interact) he will call a meeting and gather him and all other connected clients (only set 6+1 places) looking at him and him at them.

