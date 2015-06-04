﻿using Mabv.Breakout.Collisions;
using Mabv.Breakout.Commands;
using Mabv.Breakout.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mabv.Breakout.Levels
{
    public class LevelOne : ILevel
    {
        private BreakoutGame game;
        private View view;
        private Player player;
        private EntityController entityController;
        private CollisionController collisionController;
        private KeyboardController keyboardController;
        private Paddle paddle;

        public LevelOne(BreakoutGame game, View view, Player player, EntityController entityController, CollisionController collisionController, KeyboardController keyboardController)
        {
            this.game = game;
            this.view = view;
            this.player = player;
            this.entityController = entityController;
            this.collisionController = collisionController;
            this.keyboardController = keyboardController;
        }

        public void Start()
        {
            populate();
            registerControls();

            MediaPlayer.Play(Songs.IslandSwing);
            MediaPlayer.IsRepeating = true;
        }

        private void populate()
        {
            entityController.AddEntity(new JungleBackground());

            for (int i = 105 + 16; i < view.Width - 105 - 32; i += 64)
            {
                for (int j = 32; j < view.Height / 2 - 64; j += 64)
                {
                    entityController.AddEntity(new BananaBunch(new Vector2(i + 8, j + 8), player, entityController, collisionController));

                    entityController.AddEntity(new Barrel(new Vector2(i, j), entityController, collisionController));
                }
            }

            paddle = new Paddle(new Vector2(view.Width / 2, view.Height - 64), collisionController);
            entityController.AddEntity(paddle);
            entityController.AddEntity(new DonkeyKong(new Vector2(view.Width / 2, view.Height / 2), collisionController));
            entityController.AddEntity(new LevelBoundary(collisionController));

            Hud hud = new Hud();
            entityController.AddEntity(hud);
            player.Hud = hud;
        }

        private void registerControls()
        {
            ICommand moveLeftCommand = new MoveLeftCommand(paddle);
            ICommand moveRightCommand = new MoveRightCommand(paddle);
            ICommand stopMovingCommand = new StopMovingCommand(paddle);
            ICommand moveUpCommand = new MoveUpCommand(paddle);
            ICommand moveDownCommand = new MoveDownCommand(paddle);
            keyboardController.RegisterCommandKeyDown(Keys.Left, moveLeftCommand);
            keyboardController.RegisterCommandKeyDown(Keys.A, moveLeftCommand);
            keyboardController.RegisterCommandKeyDown(Keys.Right, moveRightCommand);
            keyboardController.RegisterCommandKeyDown(Keys.D, moveRightCommand);
            keyboardController.RegisterCommandKeyUp(Keys.Left, stopMovingCommand);
            keyboardController.RegisterCommandKeyUp(Keys.A, stopMovingCommand);
            keyboardController.RegisterCommandKeyUp(Keys.Right, stopMovingCommand);
            keyboardController.RegisterCommandKeyUp(Keys.D, stopMovingCommand);
            // DEBUG
            //keyboardInputController.RegisterCommandKeyDown(Keys.Space, stopMovingCommand);
            //keyboardInputController.RegisterCommandKeyDown(Keys.S, stopMovingCommand);
            //keyboardInputController.RegisterCommandKeyDown(Keys.Down, moveDownCommand);
            //keyboardInputController.RegisterCommandKeyDown(Keys.S, moveDownCommand);
            //keyboardInputController.RegisterCommandKeyDown(Keys.Up, moveUpCommand);
            //keyboardInputController.RegisterCommandKeyDown(Keys.W, moveUpCommand);

            ICommand quitGameCommand = new QuitGameCommand(game);
            keyboardController.RegisterCommandKeyDown(Keys.Q, quitGameCommand);
        }
    }
}
