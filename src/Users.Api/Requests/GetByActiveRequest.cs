namespace Users.Api.Requests;

public record GetByActiveRequest(
    int Take,
    int Skip,
    string Token
);