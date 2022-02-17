using CodeBase.Infrastructure.AssetManager;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset m_Asset;

        public GameFactory(IAsset asset)
        {
            m_Asset = asset;
        }

        public  void CreateHud() => m_Asset.Instansiate(AssetPath.HudPath);

        public GameObject CreatHero(GameObject initialPoint) => m_Asset.Instansiate(AssetPath.HeroPath,at:initialPoint.transform.position);
    }
}