using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkAuth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkAuth.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<HotelDataModel> Hotels { get; set; }
        public DbSet<RoomDataModel> Rooms { get; set; }
    }
}
