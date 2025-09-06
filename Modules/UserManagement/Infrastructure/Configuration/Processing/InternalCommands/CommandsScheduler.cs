using Booket.BuildingBlocks.Application.Data;
using Booket.BuildingBlocks.Infrastructure.Serialization;
using Booket.Modules.UserManagement.Application.Configuration.Commands;
using Booket.Modules.UserManagement.Application.Contracts;
using Dapper;
using Newtonsoft.Json;

namespace Booket.Modules.UserManagement.Infrastructure.Configuration.Processing.InternalCommands
{
    public class CommandsScheduler(ISqlConnectionFactory sqlConnectionFactory)
        : ICommandsScheduler
    {
        public async Task EnqueueAsync(ICommand command)
        {
            var connection = sqlConnectionFactory.GetOpenConnection();

            const string sqlInsert = "INSERT INTO [users].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
                                     "(@Id, @EnqueueDate, @Type, @Data)";

            await connection.ExecuteAsync(sqlInsert, new
            {
                command.Id,
                EnqueueDate = DateTime.UtcNow,
                Type = command.GetType().FullName,
                Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                })
            });
        }

        public async Task EnqueueAsync<T>(ICommand<T> command)
        {
            var connection = sqlConnectionFactory.GetOpenConnection();

            const string sqlInsert = "INSERT INTO [users].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
                                     "(@Id, @EnqueueDate, @Type, @Data)";

            await connection.ExecuteAsync(sqlInsert, new
            {
                command.Id,
                EnqueueDate = DateTime.UtcNow,
                Type = command.GetType().FullName,
                Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                })
            });
        }
    }
}