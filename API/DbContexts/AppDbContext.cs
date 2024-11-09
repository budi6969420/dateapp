// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;

namespace DateAppApi.DbContexts
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
    }
}