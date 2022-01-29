﻿using InternalAssets.Scripts.Infrastructure;
using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Services.Input;

namespace InternalAssets.Scripts.Infrastructure
{
    public class Game
    {
        public static IInputService InputServices;
        public GameStateMachine StateMachine;

        public Game()
        {
            StateMachine = new GameStateMachine();
        }

        
    }
}