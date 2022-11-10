﻿namespace Wizard_Battle_Web_API.DTOs.Authentication
{
    public class StaticRefreshTokenResponse
    {
        public string Token { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;

        public DateTime Expires_At { get; set; } = DateTime.UtcNow;
    }
}