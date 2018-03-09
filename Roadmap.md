# Roadmap
## Stage 1 Minimal Value Product.

#### Product

Trader helper which provides 

- Enhanced manual trading: trailing stop loss and stop-buy
- Autotrading based on indicators and support/resistance levels (notification mode and real trading mode)
- Scalping trading

#### Target auditory

Inner usage

#### User Stories

Store all possible currency pairs from currency exchanges. For MVP use one exchange, https://bittrex.com

Choose first 200 most capitalized coins from API https://coinmarketcap.com + first 100 top volume from bittrex and store OHLC for them from bittrex automatically and periodically to mongodb capped collection.

Coin search and display it on chart view. (Use and embed https://tradingview.com charts in iframe). If OHLC data didn't downloaded yet, download and store it.

Display current buy position on charts (Via tradingview config for charts in iframe)

Technical analysis (TA): Detecting market: bull or bear

TA: Calculate and display indicators: MACD, MA, RSI, Bollinger bands.

TA: Calculate support and resistance levels. 

Trailing stop loss and trailing stop buy orders, settings for it

Scalping trading, quick profit strategy and TA indicators strategy, using trailing stop loss and buy orders.

| Rough Term | Milestone                                       | User Stories                                                 |
| ---------- | :---------------------------------------------- | ------------------------------------------------------------ |
| Q2 2018    | Authorization, Basic charts View, User settings | Backend: Authorization via providing API Keys, and then reading keys from headers on each request<br />UI: Registration and login forms, main user dashboard (for now with empty blocks instead of charts) |
|            |                                                 | Backend: Store chosen currency pairs from trading platform list (Bittrex) to UserCurrencyPair collection <br />UI: Search and store currency pairs |
|            |                                                 | Backend: Autostoring OHLC data from trading platform (Bittrex) to mongodb capped collection. (Use Bittrex.net) System should look to the most cryptocurrencies by capitalization (use API coinmarketcap.com) and to all users settings, and automatically store all data to db |
|            |                                                 | Backend: Utilize bittrex.net and provide buy and sell functionality<br />UI: View OHLC charts on user dashboard<br />Manual buy and sell<br />Display current position, buy and sell levels<br /> |
|            |                                                 |                                                              |
| Q3 2018    | Indicators, auto-orders and scalping            | Backend: Calculate and store basic indicators: <br />- Bollinger bands,<br />- Standard deviation,<br />- Moving Average, MACD,<br />- parabolic SAR,<br />- Stochastic, RSI, Stochastic RSI<br />- CCI, <br />- Chaikin oscillator, <br />- OBV,<br />- EMA, SMA, KAMA<br />and others <br />UI: Display all of them on charts |
|            |                                                 | Backend: Calculate support and resistance levels.<br />UI: Display support and resistance levels on charts |
|            |                                                 | Backend: Autoset stop loss orders after buy. Autoraise stop loss to a bit lower then support level. Autoset take profit orders on resistance level. <br />UI: View stop loss levels, settings page for user, and in particular settings for scalping trading |
|            |                                                 | Backend: Scalping trading, settings for scalping<br />UI: Display scalping trading levels, add user settings for scalping |
|            |                                                 | Backend: Based on historical data, calculate relative weight of each indicator for most correct price forecasting for each chart separately. Use this weight for calculate final decision: buy or sell |
|            |                                                 |                                                              |
| Q4 2018    | Full-scale Autotrading                          | Backend: Implement Elliot wave analysis, auto recognition waves. Add Elliot analysis as one more indicator to decision maker<br />UI: Display Elliot waves on charts |
|            |                                                 | Backend: Utilize fundamental analysis. For that create knowledge base for crypto projects and manually fill it. Knowledge base consists of:<br />Product team, how many and who: 0..10<br />Product uniqueness: 0..10<br />Product value: 0..10<br />Level of integration with other companies and what is this companies: 0..10<br />Coin features: <br />- Protocol: 0..10 : POW = 0/POS = 10/POE=3/POI=3/ Tangle =10/ ...<br />- Smart contract support level: 0..10<br />- Scalability: 0..10<br /><br />Product roadmap and further plans: 0..10. <br /><br />Calculate coin weight based on fundamental analysis (which should be filled manually) and include it in making trading decision<br />UI: Provide clean interface for filling knowledge base |
|            |                                                 | Backend: Utilize trends analysis. Search coin in google trends, scan facebook, twitter and telegram chats for specific coin. Calculate coin weight based on trend analysis and include it in making trading decision<br />UI: page for setup chats for scanning |
|            |                                                 | Backend: Start autotrading based on all indicators above. Start with super small cache amount. Further correction of indicators weights. |
|            |                                                 |                                                              |
| Q1 2019    | Neural network autotrading                      | Backend: Utilize Neural network for more precise forecasting<br />UI: Display neural network forecasting |

# Competitors

https://www.cryptohopper.com/