using Automatonymous;
using MassTransit;
using Saga.Commands;
using System;

namespace Saga.Orchestrator
{
    public class StateMachine : MassTransitStateMachine<OrderState>
    {
        public State Ordered { get; set; }
        public State Burger { get; set; }
        public State Fries { get; set; }
        public State Drink { get; set; }
        public State Delivery { get; set; }

        public Event<OrderMade> OrderMade { get; private set; }
        public Event<BurgerMade> BurgerMade { get; private set; }
        public Event<DrinkMade> DrinkMade { get; private set; }
        public Event<FriesMade> FriesMade { get; private set; }
        public StateMachine()
        {
            Event(() => OrderMade, x => x.CorrelateById(c => c.Message.CorrelationId));
            Event(() => BurgerMade, x => x.CorrelateById(c => c.Message.CorrelationId));
            Event(() => DrinkMade, x => x.CorrelateById(c => c.Message.CorrelationId));
            Event(() => FriesMade, x => x.CorrelateById(c => c.Message.CorrelationId));

            InstanceState(x => x.CurrentState);


            Initially(When(OrderMade)
                .Then(c =>
                {
                    Console.WriteLine($"Order {c.Data.CorrelationId} received");
                    c.Instance.Burger = c.Data.Burger;
                    c.Instance.Drink = c.Data.Drink;
                    c.Instance.Fries = c.Data.Fries;
                })
                .Then(async c =>
                {
                    var command = new BurgerToMake
                    {
                        Cheese = c.Instance.Burger.Cheese,
                        CheeseQuantity = c.Instance.Burger.CheeseQuantity,
                        CorrelationId = c.Instance.CorrelationId,
                        MeatQuantity = c.Instance.Burger.MeatQuantity
                    };
                    await c.Publish(command);
                }).TransitionTo(Burger));

            During(Burger,
                When(BurgerMade)
                .Then(c =>
                {
                    Console.WriteLine($"Burger {c.Data.BurgerId} from order {c.Data.CorrelationId} received");
                    c.Instance.Burger.Id = c.Data.BurgerId;
                })
                .Then(c =>
                {
                    var command = new FriesToMake
                    {
                        Type = c.Instance.Fries.Type,
                        CorrelationId = c.Instance.CorrelationId
                    };

                    c.Publish(command);
                })
                .TransitionTo(Fries));

            During(Fries,
                When(FriesMade)
                .Then(c =>
                {
                    Console.WriteLine($"Fries {c.Data.FriesId} from order {c.Data.CorrelationId} received");
                    c.Instance.Fries.Id = c.Data.FriesId;
                })
                .Then(c =>
                {
                    var command = new DrinkToMake
                    {
                        CorrelationId = c.Instance.CorrelationId,
                        Flavor = c.Instance.Drink.Flavor,
                        Size = c.Instance.Drink.Size,
                        Type = c.Instance.Drink.Type
                    };
                    c.Publish(command);
                })
                .TransitionTo(Drink));

            During(Drink,
                When(DrinkMade)
                .Then(c =>
                {
                    Console.WriteLine($"Drink {c.Data.DrinkId} from order {c.Data.CorrelationId} received");
                    c.Instance.Drink.Id = c.Data.DrinkId;
                })
                .Then(c =>
                {
                    Console.WriteLine($"Order {c.Instance.CorrelationId} finalized");
                }).TransitionTo(Delivery)
                .Finalize()); ;

            SetCompletedWhenFinalized();
        }
    }
}
