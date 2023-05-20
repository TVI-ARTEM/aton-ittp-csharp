namespace Users.Api.Requests;

public record GetByAgeRequest(
    int Age,
    int Take,
    int Skip,
    string Token
);