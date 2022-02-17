using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManager
{
    public interface IAsset:IService

    {
    GameObject Instansiate(string path, Vector3 at);
    GameObject Instansiate(string path);
    }
}