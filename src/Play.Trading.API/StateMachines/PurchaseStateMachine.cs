using System;
using Automatonymous;
using Play.Trading.API.Contracts;

namespace Play.Trading.API.StateMachines;

public class PurchaseStateMachine : MassTransitStateMachine<PurchaseState>
{
    public State Accepted { get; }
    public State ItemsGranted { get; }
    public State Completed { get; }
    public State Faulted { get; }
    
    public Event<PurchaseRequested> PurchaseRequested { get; }

    public PurchaseStateMachine()
    {
        InstanceState(x => x.CurrentState);
        ConfigureEvents();
        ConfigureInitialState();
    }

    private void ConfigureEvents()
    {
        Event(() => PurchaseRequested);
    }

    private void ConfigureInitialState()
    {
        Initially(
                When(PurchaseRequested)
                    .Then(context =>
                    {
                        context.Instance.UserId = context.Data.UserId;
                        context.Instance.ItemId = context.Data.ItemId;
                        context.Instance.Quantity = context.Data.Quantity;
                        context.Instance.Received = DateTimeOffset.UtcNow;
                        context.Instance.LastUpdated = context.Instance.Received;
                    })
                    .TransitionTo(Accepted)
            );
    }
}