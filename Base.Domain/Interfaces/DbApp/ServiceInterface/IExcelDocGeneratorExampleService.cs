using Base.Domain.Helpers;
using System;
using System.Threading.Tasks;

namespace Base.Domain.Interfaces.DbApp.ServiceInterface
{
    public interface IExcelDocGeneratorExampleService
    {

        Task<byte[]> ExampleCreateExcelEPPLUS();



    }
}