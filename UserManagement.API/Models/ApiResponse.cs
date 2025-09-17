﻿namespace UserManagement.API.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public ApiResponse(bool success, string message, T? data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> Ok(T data, string message = "Operação realizada com sucesso")
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> Fail(string message, List<string> errors)
        {
            return new ApiResponse<T>(false, message)
            {
                Errors = errors
            };
        }
    }
}
