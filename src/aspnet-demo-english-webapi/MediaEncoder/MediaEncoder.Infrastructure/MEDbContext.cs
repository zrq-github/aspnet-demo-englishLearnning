﻿using MediaEncoder.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ron.Infrastructure.EFCore;

namespace MediaEncoder.Infrastructure
{
    public class MEDbContext : DomainBaseDbContext
    {
        public DbSet<EncodingItem> EncodingItems { get; private set; }

        public MEDbContext(DbContextOptions<MEDbContext> options, IMediator mediator)
            : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.EnableSoftDeletionGlobalFilter();
        }
    }
}
