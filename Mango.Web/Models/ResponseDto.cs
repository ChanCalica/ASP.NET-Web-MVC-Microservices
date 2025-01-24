﻿namespace Mango.Web.Models;

public class ResponseDto
{
    public object? Results { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; } = string.Empty;
}