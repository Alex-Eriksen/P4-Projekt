﻿global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using Microsoft.EntityFrameworkCore;
global using Wizard_Battle_Web_API.Database.Entities;
global using System.Security.Cryptography;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using Wizard_Battle_Web_API.Database;
global using Wizard_Battle_Web_API.Helpers;
global using System.Text.Json.Serialization;
global using Wizard_Battle_Web_API.DTOs.Authentication;
global using Wizard_Battle_Web_API.DTOs.Player;
global using Wizard_Battle_Web_API.DTOs.Account;
global using Wizard_Battle_Web_API.DTOs.Message;
global using Wizard_Battle_Web_API.DTOs.Friendship;
global using Wizard_Battle_Web_API.Services;
global using Microsoft.Extensions.Options;
global using Wizard_Battle_Web_API.Repositories;
global using BC = BCrypt.Net.BCrypt;
global using AutoMapper;
global using Microsoft.OpenApi.Models;
global using Swashbuckle.AspNetCore.Filters;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;

