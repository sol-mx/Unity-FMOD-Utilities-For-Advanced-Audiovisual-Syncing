# Unity-FMOD-Utilities-For-Advanced-Audiovisual-Syncing
a.k.a. "Digital Pit Orchestra Suite"

Includes "Audio-controlled Gameplay" (ACG) and "Visual Transition Buffer" (VTB).

ACG allows gameplay events to be cued at specific playback positions. 
It polls for the change in value of a (user-defined) parameter. Upon detection, it invokes a function with an index matching the new value.

VTB applies delay to visual transition to sync it with a (inherently, due to transition quantization) delayed audio transition.
When audio transition is queued, it calculates the remaining time until the next available transition point. 
Then using one from an array of delay options—such as manipulating time scale, stalling an animation, etc.—a delay whose length is identical to that remaining time is applied to the visual transition.
