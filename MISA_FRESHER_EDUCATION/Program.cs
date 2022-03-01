using Microsoft.Extensions.FileProviders;
using Misa.Fresher.Education.Core.Interfaces.Repository;
using Misa.Fresher.Education.Core.Interfaces.Service;
using Misa.Fresher.Education.Core.Services;
using Misa.Fresher.Education.Core.Setting;
using Misa.Fresher.Education.Infrastructure.Repository;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<EducationDatabaseSettings>(builder.Configuration.GetSection("EducationDatabase"));
builder.Services.AddSingleton(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddSingleton<ITopicRepository, TopicRepository>();
builder.Services.AddSingleton<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddSingleton<IExerciseRepository, ExerciseRepository>();
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

builder.Services.AddSingleton<EducationDatabaseSettings>(serviceProvider =>
{
    return builder.Configuration.GetSection("EducationDatabase").Get<EducationDatabaseSettings>();
});

builder.Services.AddSingleton<HostDirection>(serviceProvider =>
{
    return new HostDirection
    {
        SrcHost = Environment.CurrentDirectory
    };
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Environment.CurrentDirectory, "Image")),
    RequestPath = "/Image"
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
