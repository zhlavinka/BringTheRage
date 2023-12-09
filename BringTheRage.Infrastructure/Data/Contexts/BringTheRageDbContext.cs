using BringTheRage.Domain.Data.Contexts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BringTheRage.Infrastructure.Data.Contexts;

public class BringTheRageDbContext : IdentityDbContext, IBringTheRageDbContext {
  public BringTheRageDbContext(DbContextOptions options) : base(options) {

  }
}