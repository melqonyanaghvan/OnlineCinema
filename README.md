# OnlineCinema API

ASP.NET Core Web API for managing movies, users, and reviews.

## How to Run

1. Clone the repository:
  git clone https://github.com/melqonyanaghvan/OnlineCinema.git
  cd OnlineCinema

2. Apply migrations:
  dotnet ef database update -p OnlineCinema.Infrastructure -s OnlineCinema.API

3. Run the project:
  cd OnlineCinema.API
  dotnet run

4. Open browser:
  http://localhost:5273/swagger

## Project Structure
- **Domain** - entities (Movie, Actor, User, UserProfile, Review)
- **Infrastructure** - repositories and database
- **API** - controllers and endpoints

## Database Relationships
- User ↔ UserProfile (1:1)
- User → Reviews (1:N)
- Movie → Reviews (1:N)
- Movie ↔ Actor (N:M)
