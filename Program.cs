using System;
using Raylib_cs;
using System.Numerics;

class Program
{
    // --- Constants ---
    const int ScreenWidth = 1280;
    const int ScreenHeight = 720;

    const float TickRate = 60f;
    const float TickDt = 1f / TickRate;

    const float Gravity = 20f;
    const float JumpForce = 6.5f;

    const float GroundAccel = 50f;
    const float AirAccel = 15f;
    const float MaxGroundSpeed = 7.0f;
    const float MaxAirSpeed = 7.0f;
    const float GroundFriction = 8.0f;

    static void Main()
    {
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "Frontline Movement Sandbox");
        Raylib.SetTargetFPS(0); // uncapped render FPS

        Vector3 position = new Vector3(0, 1.8f, 0);
        Vector3 velocity = Vector3.Zero;

        float yaw = 0f;
        float pitch = 0f;
        bool grounded = false;
        bool jumpQueued = false;
        const float maxPitch = MathF.PI / 2f - 0.01f;

        float accumulator = 0f;
        double lastTime = Raylib.GetTime();

        Raylib.DisableCursor();

        while (!Raylib.WindowShouldClose())
        {
            double now = Raylib.GetTime();
            float frameTime = (float)(now - lastTime);
            lastTime = now;

            accumulator += frameTime;

            // --- Input (sampled every frame) ---
            Vector2 mouse = Raylib.GetMouseDelta();
            float sensitivity = 0.002f;
            yaw   -= mouse.X * sensitivity;
            pitch += mouse.Y * sensitivity;

            // Clamp pitch
            pitch = Math.Clamp(pitch, -maxPitch, maxPitch);

            Vector3 wishDir = Vector3.Zero;
            if (Raylib.IsKeyDown(KeyboardKey.W)) wishDir.Z += 1;
            if (Raylib.IsKeyDown(KeyboardKey.S)) wishDir.Z -= 1;
            if (Raylib.IsKeyDown(KeyboardKey.A)) wishDir.X += 1;
            if (Raylib.IsKeyDown(KeyboardKey.D)) wishDir.X -= 1;

            if (wishDir != Vector3.Zero)
                wishDir = Vector3.Normalize(wishDir);

            // Rotate input by yaw
            Matrix4x4 rot = Matrix4x4.CreateRotationY(yaw);
            wishDir = Vector3.TransformNormal(wishDir, rot);

            if (Raylib.IsKeyPressed(KeyboardKey.Space))
                jumpQueued = true;

            // --- Fixed Tick ---
            while (accumulator >= TickDt)
            {
                grounded = position.Y <= 1.8f;

                if (grounded)
                {
                    position.Y = 1.8f;
                    velocity.Y = 0;

                    // Friction
                    Vector2 horizontalVel = new Vector2(velocity.X, velocity.Z);
                    float speed = horizontalVel.Length();
                    if (speed > 0)
                    {
                        float drop = speed * GroundFriction * TickDt;
                        float newSpeed = MathF.Max(speed - drop, 0);
                        horizontalVel *= newSpeed / speed;
                        velocity.X = horizontalVel.X;
                        velocity.Z = horizontalVel.Y;
                    }

                    Accelerate(ref velocity, wishDir, MaxGroundSpeed, GroundAccel);

                    if (jumpQueued)
                    {
                        velocity.Y = JumpForce;
                        grounded = false;
                        jumpQueued = false;
                    }

                    if (!grounded && velocity.Y < 0)
                    {
                        jumpQueued = false;
                    }
                }
                else
                {
                    Accelerate(ref velocity, wishDir, MaxAirSpeed, AirAccel);
                    velocity.Y -= Gravity * TickDt;
                }

                position += velocity * TickDt;
                accumulator -= TickDt;
            }

            // --- Camera ---
            Vector3 camPos = position + new Vector3(0, 0.2f, 0);

            Matrix4x4 viewRot = Matrix4x4.CreateRotationX(pitch) * Matrix4x4.CreateRotationY(yaw);

            Vector3 forward = Vector3.TransformNormal(Vector3.UnitZ, viewRot);

            Camera3D cam = new Camera3D(
                camPos,
                camPos + forward,
                Vector3.UnitY,
                90,
                CameraProjection.Perspective
            );

            // --- Render ---
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SkyBlue);

            Raylib.BeginMode3D(cam);

            // Ground
            Raylib.DrawGrid(100, 1.0f);

            // Player debug cube
            Raylib.DrawCube(position, 0.5f, 1.8f, 0.5f, Color.Red);

            Raylib.EndMode3D();

            float horizSpeed = new Vector2(velocity.X, velocity.Z).Length();

            Raylib.DrawText($"Speed: {horizSpeed:F2}", 10, 10, 20, Color.Black);
            Raylib.DrawText($"Grounded: {grounded}", 10, 35, 20, Color.Black);
            Raylib.DrawText($"FPS: {Raylib.GetFPS()}", 10, 60, 20, Color.Black);
            Raylib.DrawText("Physics: 60u/s", 10, 85, 20, Color.Black);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    static void Accelerate(ref Vector3 velocity, Vector3 wishDir, float wishSpeed, float accel)
    {
        if (wishDir == Vector3.Zero)
            return;

        float currentSpeed = Vector3.Dot(velocity, wishDir);
        float addSpeed = wishSpeed - currentSpeed;

        if (addSpeed <= 0)
            return;

        float accelSpeed = accel * wishSpeed * TickDt;
        if (accelSpeed > addSpeed)
            accelSpeed = addSpeed;

        velocity += wishDir * accelSpeed;
    }
}
