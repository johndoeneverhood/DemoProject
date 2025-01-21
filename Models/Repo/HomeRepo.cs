using DemoProject.Models.Common;
using DemoProject.Models.IRepo;
using EmexSrv;
using System.Linq;
using System.Security.Authentication;
namespace DemoProject.Models.Repo;

public class HomeRepo : IHomeRepo
{
    const long login = 0;
    const string password = "xxxxxx";

    public async Task<List<GroupBrandItem>> GetOriginalGroups(string detailNum)
    {
        EmExServiceSoapClient _client = new EmExServiceSoapClient(EmExServiceSoapClient.EndpointConfiguration.EmExServiceSoap);

        FindDetailAdv5Response response = await _client.FindDetailAdv5Async(
                       login,
                       password,
                       null,
                       detailNum,
                       SearchSubstLevel.All,
                       SearchSubstFilter.None,
                       FindDeliveryRegionType.PRI,
                       null,
                       null,
                       null,
                       null,
                       null,
                       null);

        List<GroupBrandItem> GroupBrandItems;
        
        try
        {
                                GroupBrandItems =
                                response.FindDetailAdv5Result.Details
                                .Where(x => x.PriceGroup == PriceGroup.Original)
                                .Select(x =>
                                    new GroupBrandItem
                                    {
                                        MakeName = x.MakeName,
                                        MakeNameGroup = x.MakeName.ToLower(),
                                        NameRus = x.DetailNameRus.ToLower(),
                                        DetailNum = x.DetailNum,
                                        MakeLogo = x.MakeLogo,

                                        AnalogMinPrice = response.FindDetailAdv5Result.Details.OrderBy(z => z.ResultPrice)
                                         .Where(z => z.GroupId == x.GroupId
                                         && (z.PriceGroup == PriceGroup.ReplacementNonOriginal || z.PriceGroup == PriceGroup.ReplacementOriginal)
                                         && z.ResultPrice > 0)
                                         .Select(z => z.ResultPrice).FirstOrDefault(),

                                        OriginalMinPrice = response.FindDetailAdv5Result.Details.OrderBy(z => z.ResultPrice)
                                         .Where(z => z.GroupId == x.GroupId
                                         && z.PriceGroup == PriceGroup.Original && z.ResultPrice > 0 && z.DetailNum == detailNum && z.MakeName == x.MakeName)
                                         .Select(z => z.ResultPrice).FirstOrDefault(),

                                        AddDays = response.FindDetailAdv5Result.Details.OrderBy(z => z.ADDays)
                                         .Where(z => z.GroupId == x.GroupId
                                         && z.ADDays > 0)
                                         .Select(z => z.ADDays).FirstOrDefault()
                                    }

                                )
                                .GroupBy(x => new { x.MakeNameGroup })
                                .Select(gr => gr.First())
                                .OrderBy(x => x.MakeName)
                                .ToList()
                                ;
        }
        catch 
        {
            //throw new Exception();
            return null!;
        }

        return GroupBrandItems;
    }
}
