// <copyright file="EditorCamera.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes.Cameras;

using System;
using System.Drawing;
using System.Numerics;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Maths;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Geometry;

public sealed class EditorCamera : ICamera
{
    private bool isLocked;

    private float speed;

    public EditorCamera()
    {
        this.ViewportWidth = 1280;
        this.ViewportHeight = 720;

        this.speed = 0.5f;
        this.isLocked = false;

        this.Transform = new TransformComponent()
        {
            Position = new Vector3(0, 50, 0),
            Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(45.0f)),
        };
    }

    public Rectangle Bounds
    {
        get { return new Rectangle(0, 0, this.ViewportWidth, this.ViewportHeight); }
    }

    public Matrix4x4 Projection
    {
        get { return Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(70.0f), this.ViewportWidth / this.ViewportHeight, 0.1f, 1000.0f); }
    }

    public TransformComponent Transform { get; }

    public Matrix4x4 View
    {
        get { return this.Transform.CreateViewMatrix(Vector3.UnitY); }
    }

    public int ViewportHeight { get; set; }

    public int ViewportWidth { get; set; }

    public void Update(IKeyboard keyboard, IMouse mouse)
    {
        ArgumentNullException.ThrowIfNull(keyboard, nameof(keyboard));
        ArgumentNullException.ThrowIfNull(mouse, nameof(mouse));

        float moveAmount = this.speed;

        if (keyboard.IsKeyDown(Key.W))
        {
            this.Transform.Translate(this.Transform.Forward, moveAmount);
        }

        if (keyboard.IsKeyDown(Key.S))
        {
            this.Transform.Translate(this.Transform.Forward, -moveAmount);
        }

        if (keyboard.IsKeyDown(Key.A))
        {
            this.Transform.Translate(this.Transform.Left, -moveAmount);
        }

        if (keyboard.IsKeyDown(Key.D))
        {
            this.Transform.Translate(this.Transform.Left, moveAmount);
        }

        if (keyboard.IsKeyDown(Key.Z))
        {
            this.Transform.Translate(this.Transform.Up, moveAmount);
        }

        if (keyboard.IsKeyDown(Key.X))
        {
            this.Transform.Translate(this.Transform.Down, moveAmount);
        }

        if (keyboard.IsKeyReleased(Key.Escape))
        {
            this.isLocked = false;
        }

        var viewport = new Rectangle(0, 0, this.ViewportWidth, this.ViewportHeight);
        var centerPosition = new Vector2(viewport.Width / 2, viewport.Height / 2);

        if (mouse.IsButtonReleased(MouseButton.Right))
        {
            mouse.Location = new PointF(centerPosition.X, centerPosition.Y);

            this.isLocked = true;
        }

        if (this.isLocked)
        {
            var deltaPosition = new Vector2(mouse.Location.X - centerPosition.X, mouse.Location.Y - centerPosition.Y);

            bool canRotateX = deltaPosition.X != 0;
            bool canRotateY = deltaPosition.Y != 0;

            if (canRotateX)
            {
                this.Transform.Rotate(this.Transform.Left, -MathHelper.DegreesToRadians(deltaPosition.Y * this.speed));
            }

            if (canRotateY)
            {
                this.Transform.Rotate(Vector3.UnitY, -MathHelper.DegreesToRadians(deltaPosition.X * this.speed));
            }

            if (canRotateX || canRotateY)
            {
                mouse.Location = new PointF(
                    centerPosition.X,
                    centerPosition.Y);
            }
        }
    }
}
