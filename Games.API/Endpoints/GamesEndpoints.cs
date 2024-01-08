
using Games.API.Entities;
using Games.API.Repositories;

namespace Games.API.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {

        InMemGamesRepository repositry = new();

        var group = routes.MapGroup("/games").WithParameterValidation();

        group.MapGet("/", () => repositry.GetAll());

        group.MapGet("/{id}", (int id) =>
        {
            Game? game = repositry.Get(id);

            return game is not null ? Results.Ok(game) : Results.NotFound();
        }).WithName(GetGameEndpointName);

        group.MapPost("/", (Game game) =>
        {
            repositry.Create(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        group.MapPut("/{id}", (int id, Game updatedGame) =>
        {
            Game? existingOne = repositry.Get(id);

            if (existingOne is null)
            {
                return Results.NotFound();
            }

            existingOne.Name = updatedGame.Name;
            existingOne.Genre = updatedGame.Genre;
            existingOne.Price = updatedGame.Price;
            existingOne.ReleaseDate = updatedGame.ReleaseDate;
            existingOne.ImageUri = updatedGame.ImageUri;

            repositry.Update(existingOne);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            Game? existingOne = repositry.Get(id);

            if (existingOne is not null)
            {
                repositry.Delete(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}