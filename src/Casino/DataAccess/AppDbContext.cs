using Casino.Abstractions;
using Casino.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Casino.DataAccess
{
    /// <inheritdoc />
    public class AppDbContext : DbContext, IAppDbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public DbSet<DeckCard> DeckCards => Set<DeckCard>();

        /// <inheritdoc />
        public DbSet<Deck> Decks => Set<Deck>();

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSnakeCaseNamingConvention();
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SetupEntity(modelBuilder.Entity<DeckCard>());
            SetupEntity(modelBuilder.Entity<Deck>());
        }

        private void SetupEntity(EntityTypeBuilder<DeckCard> builder)
        {
            builder.HasKey(p => new { p.Suit, p.Value, p.DeckId });
            builder.HasIndex(p => new { p.DeckId });
        }

        private void SetupEntity(EntityTypeBuilder<Deck> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasMany(p => p.Cards).WithOne(p => p.Deck);
        }
    }
}
