using CodeBase.Data;

namespace CodeBase.Hero
{
    public interface ISavedProgress:ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }

    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}