# CustomCamera
A script for GTA V that toggles a custom camera

A simple script that creates a custom camera to be used for different purposes.

## Installation
Copy the .cs into your scripts folder. Requires Script Hook V and Script Hook V DOT NET

## Usage
Toggle the camera ON/OFF: 'H'
Camera movement forward/backward/left/right: 'W', 'S', 'A', 'D'
Camera movement up/down: 'Q', 'E'
Camera rotation: mouse
Enable fast speed: Hold 'SHIFT'

## Note
When the camera is activate the character is frozen.
To modify the camera speed edit the value for "slowMoveSpeed" in the code (float ranging between 0 and 1)
To modify the camera fast speed (when pressing 'SHIFT') edit the value for "fastMoveSpeed" (this is a float that multiplies the value of "slowMoveSpeed")
To modify the sensitivity of the mouse edit the values for "mouseSensitivityX" and "mouseSensitivityY"
