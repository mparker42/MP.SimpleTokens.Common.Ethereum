using MP.SimpleTokens.Common.Models.Tokens;

namespace MP.SimpleTokens.Common.Ethereum.Interfaces
{
    public interface IEthereumService
    {
        public Task<string?> GetTokenOwnerAddress(BlockchainInfo blockchainInfo);
        public Task<string?> GetTokenURI(BlockchainInfo blockchainInfo);
    }
}
