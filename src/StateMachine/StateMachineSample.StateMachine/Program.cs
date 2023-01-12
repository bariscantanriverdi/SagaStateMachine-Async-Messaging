using MassTransit;
using StateMachineSample.StateMachine;
using StateMachineSample.StateMachine.Settings;


Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        var configurationBuilder = new ConfigurationBuilder();
        var configuration = configurationBuilder.AddEnvironmentVariables().AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json")
            .Build();
        
        builder.Sources.Clear();
        builder.AddConfiguration(configuration);
    })
    .ConfigureServices((hostContext, services) =>
    {
        var messageBrokerQueueSettings = hostContext.Configuration.GetSection("MessageBroker:QueueSettings").Get<MessageBrokerQueueSettings>(); 
        var messageBrokerPersistenceSettings = hostContext.Configuration.GetSection("MessageBroker:StateMachinePersistence").Get<MessageBrokerPersistenceSettings>(); 

        services.AddMassTransit(x =>
        {
            x.AddSagaStateMachine<OrderStateMachine, OrderState>().MongoDbRepository(r =>
            {
                r.Connection = messageBrokerPersistenceSettings.Connection;
                r.DatabaseName = messageBrokerPersistenceSettings.DatabaseName;
                r.CollectionName = messageBrokerPersistenceSettings.CollectionName;
            });
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(messageBrokerQueueSettings.HostName, messageBrokerQueueSettings.VirtualHost, h =>
                {
                    h.Username(messageBrokerQueueSettings.UserName);
                    h.Password(messageBrokerQueueSettings.Password);
                });
                
                cfg.ConfigureEndpoints(context);
            });
        });
    }).Build().Run();