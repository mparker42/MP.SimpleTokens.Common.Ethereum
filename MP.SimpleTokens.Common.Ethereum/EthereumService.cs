using MP.SimpleTokens.Common.Ethereum.ContractFunctions;
using MP.SimpleTokens.Common.Ethereum.Interfaces;
using MP.SimpleTokens.Common.Models.Tokens;
using Nethereum.Contracts;
using Nethereum.StandardNonFungibleTokenERC721;
using Nethereum.StandardNonFungibleTokenERC721.ContractDefinition;
using Nethereum.Web3;

namespace MP.SimpleTokens.Common.Ethereum
{
    public class EthereumService : IEthereumService
    {
        private readonly Web3 _web3;

        public EthereumService(Web3 web3)
        {
            _web3 = web3;
        }

        public async Task<string?> GetTokenOwnerAddress(BlockchainInfo blockchainInfo)
        {
            var nftService = new ERC721Service(_web3, blockchainInfo.Address);

            // tokenId is required so throw an exception if not provided
            var tokenId = blockchainInfo.TokenId ?? throw new ArgumentNullException(nameof(blockchainInfo.TokenId));

            var owner = await nftService.OwnerOfQueryAsync(tokenId);

            return owner;
        }

        public async Task<string?> GetTokenURI(BlockchainInfo blockchainInfo)
        {
            var nftService = new ERC721Service(_web3, blockchainInfo.Address);

            // tokenId is required so throw an exception if not provided
            var tokenId = blockchainInfo.TokenId ?? throw new ArgumentNullException(nameof(blockchainInfo.TokenId));

            var tokenURI = await nftService.ContractHandler.QueryAsync<TokenURIFunction, string>(
                new TokenURIFunction
                {
                    TokenId = tokenId
                }
            );

            return tokenURI;
        }

        public async Task<IEnumerable<EventLog<TransferEventDTO>>?> GetTokenTransactionHistory(BlockchainInfo blockchainInfo)
        {
            var nftService = new ERC721Service(_web3, blockchainInfo.Address);

            // tokenId is required so throw an exception if not provided
            var tokenId = blockchainInfo.TokenId ?? throw new ArgumentNullException(nameof(blockchainInfo.TokenId));

            // Transactions are a form of event so they fall under the "GetEvent" umbrella
            var transferEventHandler = nftService.ContractHandler.GetEvent<TransferEventDTO>();

            // The TransferEventDTO function expects a from address, to address and then the tokenId
            var filterAllTransferEventsForContract = transferEventHandler.CreateFilterInput<string?, string?, long>(null, null, blockchainInfo.TokenId.Value);

            // Get events applying the filter
            var transfers = await transferEventHandler.GetAllChangesAsync(filterAllTransferEventsForContract);

            return transfers;
        }
    }
}
