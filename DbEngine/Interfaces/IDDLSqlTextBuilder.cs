using DBEngineProject.Entities;

namespace DBEngineProject.Interfaces
{
    public interface IDDLSqlTextBuilder
    {
        string GetSqlTextBySchema(EntityRowSchema schema);
    }
}