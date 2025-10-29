using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WorkArea.Application.DTOs;
using WorkArea.Application.Encryription;
using WorkArea.Application.Mapper;
using WorkArea.Application.RequestModels;
using WorkArea.Domain.Entities;
using WorkArea.Persistence;
using WorkArea.Persistence.Repositories;

namespace WorkArea.Application.Services;

public class UserService(IRepository<User> userRepository)
{
    readonly Cipher _cipherStatic = new Cipher("");
    public async Task<DbOperationResult<UserDto?>> WebRegister(RegisterRequestModel model)
    {
        Cipher cipher = new Cipher(model.SecretKey);
        var username = cipher.Encrypt(model.Username);
        var userExist = await userRepository.ListQueryableNoTracking
            .Where(x => x.Username == username && !x.IsDeleted).AnyAsync();
        
        if(userExist)
            return new DbOperationResult<UserDto?>(false, "Username already exists", null);

        var newUserModel = new User()
        {
            IsDeleted = false,
            Username = username,
            Password = cipher.Encrypt(model.Password),
            Email = cipher.Encrypt(model.Email),
            PhoneNumber = cipher.Encrypt(model.PhoneNumber),
            Fullname = _cipherStatic.Encrypt(model.Fullname),
            PushToken = "a",
            Avatar = "https://placehold.co/128",
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
        };

        var insert = await userRepository.Insert(newUserModel);
        if (insert.IsSucceed)
        {
            var userDto = ObjectMapper.Mapper.Map<UserDto>(newUserModel);
            return new DbOperationResult<UserDto?>(true, "", userDto);
        }
        
        return new DbOperationResult<UserDto>(false, insert.Message, insert.Errors, null);
    }
    
    public async Task<DbOperationResult<UserDto?>> WebLogin(LoginRequestModel model)
    {
        Cipher cipher = new Cipher(model.SecretKey);
        var username = cipher.Encrypt(model.Username);
        var password = cipher.Encrypt(model.Password);

        var userExist = await userRepository.ListQueryableNoTracking
            .Where(x => x.Username == username && x.Password == password && !x.IsDeleted)
            .ProjectTo<UserDto>(ObjectMapper.Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (userExist == null)
            return new DbOperationResult<UserDto?>(false, "Kullanıcı bulunamadı", null);
        
        
        
        return new DbOperationResult<UserDto?>(true, "", userExist);
    }
    
    public async Task<UserSimpleDto?> GetUserSimpleInfo(int userId)
    {
        var data = await userRepository.ListQueryableNoTracking
            .ProjectTo<UserSimpleDto>(ObjectMapper.Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == userId);

        data.Fullname = _cipherStatic.Decrypt(data.Fullname);
        
        return data;
    }
}