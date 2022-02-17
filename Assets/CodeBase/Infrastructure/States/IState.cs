namespace CodeBase.Infrastructure.States
{
    public interface IState:IExitableState
    {
        void Enter();
    }

    public interface IExitableState
    {
        void Exite();
    }

    public interface IPayLoadedState<TPayLoad>:IExitableState
    {
        void Enter(TPayLoad payLoad);
    }
}