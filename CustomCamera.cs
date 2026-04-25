using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Windows.Forms;
using Control = GTA.Control;

namespace CustomCamera
{
    public class CustomCamera : Script
    {
        private Camera myCamera;
        private float moveSpeed;
        private float slowMoveSpeed = 0.1f;//0.05f; //base speed of camera movement
        private float fastMoveSpeed = 3f; //speed multiplier when SHIFT is held 
        private float mouseSensitivityX = 3.0f; //horizontal rotation sensitivity
        private float mouseSensitivityY = 3.0f; //vertical rottion sensitivity
        public CustomCamera()
        {
            this.Tick += onTick;
            this.KeyDown += onKeyDown;
        }

        private void onTick(object sender, EventArgs e) //this function gets executed continuously 
        {
            //if the camera is active, update its point of view
            if(myCamera != null && myCamera.IsActive)
            {
                //point the camera to the player
                //myCamera.PointAt(Game.Player.Character.Position);
                UpdateCustomCamera();
            }
        }
        private void UpdateCustomCamera()
        {
            //determine camera speed movement
            if(Game.IsKeyPressed(Keys.ShiftKey))
            {
                moveSpeed = fastMoveSpeed;
            }
            else
            {
                moveSpeed = slowMoveSpeed;
            }
           

            //POSITION
            //get the current camera position and vectors
            Vector3 camPos = myCamera.Position;
            Vector3 camForward = myCamera.ForwardVector;
            Vector3 camRight = myCamera.RightVector;
            Vector3 camUp = myCamera.UpVector;

            //camera movement controls
            if (Game.IsKeyPressed(Keys.I))
            {
                camPos += camForward * moveSpeed;
            }
            if (Game.IsKeyPressed(Keys.K))
            {
                camPos -= camForward * moveSpeed;
            }
            if (Game.IsKeyPressed(Keys.J))
            {
                camPos -= camRight * moveSpeed;
            }
            if (Game.IsKeyPressed(Keys.L))
            {
                camPos += camRight * moveSpeed;
            }
            if (Game.IsKeyPressed(Keys.U))
            {
                camPos += camUp * moveSpeed;
            }
            if (Game.IsKeyPressed(Keys.O))
            {
                camPos -= camUp * moveSpeed;
            }
            //update camera position
            myCamera.Position = camPos;


            //ROTATION
            float mouseX = (Function.Call<float>(Hash.GET_CONTROL_NORMAL, 0, Control.LookLeftRight) * mouseSensitivityX);
            float mouseY = (Function.Call<float>(Hash.GET_CONTROL_NORMAL, 0, Control.LookUpDown) * mouseSensitivityY);

            //adjust the camera rotation
            Vector3 cameraRotation = myCamera.Rotation;
            cameraRotation.Z -= mouseX; //rotate around the Z axis (yaw)
            cameraRotation.X -= mouseY; //rotate around the X axis (pitch)

            //limit the vertical rotation to prevent flipping
            cameraRotation.X = Clamp(cameraRotation.X, -89f, 89f);

            //update camera rotation
            myCamera.Rotation = cameraRotation;
        }

        private float Clamp(float value, float min, float max)
        {
            return(value < min) ? min : (value > max) ? max : value;    
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.H)
            {
                if(myCamera== null || !myCamera.IsActive)
                {
                    //define camera position, rotation and field of view
                    //Vector3 camPos = Game.Player.Character.GetOffsetPosition(new Vector3(0f, 0f, 0f));//new Vector3(25.5f, 537.5f, 176f);
                    //Vector3 camRot = new Vector3(0.0f, 0.0f, 135.9f);
                    //float fov = 50.0f;

                    //create the camera
                    //myCamera = World.CreateCamera(camPos, camRot, fov);
                    myCamera = World.CreateCamera(GameplayCamera.Position, GameplayCamera.Rotation, GameplayCamera.FieldOfView);

                    //activate the camera
                    myCamera.IsActive = true;

                    //set the Camera as the active rendering camera
                    World.RenderingCamera = myCamera;

                    //player invisible
                    //Game.Player.Character.IsVisible = false;

                    //GTA.UI.Screen.ShowSubtitle("Custom Camera ON", 2000);
                }
                else
                {
                    //deactivate the camera
                    World.RenderingCamera = null;
                    myCamera.IsActive= false;

                    //delete the camera
                    myCamera.Delete();
                    myCamera= null;

                    //player is visible
                    Game.Player.Character.IsVisible = true;

                    //GTA.UI.Screen.ShowSubtitle("Custom Camera OFF", 2000);
                }

            }
        }
    }
}
