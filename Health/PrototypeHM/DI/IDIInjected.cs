namespace PrototypeHM.DI
{
    /// <summary>
    /// Маркер для объектов в которых присутствует DI-ядро.
    /// </summary>
    public interface IDIInjected
    {
        IDIKernel DIKernel { get; }
    }
}