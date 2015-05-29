﻿using Mabv.Breakout.Behaviors;
using Mabv.Breakout.Collisions;
using Mabv.Breakout.Physics;
using Mabv.Breakout.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mabv.Breakout.GameEntities
{
    public class DonkeyKong : IGameEntity
    {
        public ITransform Transform { get { return transform; } }
        private ISprite sprite;
        private IPhysics physics;
        private ICollider collider;
        private ITransform transform;
        private IBehavior behavior;

        public DonkeyKong(BreakoutGame game, Vector2 location)
        {
            this.transform = new Transform(location);
            //this.physics = new DonkeyKongPhysics(this.transform, new Vector2(0, 0));
            this.physics = new RigidBodyPhysics(this.transform);
            this.physics.Velocity = new Vector2(5, -5);
            //this.physics = new DonkeyKongPhysics(this.transform, new Vector2(0, -2));
            this.sprite = new AnimatedSprite(Textures.RotatingDonkeyKong, 1, 16, 2);
            this.behavior = new DonkeyKongBehavior(this);
            this.collider = new BoxCollider(this.sprite.Width, this.sprite.Height, this.physics, this.behavior, this);
            game.CollisionController.AddCollider(this.collider);
        }

        public void Update()
        {
            physics.Update();
            sprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, transform.Location);
        }

        // TODO Not sure if this would be better fit in a bevior, or not
        public void Bounce(bool isHorizontalBounce, bool isVerticalBounce)
        {
            Vector2 newVelocity = physics.Velocity;
            if (isHorizontalBounce)
            {
                newVelocity.X *= -1;
            }
            
            if (isVerticalBounce)
            {
                newVelocity.Y *= -1;
            }

            physics.Velocity = newVelocity;
        }
        public void PaddleBounce(float horizontalScalar, float verticalScalar)
        {
            Vector2 newVelocity = physics.Velocity;
            if (horizontalScalar != 0)
            {
                newVelocity.X *= horizontalScalar;
            }
            
            if (verticalScalar != 0)
            {
                newVelocity.Y *= verticalScalar;
            }

            physics.Velocity = newVelocity;
        }

        public void PaddleBounce(int direction)
        {
            Console.WriteLine("DonkeyKong.PaddleBounce direction = " + direction);
            float velocityX = 0;

            if (physics.Velocity.X < 0 && direction < 0)
            {
                velocityX = physics.Velocity.X * 1.2f;
            }
            else if (physics.Velocity.X > 0 && direction > 0)
            {
                velocityX = physics.Velocity.X * 1.2f;
            }
            else
            {
                velocityX = physics.Velocity.X;
            }

            if (velocityX < -10)
            {
                velocityX = -10;
            }
            if (velocityX > 10)
            {
                velocityX = 10;
            }

            physics.Velocity = new Vector2(velocityX, -1 * physics.Velocity.Y);
        }
    }
}
