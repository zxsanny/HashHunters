# Roadmap
## Stage 1 Minimal Value Product.

#### Product

Trader helper which provides enhanced manual trading and several simple autotrading strategies based on technical analysis 

#### Target auditory

Inner usage

| User Stories                                                 |
| ------------------------------------------------------------ |
| Store all possible currency pairs from currency exchanges. For MVP use one exchange, https://bittrex.com (use https://github.com/JKorf/Bittrex.Net) |
| Choose first 200 most capitalized coins from API https://coinmarketcap.com + first 100 top volume from bittrex and store OHLC for them from bittrex automatically and periodically to mongodb capped collection. |
| Coin search and display it on chart view. (Use and embed https://tradingview.com charts in iframe). If OHLC data didn't downloaded yet, download and store it. |
| Display current buy position on charts (Via tradingview config for charts in iframe) |
| Technical analysis (TA): Detecting market: bull or bear      |
| TA: Calculate and display indicators: MACD, MA, RSI, Bollinger bands. |
| TA: Calculate support and resistance levels.                 |
| Trailing stop loss and trailing stop buy orders, settings for it |
| Scalping trading, quick profit strategy and TA indicators strategy, using trailing stop loss and buy orders. |

## Stage 2 SaaS

#### Product

Full-scale multiuser autotrader using user's crypto exchange accounts, based on technical, fundamental and news analysis.

#### Target auditory

Crypto geeks. People who has accounts on cryptoexchanges, but in despair after looses of manual trading. (There are lot of them)

| User Stories                                                 |
| ------------------------------------------------------------ |
| User authorization via API Keys. Users should provide their API Key and Secret in order to give system access to their funds on several cryptocurrency exchanges |
| Add support for additional cryptocurrency exchanges: https://binance.com and https://bitfinex.com. (https://github.com/JKorf/Binance.Net, https://github.com/JKorf/Bitfinex.Net) |
| Storing user specific dashboard with chosen cryptocurrencies |
| Add and display more TA indicators, such as: parabolic SAR, RSI, Stochastic RSI, CCI, Chaikin oscillator, OBV, EMA, SMA, KAMA and others |
| Implement Elliot wave analysis, auto recognition waves. Include Elliot analysis result as indicator to the Cumulative Tech Analysis indicator (TA Indicator) |
| Fundamental analysis. For that create knowledge base for crypto projects and manually fill it. Knowledge base consists of:<br />Product team, how many and who: 0..10<br />Product uniqueness: 0..10<br />Product value: 0..10<br />Level of integration with other companies and what is this companies: 0..10<br />Coin features: <br />Protocol: 0..10 : POW = 0/POS = 10/POE=3/POI=3/ Tangle =10/ ...  <br />Smart contract support level: 0..10<br />Scalability: 0..10<br />Product roadmap and further plans: 0..10. <br />Calculate Fundamental Analysis result (FA indicator) |
| News analysis. Search coin in google trends, scan thematic Facebook groups, twitter and telegram chats for specific coin. <br />Calculate News Analysis result (NA indicator) |
| Calculate Total Trade Indicator (TTI) based on TA, FA and NA indicators. Based on historical data calculate relative weight of each indicators in tech analysis and also weights of fundamental and news analysis indicator. Do it for each trading pair separately. (Rewind history back, apply indicators, see results, compare with real, correct indicator weights in order to get more gain, and so on. ) |
| Auditory attraction. View on the first page 'System in work': realtime trading by enhanced scalping strategy, and show this account balance chart |
| Auditory attraction. Show 'Try it free for one week' on the first page where user should enter their email, password and API key(s) from exchange(s) (show help how to do it). Then analyze user assets, history, emulate history trading from the beginning with users funds and show 2 lines on chart: current balance changes, and balance if system was traded instead of user. |
| Allow to user to setup % of their funds, which system can operate. - For user security reasons. And then user can rearrange this settings |
| Monetization. For now I can propose several types of monetization:<br />1. % of trade profit (20% or 30% or 40%)<br />2. Constant initial payment + % of trade profit<br />3. Monthly const payment<br />Discussable. |

## Stage 3 Wide target auditory and integrations

#### Product

Autotrader for everyone

#### Target auditory

Investors, developers, businessmen, people who wants to make a profit from small or average amounts of money. 

| User Stories                                                 |
| ------------------------------------------------------------ |
| Neural network as more precise forecasting source. Investigate first. |
| Deposit money via bank card or bank transfer                 |
| Giving debit cards using crypto card provider, such as tenx or monaco with autosending % of gains to it |
| Possibility to liquidate all current deposit pressing one button to some crypto address (BTC/ETH/ADA/...) |
| Configurable stop losses and liquidate to the some crypto address address if total losses > configurable % of deposit (configurable) |

# Competitors

https://www.haasonline.com/

https://www.cryptohopper.com/

https://thecryptobot.com/

https://cryptoworldevolution.trade/

https://usitech-int.com/

https://gekko.wizb.it

https://gimmer.net/

http://www.btcrobot.com/

https://cryptotrader.org/
