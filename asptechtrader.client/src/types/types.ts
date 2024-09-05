export type Symbol = {
  symbolId                 : number,
  symbolName         : string,
  volume             : number,
  lastDeal           : number,
  lastDealPercentage : number,
  lastPrice          : number,
  lastPricePercentage: number,
  theFirst           : number,
  theLeast           : number,
  theMost            : number,
  demandVolume       : number,
  demandPrice        : number,
  offerPrice         : number,
  offerVolume        : number,
  state              : "ALLOWED" | "NOTALLOWED",
  chartNumber        : string,
}

export type WatchList = {
    id: number,
    title: string,
    symbols: string,
    userId : string | null

}

export type UserBoughtSymbol = {
  id        : number,
  symbolName : string,
  count      : number,

  //these fields are for making relating with the TradeAccount-model
  //relatedTradeAccount TradeAccount @relation(fields: [tradeAccountId], references: [id])
  //tradeAccountId      Int
}


export type userTradeAccountType = {
    id: number;
    userProperty: number;
    userId: string;
    userBoughtSymbols: {
        id: number;
        symbolName: string;
        count: number;
        tradeAccountId: 4;
    }[];
};