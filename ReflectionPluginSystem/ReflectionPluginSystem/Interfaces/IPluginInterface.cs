using System;

namespace ReflectionPluginSystem.Interfaces
{
    public interface IPluginInterface
    {
        string Name { get; }

        string Description { get; }

        Version Version { get; }

        string GetMessage();
    }
}
