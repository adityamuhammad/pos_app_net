using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using PointOfSales.Web.Database;
using PointOfSales.Web.Dtos;
using PointOfSales.Web.Services.Contracts;

namespace PointOfSales.Web.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        public ProductService( IDatabaseConnectionFactory databaseConnectionFactory)
        {
            _databaseConnectionFactory = databaseConnectionFactory;
        }

        public async Task<ResponseDto<ProductCreateResultDto>> Create(ProductCreateDto productCreateDto)
        {
            using var db = _databaseConnectionFactory.GetDbConnection();
            var lastInsertId = await db.QueryFirstOrDefaultAsync<long>(
                    @"insert into products
                    (name, price)
                    values
                    (@Name, @Price);
                    select last_insert_id()", productCreateDto);

            var data = await db.QueryFirstOrDefaultAsync<ProductCreateResultDto>(
                @"select name, price, created_at
                from products
                where id = @LastInsertId", new { LastInsertId = lastInsertId });
            return new ResponseDto<ProductCreateResultDto> { Data = data,Success = true, Message = "Success." };
        }

        public async Task Delete(long id)
        {
            using var db = _databaseConnectionFactory.GetDbConnection();
            await db.ExecuteAsync(@"delete from products where id = @Id", new { Id = id });
        }

        public async Task<ResponseDto<IEnumerable<ProductDto>>> GetAll()
        {
            using var db = _databaseConnectionFactory.GetDbConnection();
            var data = await db.QueryAsync<ProductDto>(
		        @"select name Name, price Price, created_at CreatedAt , id Id
                  from products");
            return new ResponseDto<IEnumerable<ProductDto>> { Success = true, Data = data };

        }

        public async Task<ResponseDto<ProductUpdateResultDto>> Update(ProductUpdateDto productUpdateDto)
        {
            using var db = _databaseConnectionFactory.GetDbConnection();
            await db.ExecuteAsync(
                    @"update products
                    set name = @Name, price = @Price
                    where id = @Id", productUpdateDto);

            var data = await db.QueryFirstOrDefaultAsync<ProductUpdateResultDto>(
                @"select name, price
                from products
                where id = @Id", productUpdateDto);
            return new ResponseDto<ProductUpdateResultDto> { Data = data,Success = true, Message = "Success." };
        }

        public async Task<ResponseDto<ProductDto>> GetById(long id)
        { 
            using var db = _databaseConnectionFactory.GetDbConnection();
            var data = await db.QueryFirstOrDefaultAsync<ProductDto>(
                @"select name, price, id
                from products
                where id = @Id", new { Id = id });
            if (data != null) { 
			    return new ResponseDto<ProductDto> { Data = data,Success = true, Message = "Success." };
	        }
		    return new ResponseDto<ProductDto> {Success = false, Message = "No data found." };


	    }
    }
}
