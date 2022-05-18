namespace SmartFamily.Backend.Utils;

internal interface IInitializableSource<TInitializeWith>
{
    internal void Initialize(TInitializeWith param);
}