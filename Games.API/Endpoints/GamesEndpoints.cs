
using Games.API.Dtos;
using Games.API.Entities;
using Games.API.Repositories;

namespace Games.API.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {

        var group = routes.MapGroup("/games").WithParameterValidation();

        group.MapGet("/", async (IGamesRepository repository) => (await repository.GetAllAsync()).Select(game => game.AsDto()));

        group.MapGet("/{id}", async (IGamesRepository repository, int id) =>
        {
            Game? game = await repository.GetAsync(id);

            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();
        }).WithName(GetGameEndpointName);

        group.MapPost("/", async (IGamesRepository repository, CreateGameDto gameDto) =>
        {
            Game game = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };

            await repository.CreateAsync(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        group.MapPut("/{id}", async (IGamesRepository repository, int id, UpdateGameDto updatedGameDto) =>
        {
            Game? existingOne = await repository.GetAsync(id);

            if (existingOne is null)
            {
                return Results.NotFound();
            }

            existingOne.Name = updatedGameDto.Name;
            existingOne.Genre = updatedGameDto.Genre;
            existingOne.Price = updatedGameDto.Price;
            existingOne.ReleaseDate = updatedGameDto.ReleaseDate;
            existingOne.ImageUri = updatedGameDto.ImageUri;

            await repository.UpdateAsync(existingOne);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (IGamesRepository repository, int id) =>
        {
            Game? existingOne = await repository.GetAsync(id);

            if (existingOne is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        });

        return group;
    }
}