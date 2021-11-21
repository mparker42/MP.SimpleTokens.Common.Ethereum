using MP.SimpleTokens.Common.Ethereum.ContractFunctions;
using MP.SimpleTokens.Common.Ethereum.Interfaces;
using MP.SimpleTokens.Common.Models.Tokens;
using Nethereum.StandardNonFungibleTokenERC721;
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
    }
}
