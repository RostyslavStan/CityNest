﻿namespace CityNest
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<PermissionEntity> Permissions { get; set; } = [];
        public ICollection<User> Users { get; set; } = [];
    }
}
