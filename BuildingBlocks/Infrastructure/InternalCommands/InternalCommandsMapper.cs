namespace Booket.BuildingBlocks.Infrastructure.InternalCommands
{
    public class InternalCommandsMapper(BiDictionary<string, Type> internalCommandsMap) : IInternalCommandsMapper
    {
        public string GetName(Type type)
        {
            return internalCommandsMap.TryGetBySecond(type, out var name) ? name : null;
        }

        public Type GetType(string name)
        {
            return internalCommandsMap.TryGetByFirst(name, out var type) ? type : null;
        }
    }
}