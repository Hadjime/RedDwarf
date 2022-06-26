using System;
using System.Collections.Generic;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.GameStateMachine.States;
using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Infrastructure.Services.Input;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.SaveLoad;
using InternalAssets.Scripts.Infrastructure.Services.StaticData;
using InternalAssets.Scripts.Infrastructure.Services.StaticDI;
using InternalAssets.Scripts.UI.Services.Factory;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using Zenject;


namespace InternalAssets.Scripts.Infrastructure.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
	{
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services, DiContainer diContainer)
        {
            // _states = new Dictionary<Type, IExitableState>()
            // {
				        //
            //     [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
            //     [typeof(LoadSceneState)] = new LoadSceneState(this, sceneLoader, services.Single<IGameFactory>(), services.Single<IPersistentProgressService>(), services.Single<IStaticDataService>(), services.Single<IUIFactory>()),
            //     [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(),services.Single<ISaveLoadService>() ),
            //     [typeof(GameLoopState)] = new GameLoopState(this),
            // };

			_states = new Dictionary<Type, IExitableState>()
			{

				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
				[typeof(LoadSceneState)] = new LoadSceneState(
					this,
					sceneLoader,
					diContainer.Resolve<IGameFactory>(),
					diContainer.Resolve<IPersistentProgressService>(),
					diContainer.Resolve<IStaticDataService>(),
					diContainer.Resolve<IUIFactory>(),
					diContainer.Resolve<IInputService>()),
				[typeof(LoadProgressState)] = new LoadProgressState(
					this,
					diContainer.Resolve<IPersistentProgressService>(),
					diContainer.Resolve<ISaveLoadService>()),
				[typeof(GameLoopState)] = new GameLoopState(this),
			};
        }
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState  state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
			CustomDebug.Log($"Exit {_activeState?.GetType().Name} state", Color.green);
			_activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
			CustomDebug.Log($"Enter {_activeState?.GetType().Name} state", Color.green);
			
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}