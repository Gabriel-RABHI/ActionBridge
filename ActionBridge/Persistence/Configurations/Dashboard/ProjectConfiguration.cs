using ActionBridge.Values;

namespace ActionBridge.Persistence.Configurations.Manager
{
    /// <summary>
    /// Settings of a code repository, located at the root path od the project.
    /// </summary>
    public class DashboardConfiguration
    {
        public List<ProjectRecord> Projects { get; set; } = new List<ProjectRecord>();
    }

    public class ProjectRecord
    {
        public DirectoryPath RootDirectoryPath { get; set; } = new DirectoryPath();
    }
}
