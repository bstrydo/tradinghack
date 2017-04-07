#Components

1. PortfolioOptimiser
  * Accepts N-day forecast
  * Accepts historic data as input (O,H,C,L,V) (for correlations etc.)
  * Accepts current portfolio
  * Accepts risk constraints
  * Produces long/short weights for target portfolio
  * Weight of stocks for portfolio should add up to 1
  * Could apply CAPM to define weights
2. BackTester
  * Tests PortfolioOptimiser
  * Accept capital e.g. $1m
  * Perform trades using target portfolio
  * Assuming live portfolio always becomes target portfolio per day
  * Uses TradingAlgorithm to perform trades
3. ForecastingAlgorithm
  * Accepts some information feed
  * Accepts historical data
  * Produces an N-day forecast for stocks
4. TradingAlgorithm
  * Accepts historic data
  * Accepts live portfolio
  * Accepts target portfolio
  * Makes trades
    - BUY/SELL on MARKET/LIMIT
