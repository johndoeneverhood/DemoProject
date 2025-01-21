using DemoProject.Models.Common;

namespace DemoProject.Models.IRepo;

public interface IHomeRepo
{
    Task<List<GroupBrandItem>> GetOriginalGroups(string detailNum)
    { return null!; }
}
