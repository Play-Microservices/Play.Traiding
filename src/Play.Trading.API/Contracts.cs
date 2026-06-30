using System;

namespace Play.Trading.API.Contracts;

public record PurchaseRequested(Guid UserId, Guid ItemId, int Quantity, Guid CorrelationId);
