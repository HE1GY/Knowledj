using UnityEngine;

namespace CodeBase.Infrastructure.AssetManager
{
    public class AssetProvider : IAsset
    {
        public GameObject Instansiate(string path,Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return  Object.Instantiate(prefab,at,Quaternion.identity);
        }

        public  GameObject Instansiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return  Object.Instantiate(prefab);
        }
    }
}