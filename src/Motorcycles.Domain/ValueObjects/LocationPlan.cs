namespace Motorcycles.Domain.ValueObjects;

public record LocationPlan(
    int Days,
    decimal DailyValue,
    decimal PercentualFee
);
