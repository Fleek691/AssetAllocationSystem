using AssetAllocationSystem.BLL.DTOs;
using AssetAllocationSystem.BLL.Interfaces;
using AssetAllocationSystem.DAL.Data;
using AssetAllocationSystem.DAL.Entities;
using AssetAllocationSystem.DAL.Interfaces;

namespace AssetAllocationSystem.BLL.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly ApplicationDbContext _context;

        public AssetService(IAssetRepository assetRepository, ApplicationDbContext context)
        {
            _assetRepository = assetRepository;
            _context = context;
        }

        public async Task<AssetResponseDto> CreateAssetAsync(CreateAssetDto model)
        {
            if (await _assetRepository.AnyAsync(a => a.SerialNumber == model.SerialNumber))
                throw new Exception("Asset with this serial number already exists.");

            var asset = new Asset
            {
                AssetName = model.AssetName,
                SerialNumber = model.SerialNumber,
                IsAssigned = false
            };

            await _assetRepository.AddAsync(asset);
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
            var asset = await _assetRepository.GetByIdAsync(id);
            if (asset == null)
                throw new Exception("Asset not found.");

            asset.AssetName = model.AssetName;

            _assetRepository.Update(asset);
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
            var asset = await _assetRepository.GetByIdAsync(id);
            if (asset == null)
                throw new Exception("Asset not found.");

            if (asset.IsAssigned)
                throw new Exception("Cannot delete an assigned asset.");

            _assetRepository.Remove(asset);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AssetResponseDto>> GetAllAssetsAsync()
        {
            var assets = await _assetRepository.GetAllAsync();

            return assets.Select(a => new AssetResponseDto
            {
                Id = a.Id,
                AssetName = a.AssetName,
                SerialNumber = a.SerialNumber,
                IsAssigned = a.IsAssigned
            });
        }
    }
}
