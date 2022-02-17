using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly LoadingCurtain _loadingCurtain;
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {
            _loadingCurtain = loadingCurtain;
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)]=new BootstrapState(this,sceneLoader, services),
                [typeof(LoadLevelState)]=new LoadLevelState(this,sceneLoader,_loadingCurtain,services.Single<IGameFactory>()),
                [typeof(GameLoopState)]=new GameLoopState(this)
            };
        }

        public void Enter<TState>() where TState:class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }
        
        public void Enter<TState,TPayLoad>(TPayLoad payLoad) where TState: class, IPayLoadedState<TPayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(payLoad);
        }
        
        private TState GetState<TState>() where TState :class, IExitableState => _states[typeof(TState)] as TState ;// преведення бо інтрефейс а не обєкт класа
        
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exite();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }
    }
}