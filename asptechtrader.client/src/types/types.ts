export type SymbolType = {
  symbolId         : string,
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

export type userWatchListsType = {
    userWatchListId: string,
    userId: string,
    userWatchListName: string,
    symbols: SymbolType[]

}



export type UserType = {
    userId: string;
    userName: string;
    emailAddress: string;
    userProperty: number;
    userSymbolProperties: UserSymbolProperties[];
    userWatchLists: userWatchListsType[];
    userRole : 0 | 1

}

export type UserSymbolProperties = {
    userSymbolPropertyId: string;
    symbolPrice: number;
    symbolQuantity: number;
    userId: string;
    symbolId: string;
    symbol: SymbolType;
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