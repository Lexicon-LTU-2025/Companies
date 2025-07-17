using AutoMapper;
using Companies.Infractructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Tests.Helpers;
public class MapperFactory
{
    public static IMapper Create()
    {
        var configExpression = new MapperConfigurationExpression();
        configExpression.AddProfile<MapperProfile>();

        var config = new MapperConfiguration(configExpression, new LoggerFactory());
        //config.AssertConfigurationIsValid();
        return config.CreateMapper();
    }
}
