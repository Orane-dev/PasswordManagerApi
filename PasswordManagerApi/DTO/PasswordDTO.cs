﻿using PasswordManagerApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace PasswordManagerApi.DTO
{
    public class PasswordDTO
    {
        public int Id { get; set; }
        [Required]
        public string TelegramUserId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string DecryptedPassword { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
