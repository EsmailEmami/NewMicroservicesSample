using RawRabbit.Configuration;

namespace Application.MessageBrokers.RabbitMq;

public class RabbitMqOptions : RawRabbitConfiguration
{
    public QueueOptions Queue { get; set; }
    public ExchangeOptions Exchange { get; set; }
}

public class QueueOptions : GeneralQueueConfiguration
{
    public string? Name { get; set; }
}

public class ExchangeOptions : GeneralExchangeConfiguration
{
    public string? Name { get; set; }
}

