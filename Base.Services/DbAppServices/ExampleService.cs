using AutoMapper;
using AutoMapper.QueryableExtensions;
using Base.Domain.Entities.DbApp;
using Base.Domain.Interfaces.DbApp.RepositoryInterface;
using Base.Domain.Interfaces.DbApp.ServiceInterface;
using Base.Domain.Models.Example;
using Base.Domain.Pagination;
using Base.Services.DbAppValidators;
using Base.Services.Helpers.ErrorHandler;
using Base.Services.Helpers.Files;
using Base.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft;
using Sieve.Models;
using Sieve.Services;

namespace Base.Services.DbAppServices
{
    public class ExampleService :  IExampleService
    {
        private readonly IExampleRepository exampleRepository;
        protected readonly IMapper mapper;
        private readonly ISieveProcessor sieveProcessor;

        public ExampleService(IMapper mapper, IExampleRepository mexampleRepository, ISieveProcessor sieveProcessor) 
        {
            this.exampleRepository = mexampleRepository;
            this.mapper = mapper;
            this.sieveProcessor = sieveProcessor;
        }


        public  async Task<long> Create(ExampleModel exampleModel)
        {
            new ExampleValidator(exampleModel).ValidateAndThrowCustomException(exampleModel);
            return await exampleRepository.Create(mapper.Map<Example>(exampleModel));
        }

        public async Task Update(long idExample, ExampleModel exampleModel)
        {
            new ExampleValidator(exampleModel).ValidateAndThrowCustomException(exampleModel);
            await exampleRepository.Create(mapper.Map<Example>(exampleModel));
        }

        public async Task <IEnumerable<ExampleModel>>GetAll()
        {
            return  mapper.Map<List<Example>, List<ExampleModel>>((List<Example>)await exampleRepository.GetAll());
        }


        public async Task Delete(long idExample)
        {

            var example = await exampleRepository.GetExample(idExample);
            if (example == null) throw new AppException("User not found");
            await exampleRepository.Delete(idExample);
        }



        public async Task<Byte[]> DownloadFile()
        {
                var result = await exampleRepository.GetAll();
                return  CsvExport<Example>.WriteCsvToMemory(result, ";", System.Text.Encoding.UTF8);
        }

        public async Task<IQueryable<Example>> GetPagination(SieveModel sieveModel)
        {
            IQueryable<Example> result = await exampleRepository.GetPagination();
            return  sieveProcessor.Apply(sieveModel, result);
        }


        public async Task<IQueryable<ExampleModel>> GetPaginationModel(SieveModel sieveModel)
        {
            IQueryable<ExampleModel> result = mapper.Map<IQueryable<Example>, IQueryable<ExampleModel>>((IQueryable<Example>)await exampleRepository.GetAll());
                return sieveProcessor.Apply(sieveModel, result);
        }



    }
}
