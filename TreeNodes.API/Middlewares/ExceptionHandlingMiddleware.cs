using AutoMapper;
using TreeNodes.API.Models.DTOs;
using TreeNodes.API.Models.Exceptions;
using TreeNodes.Data.Interfaces;
using TreeNodes.Data.Models;

namespace TreeNodes.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IJournalRepository journalRepository,
            IMapper mapper)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, journalRepository, mapper);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, IJournalRepository journalRepository,
            IMapper mapper)
        {
            var journalRecord = new JournalRecord
            {
                Type = exception is SecureException ? "Secure" : "Exception",
                Data = new TreeExceptionData { Message = exception.Message }
            };

            var mapped = mapper.Map<Journal>(journalRecord);

            await journalRepository.Create(mapped);

            journalRecord.Id = mapped.Id;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(journalRecord);
        }
    }
}
