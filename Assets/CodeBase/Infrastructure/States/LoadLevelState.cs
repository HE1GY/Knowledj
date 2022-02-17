using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState:IPayLoadedState<string>
    {
        private const string Initialpoint = "InitialPoint";

        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;


        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName,OnLoad);
        }

        public void Exite()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoad()
        {
            GameObject hero = _gameFactory.CreatHero(GameObject.FindWithTag(Initialpoint));
                
            _gameFactory.CreateHud();
            
            CameraFollow(hero);
          
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject hero) => Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }  
}