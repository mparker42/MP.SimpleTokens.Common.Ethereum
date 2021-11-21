using MP.SimpleTokens.Common.Models.Tokens;
using Nethereum.Contracts;
using Nethereum.StandardNonFungibleTokenERC721.ContractDefinition;

namespace MP.SimpleTokens.Common.Ethereum.Interfaces
{
    public interface IEthereumService
    {
        Task<string?> GetTokenOwnerAddress(BlockchainInfo blockchainInfo);
        Task<string?> GetTokenURI(BlockchainInfo blockchainInfo);
        Task<IEnumerable<EventLog<TransferEventDTO>>?> GetTokenTransactionHistory(BlockchainInfo blockchainInfo);
    }
}
