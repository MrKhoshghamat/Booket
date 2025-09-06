using Booket.BuildingBlocks.Application.Data;
using Booket.Modules.UserManagement.Application.Configuration.Commands;
using Dapper;
using Newtonsoft.Json;
using Polly;

namespace Booket.Modules.UserManagement.Infrastructure.Configuration.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
        : ICommandHandler<ProcessInternalCommandsCommand>
    {
        public async Task Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
        {
            var connection = sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                                SELECT 
                                    [Command].[Id] AS [{nameof(InternalCommandDto.Id)}], 
                                    [Command].[Type] AS [{nameof(InternalCommandDto.Type)}], 
                                    [Command].[Data] AS [{nameof(InternalCommandDto.Data)}] 
                                FROM [users].[InternalCommands] AS [Command] 
                                WHERE [Command].[ProcessedDate] IS NULL 
                                ORDER BY [Command].[EnqueueDate]
                                """;
            var commands = await connection.QueryAsync<InternalCommandDto>(sql);

            var internalCommandsList = commands.AsList();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                });

            foreach (var internalCommand in internalCommandsList)
            {
                var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommand(
                    internalCommand));

                if (result.Outcome == OutcomeType.Failure)
                {
                    await connection.ExecuteScalarAsync(
                        """
                        UPDATE [users].[InternalCommands] 
                        SET 
                            ProcessedDate = @NowDate, 
                            Error = @Error 
                        WHERE [Id] = @Id
                        """,
                        new
                        {
                            NowDate = DateTime.UtcNow,
                            Error = result.FinalException.ToString(),
                            internalCommand.Id
                        });
                }
            }
        }

        private async Task ProcessCommand(
            InternalCommandDto internalCommand)
        {
            Type type = Assemblies.Application.GetType(internalCommand.Type);
            dynamic commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type);

            await CommandsExecutor.Execute(commandToProcess);
        }

        private class InternalCommandDto
        {
            public Guid Id { get; set; }

            public string Type { get; set; }

            public string Data { get; set; }
        }
    }
}