﻿namespace Relação1N.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //relação
        public List<User> Users { get; set; } = new List<User>();
    }
}