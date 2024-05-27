using LicaFlix.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LicaFlix.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        #region Configuração do Muitos para Muitos do MovieGenre
        builder.Entity<MovieGenre>()
            .HasKey(mg => new { mg.MovieId, mg.GenreId});
    
        builder.Entity<MovieGenre>()
            .HasOne(mg => mg.Movie)
            .WithMany(m => m.Genres)
            .HasForeignKey(mg => mg.MovieId);

        builder.Entity<MovieGenre>()
            .HasOne(mg => mg.Genre)
            .WithMany(g => g.Movies)
            .HasForeignKey (mg => mg.GenreId);
        #endregion
    
        #region Popular os dados de Usuário
        // Perfil - IdentityRole
        List<IdentityRole> roles = new()
        {
            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Moderador",
                NormalizedName = "MODERADOR"
            },
            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Usuário",
                NormalizedName = "USUÁRIO"
            }
        };
            builder.Entity<IdentityRole>().HasData(roles);

            // Conta de Usuário - IdentityUser
            List<IdentityUser> users = new()
            {
                new IdentityUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "admin@licaflix.com",
                    NormalizedEmail= "ADMIN@LICAFLIX.COM",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    EmailConfirmed = true,
                    LockoutEnabled = false
                },
                new IdentityUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user@gmail.com",
                    NormalizedEmail= "USER@GMAIL.COM",
                    UserName = "User",
                    NormalizedUserName = "USER",
                    EmailConfirmed = true,
                    LockoutEnabled = true
                }
            };
            foreach (var user in users)
            {
                PasswordHasher<IdentityUser> pass = new();
                pass.HashPassword(user, "@Etec123");
            }
            builder.Entity<IdentityUser>().HasData(users);
            
            // Dados pessoais do Usuário - AppUser
            List<AppUser> appUsers = new()
            {
                new AppUser()
                {
                    AppUserId = users[0].Id,
                    Name = "Carlos Eduardo Passarelli da Silva",
                    Birthday = DateTime.Parse("23/08/2007"),
                    Photo = "/img/users/avatar.png"
                },
                new AppUser()
                {
                    AppUserId = users[1].Id,
                    Name = "Felipe Daltoé",
                    Birthday = DateTime.Parse("31/01/2008")
                }
            };
            builder.Entity<AppUser>().HasData(AppUsers);

            // Perfis dos Usuários - IdentityUserRole
            List<IdentityUserRole<string>> userRoles = new()
            {
                new IdentityUserRole<string>()
                {
                    UserId = users[0].Id,
                    RoleId = roles[0].Id
                },
                new IdentityUserRole<string>()
                {
                    UserId = users[0].Id,
                    RoleId = roles[1].Id
                },
                new IdentityUserRole<string>()
                {
                    UserId = users[0].Id,
                    RoleId = roles[2].Id
                },
                new IdentityUserRole<string>()
                {
                    UserId = users[1].Id,
                    RoleId = roles[2].Id
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            #endregion
    }   
}