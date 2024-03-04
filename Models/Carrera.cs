using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Drivers.Api.Models;

public class Carrera
{

[BsonId]//Obtener el ID auto generado por MongoDB
[BsonRepresentation(BsonType.ObjectId)]

 public string Id {get; set;} = string.Empty;
 [BsonElement("Name")]
 public string Name{get; set;} = string.Empty;




}