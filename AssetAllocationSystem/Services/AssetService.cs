using AssetAllocationSystem.Data;
using AssetAllocationSystem.DTOs;
using AssetAllocationSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AssetAllocationSystem.Services
{
    public class AssetService : IAssetService
    {
        private readonly ApplicationDbContext _context;

        public AssetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AssetResponseDto> CreateAssetAsync(CreateAssetDto model)
        {
            if (await _context.Assets.AnyAsync(a => a.SerialNumber == model.SerialNumber))
                throw new Exception("Asset with this serial number already exists.");

            var asset = new Asset
            {
                AssetName = model.AssetName,
                SerialNumber = model.SerialNumber,
                IsAssigned = false
            };

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();

            return new AssetResponseDto
            {
                Id = asset.Id,
                AssetName = asset.AssetName,
                SerialNumber = asset.SerialNumber,
                IsAssigned = asset.IsAssigned
            };
        }

        public async Task<AssetResponseDto> UpdateAssetAsync(int id, UpdateAssetDto model)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
                throw new Exception("Asset not found.");

            asset.AssetName = model.AssetName;

            await _context.SaveChangesAsync();

            return new AssetResponseDto
            {
                Id = asset.Id,
                AssetName = asset.AssetName,
                SerialNumber = asset.SerialNumber,
                IsAssigned = asset.IsAssigned
            };
        }

        public async Task<bool> DeleteAssetAsync(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
                throw new Exception("Asset not found.");

            if (asset.IsAssigned)
                throw new Exception("Cannot delete an assigned asset.");

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AssetResponseDto>> GetAllAssetsAsync()
        {
            return await _context.Assets
                .Select(a => new AssetResponseDto
                {
                    Id = a.Id,
                    AssetName = a.AssetName,
                    SerialNumber = a.SerialNumber,
                    IsAssigned = a.IsAssigned
                })
                .ToListAsync();
        }
    }
}