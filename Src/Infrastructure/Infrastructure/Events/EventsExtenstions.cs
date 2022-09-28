using System.Reflection;
using Domain.Core.Events;
using Infrastructure.MessageBrokers;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Events;

public static class EventsExtenstions
{
    public static IApplicationBuilder UseSubscribeAllEvents(this IApplicationBuilder app, Type baseType)
    {
        var types = baseType.GetTypeInfo().Assembly.GetTypes()
            .Where(mytype => mytype.GetInterfaces().Contains(typeof(IEvent)));

        foreach (var type in types)
        {
            app.UseSubscribeEvent(type);
        }

        return app;
    }
}