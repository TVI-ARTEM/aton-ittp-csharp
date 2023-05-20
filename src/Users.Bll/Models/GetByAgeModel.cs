namespace Users.Bll.Models;

public record GetByAgeModel(
    int Age,
    int Take,
    int Skip
);