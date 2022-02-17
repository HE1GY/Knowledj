using CodeBase.Infrastructure.AssetManager;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState:IState
    {
        private const string Initial="Initial";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        private AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine,SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services=services;
            
            RegistarServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial,EnterLoadLevel); 
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadLevelState,string>("Main");
        }

        private void RegistarServices()
        {
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IAsset>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAsset>()));
        }

        public void Exite()
        { 
            
        }
        
        private static IInputService  InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }

    }
}